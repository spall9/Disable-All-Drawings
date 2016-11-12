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

namespace NightHunter_Rengar.Modes
{
    class Combo : Extensions
    {
        public static void Load()
        {
            var Target = TargetSelector.GetTarget(E.Range, DamageType.Physical);

            if (Target != null)
            {
                if (UltActive)
                    return;

                if (Q.IsReady() && CheckBox(Settings.Combo, "Q"))
                {
                    if (Target.IsValidTarget(Q.Range))
                    {
                        var Pred = Q.GetPrediction(Target);

                        if (ComboBox(Settings.Combo, "Focus") != 0 && HasPassive)
                            return;

                        if (Pred.HitChance >= HitChance.High)
                        {
                            Q.Cast(Pred.CastPosition);
                        }
                    }
                }

                if (E.IsReady() && CheckBox(Settings.Combo, "E"))
                {
                    if (Target.IsValidTarget(E.Range))
                    {
                        var Pred = E.GetPrediction(Target);

                        if (ComboBox(Settings.Combo, "Focus") != 1 && HasPassive)
                            return;

                        if (Pred.HitChance >= HitChance.High)
                        {
                            E.Cast(Pred.CastPosition);
                        }
                    }
                }
            }
        }
    }
}
