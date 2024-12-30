using Box2DSharp.Dynamics;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ECS;

public class Actor
{
    private Renderer _renderer = new Renderer();
    
    private Texture2D _texture = null;
    private Spritesheet _spritesheet = null;
    private int _sprite = 0;

    protected Animator Animator { get; set; }

    public Vector2 Position;
    
    public List<Collider> Colliders { get; set; } = new List<Collider>();
    
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
        Animator = new Animator(this, _renderer);
    }

    public virtual void Update(GameTime gameTime)
    {
        Animator.Update();
    }

    public virtual void Draw()
    {
        if (_texture != null)
            _renderer.DrawTexture(_texture, Position);
        if (_spritesheet != null)
            _renderer.DrawTexture(_spritesheet.Texture, Position, _spritesheet.Sprites[_sprite]);
        
        Animator.Render();

        foreach (var collider in Colliders)
        {
            collider.Debug();
        }
    }

    public void UseGraphics(Texture2D texture)
    {
        _texture = texture;
    }
    
    public void UseGraphics(Spritesheet sheet, int sprite)
    {
        _spritesheet = sheet;
        _sprite = sprite;
    }

    public void CheckCollision(Axis axis)
    {
        var actors = new List<Actor>(Engine.CurrentState.Actors);
        foreach (var actor in actors)
        {
            if (actor != this)
            {
                foreach (var collider in Colliders)
                {
                    foreach (var otherCollider in actor.Colliders)
                    {
                        if (collider.CheckCollision(otherCollider))
                        {
                            HandleCollision(collider, otherCollider, axis);
                        }
                    }
                }
            }
        }
    }
    
    private void HandleCollision(Collider current, Collider other, Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            if (current.GetBounds().Right >= other.GetBounds().Left && current.GetBounds().Left <= other.GetBounds().Left)
            {
                Position.X = other.Actor.Position.X - current.Size.X - current.Offset.X + other.Offset.X;
            }

            if (current.GetBounds().Left <= other.GetBounds().Right && current.GetBounds().Right >= other.GetBounds().Right)
            {
                Position.X = other.Actor.Position.X + other.Size.X - current.Offset.X + other.Offset.X;
            }
        }
        else
        {
            if (current.GetBounds().Bottom >= other.GetBounds().Top && current.GetBounds().Top <= other.GetBounds().Top)
            {
                Position.Y = other.Actor.Position.Y - current.Size.Y - current.Offset.Y + other.Offset.Y;
            }

            if (current.GetBounds().Top <= other.GetBounds().Bottom && current.GetBounds().Bottom >= other.GetBounds().Bottom)
            {
                Position.Y = other.Actor.Position.Y + other.Size.Y - current.Offset.Y + other.Offset.Y;
            }
        }
    }
}