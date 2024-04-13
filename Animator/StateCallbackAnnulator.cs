#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _UTIL_
{
    static class StateCallbackAnnulator_util
    {
        [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(_UTIL_) + "/" + nameof(Add) + nameof(StateCallbackAnnulator))]
        static void Add(MenuCommand command) => ((Animator)command.context).gameObject.AddComponent<StateCallbackAnnulator>();
    }

    public class StateCallbackAnnulator : MonoBehaviour, IOnStateMachine
    {
        void IOnStateMachine.OnStateMachine(in AnimatorStateInfo stateInfo, in int layerIndex, in bool onEnter)
        {

        }
    }
}
#endif