using GameEngine.ECS;

namespace GameEngine.Graphics;

public class Animator
{
    private Renderer _renderer;
    private Actor _actor;
    
    public Dictionary<string, Animation> Animations { get; set; } = new Dictionary<string, Animation>();

    public Animation CurrentAnimation { get; set; }

    public Animator(Actor actor, Renderer renderer)
    {
        _actor = actor;
        _renderer = renderer;
    }
    
    public void AddAnimation(string animationName, Animation animation)
    {
        Animations.Add(animationName, animation);
    }

    public void PlayAnimation(string animationName)
    {
        Animation newAnimation = Animations[animationName];
        if (CurrentAnimation != newAnimation)
        {
            CurrentAnimation = newAnimation;
            CurrentAnimation?.Reset();
        }
    }

    public void Update()
    {
        CurrentAnimation?.Play();
    }

    public void Render()
    {
        if (CurrentAnimation == null) return;
        
        _renderer.DrawTexture(CurrentAnimation.Spritesheet.Texture, _actor.Position, CurrentAnimation.Spritesheet.Sprites[CurrentAnimation.Frames[CurrentAnimation.CurrentFrame]]);
    }
    
    public bool IsCurrentAnimationFinsihed()
    {
        return CurrentAnimation != null && CurrentAnimation.HasFinished;
    }
    
    public Animation GetAnimation(string animationName)
    {
        return Animations[animationName];
    }
}