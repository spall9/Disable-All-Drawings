using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Hextech_Annie
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += delegate
            {
                if (Player.Instance.Hero != Champion.Annie)
                    return;

                Extensions.Update();

                Annie.LoadModules();

                Drawing.OnDraw += delegate
                {
                    if (Menu.CheckBox(Menu.Draw, "Disable"))
                        return;

                    if (Extensions.Q.IsReady() && Menu.CheckBox(Menu.Draw, "Q"))
                    {
                        Circle.Draw(Color.DarkBlue, Extensions.Q.Range, Player.Instance.Position);
                    }

                    if (Extensions.W.IsReady() && Menu.CheckBox(Menu.Draw, "W"))
                    {
                        Circle.Draw(Color.DarkBlue, Extensions.W.Range, Player.Instance.Position);
                    }

                    if (Extensions.R.IsReady() && Menu.CheckBox(Menu.Draw, "R"))
                    {
                        Circle.Draw(Color.DarkBlue, Extensions.R.Range, Player.Instance.Position);
                    }

                    if (Extensions.Flash.IsReady() && Menu.CheckBox(Menu.Draw, "Flash"))
                    {
                        Circle.Draw(Color.Yellow, Extensions.Flash.Range + Extensions.R.Range, Player.Instance.Position);
                    }
                };
            };
        }
    }
}
