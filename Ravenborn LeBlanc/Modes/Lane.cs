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
    class Lane : Extensions
    {
        public static void Load()
        {
            var M = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range);

            if (W.IsReady() && CheckBox(Settings.Lane, "W"))
            {
                var Loc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(M, W.Width, (int)W.Range);

                if (Loc.HitNumber >= Slider(Settings.Lane, "W1"))
                {
                    W.Cast(Loc.CastPosition);
                }
            }

            if (Q.IsReady() && CheckBox(Settings.Lane, "Q"))
            {
                if (CheckBox(Settings.Lane, "Q1"))
                {
                    var MM = M.Where(x => HasMark(x) && x.IsValidTarget(Q.Range)).ToArray();

                    if (MM.Length >= Slider(Settings.Lane, "Q2"))
                    {
                        var N = 0;

                        foreach(var mm in MM.Where(x => MarkDMG(x)))
                        {
                            N++;

                            if (N >= Slider(Settings.Lane, "Q2"))
                            {
                                Q.Cast(mm);
                            }
                        }
                    }
                }
                else
                {
                    var T = M.FirstOrDefault(x => x.IsValidTarget(Q.Range));

                    Q.Cast(T);
                }
            }
        }
    }
}
