using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using i = EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Hextech_Annie
{
    class Menu
    {
        public static i.Menu Principal, Q, W, E, R, Tibbers, Misc, Draw;

        public static void Load()
        {
            Principal = i.MainMenu.AddMenu("Hextech Annie", "AnnieAkr");
            Principal.Add("Skin", new CheckBox("Skin Hack?", false));
            Principal.Add("SkinID", new Slider("Skin ID: {0}", 10, 0, 10));

            Q = Principal.AddSubMenu("Q Configs");
            Q.Add("Combo", new CheckBox("Use Q on Combo?"));
            Q.Add("Lane", new CheckBox("Use Q on Laneclear?"));
            Q.Add("Jungle", new CheckBox("Use Q on Jungleclear?"));
            Q.Add("Logic", new ComboBox("Logic Mode on Laneclear:", 0, "Mana", "Clear"));

            W = Principal.AddSubMenu("W Configs");
            W.Add("Combo", new CheckBox("Use W on Combo?"));
            W.Add("Lane", new CheckBox("Use W on Laneclear?"));
            W.Add("Jungle", new CheckBox("Use W on Jungleclear?"));

            E = Principal.AddSubMenu("E Configs");
            E.Add("Auto", new CheckBox("Auto Stack Passive Using E?"));

            R = Principal.AddSubMenu("R Configs");
            R.Add("Combo", new CheckBox("Use R on Combo?"));
            R.Add("ComboMin", new Slider("Min {0} enemies R", 3, 0, 5));
            R.Add("Logic", new CheckBox("Logic 1vs1?"));

            Tibbers = Principal.AddSubMenu("Tibbers");
            Tibbers.Add("Mode", new ComboBox("Pilot Mode:", 0, "Focuses on the nearest enemy", "Focuses on the enemy with the lowest HP"));

            Misc = Principal.AddSubMenu("Misc");
            Misc.Add("Stun", new CheckBox("Don't use spells on Laneclear, if have stun"));
            Misc.Add("Flash", new KeyBind("Flash + Ult", false, KeyBind.BindTypes.HoldActive, 'T'));
            Misc.Add("AA", new KeyBind("Disable AA", false, KeyBind.BindTypes.PressToggle, 'A'));
            Misc.Add("Int", new CheckBox("Interrupt?"));
            Misc.Add("Gap", new CheckBox("Gapcloser?"));
            Misc.Add("Ignite", new CheckBox("Auto use ignite?"));

            Draw = Principal.AddSubMenu("Draw");
            Draw.Add("Disable", new CheckBox("Disable All Draws?", false));
            Draw.Add("Q", new CheckBox("Draw Q?"));
            Draw.Add("W", new CheckBox("Draw W?"));
            Draw.Add("R", new CheckBox("Draw R?"));
            Draw.Add("Flash", new CheckBox("Draw Flash + R?"));
        }

        public static bool CheckBox(i.Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(i.Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(i.Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }

        public static int ComboBox(i.Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
    }
}