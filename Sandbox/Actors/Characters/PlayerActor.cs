using System.Reflection.Metadata;
using GameEngine;
using GameEngine.ECS;
using GameEngine.Graphics;
using GameEngine.Input;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sandbox.Utils;

namespace Sandbox.Actors;

public class PlayerActor : Actor
{
    private Vector2 _velocity = Vector2.Zero;
    private float _speed = 5f;
    private Direction _direction = Direction.Down;
    
    public PlayerActor() : base()
    {
        
    }
    
    protected override void Create()
    {
        base.Create();
        
        Colliders.Add(new Collider(this, new Vector2(16 * Engine.GameScale), new Vector2(8 * Engine.GameScale)));
        
        Animator.AddAnimation("idle_down", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 0 }, 0.1f));
        Animator.AddAnimation("idle_up", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 4 }, 0.1f));
        Animator.AddAnimation("idle_left", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 8 }, 0.1f));
        Animator.AddAnimation("idle_right", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 12 }, 0.1f));
        
        Animator.AddAnimation("walk_down", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 16, 17, 18, 19 }, 0.15f));
        Animator.AddAnimation("walk_up", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 20, 21, 22, 23 }, 0.15f));
        Animator.AddAnimation("walk_left", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 24, 25 }, 0.15f));
        Animator.AddAnimation("walk_right", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 28, 29 }, 0.15f));
        
        Animator.PlayAnimation("idle_down");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        HandleControls();
        Move();
        HandleAnimations();
    }

    private void HandleControls()
    {
        KeyboardHandler.GetState();
        
        _velocity = Vector2.Zero;

        if (KeyboardHandler.IsDown(Keys.A))
        {
            _velocity.X = -1f;
            _direction = Direction.Left;
        } 
        else if (KeyboardHandler.IsDown(Keys.D))
        {
            _velocity.X = 1f;
            _direction = Direction.Right;
        }

        if (KeyboardHandler.IsDown(Keys.W))
        {
            _velocity.Y = -1f;
            _direction = Direction.Up;
        }
        else if (KeyboardHandler.IsDown(Keys.S))
        {
            _velocity.Y = 1f;
            _direction = Direction.Down;
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

    private void HandleAnimations()
    {
        HandleIdleAnimations();
        HandleWalkAnimations();
    }

    private void HandleIdleAnimations()
    {
        if (_velocity != Vector2.Zero) return;
        
        switch (_direction)
        {
            case Direction.Down:
                Animator.PlayAnimation("idle_down");
                break;
            case Direction.Up:
                Animator.PlayAnimation("idle_up");
                break;
            case Direction.Left:
                Animator.PlayAnimation("idle_left");
                break;
            case Direction.Right:
                Animator.PlayAnimation("idle_right");
                break;
        }
    }
    
    private void HandleWalkAnimations()
    {
        if (_velocity == Vector2.Zero) return;
        
        switch (_direction)
        {
            case Direction.Down:
                Animator.PlayAnimation("walk_down");
                break;
            case Direction.Up:
                Animator.PlayAnimation("walk_up");
                break;
            case Direction.Left:
                Animator.PlayAnimation("walk_left");
                break;
            case Direction.Right:
                Animator.PlayAnimation("walk_right");
                break;
        }
    }
}