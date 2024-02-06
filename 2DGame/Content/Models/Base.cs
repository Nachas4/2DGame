using Microsoft.Xna.Framework;
using System;

namespace _2DGame.Models
{
    public class Base
    {
        internal int Hp; //should be protected, set for logging rn
        protected int MaxHp;
        internal int Attack;
        internal int Defense;

        internal bool Alive;

        internal int D6;

        internal int Level;

        //Postions
        internal Vector2 VecPosition = new(64, 64); //Starting square

        internal int XPos = 1; //should be protected, set for logging rn
        internal int YPos = 1; //should be protected, set for logging rn
        internal int PositionIndex { get { return XPos * 12 + YPos; } }

        public Base()
        {
            Level = 1;
            Alive = true;

            //These are defaults for Player
            D6 = new Random().Next(1, 7);

            //Hp = 20 + 3 * D6;
            Hp = 1000;
            MaxHp = Hp;
            Defense = 2 * D6;
            Attack = 5 + D6;
        }

        public bool TakeDamage(int dmg)
        {
            if (!Alive) return true;

            if (dmg > Defense)
            {
                Hp -= dmg - Defense;

                //Player dies
                if (Hp < 0)
                {
                    Alive = !Alive;
                    return true;
                }
            }

            return false;
        }
    }
}
