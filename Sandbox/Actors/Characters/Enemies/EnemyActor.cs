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
    
    public EnemyState CurrentState { get; set; }
    
    protected Action OnIdleState;
    protected Action OnPatrolState;
    protected Action OnAttackState;

    public EnemyActor()
    {
        base.Tag = "enemy";
        base.Name = "Enemy";
        base.Layer = Globals.Layers.EnemiesLayer;
        base.CollisionIgnoreList.Add(typeof(PlayerActor));
    }
    
    protected override void Create()
    {
        base.Create();

        CurrentState = EnemyState.None;
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