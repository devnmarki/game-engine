﻿using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace MonoGame.ImGui.Data;

/// <summary>
///     Contains the GUIRenderer's input data elements.
/// </summary>
public class InputData {
    public List<int> KeyMap { get; private set; }
    public int Scrollwheel { get; private set; }

    public InputData() {
        Scrollwheel = 0;
        KeyMap = new List<int>();
    }

    public void Update(Game game) {
        if (!game.IsActive)
            return;

        var io = ImGuiNET.ImGui.GetIO();
        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();

        // Update keys
        for (var i = 0; i < KeyMap.Count; i++)
            io.KeysDown[KeyMap[i]] = keyboard.IsKeyDown((Keys)KeyMap[i]);

        // Update modifier keys
        io.KeyShift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);
        io.KeyCtrl = keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl);
        io.KeyAlt = keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt);
        io.KeySuper = keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows);

        // Update display size
        io.DisplaySize = new Vector2(
            game.GraphicsDevice.PresentationParameters.BackBufferWidth,
            game.GraphicsDevice.PresentationParameters.BackBufferHeight
        );
        io.DisplayFramebufferScale = new Vector2(1f, 1f);

        // Update mouse state
        io.MousePos = new Vector2(mouse.X, mouse.Y);
        io.MouseDown[0] = mouse.LeftButton == ButtonState.Pressed;
        io.MouseDown[1] = mouse.RightButton == ButtonState.Pressed;
        io.MouseDown[2] = mouse.MiddleButton == ButtonState.Pressed;

        // Update scroll wheel
        var scrollDelta = mouse.ScrollWheelValue - Scrollwheel;
        io.MouseWheel = scrollDelta > 0 ? 1 : scrollDelta < 0 ? -1 : 0;
        Scrollwheel = mouse.ScrollWheelValue;

        // Update cursor (commented out as MonoGame does not natively support cursor management)
        // UpdateCursor();
    }

    // Platform-specific cursor management would go here if needed.

    public InputData Initialize(Game game) {
        var io = ImGuiNET.ImGui.GetIO();

        // Map ImGui keys to MonoGame keys
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Back);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y);
        KeyMap.Add(io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z);

        // Add TextInput event handler for character input
        game.Window.TextInput += (sender, args) => {
            if (args.Character != '\t') // Ignore tab as it is handled elsewhere
                io.AddInputCharacter(args.Character);
        };

        // Initialize default font
        io.Fonts.AddFontDefault();
        return this;
    }
}
