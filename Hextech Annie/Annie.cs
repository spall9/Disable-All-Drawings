using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

using System.Reflection;
using System.Net;
using Version = System.Version;
using System.Text.RegularExpressions;

namespace Hextech_Annie
{
    class Annie : Extensions
    {
        public static void LoadModules()
        {
            Menu.Load();
            LoadSpells();
            LoadEvents();
        }

        private static void LoadSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 550, SkillShotType.Cone, 250, int.MaxValue, 250);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 200, int.MaxValue, 250);
            Flash = new Spell.Skillshot(Player.Instance.GetSpellSlotFromName("summonerflash"), 425, SkillShotType.Linear);
            Ignite = new Spell.Targeted(Player.Instance.GetSpellSlotFromName("summonerdot"), 550);
        } 

        private static void LoadEvents()
        {
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            GameObject.OnCreate += Tibbers.GameObject_OnCreate;
            GameObject.OnDelete += Tibbers.GameObject_OnDelete;
        }

        private static void Game_OnTick(EventArgs args)
        {
            try
            {
                if (Menu.Keybind(Menu.Misc, "AA") && Combo)
                {
                    Orbwalker.DisableAttacking = true;
                }
                else
                {
                    Orbwalker.DisableAttacking = false;
                }

                if (Menu.Keybind(Menu.Misc, "Flash") && R.IsReady() && Flash.IsReady())
                {
                    var New = TargetSelector.GetTarget(Flash.Range + R.Range, DamageType.Magical);

                    if (New != null)
                    {
                        if (!New.IsValidTarget(R.Range) && Stun)
                        {
                            var Pred = R.GetPrediction(New);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                Flash.Cast(Player.Instance.Position.Extend(New, Flash.Range).To3D());
                                Core.DelayAction(() => R.Cast(Pred.CastPosition), 10);
                            }
                        }
                    }
                }

                if (Menu.CheckBox(Menu.Principal, "Skin"))
                {
                    if (Player.Instance.Model != Player.Instance.ChampionName)
                        Player.Instance.SetModel(Player.Instance.ChampionName);

                    Player.Instance.SetSkinId(Menu.Slider(Menu.Principal, "SkinID"));

                    foreach (var x in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.Name.ToLower().Equals("tibbers") && x.IsValid && !x.IsDead))
                    {
                        x.SetSkinId(Menu.Slider(Menu.Principal, "SkinID"));
                    }
                }

                if (Ignite.Slot != SpellSlot.Unknown)
                {
                    if (Ignite.IsReady() && Menu.CheckBox(Menu.Misc, "Ignite"))
                    {
                        foreach (var Hero in EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(Ignite.Range)).OrderBy(x => x.Health))
                        {
                            if (Hero.Health <= Player.Instance.GetSummonerSpellDamage(Hero, DamageLibrary.SummonerSpells.Ignite))
                            {
                                Ignite.Cast(Hero);
                            }
                        }
                    }
                }

                if (Menu.CheckBox(Menu.E, "Auto") && E.IsReady())
                {
                    if (!Player.Instance.IsRecalling())
                    {
                        E.Cast();
                    }
                }

                if (Jungleclear)
                {
                    var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(X => X.IsValidTarget(Q.Range)).OrderBy(x => x.MaxHealth).FirstOrDefault();

                    if (Monsters != null)
                    {
                        if (Q.IsReady() && Menu.CheckBox(Menu.Q, "Jungle"))
                        {
                            Q.Cast(Monsters);
                        }

                        if (W.IsReady() && Menu.CheckBox(Menu.W, "Jungle"))
                        {
                            W.Cast(Monsters);
                        }
                    }
                }

                if (Laneclear)
                {
                    var Minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Q.Range).FirstOrDefault();

                    if (Minion != null)
                    {
                        if (Q.IsReady() && Menu.CheckBox(Menu.Q, "Lane"))
                        {
                            if (Menu.ComboBox(Menu.Q, "Logic") == 0)
                            {
                                if (Menu.CheckBox(Menu.Misc, "Stun") && Stun)
                                    return;

                                if (Minion.Health - Damages.QDamage(Minion) <= 0)
                                {
                                    Q.Cast(Minion);
                                }
                            }
                            else
                            {
                                Q.Cast(Minion);
                            }
                        }

                        if (W.IsReady() && Menu.CheckBox(Menu.W, "Lane"))
                        {
                            if (Menu.CheckBox(Menu.Misc, "Stun") && Stun)
                                return;

                            if (Minion.IsValidTarget(W.Range))
                            {
                                W.Cast(Minion);
                            }
                        }
                    }
                }

                if (Combo && Target != null)
                {
                    if (Q.IsReady() && Menu.CheckBox(Menu.Q, "Combo"))
                    {
                        if (Stun)
                        {
                            if (!Target.HasBuffOfType(BuffType.SpellShield))
                            {
                                Q.Cast(Target);
                            }
                        }
                        else
                        {
                            Q.Cast(Target);
                        }
                    }

                    if (W.IsReady() && Menu.CheckBox(Menu.W, "Combo"))
                    {
                        if (Stun)
                        {
                            if (!Target.HasBuffOfType(BuffType.SpellShield))
                            {
                                var Pred = W.GetPrediction(Target);

                                if (Pred.HitChance >= HitChance.High)
                                {
                                    W.Cast(Target);
                                }
                            }
                        }
                        else
                        {
                            var Pred = W.GetPrediction(Target);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                W.Cast(Target);
                            }
                        }
                    }

                    if (R.IsReady() && Menu.CheckBox(Menu.W, "Combo"))
                    {
                        if (Stun && Menu.CheckBox(Menu.R, "Logic"))
                        {
                            if (!Target.HasBuffOfType(BuffType.SpellShield))
                            {
                                var Pred = R.GetPrediction(Target);

                                if (Pred.HitChance >= HitChance.High)
                                {
                                    R.Cast(Target);
                                }
                            }
                        }
                        else
                        {
                            var Pos = Vectors.GetBestRPos(Target.Position.To2D());

                            if (Pos.First().Value >= Menu.Slider(Menu.R, "ComboMin"))
                            {
                                R.Cast(Pos.First().Key.To3D());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsAlly || sender != null || sender.IsMe)
                return;

            if (Menu.CheckBox(Menu.Misc, "Int") && Stun)
            {
                if (Q.IsReady() && sender.IsValidTarget(Q.Range))
                {
                    Q.Cast(sender);
                }
                else if (W.IsReady() && sender.IsValidTarget(W.Range))
                {
                    W.Cast(sender);
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsAlly || sender != null || sender.IsMe)
                return;

            if (Menu.CheckBox(Menu.Misc, "Int") && Stun)
            {
                if (Q.IsReady() && sender.IsValidTarget(Q.Range))
                {
                    Q.Cast(sender);
                }
                else if (W.IsReady())
                {
                    if (e.End.Distance(Player.Instance.Position) <= W.Range)
                    {
                        W.Cast(e.End);
                    }else if (e.Start.Distance(Player.Instance.Position) <= W.Range)
                    {
                        W.Cast(e.Start);
                    }
                    else
                    {
                        if (sender.IsValidTarget(W.Range))
                        {
                            W.Cast(sender);
                        }
                    }
                }
            }
        }
    }
}
