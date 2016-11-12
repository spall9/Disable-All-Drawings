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
    class Jungle : Extensions
    {
        public static void Load()
        {
            var M = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, W.Range).FirstOrDefault();

            if (M != null)
            {
                if (Q.IsReady() && CheckBox(Settings.Jungle, "Q"))
                {
                    Q.Cast(M);
                }

                if (W.IsReady() && CheckBox(Settings.Jungle, "W"))
                {
                    W.Cast(M);
                }
            }
        }
    }
}
