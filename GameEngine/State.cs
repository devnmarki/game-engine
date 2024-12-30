using GameEngine.ECS;
using Microsoft.Xna.Framework;
using MonoGame.ImGui;

namespace GameEngine;

public abstract class State
{
    public List<Actor> Actors { get; set; } = new List<Actor>();
    public Game Game { get; set; } = null;
    public ImGuiRenderer GuiRenderer { get; set; } = null;
    
    public abstract void Enter();
    public abstract void Update(GameTime gameTime);
    public abstract void Render();
    public abstract void RenderGui();
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