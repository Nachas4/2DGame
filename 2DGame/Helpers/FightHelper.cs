using _2DGame.Content.Models;
using Content.Models;
using System;

namespace _2DGame.Helpers
{
    public static class FightHelper
    {
        internal static Player Player;
        private static Enemy Enemy;

        // Switches whether enemy or player should attack at NextBlow()
        private static bool BlowSwitcher;

        internal static void NextBlow()
        {
            if (BlowSwitcher)
            {
                Enemy.TakeDamage(Player);
            }
            else
            {
                Player.TakeDamage(Enemy);
            }
        }
    }
}
