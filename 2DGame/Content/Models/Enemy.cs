using _2DGame.Content.Globals;
using _2DGame.Models;
using MonoGame.Extended.Sprites;
using System;

namespace _2DGame.Content.Models
{
    public class Enemy : Base
    {
        public bool IsBoss { get; private set; }
        public bool HasKey { get; private set; }

        public AnimatedSprite EnemySprite { get; private set; }

        public Enemy(string[] spawnPos, int mapLvl, bool isBoss = false, bool hasKey = false) : base()
        {
            IsBoss = isBoss;

            D6 = new Random().Next(1, 7);

            int[] lvlSelection = new int[] { mapLvl, mapLvl, mapLvl, mapLvl, mapLvl, mapLvl, mapLvl++, mapLvl++, mapLvl++, mapLvl++, mapLvl + 2 }; // X: 50% -- X + 1: 40% -- X + 2: 10%
            Level = lvlSelection[new Random().Next(lvlSelection.Length)];

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
