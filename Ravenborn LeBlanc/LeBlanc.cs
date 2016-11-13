using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

#region EloBuddy
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
#endregion

namespace Ravenborn_LeBlanc
{
    class LeBlanc : Extensions
    {
        public static DamageIndicator Damage;

        public static void Loading(EventArgs arg)
        {
            if (Player.Instance.Hero != Champion.Leblanc)
                return;

            Settings.Load();
            Damage = new DamageIndicator();
            Updated();

            Game.OnTick += delegate
            {
                if (!W.IsReady())
                {
                    if (GetState(SpellSlot.W) == State.WReturn)
                        return;

                    var Remove = About.FirstOrDefault(x => x.NetworkID == Player.Instance.NetworkId && x.IsRW == false);

                    if (Remove != null)
                    {
                        About.Remove(Remove);
                    }
                }

                if (!R.IsReady())
                {
                    if (GetState(SpellSlot.R) == State.RWReturn)
                        return;

                    var Remove = About.FirstOrDefault(x => x.NetworkID == Player.Instance.NetworkId && x.IsRW == true);

                    if (Remove != null)
                    {
                        About.Remove(Remove);
                    }
                }

                if (Keybind(Settings.Harass, "Key"))
                {
                    Modes.Harass.Load();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    Modes.Combo.Load();

                    if (CheckBox(Settings.Combo, "W1"))
                    {
                        Logic.Return();
                    }
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                {
                    Modes.Lane.Load();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                {
                    Modes.Jungle.Load();
                }
            };

            Obj_AI_Base.OnProcessSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                if (sender.IsMe)
                {
                    if (args.SData.Name == "LeblancW")
                    {
                        About.Add(new Base
                        {
                            Pos = args.Start,
                            IsRW = false,
                            NetworkID = sender.NetworkId
                        });
                    }

                    if (args.SData.Name == "LeblancRW")
                    {
                        About.Add(new Base
                        {
                            Pos = args.Start,
                            IsRW = true,
                            NetworkID = sender.NetworkId
                        });
                    }
                }
            };

            Gapcloser.OnGapcloser += delegate(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
            {
                if (sender.IsEnemy && sender != null)
                {
                    if (E.IsReady() && CheckBox(Settings.Misc, "Gap"))
                    {
                        if (sender.IsValidTarget(E.Range))
                        {
                            var Pred = E.GetPrediction(sender);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                E.Cast(Pred.UnitPosition);
                            }
                        }
                    }

                    if (R.IsReady() && CheckBox(Settings.Misc, "Gap2"))
                    {
                        Logic.LR(SpellSlot.E, sender);
                    }
                }
            };

            Interrupter.OnInterruptableSpell += delegate(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
            {
                if (sender.IsEnemy && sender != null)
                {
                    if (E.IsReady() && CheckBox(Settings.Misc, "Int"))
                    {
                        if (sender.IsValidTarget(E.Range))
                        {
                            var Pred = E.GetPrediction(sender);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                E.Cast(Pred.UnitPosition);
                            }
                        }
                    }

                    if (R.IsReady() && CheckBox(Settings.Misc, "Int2"))
                    {
                        var s = sender as AIHeroClient;

                        Logic.LR(SpellSlot.E, s);
                    }
                }
            };

            Drawing.OnEndScene += delegate
            {
                foreach (var L in About)
                {
                    if (L.IsRW)
                    {
                        Drawing.DrawCircle(L.Pos, 120, Color.Purple);
                    }

                    Drawing.DrawCircle(L.Pos, 120, Color.DarkOrange);
                }

                if (CheckBox(Settings.Draw, "Q") && Q.IsReady())
                {
                    Q.DrawRange(Color.Purple, 4);
                }

                if (CheckBox(Settings.Draw, "W" ) && W.IsReady())
                {
                    W.DrawRange(Color.DarkBlue, 4);
                }

                if (CheckBox(Settings.Draw, "E") && E.IsReady())
                {
                    E.DrawRange(Color.DarkBlue, 4);
                }
            };
        }

        public static void Example()
        {
            Dash.OnDash += Dash_OnDash;
        }

        private static void Dash_OnDash(Obj_AI_Base sender, Dash.DashEventArgs e)
        {
            Chat.Print("Sender: " + sender.BaseSkinName);
        }
    }
}