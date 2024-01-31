using _2DGame.Content.Globals;
using _2DGame.Content.Models;
using _2DGame.Models;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Content.Models
{
    public class Player : Base
    {
        private bool SpacekeyUp;
        public bool InFight;

        public Player() : base()
        {
            SpacekeyUp = true;
        }

        public void TakeDamage(Enemy attacker)
        {
            if (!Alive) return;

            int dmg = attacker.Attack + D6 * D6;

            if (dmg > Defense)
            {
                Hp -= dmg - Defense;

                //Player dies
                if (Hp < 0)
                    Alive = !Alive;
            }
        }

        public void MakeDamage(int playerPos, KeyboardState keyboardState)
        {
            //SPACE key
            if (keyboardState.IsKeyUp(Keys.Space))
            {
                SpacekeyUp = true;
            }
            if (SpacekeyUp)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    int[] attackedTiles = new int[]
                    {
                        //Left and right
                        playerPos - 1,
                        playerPos + 1,
                        
                        //Top row
                        playerPos - 13,
                        playerPos - 12,
                        playerPos - 11,

                        //Bottom row
                        playerPos + 11,
                        playerPos + 12,
                        playerPos + 13,
                    };

                    var enemiesInVicinity = GVars.CurrentMap.Enemies.Where(x => attackedTiles.Contains(x.PositionIndex));

                    foreach (Enemy enemy in enemiesInVicinity)
                    {
                        enemy.TakeDamage(this);
                    }

                    SpacekeyUp = false;
                }
            }
        }


        public void MovePlayer(Map map, KeyboardState keyboardState)
        {
            //W key
            if (keyboardState.IsKeyDown(Keys.W) && map.Tiles[PositionIndex - 12].Sprite.Name == "grasstile")
            {

                VecPosition.Y = Math.Max(0, VecPosition.Y - GVars.CurrentMap.TileSize);

                XPos -= 1;
                GVars.PlayerSprite.Play("up");

                if (keyboardState.IsKeyUp(Keys.W))
                {
                    GVars.PlayerSprite.Play("upStand");
                }
            }



            //A key
            if (keyboardState.IsKeyDown(Keys.A) && map.Tiles[PositionIndex - 1].Sprite.Name == "grasstile")
            {
                VecPosition.X = Math.Max(0, VecPosition.X - GVars.CurrentMap.TileSize);

                YPos -= 1;
                GVars.PlayerSprite.Play("left");

                if (keyboardState.IsKeyUp(Keys.A))
                {
                    GVars.PlayerSprite.Play("leftStand");
                }
            }


            //S key
            if (keyboardState.IsKeyDown(Keys.S) && map.Tiles[PositionIndex + 12].Sprite.Name == "grasstile")
            {
                VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, VecPosition.Y + GVars.CurrentMap.TileSize);

                XPos += 1;
                GVars.PlayerSprite.Play("down");
                
                if (keyboardState.IsKeyUp(Keys.S))
                {
                    GVars.PlayerSprite.Play("downStand");
                }
            }



            //D key
            if (keyboardState.IsKeyDown(Keys.D) && map.Tiles[PositionIndex + 1].Sprite.Name == "grasstile")
            {
                VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, VecPosition.X + GVars.CurrentMap.TileSize);

                YPos += 1;
                GVars.PlayerSprite.Play("right");

                if (keyboardState.IsKeyUp(Keys.D))
                {
                    GVars.PlayerSprite.Play("rightStand");
                }
            }
        }
    }
}
