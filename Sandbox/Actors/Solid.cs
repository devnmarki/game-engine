using Box2DSharp.Dynamics;
using GameEngine;
using GameEngine.ECS;
using GameEngine.Input;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox.Actors;

public class Solid : Actor
{
    private Vector2 _velocity = Vector2.Zero;
    private float _speed = 200f;
    
    public Solid(Vector2 position) : base(position)
    {

    }
    
    protected override void Create()
    {
        base.Create();
        
        Colliders.Add(new Collider(this, new Vector2(16 * Engine.GameScale), new Vector2(8 * Engine.GameScale)));
        
        UseGraphics(Assets.Spritesheets.Characters.FynnSpritesheet, 0);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}