using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

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

namespace Dragonblade_Talon
{
    class Talon : Extensions
    {
        public static void Loading(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Talon)
                return;

            Settings.Load();

            Game.OnTick += delegate
            {
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                    Modes.Combo.Load();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                    Modes.Lane.Load();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                    Modes.Jungle.Load();
            };

            Drawing.OnEndScene += delegate
            {
                if (CheckBox(Settings.Draw, "Q") && Q.IsReady())
                {
                    Q.DrawRange(Color.Gray, 4);
                }

                if (CheckBox(Settings.Draw, "W") && W.IsReady())
                {
                    W.DrawRange(Color.Gray, 4);
                }
            };
        }
    }
}
