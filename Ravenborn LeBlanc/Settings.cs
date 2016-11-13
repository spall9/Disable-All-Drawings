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

namespace Ravenborn_LeBlanc
{
    class Settings
    {
        public static Menu Principal, Combo, Harass, Lane, Jungle, Misc, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("LeBlanc", "Ravenborn");
            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q?"));
            Combo.Add("W", new CheckBox("Use W?"));
            Combo.Add("W1", new CheckBox("Auto Return W/R?"));
            Combo.Add("E", new CheckBox("Use E?"));
            Combo.Add("R", new CheckBox("Use R?"));
            Combo.Add("SR", new ComboBox("Focus R Spell:", 0, "Q", "W", "E"));

            Harass = Principal.AddSubMenu("Harass", "Harass");
            Harass.Add("Key", new KeyBind("Harass Key:", false, KeyBind.BindTypes.PressToggle, 'H'));
            Harass.Add("Q", new CheckBox("Use Q?"));
            Harass.Add("W", new CheckBox("Use W?"));
            Harass.Add("E", new CheckBox("Use E?"));

            Lane = Principal.AddSubMenu("Laneclear", "Lane");
            Lane.AddLabel("About Q:");
            Lane.Add("Q", new CheckBox("Use Q?"));
            Lane.Add("Q1", new CheckBox("Use Q only if minions have (Passive)"));
            Lane.Add("Q2", new Slider("Min {0} minions to use Q with (Passive)", 3, 1, 5));
            Lane.AddSeparator(2);
            Lane.AddLabel("About W:");
            Lane.Add("W", new CheckBox("Use W?"));
            Lane.Add("W1", new Slider("Min {0} minions to use W", 3, 1, 5));

            Jungle = Principal.AddSubMenu("Jungleclear", "Jungle");
            Jungle.Add("Q", new CheckBox("Use Q?"));
            Jungle.Add("W", new CheckBox("Use W?"));
            Jungle.Add("E", new CheckBox("Use E?"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Int", new CheckBox("Use E to Interrupt?"));
            Misc.Add("Int2", new CheckBox("Use R(E) to Interrupt?"));
            Misc.Add("Gap", new CheckBox("Use E on Gapcloser?"));
            Misc.Add("Gap2", new CheckBox("Use R(E) on Gapcloser?"));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.Add("Q", new CheckBox("Draw Q?"));
            Draw.Add("W", new CheckBox("Draw W?"));
            Draw.Add("E", new CheckBox("Draw E?"));
            Draw.Add("DMG", new CheckBox("Draw Combo Damage?"));
        }
    }
}