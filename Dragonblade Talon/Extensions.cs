using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using Version = System.Version;
using System.Net;

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
    public class Extensions
    {
        public static Spell.Targeted Q = new Spell.Targeted(SpellSlot.Q, 550);
        public static Spell.Skillshot W = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Cone, 250, 1850, 60, DamageType.Physical);
        public static Spell.Active E = new Spell.Active(SpellSlot.E, 700);
        public static Spell.Active R = new Spell.Active(SpellSlot.R, 500);

        public static bool Stealth = Player.Instance.HasBuff("TalonRStealth");

        #region Menu
        public static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }

        public static int ComboBox(Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
        #endregion
    }
}