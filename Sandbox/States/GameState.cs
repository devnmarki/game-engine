using System;
using System.Diagnostics;
using GameEngine;
using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGame.ImGui;
using Sandbox.Actors;

namespace Sandbox.States;

public class GameState : State
{
    public override void Enter()
    {
        Console.WriteLine("Entering Game State");
        
        AddActor(new Player(new Vector2(200, 200)));
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Render()
    {
        
    }

    public override void Leave()
    {
        Console.WriteLine("Left Game State");
    }
}