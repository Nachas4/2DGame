using _2DGame.Models;
using Content.Models;

namespace _2DGame.Content.Models
{
    public class Enemy : Base
    {
        public bool Alive { get; private set; }
        public bool IsBoss { get; private set; }

        public Enemy(string[] spawnPos, bool isBoss = false) : base()
        {
            Alive = true;
            IsBoss = isBoss;

            XPos = int.Parse(spawnPos[0]);
            YPos = int.Parse(spawnPos[1]);

            VecPosition.X *= XPos;
            VecPosition.Y *= YPos;
        }

        public void MakeDamage(Player player)
        {
            player.TakeDamage(Attack);
        }

        public void TakeDamage(int dmg)
        {
            if (!Alive)
                return;

            if (Hp > 0 && Alive)
                Hp -= dmg;
            else
                Alive = !Alive;
        }
    }
}
