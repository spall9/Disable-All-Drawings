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

namespace Dragonblade_Talon.Modes
{
    class Lane : Extensions
    {
        public static void Load()
        {
            var M = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, W.Range);

            if (M != null)
            {
                var L = EntityManager.MinionsAndMonsters.GetLineFarmLocation(M, W.Width, (int)W.Range);

                if (W.IsReady() && CheckBox(Settings.Lane, "W"))
                {
                    if (L.HitNumber >= Slider(Settings.Lane, "W1"))
                    {
                        W.Cast(L.CastPosition);
                    }
                }

                if (Q.IsReady() && CheckBox(Settings.Lane, "Q"))
                {
                    var MM = M.FirstOrDefault();

                    if (MM != null)
                    {
                        if (ComboBox(Settings.Lane, "Q1") == 0)
                        {
                            if (Player.Instance.GetSpellDamage(MM, SpellSlot.Q, DamageLibrary.SpellStages.Default) >= MM.Health)
                            {
                                Q.Cast(MM);
                            }
                        }
                        else
                        {
                            Q.Cast(MM);
                        }
                    }
                }
            }
        }
    }
}
