using _2DGame.Content.Globals;
using _2DGame.Content.Models;
using Content.Models;
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
            if (PlayerTurn)
            {
                bool deathOccured = Enemy.TakeDamage(Player.Attack + Player.D6 * 2);

                if (deathOccured)
                {
                    if (Enemy.IsBoss) Player.HasKilledBoss = true;
                    
                    if (Enemy.HasKey) Player.HasKey = true;

                    Player.InFight = false;
                    Player.LevelUp();

                    MapHelper.LoadNextMap(GVars.Player.HasKey && GVars.Player.HasKilledBoss);
                }
            }
            else
                Player.TakeDamage(Enemy.Attack + Enemy.D6 * 2);

            PlayerTurn = !PlayerTurn;   
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
