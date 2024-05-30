using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _UTIL_
{
    [System.Obsolete]
    public partial class InputsController : MonoBehaviour
    {
        [System.Serializable]
        public struct Axis2
        {
            [Range(0, 1)] public float sqr;
            public Vector2 value;
        }

        static readonly HashSet<InputsController> controllers = new();

        [Header("~@ Inputs @~")]
        public bool all;
        public readonly OnValue<InputDevice> lastDevice = new();
        public static bool isTyping;
#if UNITY_EDITOR
        public static bool _nopc = false;
#endif

        public OnAxis_f triggerL, triggerR;
        public OnAxis_v2 axisL, axisR, mouseDelta, scroll;
        public Vector2Int nav;

        public float axisDriveZ, axisDriveX;
        public Axis2 axisMove;

        //----------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        public static void StaticInit() => controllers.Clear();
#endif

        //----------------------------------------------------------------------------------------------------------

        public void Init() => controllers.Add(this);

        //----------------------------------------------------------------------------------------------------------

        private void OnDestroy() => controllers.Remove(this);
    }
}