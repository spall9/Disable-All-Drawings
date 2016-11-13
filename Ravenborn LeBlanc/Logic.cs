using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Logic : Extensions
    {
        public static bool Return()
        {
            if (GetState(SpellSlot.W) == State.WReturn || GetState(SpellSlot.W) == State.RWReturn)
            {
                foreach (var Hero in EntityManager.Heroes.Enemies)
                {
                    foreach (var Loc in About)
                    {
                        if (Hero.HasBuff("LeBlancEBeam") && Hero.IsValidTarget(E.Range))
                            return false;

                        var EPos = Player.Instance.CountEnemiesInRange(Player.Instance.GetAutoAttackRange());
                        var EWPos = Loc.Pos.CountEnemiesInRange(Player.Instance.GetAutoAttackRange());
                        var APos = Player.Instance.CountAlliesInRange(Player.Instance.GetAutoAttackRange());
                        var AWPos = Loc.Pos.CountAlliesInRange(Player.Instance.GetAutoAttackRange());

                        if (EPos > APos || EWPos > AWPos || UnderEnemyTurret(Loc.Pos))
                            return false;

                        if (GetState(SpellSlot.W) == State.WReturn)
                        {
                            Player.CastSpell(SpellSlot.W);
                        }

                        if (GetState(SpellSlot.R) == State.RWReturn)
                        {
                            Player.CastSpell(SpellSlot.R);
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        public static void LR(SpellSlot S, AIHeroClient T)
        {
            if (T == null)
                return;

            if (R.IsReady() && GetState(SpellSlot.R) == State.RNormal)
            {
                if (T.IsValidTarget(Q.Range) && S == SpellSlot.Q)
                {
                    if (GetState(SpellSlot.R) != State.RRCast)
                    {
                        R.Cast();
                    }

                    Q.Cast(T);
                }

                if (T.IsValidTarget(W.Range) && S == SpellSlot.W)
                {
                    var PredW = W.GetPrediction(T);

                    if (PredW.HitChance >= HitChance.High)
                    {
                        if (GetState(SpellSlot.R) != State.RRCast)
                        {
                            R.Cast();
                        }

                        W.Cast(PredW.CastPosition);
                    }
                }

                if (T.IsValidTarget(E.Range) && S == SpellSlot.E)
                {
                    var PredE = E.GetPrediction(T);

                    if (PredE.HitChance >= HitChance.High)
                    {
                        if (GetState(SpellSlot.R) != State.RRCast)
                        {
                            R.Cast();
                        }

                        E.Cast(PredE.CastPosition);
                    }
                }
            }
        }
    }
}
