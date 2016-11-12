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
    public class Extensions
    {
        public static Spell.Skillshot Q = new Spell.Skillshot(SpellSlot.Q, (uint)Player.Instance.GetAutoAttackRange() + 325, SkillShotType.Linear, 250, 1500, 70, DamageType.Physical);
        public static Spell.Active W = new Spell.Active(SpellSlot.W, 500);
        public static Spell.Skillshot E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 1500, 70, DamageType.Physical);

        public static bool HasPassive => Player.Instance.Mana == 4;
        public static bool UltActive = Player.Instance.HasBuff("rengarr");
        public static BuffType[] Buffs = new BuffType[] { BuffType.Charm, BuffType.Snare, BuffType.Stun, BuffType.Suppression, BuffType.Taunt, BuffType.Silence, BuffType.Knockback };

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
