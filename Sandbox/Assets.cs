using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox;

public static class Assets
{
    public static class Textures
    {
        public static class Characters
        {
            public static readonly Texture2D FynnTexture = Engine.Content.Load<Texture2D>("entities/fynn_spritesheet");
        }
    }
    
    public static class Spritesheets
    {
        public static class Characters
        {
            public static readonly Spritesheet FynnSpritesheet = new Spritesheet(Textures.Characters.FynnTexture, 8, 4, new Vector2(32, 32));
        }
    }
}