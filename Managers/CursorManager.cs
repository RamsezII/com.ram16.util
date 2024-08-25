using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public static class CursorManager
    {
        public interface IUser
        {
        }

        static readonly HashSet<IUser> users = new();

        //--------------------------------------------------------------------------------------------------------------

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            users.Clear();
            RefreshCursorState();
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void ToggleUser(in IUser user, in bool state)
        {
            if (state)
                users.Add(user);
            else
                users.Remove(user);
            RefreshCursorState();
        }

        public static void RefreshCursorState()
        {
            bool target = users.Count > 0;
            Cursor.visible = target;
            Cursor.lockState = target ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}