using GameEngine;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace Sandbox.Actors.Enemies;

public class RocklingEnemy : EnemyActor
{
    protected override void Create()
    {
        base.Create();

        base.Name = "Rockling";
        base.Colliders.Add(new Collider(this, new Vector2(16 * Engine.GameScale), new Vector2(8 * Engine.GameScale)));
        
        base.Animator.AddAnimation("idle_down", new Animation(Assets.Spritesheets.Characters.RocklingSpritesheet, new int[] { 0 }, 0.1f));
        base.Animator.PlayAnimation("idle_down");
    }
}