using GameEngine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL;

namespace GameEngine;

public class Engine
{
    public static ContentManager Content { get; set; }
    
    public Vector2 WindowSize { get; set; } = new Vector2(1280, 720);
    public string GameTitle { get; set; } = "Game | v0.1";
    public static float GameScale { get; set; } = 4f;
    
    public static SpriteBatch SpriteBatch { get; set; }
    
    private GraphicsDeviceManager _graphicsManager;

    private Dictionary<string, State> _states = new Dictionary<string, State>();
    private State _currentState = null;
    private State _previousState = null;
    
    public Engine(GraphicsDeviceManager graphicsManager, GraphicsDevice graphics)
    {
        SpriteBatch = new SpriteBatch(graphics);
        _graphicsManager = graphicsManager;
    }

    public void Init(GameWindow window, ContentManager content)
    {
        _graphicsManager.PreferredBackBufferWidth = (int)WindowSize.X;
        _graphicsManager.PreferredBackBufferHeight = (int)WindowSize.Y;
        _graphicsManager.ApplyChanges();
        
        window.Title = GameTitle;
        Content = content;
    }

    public void Load()
    {
        
    }

    public void Draw()
    {
        _currentState?.Render();

        List<Actor> actorsCopy = new List<Actor>(_currentState?.Actors);
        foreach (var actor in actorsCopy)
        {
            actor.Draw();
        }
    }

    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);

        List<Actor> actorsCopy = new List<Actor>(_currentState?.Actors);
        foreach (var actor in actorsCopy)
        {
            actor.Update(gameTime);
        }
    }

    public void AddState(string stateName, State state)
    {
        _states.Add(stateName, state);
    }

    public void ReloadCurrentState()
    {
        _previousState?.Actors.Clear();
        _currentState.Enter();
    }

    public void SwitchState(string stateName)
    {
        if (_states.TryGetValue(stateName, out State newState))
        {
            if (_currentState != newState)
            {
                _previousState = _currentState;
                _currentState = newState;
                _previousState?.Leave();
                ReloadCurrentState();
            }
        }
        else
        {
            throw new ArgumentException($"State '{stateName}' does not exist.", nameof(stateName));
        }
    }
}