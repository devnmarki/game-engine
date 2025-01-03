using System;
using System.Diagnostics;
using GameEngine;
using GameEngine.ECS;
using GameEngine.Tilemap;
using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGame.ImGui;
using Sandbox.Actors;

namespace Sandbox.States;

public class GameState : State
{
    private TilemapManager _tilemapManager;
    
    public override void Enter()
    {
        Console.WriteLine("Entering Game State");

        _tilemapManager = new TilemapManager(Assets.Maps.TestMap, Assets.Textures.Tilesets.PlainsTilesetTexture);
        _tilemapManager.LoadGameObjects();
        _tilemapManager.CreateColliders(Vector2.Zero);
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Render()
    {
        _tilemapManager.Draw("Grass", Vector2.Zero, Color.White);
        _tilemapManager.Draw("Dirt", Vector2.Zero, Color.White);
    }

    public override void RenderGui()
    {
        bool debugMode = Engine.DebugMode;
        
        GuiRenderer.BeginLayout();
        ImGui.Checkbox("Debug", ref debugMode);
        GuiRenderer.EndLayout();

        Engine.DebugMode = debugMode;
    }

    public override void Leave()
    {
        Console.WriteLine("Left Game State");
    }
}