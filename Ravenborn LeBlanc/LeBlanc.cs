using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

#region
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
        public static void Loading(EventArgs arg)
        {
            if (Player.Instance.Hero != Champion.Leblanc)
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

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                {
                    Chat.Print(GetState(SpellSlot.R));
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
    }
}