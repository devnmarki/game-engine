using GameEngine.Graphics;

namespace Sandbox.Models;

public class WeaponModel
{
    public static class Models
    {
        public static readonly Weapon FynnSword = new Weapon("Fynn Sword", Assets.Spritesheets.Weapons.FynnSwordSpritesheet, 1);
    }
    
    public class Weapon
    {
        public string Name { get; set; }
        public Spritesheet Spritesheet { get; set; }
        public int Damage { get; set; }
        
        public Weapon(string name, Spritesheet spritesheet, int damage)
        {
            Name = name;
            Spritesheet = spritesheet;
            Damage = damage;
        } 
    }
}