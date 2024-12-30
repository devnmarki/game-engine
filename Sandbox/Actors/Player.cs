using GameEngine;
using GameEngine.ECS;
using GameEngine.Input;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox.Actors;

public class Player : Actor
{
    private Vector2 _velocity = Vector2.Zero;
    private float _speed = 7f;
    
    public Player(Vector2 position) : base(position)
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
        
        HandleControls();
        Move();
    }

    private void HandleControls()
    {
        KeyboardHandler.GetState();
        
        _velocity = Vector2.Zero;

        if (KeyboardHandler.IsDown(Keys.A))
        {
            _velocity.X = -1f;
        } 
        else if (KeyboardHandler.IsDown(Keys.D))
        {
            _velocity.X = 1f;
        }

        if (KeyboardHandler.IsDown(Keys.W))
        {
            _velocity.Y = -1f;
        }
        else if (KeyboardHandler.IsDown(Keys.S))
        {
            _velocity.Y = 1f;
        }
    }

    private void Move()
    {
        if (_velocity.X != 0 && _velocity.Y != 0)
        {
            _velocity.X *= 0.7f;
            _velocity.Y *= 0.7f;
        }
        
        Position.X += _velocity.X * _speed;
        CheckCollision(Axis.Horizontal);
        
        Position.Y += _velocity.Y * _speed;
        CheckCollision(Axis.Vertical);
    }
}