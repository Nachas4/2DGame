using _2DGame.Content.Globals;
using _2DGame.Content.Models;
using _2DGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Content.Models
{
    public class Player : Base
    {
        private static bool WkeyUp = true;
        private static bool AkeyUp = true;
        private static bool SkeyUp = true;
        private static bool DkeyUp = true;
        private static bool SpacekeyUp = true;

        public Player() : base()
        {
            Hp = 65;
            Attack = 7;
        }

        public void TakeDamage(int dmg)
        {
            Hp -= dmg;
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

                    var bruh = GVars.CurrentMap.Enemies.Where(x => attackedTiles.Contains(x.PositionIndex));

                    foreach (Enemy enemy in bruh)
                    {
                        enemy.TakeDamage(Attack);
                    }

                    SpacekeyUp = false;
                }
            }
        }


        public void MovePlayer(Map map, KeyboardState keyboardState)
        {

            //W key
            if (keyboardState.IsKeyUp(Keys.W))
            {
                WkeyUp = true;
            }
            if (true)
            {
                if (keyboardState.IsKeyDown(Keys.W) && map.Tiles[PositionIndex - 12].Sprite.Name == "grasstile")
                {
     
                    GVars.sprite.Play("up");
                    VecPosition.Y = Math.Max(0, VecPosition.Y - GVars.CurrentMap.TileSize);
            
                    WkeyUp = false;
                    XPos -= 1;
                }
            }


            //A key
            if (keyboardState.IsKeyUp(Keys.A)) AkeyUp = true;
            if (true)
            {
                if (keyboardState.IsKeyDown(Keys.A) && map.Tiles[PositionIndex - 1].Sprite.Name == "grasstile")
                {
                    VecPosition.X = Math.Max(0, VecPosition.X - GVars.CurrentMap.TileSize);
                    AkeyUp = false;
                    YPos -= 1;
                    GVars.sprite.Play("left");
                }
            }


            //S key
            if (keyboardState.IsKeyUp(Keys.S)) SkeyUp = true;
            if (true)
            {
                if (keyboardState.IsKeyDown(Keys.S) && map.Tiles[PositionIndex + 12].Sprite.Name == "grasstile")
                {
                    VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, VecPosition.Y + GVars.CurrentMap.TileSize);
                    SkeyUp = false;
                    XPos += 1;
                    GVars.sprite.Play("down");
                }
            }


            //D key
            if (keyboardState.IsKeyUp(Keys.D)) DkeyUp = true;
            if (true)
            {
                if (keyboardState.IsKeyDown(Keys.D) && map.Tiles[PositionIndex + 1].Sprite.Name == "grasstile")
                {
                    VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, VecPosition.X + GVars.CurrentMap.TileSize);
                    DkeyUp = false;
                    YPos += 1;
                    GVars.sprite.Play("right");
                }
            }
        }
    }
}
