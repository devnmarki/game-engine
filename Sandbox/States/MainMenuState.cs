using System;
using System.Diagnostics;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Sandbox.States;

public class MainMenuState : State
{
    public override void Enter()
    {
        Console.WriteLine("Entering Main Menu State");
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Render()
    {
        
    }

    public override void RenderGui()
    {
        
    }

    public override void Leave()
    {
        Console.WriteLine("Left Main Menu State");
    }
}