using Box2DSharp.Dynamics;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ECS;

public class Actor
{
    public Vector2 Position;
    public string Tag;
    public string Name;
    
    private Renderer _renderer = new Renderer();
    
    private Texture2D _texture = null;
    private Spritesheet _spritesheet = null;
    private int _sprite = 0;

    public Animator Animator { get; set; }
    
    public List<Collider> Colliders { get; set; } = new List<Collider>();
    public List<object> CollisionIgnoreList { get; set; } = new List<object>();

    public bool Visible { get; set; } = true;
    public float Layer { get; set; } = 900f;
    
    public Actor()
    {
        Position = Vector2.Zero;
        Tag = "untagged";
        Name = "Actor"; 
            
        Create();
    }

    public Actor(Vector2 position)
    {
        Position = position;
        Tag = "untagged";
        Name = "Actor";

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
        if (Visible)
        {
            if (_texture != null)
                _renderer.DrawTexture(_texture, Position, Layer / 1000f);
            if (_spritesheet != null)
                _renderer.DrawTexture(_spritesheet.Texture, Position, _spritesheet.Sprites[_sprite], Layer / 1000f);
            
            Animator.Render(Layer);
        }

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
        bool isIgnoredType = CollisionIgnoreList.Contains(other.Actor) || 
                             CollisionIgnoreList.Contains(other.Actor.GetType());
        
        if (axis == Axis.Horizontal)
        {
            if (current.GetBounds().Right >= other.GetBounds().Left && current.GetBounds().Left <= other.GetBounds().Left)
            {
                if (!isIgnoredType)
                    Position.X = other.Actor.Position.X - current.Size.X - current.Offset.X + other.Offset.X;
            }

            if (current.GetBounds().Left <= other.GetBounds().Right && current.GetBounds().Right >= other.GetBounds().Right)
            {
                if (!isIgnoredType)
                    Position.X = other.Actor.Position.X + other.Size.X - current.Offset.X + other.Offset.X;
            }
        }
        else
        {
            if (current.GetBounds().Bottom >= other.GetBounds().Top && current.GetBounds().Top <= other.GetBounds().Top)
            {
                if (!isIgnoredType)
                    Position.Y = other.Actor.Position.Y - current.Size.Y - current.Offset.Y + other.Offset.Y;
            }

            if (current.GetBounds().Top <= other.GetBounds().Bottom && current.GetBounds().Bottom >= other.GetBounds().Bottom)
            {
                if (!isIgnoredType)
                    Position.Y = other.Actor.Position.Y + other.Size.Y - current.Offset.Y + other.Offset.Y;
            }
        }
    }
}