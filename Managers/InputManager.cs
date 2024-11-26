using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

/*
 * Handles all input logic, such as keyboard, and centralizes input detection.
 * ## Detect specific key presses or mouse events for controlling the player.
 * ## Send relevant commands to other managers like SceneManager or fire projectiles in PlayerManager).
 */

namespace RetroShooter.Managers
{
    public class InputManager
    {
        public KeyboardState _currentKeyboardState;
        public KeyboardState _previousKeyboardState;
        public MouseState _currentMouseState;
        public MouseState _previousMouseState;

        public InputManager()
        {
            _currentKeyboardState = Keyboard.GetState();
            _previousKeyboardState = _currentKeyboardState;
            _currentMouseState = Mouse.GetState();
            _previousMouseState = _currentMouseState;
        }

        public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        public bool IsMouseButtonPressed(ButtonState button)
        {
            return _currentMouseState.LeftButton == button && _previousMouseState.LeftButton != button;
        }

        public Point GetMousePosition()
        {
            return _currentMouseState.Position;
        }

        public KeyboardState GetCurrentKeyboardState()
        {
            return _currentKeyboardState;
        }

        public MouseState GetCurrentMouseState() 
        {
            return _currentMouseState;
        }

    }
}