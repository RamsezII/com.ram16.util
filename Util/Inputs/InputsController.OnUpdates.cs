using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace _UTIL_
{
    public partial class InputsController
    {
        public static void OnUpdates()
        {
            foreach (InputsController controller in controllers)
                if (controller.gameObject.activeInHierarchy)
                    controller.OnUpdate();
        }

        void OnUpdate()
        {
            nav = Vector2Int.zero;
            axisMove = default;
            axisDriveZ = axisDriveX = 0;

            void AddToNav(in Vector2 value)
            {
                if (value.y > 0)
                    ++nav.y;
                if (value.y < 0)
                    --nav.y;
                if (value.x > 0)
                    ++nav.x;
                if (value.x < 0)
                    --nav.x;
            }

            if (Application.isFocused)
                if (all)
                {
                    foreach (InputDevice device in InputSystem.devices)
                        if (device is Gamepad gamepad)
                            ReadGamepad(gamepad);
                        else if (!isTyping)
                            if (device is Keyboard keyboard)
                                ReadKeyboard(keyboard);
                            else if (device is Mouse mouse)
                                ReadMouse(mouse);
                }
                else
                {
                    if (gamepad != null)
                        ReadGamepad(gamepad);

                    if (!isTyping)
#if UNITY_EDITOR
                        if (!_nopc)
#endif
                        {
                            if (keyboard != null)
                                ReadKeyboard(keyboard);
                            if (mouse != null)
                                ReadMouse(mouse);
                        }
                }

            nav.x = Mathf.Clamp(nav.x, -1, 1);
            nav.y = Mathf.Clamp(nav.y, -1, 1);

            triggerL.Update();
            triggerR.Update();
            axisL.Update();
            axisR.Update();
            mouseDelta.Update();
            scroll.Update();

            axisMove.sqr = axisMove.value.sqrMagnitude;
            if (axisMove.sqr != 0)
            {
                axisMove.value.Normalize();
                axisMove.sqr = 1;
            }

            axisDriveX = Mathf.Clamp(axisDriveX, -1, 1);
            axisDriveZ = Mathf.Clamp(axisDriveZ, -1, 1);

            void ReadGamepad(in Gamepad gamepad)
            {
                if (gamepad.dpad.ReadValue() != Vector2.zero)
                {
                    lastDevice.Update(gamepad);
                    if (IsState(GamepadButton.DpadUp))
                        ++nav.y;
                    else if (IsState(GamepadButton.DpadDown))
                        --nav.y;
                    else if (IsState(GamepadButton.DpadLeft))
                        --nav.x;
                    else if (IsState(GamepadButton.DpadRight))
                        ++nav.x;
                }

                float trgL = gamepad.leftTrigger.ReadValue();
                if (triggerL.ReadValue(trgL))
                    lastDevice.Update(gamepad);
                float trgR = gamepad.rightTrigger.ReadValue();
                if (triggerR.ReadValue(trgR))
                    lastDevice.Update(gamepad);

                axisDriveZ += trgR - trgL;

                if (axisR.ReadValue(gamepad.rightStick.ReadValue()))
                    lastDevice.Update(gamepad);

                Vector2 temp = gamepad.leftStick.ReadValue();
                axisMove.value += temp;
                axisDriveX += temp.x;
                if (axisL.ReadValue(temp))
                    lastDevice.Update(gamepad);
            }

            void ReadKeyboard(in Keyboard keyboard)
            {
                Vector2 temp = keyboard.GetZqsd();
                axisMove.value += temp;
                axisDriveX += temp.x;
                axisDriveZ += temp.y;

                temp = keyboard.GetArrowsDown();
                if (temp.sqrMagnitude > 0)
                    AddToNav(temp);
            }

            void ReadMouse(in Mouse mouse)
            {
                Vector2 delta = mouse.delta.ReadValue();
                if (delta.sqrMagnitude != 0)
                {
                    mouseDelta.current += delta;
                    lastDevice.Update(mouse);
                }

                delta = mouse.scroll.ReadValue();
                if (delta.sqrMagnitude != 0)
                {
                    scroll.current += delta;
                    lastDevice.Update(mouse);
                    AddToNav(delta);
                }
            }
        }
    }
}