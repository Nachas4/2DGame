using _2DGame.Content.Models;
using Content.Models;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;

namespace _2DGame.Content.Globals
{
    public static class GVars
    {
        internal static Player Player = new();
        internal static Map CurrentMap;
        internal static int CurrentMapNum = 0;
        internal readonly static List<Map> Maps = new();

        // Sprites
        internal static SpriteSheet PlayerSpriteSheet;

        internal static SpriteSheet EnemySpriteSheet; 

        internal static Texture2D BlankTexture;
        internal static Texture2D HealthTexture;
        internal static Texture2D DefenseTexture;
        internal static Texture2D LevelTexture;
        internal static Texture2D Keyframe;
        internal static Texture2D Key;

        internal static Texture2D GrassTexture;
        internal static Texture2D WallTexture;

        internal static Texture2D BossTexture;

        // Other logic
        internal static bool EnemiesShouldMove = true; //Is reversed at every player step, enemies move at true
        internal static bool MovementDelay = true;
        internal static bool YouWon = false;

        //internal static Map RndMap;
    }
}
