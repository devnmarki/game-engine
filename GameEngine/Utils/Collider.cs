using GameEngine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Utils;

public class Collider
{
    public Actor Actor { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Offset { get; set; }
    
    public Collider(Actor actor, Vector2 size)
    {
        Actor = actor;
        Size = size;
    }

    public Collider(Actor actor, Vector2 size, Vector2 offset)
    {
        Actor = actor;
        Size = size;
        Offset = offset;
    }
    
    public Rectangle GetBounds()
    {
        return new Rectangle(
            (int)(Actor.Position.X + Offset.X),
            (int)(Actor.Position.Y + Offset.Y),
            (int)Size.X,
            (int)Size.Y
        );
    }
    
    public bool CheckCollision(Collider other)
    {
        return GetBounds().Intersects(other.GetBounds());
    }
    
    public void Debug()
    {
        if (Engine.DebugMode)
        {
            Rectangle colliderRect = new Rectangle(GetBounds().Left, GetBounds().Top, (int)Size.X, (int)Size.Y);
            Engine.SpriteBatch.Draw(Engine.RectangleTexture, colliderRect, null, Color.Green * 0.5f, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }
    }
}