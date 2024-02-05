using _2DGame.Content.Globals;
using _2DGame.Models;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;

namespace Content.Models
{
    public class Player : Base
    {
        public AnimatedSprite PlayerSprite;
        public bool InFight;

        public Player() : base()
        {

        }

        public void MovePlayer(KeyboardState keyboardState)
        {
            //W key
            if (keyboardState.IsKeyDown(Keys.W) && GVars.CurrentMap.Tiles[PositionIndex - 12].Sprite == GVars.GrassTexture)
            {
                VecPosition.Y = Math.Max(0, VecPosition.Y - GVars.CurrentMap.TileSize);

                XPos -= 1;
                PlayerSprite.Play("up");

                if (keyboardState.IsKeyUp(Keys.W))
                {
                    PlayerSprite.Play("upStand");
                }
            }

            //A key
            if (keyboardState.IsKeyDown(Keys.A) && GVars.CurrentMap.Tiles[PositionIndex - 1].Sprite == GVars.GrassTexture)
            {
                VecPosition.X = Math.Max(0, VecPosition.X - GVars.CurrentMap.TileSize);

                YPos -= 1;
                PlayerSprite.Play("left");

                if (keyboardState.IsKeyUp(Keys.A))
                {
                    PlayerSprite.Play("leftStand");
                }
            }

            //S key
            if (keyboardState.IsKeyDown(Keys.S) && GVars.CurrentMap.Tiles[PositionIndex + 12].Sprite == GVars.GrassTexture)
            {
                VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, VecPosition.Y + GVars.CurrentMap.TileSize);

                XPos += 1;
                PlayerSprite.Play("down");

                if (keyboardState.IsKeyUp(Keys.S))
                {
                    PlayerSprite.Play("downStand");
                }
            }

            //D key
            if (keyboardState.IsKeyDown(Keys.D) && GVars.CurrentMap.Tiles[PositionIndex + 1].Sprite == GVars.GrassTexture)
            {
                VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, VecPosition.X + GVars.CurrentMap.TileSize);

                YPos += 1;
                PlayerSprite.Play("right");

                GVars.EnemiesShouldMove = !GVars.EnemiesShouldMove;

                if (keyboardState.IsKeyUp(Keys.D))
                {
                    PlayerSprite.Play("rightStand");
                }
            }
        }
    }
}
