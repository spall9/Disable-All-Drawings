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

using System.Reflection;
using System.Net;
using Version = System.Version;
using System.Text.RegularExpressions;

namespace Hextech_Annie
{
    internal class Extensions
    {
        public static Spell.Targeted Q, Ignite;
        public static Spell.Skillshot W, R, Flash;
        public static Spell.Active E;

        public static void Update()
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
                    Chat.Print("[" + Assembly.GetExecutingAssembly().GetName().Name + "] - Updated", System.Drawing.Color.Green);
                }
            }
        }

        public static AIHeroClient Target => TargetSelector.GetTarget(Q.Range, DamageType.Magical);
        public static bool Stun => Player.Instance.HasBuff("pyromania_particle");
        public static bool Combo => Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        public static bool Laneclear => Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        public static bool Jungleclear => Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
    }
}
