using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _UTIL_
{
    public partial class InputsController
    {
        [Header("~@ Devices @~")]
        public Gamepad gamepad;
        public Keyboard keyboard;
        public Mouse mouse;
        public InputDevice device_last;

        //----------------------------------------------------------------------------------------------------------

        public static bool IsReserved(in InputDevice device)
        {
            foreach (InputsController ctr in controllers)
                if (device == ctr.gamepad || device == ctr.keyboard || device == ctr.mouse)
                    return true;
            return false;
        }

        public IEnumerable<Keyboard> EKeyboards()
        {
            if (all)
            {
                foreach (InputDevice device in InputSystem.devices)
                    if (device is Keyboard keyboard)
                        yield return keyboard;
            }
            else if (keyboard != null)
                yield return keyboard;
        }

        public IEnumerable<Mouse> EMouses()
        {
            if (all)
            {
                foreach (InputDevice device in InputSystem.devices)
                    if (device is Mouse mouse)
                        yield return mouse;
            }
            else if (mouse != null)
                yield return mouse;
        }

        public IEnumerable<Gamepad> EGamepads()
        {
            if (all)
            {
                foreach (InputDevice device in InputSystem.devices)
                    if (device is Gamepad gamepad)
                        yield return gamepad;
            }
            else if (gamepad != null)
                yield return gamepad;
        }
    }
}