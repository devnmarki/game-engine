using Microsoft.Xna.Framework.Input;

namespace GameEngine.Input;

public class KeyboardHandler
{
	static KeyboardState currentKeyState;
	static KeyboardState previousKeyState;

	public static KeyboardState GetState()
	{
		previousKeyState = currentKeyState;
		currentKeyState = Keyboard.GetState();
		return currentKeyState;
	}

	public static bool IsDown(Keys key)
	{
		return currentKeyState.IsKeyDown(key);
	}

	public static bool IsUp(Keys key)
	{
		return currentKeyState.IsKeyUp(key);
	}

	public static bool IsPressed(Keys key)
	{
		return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
	}
}