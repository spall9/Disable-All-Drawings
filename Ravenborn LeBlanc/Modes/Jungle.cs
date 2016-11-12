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
    class Jungle : Extensions
    {
        public static void Load()
        {
            foreach (var M in EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, E.Range))
            {
                if (CheckBox(Settings.Jungle, "E") && E.IsReady())
                {
                    if (M.IsValidTarget(E.Range))
                    {
                        E.Cast(M);
                    }
                }

                if (CheckBox(Settings.Jungle, "W") && W.IsReady())
                {
                    if (M.IsValidTarget(W.Range))
                    {
                        W.Cast(M);
                    }
                }

                if (CheckBox(Settings.Jungle, "Q") && Q.IsReady())
                {
                    if(M.IsValidTarget(Q.Range))
                    {
                        Q.Cast(M);
                    }
                }
            }
        }
    }
}
