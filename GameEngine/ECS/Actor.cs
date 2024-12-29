using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ECS;

public class Actor
{
    public Vector2 Position { get; set; }
    
    public Actor()
    {
        Position = Vector2.Zero;
        Create();
    }

    public Actor(Vector2 position)
    {
        Position = position;
        Create();
    }

    protected virtual void Create()
    {
        
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw()
    {
        
    }
}