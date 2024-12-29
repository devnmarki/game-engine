using GameEngine.ECS;
using Microsoft.Xna.Framework;

namespace GameEngine;

public abstract class State
{
    public List<Actor> Actors { get; set; } = new List<Actor>();
    
    public abstract void Enter();
    public abstract void Update(GameTime gameTime);
    public abstract void Render();
    public abstract void Leave();

    public void AddActor(Actor actor)
    {
        Actors.Add(actor);
    }

    public void RemoveActor(Actor actor)
    {
        Actors.Remove(actor);
    }
}