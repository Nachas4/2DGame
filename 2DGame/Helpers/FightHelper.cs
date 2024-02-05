using _2DGame.Content.Globals;
using _2DGame.Content.Models;
using Content.Models;
using System;
using System.Linq;

namespace _2DGame.Helpers
{
    public static class FightHelper
    {
        internal static Player Player = GVars.Player;
        private static Enemy Enemy;

        // Switches whether enemy or player should attack at NextBlow()
        private static bool PlayerTurn;

        internal static void NextBlow()
        {
            bool deathOccured;

            if (PlayerTurn)
                deathOccured = Enemy.TakeDamage(Player.Attack + (int)Math.Pow(Player.D6, 2));
            else
                deathOccured = Player.TakeDamage(Enemy.Attack + (int)Math.Pow(Enemy.D6, 2));

            PlayerTurn = !PlayerTurn;

            if (deathOccured)
                Player.InFight = false;
        }

        internal static void CheckForFight() 
        {
            Enemy = GVars.CurrentMap.Enemies.Where(x => x.Alive && x.PositionIndex == Player.PositionIndex).FirstOrDefault();

            Player.InFight = Enemy != null;

            if (Player.InFight)
                PlayerTurn = GVars.EnemiesShouldMove;
        }
    }
}
