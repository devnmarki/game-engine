using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using GameEngine.ECS;
using GameEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGui;
using MonoGame.OpenGL;

namespace GameEngine;

public class Engine
{
    public string GameTitle { get; set; } = "Game | v0.1";
    public static float GameScale { get; set; } = 4f;
    public static SpriteBatch SpriteBatch { get; set; }
    public static ContentManager Content { get; set; }
    public static GameTime GameTime { get; set; }
    public static float DeltaTime { get; set; }
    
    public const float TileSize = 16f;
    public static readonly float ScaledTileSize = TileSize * GameScale;
    public Vector2 WindowSize { get; set; } = new Vector2(20 * ScaledTileSize, 12 * ScaledTileSize);

    private GraphicsDeviceManager _graphicsManager;
    private GraphicsDevice _graphics; 
    
    private Game _game;
    
    public static Texture2D RectangleTexture { get; set; }

    private Dictionary<string, State> _states = new Dictionary<string, State>();
    public static State CurrentState = null;
    private static State _previousState = null;

    private ImGuiRenderer _guiRenderer;

    public static bool DebugMode { get; set; } = false;
    
    public Engine(Game game, GraphicsDeviceManager graphicsManager, GraphicsDevice graphics)
    {
        _game = game;
        _graphics = graphics;
        _graphicsManager = graphicsManager;
        SpriteBatch = new SpriteBatch(graphics);
    }

    public void Init(GameWindow window, ContentManager content)
    {
        _graphicsManager.PreferredBackBufferWidth = (int)WindowSize.X;
        _graphicsManager.PreferredBackBufferHeight = (int)WindowSize.Y;
        _graphicsManager.ApplyChanges();
        
        window.Title = GameTitle;
        Content = content;

        _guiRenderer = new ImGuiRenderer(_game).Initialize().RebuildFontAtlas();
    }

    public void Load()
    {
        RectangleTexture = new Texture2D(_graphics, 1, 1);
        RectangleTexture.SetData(new Color[] { Color.White });
    }

    public void Draw()
    {
        CurrentState?.Render();
        
        List<Actor> actorsCopy = new List<Actor>(CurrentState?.Actors);
        foreach (var actor in actorsCopy)
        {
            actor.Draw();
        }
        
        CurrentState?.RenderGui();
    }

    public void Update(GameTime gameTime)
    {
        HandleDebugMode();
        
        CurrentState?.Update(gameTime);

        List<Actor> actorsCopy = new List<Actor>(CurrentState?.Actors);
        foreach (var actor in actorsCopy)
        {
            actor.Update(gameTime);
        }
        
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    
    public void AddState(string stateName, State state)
    {
        state.Game = _game;
        state.GuiRenderer = _guiRenderer;
        _states.Add(stateName, state);
    }

    public static void ReloadCurrentState()
    {
        _previousState?.Actors.Clear();
        CurrentState.Enter();
    }

    public void SwitchState(string stateName)
    {
        if (_states.TryGetValue(stateName, out State newState))
        {
            if (CurrentState != newState)
            {
                _previousState = CurrentState;
                CurrentState = newState;
                _previousState?.Leave();
                ReloadCurrentState();
            }
        }
        else
        {
            throw new ArgumentException($"State '{stateName}' does not exist.", nameof(stateName));
        }
    }

    public static Actor FindActorWithTag(string tag)
    {
        Actor target = null!;
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.Tag != tag) continue;

            target = actor;
            break;
        }
        
        if (target == null) Console.WriteLine("Unable to find actor with tag " + tag);

        return target;
    }

    public static List<Actor> FindActorsWithTag(string tag)
    {
        List<Actor> targetList = new List<Actor>();
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.Tag != tag) continue;
            
            targetList.Add(actor);
        }

        return targetList;
    }
    
    public static Actor FindActorWithName(string name)
    {
        Actor target = null!;
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.Name != name) continue;

            target = actor;
            break;
        }
        
        if (target == null) Console.WriteLine("Unable to find actor with name " + name);

        return target;
    }
    
    public static List<Actor> FindActorsWithName(string name)
    {
        List<Actor> targetList = new List<Actor>();
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.Name != name) continue;
            
            targetList.Add(actor);
        }

        return targetList;
    }

    public static Actor FindActorOfType(Type type)
    {
        Actor target = null!;
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.GetType() != type) continue;

            target = actor;
            break;
        }
        
        if (target == null) Console.WriteLine("Unable to find actor of type " + type);

        return target;
    }
    
    public static List<Actor> FindActorsOfType(Type type)
    {
        List<Actor> targetList = new List<Actor>();
        foreach (var actor in CurrentState.Actors)
        {
            if (actor.GetType() != type) continue;
            
            targetList.Add(actor);
        }

        return targetList;
    }
    
    private void HandleDebugMode()
    {
        KeyboardHandler.GetState();

        if (KeyboardHandler.IsPressed(Keys.Tab))
        {
            DebugMode = !DebugMode;
        }
    }

}