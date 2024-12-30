using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Sandbox;

public static class Assets
{
    public static class Maps
    {
        public static TmxMap TestMap { get; set; } = new TmxMap("../../../Content/maps/test_map.tmx");
    }
    
    public static class Textures
    {
        public static class Characters
        {
            public static readonly Texture2D FynnTexture = Engine.Content.Load<Texture2D>("Characters/fynn_spritesheet");
        }

        public static class Tilesets
        {
            public static readonly Texture2D PlainsTilesetTexture = Engine.Content.Load<Texture2D>("Tilesets/plains_tileset");
        }
    }
    
    public static class Spritesheets
    {
        public static class Characters
        {
            public static readonly Spritesheet FynnSpritesheet = new Spritesheet(Textures.Characters.FynnTexture, 12, 4, new Vector2(32, 32));
        }
    }
}