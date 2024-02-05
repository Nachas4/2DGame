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

        public Enemy(string[] spawnPos, bool isBoss = false, bool hasKey = false) : base()
        {
            D6 = new Random().Next(1, 7);

            Hp = 2 * Level * D6;
            Defense = Level / 2 * D6;
            Attack = Level + D6;

            IsBoss = isBoss;
            HasKey = hasKey;

            //Spawn position
            XPos = int.Parse(spawnPos[0]);
            YPos = int.Parse(spawnPos[1]);

            VecPosition.Y *= XPos;
            VecPosition.X *= YPos;


            EnemySprite = new AnimatedSprite(GVars.EnemySpriteSheet);
        }
    }
}
