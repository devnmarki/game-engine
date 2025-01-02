using GameEngine.ECS;
using Microsoft.Xna.Framework;
using Sandbox.Models;

namespace Sandbox.Actors.Weapons;

public class SwordWeapon : Actor
{
    public enum SwordType
    {
        Fynn
    }
    
    public WeaponModel.Weapon Model { get; set; }
    
    public SwordWeapon(WeaponModel.Weapon model, SwordType type)
    {
        Model = model;
        
        Create();
    }

    protected override void Create()
    {
        base.Create();

        if (Model == null) return;
        
        base.Tag = "weapon";
        base.Name = Model.Name;
        base.Layer = Globals.Layers.ItemsLayer;
    }
}