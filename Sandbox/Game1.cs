using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;
using ImGuiNET;
using MonoGame.ImGui;
using Sandbox.States;

namespace Sandbox;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    
    private Engine _engine;
    private ImGuiRenderer _renderer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _engine = new Engine(this, _graphics, GraphicsDevice);
        _engine.Init(Window, Content);

        _renderer = new ImGuiRenderer(this).Initialize().RebuildFontAtlas();
     
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _engine.Load();
        
        _engine.AddState("game", new GameState());
        _engine.AddState("main_menu", new MainMenuState());
        _engine.SwitchState("game");
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _engine.Update(gameTime);
        
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
            _engine.SwitchState("game");
        else if (Keyboard.GetState().IsKeyDown(Keys.E))
            _engine.SwitchState("main_menu");

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        Engine.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _engine.Draw();
        Engine.SpriteBatch.End();
        
        _renderer.BeginLayout(gameTime);
        ImGui.Text("Hello");
        if (ImGui.Button("Click Me"))
        {
            Console.WriteLine("Clicked :)");
        }
        _renderer.EndLayout();

        base.Draw(gameTime);
    }
}