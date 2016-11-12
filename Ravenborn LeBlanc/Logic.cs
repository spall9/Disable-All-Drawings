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
        private static bool QW;

        public static void LQ()
        {
            /*
             *  
             * 
             * 
            */    
        }

        public static void LW()
        {

        }

        public static void LR(SpellSlot S, AIHeroClient T)
        {
            if (T == null)
                return;

            if (R.IsReady() && GetState(SpellSlot.R) == State.RNormal)
            {
                switch (S)
                {
                    case SpellSlot.Q:
                        if (T.IsValidTarget(Q.Range))
                        {
                            R.Cast();
                            Player.CastSpell(SpellSlot.R, T);
                        }
                        break;

                    case SpellSlot.W:
                        if (T.IsValidTarget(W.Range))
                        {
                            var Pred = W.GetPrediction(T);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                R.Cast();
                                Player.CastSpell(SpellSlot.R, Pred.CastPosition);
                            }
                        }
                        break;

                    case SpellSlot.E:
                        if (T.IsValidTarget(E.Range))
                        {
                            var Pred = E.GetPrediction(T);

                            if (Pred.HitChance >= HitChance.High)
                            {
                                R.Cast();
                                Player.CastSpell(SpellSlot.R, Pred.CastPosition);
                            }
                        }
                        break;
                }
            }
        }
    }
}
