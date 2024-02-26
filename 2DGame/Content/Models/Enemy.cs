using _2DGame.Content.Globals;
using _2DGame.Models;
using MonoGame.Extended.Sprites;
using System;

namespace _2DGame.Content.Models
{
    internal class Enemy : Base
    {
        internal bool IsBoss { get; private set; }
        internal bool HasKey { get; private set; }

        internal AnimatedSprite EnemySprite { get; private set; }

        internal int LastMove { get; set; } // 0: left -- 1: right -- 2: up -- 3: down

        internal Enemy(string[] spawnPos, int mapLvl, bool isBoss = false, bool hasKey = false) : base()
        {
            Random rnd = new();

            IsBoss = isBoss;

            D6 = rnd.Next(1, 7);

            int[] lvlSelection = new int[] { mapLvl, mapLvl, mapLvl, mapLvl, mapLvl, mapLvl, mapLvl++, mapLvl++, mapLvl++, mapLvl++, mapLvl + 2 }; // X: 50% -- X + 1: 40% -- X + 2: 10%
            Level = lvlSelection[rnd.Next(lvlSelection.Length)];

            if (isBoss)
            {
                Hp = 2 * Level * D6 + D6;
                MaxHp = Hp;
                Defense = Level / 2 * D6;
                Attack = Level + D6 + Level;
            }
            else
            {
                Hp = 2 * Level * D6;
                MaxHp = Hp;
                Defense = Level / 2 * D6;
                Attack = Level + D6;
            }

            HasKey = hasKey;

            //Spawn position
            XPos = int.Parse(spawnPos[0]);
            YPos = int.Parse(spawnPos[1]);

            VecPosition.Y *= XPos;
            VecPosition.X *= YPos;


            EnemySprite = new AnimatedSprite(GVars.EnemySpriteSheet); //doesn't iterfere with boss sprite
        }
    }
}
