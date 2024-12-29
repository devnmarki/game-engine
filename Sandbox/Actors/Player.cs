using GameEngine;
using GameEngine.ECS;
using GameEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox.Actors;

public class Player : Actor
{
    private Vector2 _velocity = Vector2.Zero;
    
    public Player(Vector2 position) : base(position)
    {
        
    }
    
    protected override void Create()
    {
        base.Create();
        
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

        if (KeyboardHandler.IsDown(Keys.A))
        {
            _velocity.X = -1f;
        } 
        else if (KeyboardHandler.IsDown(Keys.D))
        {
            _velocity.X = 1f;
        }
        else
        {
            _velocity.X = 0f;
        }

        if (KeyboardHandler.IsDown(Keys.W))
        {
            _velocity.Y = -1f;
        }
        else if (KeyboardHandler.IsDown(Keys.S))
        {
            _velocity.Y = 1f;
        }
        else
        {
            _velocity.Y = 0f;
        }
    }

    private void Move()
    {
        Position += _velocity * new Vector2(5f, 5f);
    }
}