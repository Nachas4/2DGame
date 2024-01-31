using _2DGame.Content.Globals;
using _2DGame.Content.Models;
using Content.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using MonoGame.Extended;
using _2DGame.Helpers;

namespace _2DGame
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteSheet spriteSheet;

        //Own
        private readonly Player Player = new();

        private Texture2D EnemyTexture;

        private Texture2D GrassTexture;
        private Texture2D WallTexture;

        SpriteFont font;

        public Game1()
        {
            _graphics = new(this)
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

            EnemyTexture = Content.Load<Texture2D>("enemy_dummy");

            spriteSheet = Content.Load<SpriteSheet>("player.sf", new JsonContentLoader());
            GVars.PlayerSprite = new AnimatedSprite(spriteSheet);
            GVars.PlayerSprite.Play("downStand");

            GVars.GrassTexture = Content.Load<Texture2D>("grasstile");
            GVars.WallTexture = Content.Load<Texture2D>("walltile");

            // Creating random map
            //GVars.RndMap = new(GrassTexture, WallTexture, 1, rnd: true);

            // Creating maps from map files
            GVars.Map1 = new(GrassTexture, WallTexture, 1);
            //GVars.Map2 = new(GrassTexture, WallTexture, 2);
            //GVars.Map3 = new(GrassTexture, WallTexture, 3);
            //GVars.Map4 = new(GrassTexture, WallTexture, 4);
            //GVars.Map5 = new(GrassTexture, WallTexture, 5);
            //GVars.Map6 = new(GrassTexture, WallTexture, 6);
            //GVars.Map7 = new(GrassTexture, WallTexture, 7);
            //GVars.Map8 = new(GrassTexture, WallTexture, 8);
            //GVars.Map9 = new(GrassTexture, WallTexture, 9);
            //GVars.Map10 = new(GrassTexture, WallTexture, 10);

            GVars.CurrentMap = GVars.Map1;

            font = Content.Load<SpriteFont>("font");

            FightHelper.Player = Player;
        }


        private float elapsedTime = 0.0f;

        protected override void Update(GameTime gameTime)
        {
            if (!Player.Alive)
            {
                throw new System.Exception("You are dead. Not big suprice.");
            }

            if (Player.InFight)
            {
                FightHelper.NextBlow();
                return;
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) Exit();

            // Movement
            elapsedTime += gameTime.GetElapsedSeconds();

            if (elapsedTime >= 0.3f)
            {
                Player.MovePlayer(GVars.CurrentMap, keyboardState);
                elapsedTime = 0.0f;
            }

            GVars.PlayerSprite.Update(gameTime);

            Player.MakeDamage(Player.PositionIndex, keyboardState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw the Map
            GVars.CurrentMap.DrawMap(_spriteBatch);

            // Draw enemies
            foreach (Enemy item in GVars.CurrentMap.Enemies.Where(x => x.Alive))
            {
                _spriteBatch.Draw(EnemyTexture, item.VecPosition, Color.White);
            }

            // Draw animated Player
            GVars.PlayerSprite.Draw(_spriteBatch, Player.VecPosition + new Vector2(32, 25), 0f, new Vector2(1.4f, 1.4f));

            // Debug text
            string debugText = $"x: {Player.XPos}, y: {Player.YPos}";
            _spriteBatch.DrawString(font, debugText, new Vector2(10, 10), Color.White);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}