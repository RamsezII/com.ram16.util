using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;
using UnityEngine;

namespace _UTIL_
{
    public partial class InputsController
    {
        public bool HasPressed_next => IsState(MouseButton.Left) || IsState(GamepadButton.South) || IsState(Key.Enter);
        public bool HasPressed_back => IsState(Key.Escape) || IsState(Key.Backspace) || IsState(GamepadButton.East);
        public bool HasPressed_enter => IsState(Key.Enter) || IsState(Key.NumpadEnter) || IsState(GamepadButton.South);

        //----------------------------------------------------------------------------------------------------------

        public bool IsState(in Key key, in HoldStates state = HoldStates.Down)
        {
            if (isTyping)
                return false;
            if (keyboard != null && keyboard[key].IsState(state))
                return true;
            else if (all)
                foreach (InputDevice device in InputSystem.devices)
                    if (device is Keyboard keyboard && keyboard[key].IsState(state))
                        return true;
            return false;
        }

        public bool IsState(in HoldStates state = HoldStates.Down, params Key[] keys)
        {
            foreach (var key in keys)
                if (IsState(key, state))
                    return true;
            return false;
        }

        public bool IsState(in GamepadButton button, in HoldStates state = HoldStates.Down)
        {
            if (gamepad != null && gamepad[button].IsState(state))
                return true;
            else if (all)
                foreach (InputDevice device in InputSystem.devices)
                    if (device is Gamepad gamepad && gamepad[button].IsState(state))
                        return true;
            return false;
        }

        public bool IsState(in MouseButton button, in HoldStates state = HoldStates.Down)
        {
            if (isTyping)
                return false;
            foreach (var mouse in EMouses())
                switch (button)
                {
                    case MouseButton.Left:
                        if (mouse.leftButton.IsState(state))
                            return true;
                        break;
                    case MouseButton.Right:
                        if (mouse.rightButton.IsState(state))
                            return true;
                        break;
                    case MouseButton.Middle:
                        if (mouse.middleButton.IsState(state))
                            return true;
                        break;
                    case MouseButton.Forward:
                        if (mouse.forwardButton.IsState(state))
                            return true;
                        break;
                    case MouseButton.Back:
                        if (mouse.backButton.IsState(state))
                            return true;
                        break;
                    default:
                        Debug.LogError($"Unknown mouse button: {button}");
                        return false;
                }
            return false;
        }
    }
}