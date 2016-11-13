using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

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
    internal class DamageIndicator
    {
        private const int BarWidth = 106;

        public DamageIndicator()
        {
            Drawing.OnEndScene += OnEndScene;
        }

        public static void OnEndScene(EventArgs args)
        {
            if (Extensions.CheckBox(Settings.Draw, "DMG"))
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered))
                {
                    var damage = Extensions.Damage(unit);

                    if (damage <= 0)
                    {
                        continue;
                    }
                    var damagePercentage = ((unit.TotalShieldHealth() - damage) > 0
                        ? (unit.TotalShieldHealth() - damage)
                        : 0) /
                                           (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var currentHealthPercentage = unit.TotalShieldHealth() /
                                                  (unit.MaxHealth + unit.AllShield + unit.AttackShield +
                                                   unit.MagicShield);

                    var startPoint = new Vector2((int)(unit.HPBarPosition.X + damagePercentage * BarWidth),
                        (int)unit.HPBarPosition.Y - 5 + 14);
                    var endPoint = new Vector2((int)(unit.HPBarPosition.X + currentHealthPercentage * BarWidth) + 1,
                        (int)unit.HPBarPosition.Y - 5 + 14);

                    Line.DrawLine(Color.DarkOrange, 7, startPoint, endPoint);
                }
            }
        }
    }
}