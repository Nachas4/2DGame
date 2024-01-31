using Microsoft.Xna.Framework;
using System;

namespace _2DGame.Models
{
    public class Base
    {
        protected int Hp;
        public int Attack;
        protected int Debuffs;

        public Vector2 VecPosition = new(64, 64);

        public int XPos = 1; //should be protected, set for logging rn
        public int YPos = 1;
        public int PositionIndex
        {
            get { return XPos * 12 + YPos; }
        }

        public Base()
        {
            Random rdn = new();
            int r = rdn.Next(20, 27);
            Hp = r;
            r = rdn.Next(3, 5);
            Attack = r;
        }
    }
}
