# MonoGame.EasyInput

Provide extended Mouse & Keyboard input for MonoGame.


## EasyMouse

`EasyMouse` provide extended functionality for mouse input. 

To use `EasyMouse` you need to instantiate it and call `Update()` every frame (from your MonoGame Update() handler).

Most of its key functionality is covered in the following example:

```cs
// check if mouse button is down
if (_easyMouse.IsPressed(MouseButtons.Left))
{
	System.Console.WriteLine("Mouse left mouse button is down!");
}

// check if mouse button is up
if (_easyMouse.IsPressed(MouseButtons.Right))
{
	System.Console.WriteLine("Mouse right mouse button is up!");
}

// check if mouse was pressed this frame
if (_easyMouse.PressedThisFrame(MouseButtons.Left))
{
	System.Console.WriteLine("Mouse left mouse button was pressed this frame!");
}

// check if mouse button was released this frame
if (_easyMouse.ReleasedThisFrame(MouseButtons.Right))
{
	System.Console.WriteLine("Mouse right mouse button was released this frame");
}

// get mouse current position
var currPos = _easyMouse.Position;

// check if mouse is moving and print mouse movement delta
if (_easyMouse.IsMoving)
{
	System.Console.WriteLine("Mouse is currently moving: " + _easyMouse.PositionDelta);
}

// check if mouse wheel is currently scrolling and print scrolling delta
if (_easyMouse.IsScrolling)
{
	System.Console.WriteLine("Mouse is wheel is currently scrolling: " + _easyMouse.ScrollWheelDelta);
}

// set mouse position
_easyMouse.Position = new Point(100, 100);
```

In addition, `EasyMouse` support several events you can register to:

```cs
// mouse button pressed
_easyMouse.OnMouseButtonPressed += (MouseButtons button) =>
{
	System.Console.WriteLine("Mouse button pressed: " + button.ToString());
};

// mouse button released
_easyMouse.OnMouseButtonReleased += (MouseButtons button) =>
{
	System.Console.WriteLine("Mouse button released: " + button.ToString());
};

// mouse is moving
_easyMouse.OnMouseMove += (Point delta) =>
{
	System.Console.WriteLine("Mouse moved: " + delta.ToString());
};

// mouse scroll wheel is moved
_easyMouse.OnScrollWheelUse += (int delta) =>
{
	System.Console.WriteLine("Mouse wheel scrolled: " + delta.ToString());
};
```


## EasyKeyboard

`EasyKeyboard` provide extended functionality for keyboard input. 

To use `EasyKeyboard` you need to instantiate it and call `Update()` every frame (from your MonoGame Update() handler).

Most of its key functionality is covered in the following example:

```cs
// key is pressed
if (_easyKeyboard.IsPressed(Keys.A))
{
	System.Console.WriteLine("'A' Key is currently pressed.");
}

// key is released
if (_easyKeyboard.IsReleased(Keys.A))
{
	System.Console.WriteLine("'A' Key is currently released.");
}

// key pressed this frame
if (_easyKeyboard.PressedThisFrame(Keys.A))
{
	System.Console.WriteLine("'A' Key was pressed this frame.");
}

// key released this frame
if (_easyKeyboard.ReleasedThisFrame(Keys.A))
{
	System.Console.WriteLine("'A' Key was released this frame.");
}

// any key is down
if (_easyKeyboard.AnyKeyPressed)
{
	System.Console.WriteLine("There's a key down!");
}

// alt is down
if (_easyKeyboard.AltPressed)
{
	System.Console.WriteLine("Alt is down!");
}

// control is down
if (_easyKeyboard.CtrPressed)
{
	System.Console.WriteLine("Control is down!");
}

// Shift is down
if (_easyKeyboard.ShiftPressed)
{
	System.Console.WriteLine("Shift is down!");
}

// capslock is active
if (_easyKeyboard.Capslock)
{
	System.Console.WriteLine("Capslock active!");
}
```

In addition, `EasyKeyboard` support several events you can register to:

```cs
// keyboard key was pressed
_easyKeyboard.OnKeyPressed += (Keys key) =>
{
	System.Console.WriteLine("Key button pressed: " + key.ToString());
};

// keyboard key was released
_easyKeyboard.OnKeyReleased += (Keys key) =>
{
	System.Console.WriteLine("Key button released: " + key.ToString());
};
```


# License

`MonoGame.EasyInput` is distributed under the permissive MIT license, and is absolutely free to use for any purpose.
