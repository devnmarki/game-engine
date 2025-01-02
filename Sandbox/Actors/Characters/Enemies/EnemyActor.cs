using System;
using GameEngine.ECS;
using Microsoft.Xna.Framework;

namespace Sandbox.Actors.Enemies;

public class EnemyActor : Actor
{
    public enum EnemyState
    {
        None,
        Idle,
        Patrol,
        Attack
    }
    
    protected string Name { get; set; }

    public EnemyState CurrentState { get; set; } = EnemyState.None;
    
    protected Action OnIdleState;
    protected Action OnPatrolState;
    protected Action OnAttackState;
    
    protected override void Create()
    {
        base.Create();

        base.Tag = "enemy";
        base.Name = Name;
        
        base.CollisionIgnoreList.Add(typeof(PlayerActor));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        HandleCurrentState();
    }

    private void HandleCurrentState()
    {
        switch (CurrentState)
        {
            case EnemyState.Idle:
                OnIdleState();
                break;
            case EnemyState.Patrol:
                OnPatrolState();
                break;
            case EnemyState.Attack:
                OnAttackState();
                break;
        }
    }
}