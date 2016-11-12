using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Championship_Riven
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += delegate
            {
                if (Player.Instance.Hero != Champion.Riven)
                    return;

                Extensions.Update();

                Riven.LoadModules();

                Drawing.OnDraw += delegate
                {
                    if (Menu.CheckBox(Menu.Draw, "Disable"))
                        return;

                    if (Menu.CheckBox(Menu.Draw, "Status"))
                    {
                        Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position).X - 40,
                            Drawing.WorldToScreen(Player.Instance.Position).Y + 20, System.Drawing.Color.White, "ForceR");
                        Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position).X + 12,
                            Drawing.WorldToScreen(Player.Instance.Position).Y + 20,
                            Menu.Keybind(Menu.R, "Force") ? System.Drawing.Color.LimeGreen : System.Drawing.Color.Red,
                            Menu.Keybind(Menu.R, "Force") ? "(On)" : "(Off)");
                    }

                    if (Menu.CheckBox(Menu.Draw, "Burst"))
                    {
                        Player.Instance.DrawCircle(
                            (int)
                            (Extensions.Flash.IsReady()
                                ? Extensions.Flash.Range + Extensions.E.Range
                                : Extensions.E.Range + Player.Instance.GetAutoAttackRange()), System.Drawing.Color.Orange);
                    }

                    if (Menu.CheckBox(Menu.Draw, "Q") && Extensions.Q.IsReady())
                    {
                        Player.Instance.DrawCircle(Extensions.RealQ(), System.Drawing.Color.BlueViolet);
                    }

                    if (Menu.CheckBox(Menu.Draw, "W") && Extensions.W.IsReady())
                    {
                        Player.Instance.DrawCircle(Extensions.RealW(), System.Drawing.Color.Purple);
                    }

                    if (Menu.CheckBox(Menu.Draw, "E") && Extensions.E.IsReady())
                    {
                        Player.Instance.DrawCircle((int) Extensions.E.Range, System.Drawing.Color.Yellow);
                    }

                    if (Menu.CheckBox(Menu.Draw, "R") && Extensions.R.IsReady())
                    {
                        Player.Instance.DrawCircle((int) Extensions.R2.Range, System.Drawing.Color.Red);
                    }
                };
            };
        }
    }
}
