using GameEngine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sandbox.Actors;
using TiledSharp;

namespace GameEngine.Tilemap;

public class TilemapManager
{
    TmxMap map;
    Texture2D tileset;

    int tileWidth;
    int tileHeight;
    int tilesetTilesWide;
    int tilesetTilesHigh;

    public static Dictionary<string, Func<Actor>> Actors { get; set; } = new Dictionary<string, Func<Actor>>();
    
    public TilemapManager(TmxMap map, Texture2D tileset)
    {
        this.map = map;
        this.tileset = tileset;
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = tileset.Width / tileWidth;
    }

    public void Draw(Vector2 startPosition, Color color, float layerDepth = 0f)
    {
        foreach (var layer in map.Layers)
        {
            for (int i = 0; i < layer.Tiles.Count; i++)
            {
                int gid = layer.Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = startPosition.X + (i % map.Width) * tileWidth * Engine.GameScale;
                    float y = startPosition.Y + (float)Math.Floor(i / (double)map.Width) * tileHeight * Engine.GameScale;

                    Rectangle tilesetRect = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    Engine.SpriteBatch.Draw(tileset, 
                        new Rectangle((int)x, (int)y, (int)(tileWidth * Engine.GameScale), (int)(tileHeight * Engine.GameScale)), 
                        tilesetRect, 
                        color,
                        0f,
                        Vector2.Zero, 
                        SpriteEffects.None,
                        layerDepth
                        );
                }
            }
        }
    }
    
    public void Draw(string layerName, Vector2 startPosition, Color color, float layerDepth = 0f)
    {
        var layer = map.Layers[layerName];
        
        for (int i = 0; i < layer.Tiles.Count; i++)
        {
            int gid = layer.Tiles[i].Gid;

            if (gid != 0)
            {
                int tileFrame = gid - 1;
                int column = tileFrame % tilesetTilesWide;
                int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                float x = startPosition.X + (i % map.Width) * tileWidth * Engine.GameScale;
                float y = startPosition.Y + (float)Math.Floor(i / (double)map.Width) * tileHeight * Engine.GameScale;

                Rectangle tilesetRect = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                Engine.SpriteBatch.Draw(tileset, 
                    new Rectangle((int)x, (int)y, (int)(tileWidth * Engine.GameScale), (int)(tileHeight * Engine.GameScale)), 
                    tilesetRect, 
                    color,
                    0f,
                    Vector2.Zero, 
                    SpriteEffects.None,
                    layerDepth
                );
            }
        }
    }

    public List<TmxLayerTile> GetTiles(string layerName)
    {
        var layer = map.Layers[layerName];

        return layer.Tiles.Where(tile => tile.Gid != 0).ToList();
    }
    
    public void CreateColliders(Vector2 startPosition)
    {
        foreach (var obj in map.ObjectGroups["Collision"].Objects)
        {
            double x = startPosition.X + obj.X * Engine.GameScale;
            double y = startPosition.Y + obj.Y * Engine.GameScale;
            double width = obj.Width * Engine.GameScale;
            double height = obj.Height * Engine.GameScale;

            Solid collider = new Solid(new Vector2((float)x, (float)y), new Vector2((float)width, (float)height));
            Engine.CurrentState.AddActor(collider);
        }
    }

    public static void AddGameObjectToLoad(string name, Func<Actor> actor)
    {
        Actors.Add(name, actor);
    }
    
    public void LoadGameObjects()
    {
        if (map.ObjectGroups.TryGetValue("Game Objects", out var gameObjectsLayer))
        {
            foreach (var obj in gameObjectsLayer.Objects)
            {
                if (Actors.TryGetValue(obj.Name, out var factory))
                {
                    Actor actor = factory();

                    actor.Position = new Vector2((float)obj.X * Engine.GameScale, (float)obj.Y * Engine.GameScale);
                    
                    Engine.CurrentState.AddActor(actor);
                }
            }
        }
    }
}