using Godot;
using System;

namespace BadBunnyGames
{
    static class ExtensionMethods
    {
        public static Texture2D Scale(this Texture2D texture, Int32 width, Int32 height)
        {
            Image image = texture.GetImage();
            image.Resize(width, height);
            return ImageTexture.CreateFromImage(image);
        }
    }
}