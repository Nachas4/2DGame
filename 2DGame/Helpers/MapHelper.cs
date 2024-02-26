using _2DGame.Content.Globals;
using Microsoft.Xna.Framework;
using System;

namespace _2DGame.Helpers
{
    internal static class MapHelper
    {
        internal static void LoadNextMap(bool lvlCompleted)
        {
            if (!lvlCompleted)
                return;

            ResetPlayerValues();

            //Heal player
            Random rnd = new();
            int thirdOfHp = GVars.Player.MaxHp / 3;
            int tenthOfHp = GVars.Player.MaxHp / 10;

            GVars.Player.Hp += new int[] { GVars.Player.MaxHp, thirdOfHp, thirdOfHp, thirdOfHp, thirdOfHp, tenthOfHp, tenthOfHp, tenthOfHp, tenthOfHp, tenthOfHp, tenthOfHp }[rnd.Next(10)]; // full: 10% -- third: 40% -- tenth: 50%

            if (GVars.Player.Hp > GVars.Player.MaxHp) GVars.Player.Hp = GVars.Player.MaxHp;

            //Load next map
            GVars.CurrentMapNum++;
            GVars.CurrentMap = GVars.Maps[GVars.CurrentMapNum];
        }

        private static void ResetPlayerValues()
        {
            GVars.Player.HasKey = false;
            GVars.Player.HasKilledBoss = false;

            GVars.Player.XPos = 1;
            GVars.Player.YPos = 1;
            GVars.Player.VecPosition = new Vector2(64, 64);
        }
    }
}
