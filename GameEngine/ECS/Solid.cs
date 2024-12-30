using Box2DSharp.Dynamics;
using GameEngine;
using GameEngine.ECS;
using GameEngine.Input;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox.Actors;

public class Solid : Actor
{
    public Solid(Vector2 position, Vector2 size) : base(position)
    {
        Colliders.Add(new Collider(this, size));
    }
}