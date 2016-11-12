using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace Dragonblade_Talon.Modes
{
    class Combo : Extensions
    {
        public static void Load()
        {
            var Target = TargetSelector.GetTarget(W.Range, DamageType.Physical);

            if (Target != null)
            {
                if (Q.IsReady() && CheckBox(Settings.Combo, "Q"))
                {
                    if (Target.IsValidTarget(Q.Range))
                    {
                        Q.Cast(Target);
                    }
                }

                if (W.IsReady() && CheckBox(Settings.Combo, "W"))
                {
                    if (Target.IsValidTarget(W.Range))
                    {
                        if (CheckBox(Settings.Combo, "W1") && !Stealth)
                        {
                            var Pred = W.GetPrediction(Target);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                W.Cast(Pred.UnitPosition);
                            }
                        }
                        else
                        {
                            var Pred = W.GetPrediction(Target);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                W.Cast(Pred.UnitPosition);
                            }
                        }
                    }
                }

                if (R.IsReady() && CheckBox(Settings.Combo, "R"))
                {
                    if (Target.IsValidTarget(R.Range))
                    {
                        if (Target.HealthPercent <= Slider(Settings.Combo, "R1"))
                        {
                            R.Cast();
                        }
                    }
                }
            }
        }
    }
}
