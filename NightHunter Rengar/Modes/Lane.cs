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
    class Lane : Extensions
    {
        public static void Load()
        {
            var M = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range);

            if (M != null)
            {
                if (HasPassive && CheckBox(Settings.Lane, "Passive"))
                    return;

                if (Q.IsReady() && CheckBox(Settings.Lane, "Q"))
                {
                    if (M.FirstOrDefault().IsValidTarget(Q.Range))
                    {
                        var L = EntityManager.MinionsAndMonsters.GetLineFarmLocation(M, E.Width, (int)E.Range);

                        if (L.HitNumber >= Slider(Settings.Lane, "Q1"))
                        {
                            Q.Cast(L.CastPosition);
                        }
                    }
                }

                if (E.IsReady() && CheckBox(Settings.Lane, "E"))
                {
                    if (M.FirstOrDefault().IsValidTarget(E.Range))
                    {
                        E.Cast(M.FirstOrDefault());
                    }
                }
            }
        }
    }
}
