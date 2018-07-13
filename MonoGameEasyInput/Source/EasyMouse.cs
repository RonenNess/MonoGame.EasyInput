/*
 * Provide mouse-related helper functions.
 * Author: Ronen Ness.
 * Since: 2018.
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.EasyInput
{
    /// <summary>
    /// All mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        /// <summary>
        /// Left mouse button.
        /// </summary>
        Left,

        /// <summary>
        /// Middle mouse button.
        /// </summary>
        Middle,

        /// <summary>
        /// Right mouse button.
        /// </summary>
        Right, 

        /// <summary>
        /// X1 button.
        /// </summary>
        X1,

        /// <summary>
        /// X2 button.
        /// </summary>
        X2,
    }

    /// <summary>
    /// Callbacks for mouse button-related events.
    /// </summary>
    public delegate void MouseButtonEvent(MouseButtons button);

    /// <summary>
    /// Callbacks for mouse position-related events.
    /// </summary>
    public delegate void MousePositionEvent(Point posOrDelta);

    /// <summary>
    /// Callbacks for mouse wheel-related events.
    /// </summary>
    public delegate void MouseWheelEvent(int val);

    /// <summary>
    /// Input helper for mouse events.
    /// </summary>
    public class EasyMouse
    {
        // previous mouse state
        private MouseState _prevState;

        // current mouse state
        private MouseState _newState;

        // store mouse position seperately, since we can change it.
        private Point _mousePos;

        /// <summary>
        /// Callback to call on mouse button press.
        /// Param is button code.
        /// </summary>
        public MouseButtonEvent OnMouseButtonPressed = null;

        /// <summary>
        /// Callback to call on mouse button release.
        /// Param is button code.
        /// </summary>
        public MouseButtonEvent OnMouseButtonReleased = null;

        /// <summary>
        /// Callback to call when mouse moves. 
        /// Param is mouse position delta.
        /// </summary>
        public MousePositionEvent OnMouseMove = null;

        /// <summary>
        /// Callback to call when mouse scroll wheel is used. 
        /// Param is scroll wheel delta.
        /// </summary>
        public MouseWheelEvent OnScrollWheelUse = null;

        /// <summary>
        /// Get if the mouse moved this frame.
        /// Note: if you change mouse position manually, it will not turn true.
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Get if mouse wheel is currently scrolling.
        /// </summary>
        public bool IsScrolling { get { return ScrollWheelDelta != 0; } }

        /// <summary>
        /// Create the easy mouse helper.
        /// </summary>
        public EasyMouse()
        {
            _prevState = Mouse.GetState();
        }

        /// <summary>
        /// Update mouse-related events.
        /// Call this function at the begining of every Update() frame.
        /// </summary>
        public void Update()
        {
            // store previous state and get new state
            _prevState = _newState;
            _newState = Mouse.GetState();

            // get new mouse position
            _mousePos = _newState.Position;

            // generate mouse-button events
            foreach (MouseButtons button in System.Enum.GetValues(typeof(MouseButtons)))
            {
                // mouse button release event
                if (ReleasedThisFrame(button))
                    OnMouseButtonReleased?.Invoke(button);

                // mouse button pressed event
                if (PressedThisFrame(button))
                    OnMouseButtonPressed?.Invoke(button);
            }

            // is mouse moving this frame?
            IsMoving = PositionDelta != Point.Zero;

            // generate mouse-move events
            if (IsMoving)
            {
                OnMouseMove?.Invoke(PositionDelta);
            }

            // gerate scroll wheel events
            if (ScrollWheelDelta != 0)
            {
                OnScrollWheelUse?.Invoke(ScrollWheelDelta);
            }
        }

        /// <summary>
        /// Get / set mouse position.
        /// </summary>
        public Point Position
        {
            set { Mouse.SetPosition(value.X, value.Y); _mousePos = value; }
            get { return _mousePos; }
        }

        /// <summary>
        /// Get previous mouse position.
        /// </summary>
        public Point PrevPosition
        {
            get { return _prevState.Position; }
        }

        /// <summary>
        /// Get scroll wheel value.
        /// </summary>
        public int ScrollWheelValue
        {
            get { return _newState.ScrollWheelValue; }
        }

        /// <summary>
        /// Get previous scroll wheel value.
        /// </summary>
        public int PrevScrollWheelValue
        {
            get { return _prevState.ScrollWheelValue; }
        }

        /// <summary>
        /// Get scroll wheel delta.
        /// </summary>
        public int ScrollWheelDelta
        {
            get { return _newState.ScrollWheelValue - _prevState.ScrollWheelValue; }
        }

        /// <summary>
        /// Return mouse delta since last frame.
        /// </summary>
        public Point PositionDelta
        {
            get { return _mousePos - _prevState.Position; }
        }

        /// <summary>
        /// Return if a mouse button is pressed.
        /// </summary>
        /// <param name="state">Mouse state to check.</param>
        /// <param name="button">Button to check.</param>
        /// <returns>If button is down.</returns>
        private bool IsPressed(ref MouseState state, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return state.LeftButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return state.RightButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return state.MiddleButton == ButtonState.Pressed;

                case MouseButtons.X1:
                    return state.XButton1 == ButtonState.Pressed;

                case MouseButtons.X2:
                    return state.XButton2 == ButtonState.Pressed;

                default:
                    throw new System.IndexOutOfRangeException("Invalid mouse button!");
            }
        }

        /// <summary>
        /// Return if a mouse button is currently pressed.
        /// </summary>
        /// <param name="button">Mouse button to check.</param>
        /// <returns>If mouse button is pressed.</returns>
        public bool IsPressed(MouseButtons button)
        {
            return IsPressed(ref _newState, button);
        }

        /// <summary>
        /// Return if mouse button is currently released.
        /// </summary>
        /// <param name="button">Mouse button to check.</param>
        /// <returns>If mouse button is released.</returns>
        public bool IsReleased(MouseButtons button)
        {
            return !IsPressed(button);
        }

        /// <summary>
        /// Return if a mouse button was pressed this frame.
        /// </summary>
        /// <param name="button">Mouse button to check.</param>
        /// <returns>If mouse button is pressed this frame.</returns>
        public bool PressedThisFrame(MouseButtons button)
        {
            return IsPressed(ref _newState, button) && !IsPressed(ref _prevState, button);
        }

        /// <summary>
        /// Return if mouse button was released this frame.
        /// </summary>
        /// <param name="button">Mouse button to check.</param>
        /// <returns>If mouse button is released this frame.</returns>
        public bool ReleasedThisFrame(MouseButtons button)
        {
            return !IsPressed(button) && IsPressed(ref _prevState, button);
        }

        /// <summary>
        /// Set mouse cursor.
        /// </summary>
        /// <param name="cursor">New mouse cursor.</param>
        public void SetCursor(MouseCursor cursor)
        {
            Mouse.SetCursor(cursor);
        }
    }
}
