using _2DGame.Content.Globals;
using _2DGame.Models;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;

namespace Content.Models
{
    internal class Player : Base
    {
        internal AnimatedSprite PlayerSprite;
        internal bool InFight;

        internal bool HasKey;
        internal bool HasKilledBoss;

        public Player() : base()
        {
            Random rnd = new();

            D6 = rnd.Next(1, 7);

            Hp = 20 + 3 * D6;
            //Hp = 400;
            MaxHp = Hp;
            Defense = 2 * D6;
            Attack = 5 + D6;

            HasKey = false;
            HasKilledBoss = false;
        }

        public void MovePlayer(KeyboardState keyboardState)
        {
            //W key
            if (keyboardState.IsKeyDown(Keys.W) && GVars.CurrentMap.Tiles[PositionIndex - 12].Sprite == GVars.GrassTexture)
            {
                VecPosition.Y = Math.Max(0, VecPosition.Y - GVars.CurrentMap.TileSize);

                XPos -= 1;
                PlayerSprite.Play("up");

                GVars.EnemiesShouldMove = !GVars.EnemiesShouldMove;

                //Enemies move every second step
                if (GVars.EnemiesShouldMove)
                    GVars.CurrentMap.MoveEnemies();

                if (keyboardState.IsKeyUp(Keys.W))
                {
                    PlayerSprite.Play("upStand");
                }

                GVars.MovementDelay = false;
            }

            //A key
            if (keyboardState.IsKeyDown(Keys.A) && GVars.CurrentMap.Tiles[PositionIndex - 1].Sprite == GVars.GrassTexture)
            {
                VecPosition.X = Math.Max(0, VecPosition.X - GVars.CurrentMap.TileSize);

                YPos -= 1;
                PlayerSprite.Play("left");

                GVars.EnemiesShouldMove = !GVars.EnemiesShouldMove;

                //Enemies move every second step
                if (GVars.EnemiesShouldMove)
                    GVars.CurrentMap.MoveEnemies();

                if (keyboardState.IsKeyUp(Keys.A))
                {
                    PlayerSprite.Play("leftStand");
                }

                GVars.MovementDelay = false;
            }

            //S key
            if (keyboardState.IsKeyDown(Keys.S) && GVars.CurrentMap.Tiles[PositionIndex + 12].Sprite == GVars.GrassTexture)
            {
                VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, VecPosition.Y + GVars.CurrentMap.TileSize);

                XPos += 1;
                PlayerSprite.Play("down");

                GVars.EnemiesShouldMove = !GVars.EnemiesShouldMove;

                //Enemies move every second step
                if (GVars.EnemiesShouldMove)
                    GVars.CurrentMap.MoveEnemies();

                if (keyboardState.IsKeyUp(Keys.S))
                {
                    PlayerSprite.Play("downStand");
                }

                GVars.MovementDelay = false;
            }

            //D key
            if (keyboardState.IsKeyDown(Keys.D) && GVars.CurrentMap.Tiles[PositionIndex + 1].Sprite == GVars.GrassTexture)
            {
                VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, VecPosition.X + GVars.CurrentMap.TileSize);

                YPos += 1;
                PlayerSprite.Play("right");

                GVars.EnemiesShouldMove = !GVars.EnemiesShouldMove;

                //Enemies move every second step
                if (GVars.EnemiesShouldMove)
                    GVars.CurrentMap.MoveEnemies();

                if (keyboardState.IsKeyUp(Keys.D))
                {
                    PlayerSprite.Play("rightStand");
                }

                GVars.MovementDelay = false;
            }
        }

        internal void LevelUp()
        {
            Level++;

            MaxHp += D6;
            Attack += D6;
            Defense += D6;
        }
    }
}
