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
    class Jungle : Extensions
    {
        public static void Load()
        {
            var M = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, E.Range).FirstOrDefault();

            if (M != null)
            {

                if (Q.IsReady() && CheckBox(Settings.Jungle, "Q"))
                {
                    if (M.IsValidTarget(Q.Range))
                    {
                        Q.Cast(M);
                    }
                }

                if (W.IsReady() && CheckBox(Settings.Jungle, "W"))
                {
                    if (M.IsValidTarget(W.Range))
                    {
                        W.Cast();
                    }
                }

                if (E.IsReady() && CheckBox(Settings.Jungle, "E"))
                {
                    if (M.IsValidTarget(Q.Range))
                    {
                        E.Cast(M);
                    }
                }
            }
        }
    }
}
