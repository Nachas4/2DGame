﻿using _2DGame.Content.Globals;
using _2DGame.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Linq;

namespace _2DGame
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;

        //Own
        SpriteFont font;

        public Game1()
        {
            _ = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 768,
                PreferredBackBufferHeight = 768,
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            GVars.PlayerSpriteSheet = Content.Load<SpriteSheet>("player.sf", new JsonContentLoader());
            GVars.Player.PlayerSprite = new AnimatedSprite(GVars.PlayerSpriteSheet);
            GVars.Player.PlayerSprite.Play("downStand");

            GVars.EnemySpriteSheet = Content.Load<SpriteSheet>("enemy.sf", new JsonContentLoader());

            //GVars.EnemyTexture = Content.Load<Texture2D>("enemy_dummy");
            GVars.BossTexture = Content.Load<Texture2D>("boss");

            GVars.GrassTexture = Content.Load<Texture2D>("grasstile");
            GVars.WallTexture = Content.Load<Texture2D>("walltile");

            // Creating random map
            //GVars.RndMap = new(1, rnd: true);

            // Creating maps from map files
            for (int i = 1; i < 11; i++)
                GVars.Maps.Add(new(i));

            GVars.CurrentMap = GVars.Maps[GVars.CurrentMapNum];

            font = Content.Load<SpriteFont>("font");
        }

        private float elapsedTime = 0.0f;

        protected override void Update(GameTime gameTime)
        {
            if (!GVars.Player.Alive)
            {
                throw new System.Exception("You are dead. Not big suprice.");
            }

            if (GVars.Player.InFight)
            {
                FightHelper.NextBlow();
                return;
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) Exit();

            // Movement
            elapsedTime += gameTime.GetElapsedSeconds();

            if (GVars.MovementDelay)
            {
                GVars.Player.MovePlayer(keyboardState);

            }

            if (elapsedTime >= 0.3f)
            {
                GVars.MovementDelay = true;
                elapsedTime = 0.0f;
            }

            GVars.Player.PlayerSprite.Update(gameTime);
            
            foreach (var item in GVars.CurrentMap.Enemies.Where(x => x.Alive))
                item.EnemySprite.Update(gameTime);

            FightHelper.CheckForFight();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw the Map
            GVars.CurrentMap.DrawMap(_spriteBatch);

            // Draw animated enemies and boss
            var enemies = GVars.CurrentMap.Enemies.Where(x => x.Alive).ToList();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsBoss)
                    _spriteBatch.Draw(GVars.BossTexture, enemies[i].VecPosition, Color.White);
                else
                    enemies[i].EnemySprite.Draw(_spriteBatch, enemies[i].VecPosition + new Vector2(32, 25), 0f, new Vector2(1.4f, 1.4f));
            }

            // Draw animated Player
            GVars.Player.PlayerSprite.Draw(_spriteBatch, GVars.Player.VecPosition + new Vector2(32, 25), 0f, new Vector2(1.4f, 1.4f));

            // Debug text
            string debugText = $"x: {GVars.Player.XPos}, y: {GVars.Player.YPos}\n" +
                $"HP: {GVars.Player.Hp}\n" +
                $"Level: {GVars.Player.Level}";
            _spriteBatch.DrawString(font, debugText, new Vector2(10, 10), Color.White);

            _spriteBatch.DrawRectangle(new RectangleF(10, 10, 200, 100), Color.SaddleBrown);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}