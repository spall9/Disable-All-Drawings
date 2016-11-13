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

namespace Ravenborn_LeBlanc.Modes
{
    class Harass : Extensions
    {
        public static void Load()
        {
            var Target = TargetSelector.GetTarget(E.Range, DamageType.Magical);

            if (Target != null && GetState(SpellSlot.R) != State.RRCast)
            {
                if (W.IsReady() && CheckBox(Settings.Harass, "W"))
                {
                    if (Target.IsValidTarget(W.Range) && GetState(SpellSlot.W) != State.WReturn)
                    {
                        var Pred = W.GetPrediction(Target);

                        if (Pred.HitChance >= HitChance.High)
                        {
                            W.Cast(Pred.UnitPosition);
                        }
                    }
                }

                if (Q.IsReady() && CheckBox(Settings.Harass, "Q"))
                {
                    if (Target.IsValidTarget(Q.Range))
                    {
                        Q.Cast(Target);
                    }
                }

                if (E.IsReady() && CheckBox(Settings.Harass, "E"))
                {
                    if (Target.IsValidTarget(E.Range))
                    {
                        var Pred = E.GetPrediction(Target);

                        if (Pred.HitChance >= HitChance.High)
                        {
                            E.Cast(Pred.UnitPosition);
                        }
                    }
                }
            }
        }
    }
}
