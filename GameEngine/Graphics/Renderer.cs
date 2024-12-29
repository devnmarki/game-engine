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
        Engine.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, Engine.GameScale, SpriteEffects.None, 0f);
    }
    
    public void DrawTexture(Texture2D texture, Vector2 position, Rectangle src, float layerDepth = 0f)
    {
        Engine.SpriteBatch.Draw(texture, position, src, Color.White, 0f, Vector2.Zero, Engine.GameScale, SpriteEffects.None, layerDepth);
    }
}