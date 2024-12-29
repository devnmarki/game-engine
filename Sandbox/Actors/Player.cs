using GameEngine;
using GameEngine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox.Actors;

public class Player : Actor
{
    public Player(Vector2 position) : base(position)
    {
        
    }
    
    protected override void Create()
    {
        base.Create();
        
        UseGraphics(Assets.Spritesheets.Characters.FynnSpritesheet, 0);
    }
}