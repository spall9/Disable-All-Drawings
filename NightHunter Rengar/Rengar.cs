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

namespace NightHunter_Rengar
{
    class Rengar : Extensions
    {
        public static void Loading(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Rengar)
                return;

            Settings.Load();

            Game.OnTick += delegate
            {
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    Modes.Combo.Load();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                    Modes.Lane.Load();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                    Modes.Jungle.Load();

                if (W.IsReady() && CheckBox(Settings.Combo, "W"))
                {
                    if (HasPassive)
                    {
                        foreach (var P in Player.Instance.Buffs)
                        {
                            if (Buffs.Contains(P.Type) && CheckBox(Settings.Combo, "W/" + P.Type.ToString()))
                            {
                                var T = P.StartTime - P.EndTime <= Slider(Settings.Combo, "W1") * 1000;

                                if (T)
                                {
                                    W.Cast();
                                }
                            }
                        }
                    }
                }
            };

            Gapcloser.OnGapcloser += delegate (AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
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
                }
            };

            Interrupter.OnInterruptableSpell += delegate (Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
            {
                if (sender.IsEnemy && sender != null)
                {
                    if (E.IsReady() && CheckBox(Settings.Misc, "Int"))
                    {
                        if (sender.IsValidTarget(E.Range) && HasPassive)
                        {
                            var Pred = E.GetPrediction(sender);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                E.Cast(Pred.UnitPosition);
                            }
                        }
                    }
                }
            };

            Drawing.OnEndScene += delegate
            {
                if (Q.IsReady() && CheckBox(Settings.Draw, "Q"))
                {
                    Q.DrawRange(Color.DarkBlue, 4);
                }

                if (W.IsReady() && CheckBox(Settings.Draw, "W"))
                {
                    W.DrawRange(Color.Brown, 4);
                }

                if (E.IsReady() && CheckBox(Settings.Draw, "E"))
                {
                    E.DrawRange(Color.Brown, 4);
                }
            };
        }
    }
}
