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

namespace Ravenborn_LeBlanc
{
    public class Base
    {
        public int NetworkID { get; set; }
        public Vector3 Pos { get; set; }
        public bool IsRW { get; set; }
    }

    public class Extensions
    {
        public static Spell.Targeted Q = new Spell.Targeted(SpellSlot.Q, 700, DamageType.Magical);
        public static Spell.Skillshot W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Circular, 600, 1450, 220);
        public static Spell.Skillshot E = new Spell.Skillshot(SpellSlot.E, 820, SkillShotType.Linear, 300, 1650, 55);
        public static Spell.Active R = new Spell.Active(SpellSlot.R);

        public static bool HasMark(Obj_AI_Base T) => T.HasBuff("leblancpminion");
        public static bool MarkDMG(Obj_AI_Base T) => Math.Max(0, T.GetBuff("leblancpminion").EndTime - Game.Time) <= 2.5;
        public static List<Base> About = new List<Base>();

        public static bool UnderEnemyTurret(Vector3 pos)
        {
            return EntityManager.Turrets.Enemies.Any(t => t.IsValidTarget() && pos.IsInRange(t, t.GetAutoAttackRange()));
        }

        #region Damage
        public static float Damage(AIHeroClient T)
        {
            float Damage = 0f;

            //Chat.Print(Player.Instance.TotalMagicalDamage * 0.511f); // Q and E // R - W
            //Chat.Print(Player.Instance.TotalMagicalDamage * 0.6f); // RQ and W
            //Chat.Print(Player.Instance.TotalMagicalDamage * 0.4f); // RE
            /*
             * Q = 55, 80, 105, 130, 155
             * W = 85, 125, 165, 205, 245 
             * E = 40, 65, 90, 115, 140
             * 
             * Q = 150, 275, 400
             * W = 125, 225, 325
             * E = 100, 160, 220
             */

            if (Q.IsReady())
                Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 55, 80, 105, 130, 155 }[Q.Level - 1] + Player.Instance.TotalMagicalDamage * 0.511f);

            if (W.IsReady())
                Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 85, 125, 165, 205, 245 }[W.Level - 1] + Player.Instance.TotalMagicalDamage * 0.6f);

            if (E.IsReady())
                Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 40, 65, 90, 115, 140 }[E.Level - 1] + Player.Instance.TotalMagicalDamage * 0.511f);

            if (R.IsReady())
            {
                SpellSlot S = ReturnSlot(ComboBox(Settings.Combo, "SR"));

                switch(S)
                {
                    case SpellSlot.Q:
                        Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 150, 275, 400 }[R.Level - 1] + Player.Instance.TotalMagicalDamage * 0.6f);
                        break;

                    case SpellSlot.W:
                        Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 125, 225, 325 }[R.Level - 1] + Player.Instance.TotalMagicalDamage * 0.511f);
                        break;

                    case SpellSlot.E:
                        Damage += T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 100, 160, 220 }[R.Level - 1] + Player.Instance.TotalMagicalDamage * 0.4f);
                        break;
                }
            }

            return Damage;
        }
        #endregion

        #region Update Check
        public static void Updated()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/EloBuddy/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Chat.Print("[" + Assembly.GetExecutingAssembly().GetName().Name + "] - Outdated", System.Drawing.Color.Red);
                }
                else
                {
                    Chat.Print("[" + Assembly.GetExecutingAssembly().GetName().Name + "] - Updated", System.Drawing.Color.Purple);
                }
            }
        }
        #endregion

        #region About Spells
        public static SpellSlot ReturnSlot(int S)
        {
            switch (S)
            {
                case 0:
                    return SpellSlot.Q;

                case 1:
                    return SpellSlot.W;

                case 2:
                    return SpellSlot.E;
            }

            return SpellSlot.Unknown;
        }

        public static State GetState(SpellSlot S)
        {
            if (S == SpellSlot.W)
            {
                if (Player.GetSpell(SpellSlot.W).Name == "LeblancW")
                {
                    return State.WNormal;
                }else
                {
                    return State.WReturn;
                }
            }

            if (R.IsLearned)
            {
                if (S == SpellSlot.R)
                {
                    switch (Player.GetSpell(SpellSlot.R).Name)
                    {
                        case "LeblancRToggle":
                            return State.RNormal;

                        case "LeblancRW":
                            return State.RWCast;

                        case "LeblancRWReturn":
                            return State.RWReturn;

                        case "LeblancR":
                            return State.RRCast;
                    }
                }
            }

            return State.Null;
        }
        #endregion

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

    public enum State
    {
        WNormal, WReturn, // W
        RNormal, RWCast, RWReturn, RRCast, // R
        Null
    }
}
