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

namespace NightHunter_Rengar
{
    class Settings : Extensions
    {
        public static Menu Principal, Combo, Lane, Jungle, Misc, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Rengar", "NightHunter");

            Combo = Principal.AddSubMenu("Combo");
            Combo.AddLabel("About Q:");
            Combo.Add("Q", new CheckBox("Use Q?"));
            Combo.AddSeparator(2);
            Combo.AddLabel("About W:");
            Combo.Add("W", new CheckBox("Auto use W?"));
            Combo.Add("W1", new Slider("Min Duration (Buff):", 400, 1, 1600));
            foreach (var B in Buffs)
            {
                Combo.Add("W/" + B.ToString(), new CheckBox("Use W on Buff " + B.ToString()));
            }
            Combo.AddSeparator(2);
            Combo.AddLabel("About E:");
            Combo.Add("E", new CheckBox("Use E?"));
            Combo.AddLabel("About Passive:");
            Combo.Add("Focus", new ComboBox("Focus Spell:", 0, "Q", "E"));

            Lane = Principal.AddSubMenu("Laneclear");
            Lane.Add("Passive", new CheckBox("Don't use spells if have Passive?"));
            Lane.AddLabel("About Q:");
            Lane.Add("Q", new CheckBox("Use Q?"));
            Lane.Add("Q1", new Slider("Min {0} minions to use Q", 3, 1, 5));
            Lane.AddSeparator(2);
            Lane.AddLabel("About E:");
            Lane.Add("E", new CheckBox("Use E?"));

            Jungle = Principal.AddSubMenu("Junglecler");
            Jungle.Add("Q", new CheckBox("Use Q?"));
            Jungle.Add("W", new CheckBox("Use W?"));
            Jungle.Add("E", new CheckBox("Use E?"));

            Misc = Principal.AddSubMenu("Misc");
            Misc.Add("Gap", new CheckBox("Use E on Gapcloser?"));
            Misc.Add("Int", new CheckBox("Use E to Interrupt?"));

            Draw = Principal.AddSubMenu("Draw");
            Draw.Add("Q", new CheckBox("Draw Q?"));
            Draw.Add("W", new CheckBox("Draw W?"));
            Draw.Add("E", new CheckBox("Draw E?"));
        }
    }
}
