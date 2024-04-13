using UnityEngine;
using UnityEngine.InputSystem;

namespace _UTIL_
{
    public class UI_Input : MonoBehaviour
    {
        public void OnSet(in InputDevice device)
        {
            bool gp_b = device is Gamepad;

            try
            {
                transform.Find("kb").gameObject.SetActive(!gp_b);
                transform.Find("ctr").gameObject.SetActive(gp_b);
            }
            catch
            {
#if UNITY_EDITOR
                print(device + " -> " + transform.GetPath(null));
#endif
            }
        }
    }
}