using Microsoft.Xna.Framework.Graphics;

namespace _2DGame.Content.Models
{
    public class Tile
    {
        public Texture2D Sprite { get; set; }
        public int Position { get; private set; }

        public Tile(Texture2D sprite, int position)
        {
            Sprite = sprite;
            Position = position;
        }
    }
}
