/*
 * Provide keyboard-related helper functions.
 * Author: Ronen Ness.
 * Since: 2018.
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.EasyInput
{
    /// <summary>
    /// Callbacks for keyboard button-related events.
    /// </summary>
    public delegate void KeyboardButtonEvent(Keys button);

    /// <summary>
    /// Input helper for keyboard events.
    /// </summary>
    public class EasyKeyboard
    {
        // previous keyboard state
        private KeyboardState _prevState;

        // current keyboard state
        private KeyboardState _newState;

        /// <summary>
        /// Callback to call on keyboard button press.
        /// Param is button code.
        /// </summary>
        public KeyboardButtonEvent OnKeyPressed;

        /// <summary>
        /// Callback to call on keyboard button release.
        /// Param is button code.
        /// </summary>
        public KeyboardButtonEvent OnKeyReleased;

        /// <summary>
        /// Get if capslock is active.
        /// </summary>
        public bool Capslock
        {
            get { return _newState.CapsLock; }
        }

        /// <summary>
        /// Get if numlock is active.
        /// </summary>
        public bool NumLock
        {
            get { return _newState.NumLock; }
        }

        /// <summary>
        /// Return if any key is currently down.
        /// </summary>
        public bool AnyKeyPressed
        {
            get
            {
                return _newState.GetPressedKeys().Length > 0;
            }
        }

        /// <summary>
        /// Return if any of the alt buttons is down.
        /// </summary>
        public bool AltPressed
        {
            get
            {
                return _newState.IsKeyDown(Keys.LeftAlt) || _newState.IsKeyDown(Keys.RightAlt);
            }
        }

        /// <summary>
        /// Return if any of the ctrl buttons is down.
        /// </summary>
        public bool CtrPressed
        {
            get
            {
                return _newState.IsKeyDown(Keys.LeftControl) || _newState.IsKeyDown(Keys.RightControl);
            }
        }

        /// <summary>
        /// Return if any of the shift buttons is down.
        /// </summary>
        public bool ShiftPressed
        {
            get
            {
                return _newState.IsKeyDown(Keys.LeftShift) || _newState.IsKeyDown(Keys.RightShift);
            }
        }

        /// <summary>
        /// Create the easy keyboard helper.
        /// </summary>
        public EasyKeyboard()
        {
            _prevState = Keyboard.GetState();
        }

        /// <summary>
        /// Update keyboard-related events.
        /// Call this function at the begining of every Update() frame.
        /// </summary>
        public void Update()
        {
            // store previous state and get new state
            _prevState = _newState;
            _newState = Keyboard.GetState();
            
            // generate events
            if (OnKeyReleased != null || OnKeyPressed != null)
            {
                foreach (Keys button in System.Enum.GetValues(typeof(Keys)))
                {
                    if (ReleasedThisFrame(button))
                    {
                        OnKeyReleased?.Invoke(button);
                    }
                    else if (PressedThisFrame(button))
                    {
                        OnKeyPressed?.Invoke(button);
                    }
                }
            }
        }

        /// <summary>
        /// Return if a keyboard button is currently pressed.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is pressed.</returns>
        public bool IsPressed(Keys button)
        {
            return _newState.IsKeyDown(button);
        }

        /// <summary>
        /// Return if keyboard button is currently released.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is released.</returns>
        public bool IsReleased(Keys button)
        {
            return _newState.IsKeyUp(button);
        }

        /// <summary>
        /// Return if a keyboard button was pressed this frame.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is pressed this frame.</returns>
        public bool PressedThisFrame(Keys button)
        {
            return _newState.IsKeyDown(button) && _prevState.IsKeyUp(button);
        }

        /// <summary>
        /// Return if keyboard button was released this frame.
        /// </summary>
        /// <param name="button">Keyboard button to check.</param>
        /// <returns>If keyboard button is released this frame.</returns>
        public bool ReleasedThisFrame(Keys button)
        {
            return _newState.IsKeyUp(button) && _prevState.IsKeyDown(button);
        }
    }
}
