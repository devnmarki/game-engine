using System;
using System.Diagnostics;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Sandbox.States;

public class GameState : State
{
    public override void Enter()
    {
        Console.WriteLine("Entering Game State");
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