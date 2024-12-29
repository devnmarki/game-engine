using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ECS;

public class Actor
{
    private Renderer _renderer = new Renderer();
    
    private Texture2D _texture = null;
    private Spritesheet _spritesheet = null;
    private int _sprite = 0;
    
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
        if (_texture != null)
            _renderer.DrawTexture(_texture, Position);
        if (_spritesheet != null)
            _renderer.DrawTexture(_spritesheet.Texture, Position, _spritesheet.Sprites[_sprite]);
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
}