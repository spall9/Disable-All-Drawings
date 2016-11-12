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

namespace Dragonblade_Talon
{
    class Settings
    {
        public static Menu Principal, Combo, Lane, Jungle, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Talon", "Dragonblade");
            Combo = Principal.AddSubMenu("Combo");
            Combo.Add("Q", new CheckBox("Use Q?"));
            Combo.Add("W", new CheckBox("Use W?"));
            Combo.Add("W1", new CheckBox("No use W if ult is active?"));
            Combo.Add("R", new CheckBox("Use R?"));
            Combo.Add("R1", new Slider("Min Enemy Hp {0}% to use R", 40, 10, 100));

            Lane = Principal.AddSubMenu("Lane");
            Lane.AddLabel("About Q:");
            Lane.Add("Q", new CheckBox("Use Q?"));
            Lane.Add("Q1", new ComboBox("Mode Q:", 0, "Last hit", "Lane clear"));
            Lane.AddSeparator(2);
            Lane.AddLabel("About W:");
            Lane.Add("W", new CheckBox("Use W?"));
            Lane.Add("W1", new Slider("Min {0} minions to cast W", 3, 1, 5));

            Jungle = Principal.AddSubMenu("Jungle");
            Jungle.Add("Q", new CheckBox("Use Q?"));
            Jungle.Add("W", new CheckBox("Use W?"));

            Draw = Principal.AddSubMenu("Draw");
            Draw.Add("Q", new CheckBox("Draw Q?"));
            Draw.Add("W", new CheckBox("Draw W?"));
        }
    }
}
