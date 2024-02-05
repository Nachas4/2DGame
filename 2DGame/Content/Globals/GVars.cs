using _2DGame.Content.Models;
using Content.Models;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace _2DGame.Content.Globals
{
    public static class GVars
    {
        public static Player Player;
        public static Map CurrentMap;

        // Sprites
        public static SpriteSheet PlayerSpriteSheet;

        public static SpriteSheet EnemySpriteSheet; 

        public static Texture2D GrassTexture;
        public static Texture2D WallTexture;

        public static Texture2D BossTexture;

        // Other logic
        public static bool EnemiesShouldMove = true; //Is reversed at every player step, enemies move at true

        // Maps
        public static Map RndMap;
        
        public static Map Map1;
        public static Map Map2;
        public static Map Map3;
        public static Map Map4;
        public static Map Map5;
        public static Map Map6;
        public static Map Map7;
        public static Map Map8;
        public static Map Map9;
        public static Map Map10;



        
    }
}
