using Microsoft.Xna.Framework.Media;
namespace _2DGame.Helpers
{
    internal static class MusicHelper
    {
        internal static void PlayBgMusic(Song music)
        {
            MediaPlayer.Volume = 1.0f;
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(music);
        }
    }
}