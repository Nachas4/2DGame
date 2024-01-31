using Microsoft.Xna.Framework;
using System;

namespace _2DGame.Models
{
    public class Base
    {
        protected int Hp;
        public int Attack;
        public int Defense;

        protected int Level;

        public bool Alive;

        protected int D6;
        

        //Postions
        public Vector2 VecPosition = new(64, 64); //Starting square

        public int XPos = 1; //should be protected, set for logging rn
        public int YPos = 1;
        public int PositionIndex { get { return XPos * 12 + YPos; } }

        public Base()
        {
            Level = 1;
            Alive = true;

            //These are defaults for Player
            D6 = new Random().Next(1, 7);

            Hp = 20 + 3 * D6;
            Defense = 2 * D6;
            Attack = 5 + D6;
        }
    }
}
