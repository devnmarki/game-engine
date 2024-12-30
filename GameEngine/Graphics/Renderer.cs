using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics;

public class Renderer
{
    public Renderer()
    {
        
    }

    public void DrawTexture(Texture2D texture, Vector2 position)
    {
        Engine.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, Engine.GameScale, SpriteEffects.None, 0.9f);
    }
    
    public void DrawTexture(Texture2D texture, Vector2 position, Rectangle src, float layerDepth = 0.9f)
    {
        Engine.SpriteBatch.Draw(texture, position, src, Color.White, 0f, Vector2.Zero, Engine.GameScale, SpriteEffects.None, layerDepth);
    }

    public void DrawTexture(Texture2D texture, Rectangle dst, Color color)
    {
        Engine.SpriteBatch.Draw(texture, dst, color);
    }
}