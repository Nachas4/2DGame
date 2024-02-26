using _2DGame.Content.Globals;
using _2DGame.Helpers;
using Content.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using SharpDX.MediaFoundation;
using System.Linq;

namespace _2DGame
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;

        //Own
        SpriteFont Font;

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

            GVars.BlankTexture = Content.Load<Texture2D>("blank");
            GVars.HealthTexture = Content.Load<Texture2D>("red");
            GVars.DefenseTexture = Content.Load<Texture2D>("shield");
            GVars.LevelTexture = Content.Load<Texture2D>("level");

            GVars.GrassTexture = Content.Load<Texture2D>("grasstile");
            GVars.WallTexture = Content.Load<Texture2D>("walltile");

            // Creating random map
            //GVars.RndMap = new(1, rnd: true);

            // Creating maps from map files
            for (int i = 1; i < 11; i++)
                GVars.Maps.Add(new(i));

            GVars.CurrentMap = GVars.Maps[GVars.CurrentMapNum];

            Font = Content.Load<SpriteFont>("font");
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
            FightHelper.CheckForFight();

            GVars.Player.PlayerSprite.Update(gameTime);
            
            foreach (var item in GVars.CurrentMap.Enemies.Where(x => x.Alive))
                item.EnemySprite.Update(gameTime);


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
            //_spriteBatch.DrawString(Font, debugText, new Vector2(10, 10), Color.White);

            DrawplayerStats();

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private void DrawplayerStats()
        {
            // Draw defense
            _spriteBatch.Draw(GVars.DefenseTexture, new Rectangle(1,64*11, 64, 64), Color.White);
            _spriteBatch.DrawString(Font, GVars.Player.Defense.ToString(), new Vector2(60, 64 * 11), Color.White);

            // Draw level
            _spriteBatch.Draw(GVars.LevelTexture, new Rectangle(64 * 10 - 10, 0, 64, 64), Color.White);
            _spriteBatch.DrawString(Font, GVars.Player.Level.ToString(), new Vector2(64 * 11 - 12, 0), Color.White);

            int healthUnit = 3; // 1 unit in px

            int currentHealth = GVars.Player.Hp;

            // Calculate the width of the health bar based on player's health
            int maxHealthBar = healthUnit * GVars.Player.MaxHp;
            int currentWidth = healthUnit * currentHealth;


         

            // Draw the filled portion of the health bar
            _spriteBatch.Draw(GVars.HealthTexture, new Rectangle(12, 8, currentWidth, 48), Color.Red);

            //Draw the background of the health bar
            _spriteBatch.DrawRectangle(new Rectangle(10, 6, maxHealthBar + 4, 52), Color.Black, 2);

            _spriteBatch.DrawString(Font, currentHealth.ToString(), new Vector2(18, 0), Color.White);
        }

    }
}