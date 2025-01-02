using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Sandbox.Actors;
using Sandbox.Utils;

namespace Sandbox.Sprites;

public class PlayerSprite
{
    private PlayerActor _player;
    
    public PlayerSprite(PlayerActor player)
    {
        _player = player;
    }
    
    public void Load()
    {
        _player.Animator.AddAnimation("idle_down", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 0 }, 0.1f));
        _player.Animator.AddAnimation("idle_up", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 4 }, 0.1f));
        _player.Animator.AddAnimation("idle_left", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 8 }, 0.1f));
        _player.Animator.AddAnimation("idle_right", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 12 }, 0.1f));
        
        _player.Animator.AddAnimation("walk_down", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 16, 17, 18, 19 }, 0.15f));
        _player.Animator.AddAnimation("walk_up", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 20, 21, 22, 23 }, 0.15f));
        _player.Animator.AddAnimation("walk_left", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 24, 25 }, 0.15f));
        _player.Animator.AddAnimation("walk_right", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 28, 29 }, 0.15f));
        
        _player.Animator.AddAnimation("attack_down", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 32, 32 }, 0.25f, false));
        _player.Animator.AddAnimation("attack_up", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 36, 36 }, 0.25f, false));
        _player.Animator.AddAnimation("attack_left", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 40, 40 }, 0.25f, false));
        _player.Animator.AddAnimation("attack_right", new Animation(Assets.Spritesheets.Characters.FynnSpritesheet, new int[] { 44, 44 }, 0.25f, false));
        
        _player.Animator.PlayAnimation("idle_down");
    }
    
    public void Update()
    {
        if (_player.InAction)
        {
            if (_player.Animator.IsCurrentAnimationFinsihed())
            {
                _player.InAction = false;
                _player.Sword.Visible = false;
            }
            return;
        }
        
        HandleIdleAnimations();
        HandleWalkAnimations();
    }
    
    private void HandleIdleAnimations()
    {
        if (_player.Velocity != Vector2.Zero) return;
        
        switch (_player.FacingDirection)
        {
            case Direction.Down:
                _player.Animator.PlayAnimation("idle_down");
                break;
            case Direction.Up:
                _player.Animator.PlayAnimation("idle_up");
                break;
            case Direction.Left:
                _player.Animator.PlayAnimation("idle_left");
                break;
            case Direction.Right:
                _player.Animator.PlayAnimation("idle_right");
                break;
        }
    }
    
    private void HandleWalkAnimations()
    {
        if (_player.Velocity == Vector2.Zero) return;
        
        switch (_player.FacingDirection)
        {
            case Direction.Down:
                _player.Animator.PlayAnimation("walk_down");
                break;
            case Direction.Up:
                _player.Animator.PlayAnimation("walk_up");
                break;
            case Direction.Left:
                _player.Animator.PlayAnimation("walk_left");
                break;
            case Direction.Right:
                _player.Animator.PlayAnimation("walk_right");
                break;
        }
    }
    
    public void HandleAttackAnimations()
    {
        switch (_player.FacingDirection)
        {
            case Direction.Down:
                _player.Animator.PlayAnimation("attack_down");
                break;
            case Direction.Up:
                _player.Animator.PlayAnimation("attack_up");
                break;
            case Direction.Left:
                _player.Animator.PlayAnimation("attack_left");
                break;
            case Direction.Right:
                _player.Animator.PlayAnimation("attack_right");
                break;
        }
    }

    public void HandleSwordSprite()
    {
        Vector2 swordOffset = Vector2.Zero;
        
        if (_player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_down"))
        {
            _player.Sword.UseGraphics(Assets.Spritesheets.Weapons.FynnSwordSpritesheet, 1);
            _player.Sword.Layer = Globals.Layers.ItemsLayer;
            swordOffset = new Vector2(8, 20);
        }
        else if (_player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_up"))
        {
            _player.Sword.UseGraphics(Assets.Spritesheets.Weapons.FynnSwordSpritesheet, 0);
            _player.Sword.Layer = _player.Layer - 1;
            swordOffset = new Vector2(8, 0);
        }
        else if (_player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_left"))
        {
            _player.Sword.UseGraphics(Assets.Spritesheets.Weapons.FynnSwordSpritesheet, 2);
            _player.Sword.Layer = Globals.Layers.ItemsLayer;
            swordOffset = new Vector2(0, 14);
        }
        else if (_player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_right"))
        {
            _player.Sword.UseGraphics(Assets.Spritesheets.Weapons.FynnSwordSpritesheet, 3);
            _player.Sword.Layer = Globals.Layers.ItemsLayer;
            swordOffset = new Vector2(15, 14);
        }

        _player.Sword.Position = _player.Position + swordOffset * Engine.GameScale;
    }

    private bool AttackAnimationPlaying()
    {
        return _player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_down") ||
               _player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_up") ||
               _player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_left") ||
               _player.Animator.CurrentAnimation == _player.Animator.GetAnimation("attack_right");
    }
}