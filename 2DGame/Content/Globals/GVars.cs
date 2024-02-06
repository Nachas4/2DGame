using _2DGame.Content.Models;
using Content.Models;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using SharpDX.MediaFoundation;
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

        internal static Texture2D GrassTexture;
        internal static Texture2D WallTexture;

        internal static Texture2D BossTexture;

        // Other logic
        internal static bool EnemiesShouldMove = true; //Is reversed at every player step, enemies move at true
        internal static bool MovementDelay = true;

        // Maps
        //internal static Map RndMap;

        
        //internal static Map Map1;
        //internal static Map Map2;
        //internal static Map Map3;
        //internal static Map Map4;
        //internal static Map Map5;
        //internal static Map Map6;
        //internal static Map Map7;
        //internal static Map Map8;
        //internal static Map Map9;
        //internal static Map Map10;
    }
}
