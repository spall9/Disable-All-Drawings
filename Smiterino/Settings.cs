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
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace Smiterino
{
    class Settings
    {
        public static Menu Principal;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Smiterino", "Smiterino");
            Principal.Add("Enable", new KeyBind("Enable Smite?", false, KeyBind.BindTypes.PressToggle, 'H'));
            Principal.Add("Draw", new CheckBox("Enable Drawings?"));
            Principal.AddSeparator(2);

            if (Extensions.Map == GameMapId.SummonersRift)
            {
                Principal.AddGroupLabel("Dragons");
                Principal.Add("SRU_Dragon_Air", new CheckBox("Air Dragon?"));
                Principal.Add("SRU_Dragon_Fire", new CheckBox("Fire Dragon?"));
                Principal.Add("SRU_Dragon_Earth", new CheckBox("Earth Dragon?"));
                Principal.Add("SRU_Dragon_Water", new CheckBox("Water Dragon?"));
                Principal.Add("SRU_Dragon_Elder", new CheckBox("Elder Dragon?"));
                Principal.AddSeparator(2);
                Principal.AddGroupLabel("Big Mobs");
                Principal.Add("SRU_Baron", new CheckBox("Baron?"));
                Principal.Add("SRU_Blue", new CheckBox("Blue?"));
                Principal.Add("SRU_Red", new CheckBox("Red?"));
                Principal.Add("SRU_RiftHerald", new CheckBox("Rift Herald?"));
                Principal.AddSeparator(2);
                Principal.AddGroupLabel("Small Mobs");
                Principal.Add("SRU_Gromp", new CheckBox("Gromp?"));
                Principal.Add("SRU_Murkwolf", new CheckBox("Wolves?"));
                Principal.Add("SRU_Krug", new CheckBox("Krug?"));
                Principal.Add("SRU_Razorbeak", new CheckBox("Razor?"));
                Principal.Add("SRU_Crab", new CheckBox("Crab?"));
            }
        }
    }
}