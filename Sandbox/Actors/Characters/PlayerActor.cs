using System;
using System.Reflection.Metadata;
using GameEngine;
using GameEngine.ECS;
using GameEngine.Graphics;
using GameEngine.Input;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sandbox.Actors.Weapons;
using Sandbox.Models;
using Sandbox.Sprites;
using Sandbox.Utils;

namespace Sandbox.Actors;

public class PlayerActor : Actor
{
    private PlayerSprite _sprite;
    
    private Vector2 _velocity = Vector2.Zero;
    private float _speed = 5f;
    private Direction _facingDirection = Direction.Down;

    private bool _inAction = false;
    private bool _canMove = true;

    private SwordWeapon _sword;

    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    public Direction FacingDirection
    {
        get => _facingDirection;
        set => _facingDirection = value;
    }
    
    public bool InAction
    {
        get => _inAction;
        set => _inAction = value;
    }

    public SwordWeapon Sword
    {
        get => _sword;
        set => _sword = value;
    }
    
    protected override void Create()
    {
        base.Create();
        
        base.Layer = Globals.Layers.PlayerLayer;
        base.Colliders.Add(new Collider(this, new Vector2(16 * Engine.GameScale), new Vector2(8 * Engine.GameScale)));
        
        _sword = new SwordWeapon(WeaponModel.Models.FynnSword, SwordWeapon.SwordType.Fynn)
        {
            Position = Position
        };
        Engine.CurrentState.AddActor(_sword);
        
        _sprite = new PlayerSprite(this);
        _sprite.Load();
    }

    public override void Update(GameTime gameTime)
    {
        KeyboardHandler.GetState();
        
        base.Update(gameTime);
        
        HandleControls();
        Move();
        
        _sprite.Update();

        _canMove = true;
        if (_inAction)
        {
            _canMove = false;
        }
    }

    private void HandleControls()
    {
        _velocity = Vector2.Zero;

        if (KeyboardHandler.IsDown(Keys.Left))
        {
            _velocity.X = -1f;
            _facingDirection = Direction.Left;
        } 
        else if (KeyboardHandler.IsDown(Keys.Right))
        {
            _velocity.X = 1f;
            _facingDirection = Direction.Right;
        }

        if (KeyboardHandler.IsDown(Keys.Up))
        {
            _velocity.Y = -1f;
            _facingDirection = Direction.Up;
        }
        else if (KeyboardHandler.IsDown(Keys.Down))
        {
            _velocity.Y = 1f;
            _facingDirection = Direction.Down;
        }

        if (KeyboardHandler.IsDown(Keys.Z) && !_inAction)
        {
            _inAction = true;
            Attack();
        }
    }

    private void Move()
    {
        if (!_canMove) return;
        
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

    private void Attack()
    {
        _sprite.HandleAttackAnimations();

        Console.WriteLine("Attack");
        
        _sword.Visible = true;
        _sprite.HandleSwordSprite();
    }
}