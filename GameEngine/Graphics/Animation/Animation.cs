namespace GameEngine.Graphics;

public class Animation
{
    public Spritesheet Spritesheet { get; set; }
    public int[] Frames { get; set; }
    public float FrameDuration { get; set; }
    public bool Loop { get; set; }

    private int _currentFrame = 0;
    private float _currentTime;

    public int CurrentFrame
    {
        get => _currentFrame;
        set => _currentFrame = value;
    }
    
    public bool HasFinished => !Loop && _currentFrame == Frames.Length - 1;
    
    public Animation(Spritesheet spritesheet, int[] frames, float frameDuration, bool loop = true)
    {
        Spritesheet = spritesheet;
        Frames = frames;
        FrameDuration = frameDuration;
        Loop = loop;
    }

    public void Play()
    {
        _currentTime += Engine.DeltaTime;
        if (_currentTime >= FrameDuration)
        {
            _currentFrame++;
            if (_currentFrame >= Frames.Length)
            {
                _currentFrame = Loop ? 0 : Frames.Length - 1;
            }
            
            _currentTime = 0f;
        }
    }

    public void Reset()
    {
        _currentFrame = 0;
        _currentTime = 0f;
    }
}