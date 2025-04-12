using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using _UTIL_;
using System;

namespace _UTIL_e
{
    internal class AnimatorStateChecker : MonoBehaviour
    {
        [SerializeField] AnimatorController selectedController;
        [SerializeField] int selectedLayerIndex;

        //--------------------------------------------------------------------------------------------------------------

        [MenuItem("Assets/" + nameof(_UTIL_e) + "/" + nameof(AnimatorStateChecker))]
        static void InstantiateInScene() => Util.InstantiateOrCreateIfAbsent<AnimatorStateChecker>();

        [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(_UTIL_e) + "/" + nameof(AnimatorStateChecker))]
        static void InstantiateInScene(MenuCommand command)
        {
            Animator animator = (Animator)command.context;
            AnimatorStateChecker instance = Util.InstantiateOrCreateIfAbsent<AnimatorStateChecker>();
            instance.selectedController = (AnimatorController)animator.runtimeAnimatorController;
        }

        //--------------------------------------------------------------------------------------------------------------

        [ContextMenu(nameof(CheckLayer))]
        void CheckLayer()
        {
            if (selectedController == null)
                Debug.LogWarning("no animator selected", this);
            else
            {
                string[] layerNames = Array.ConvertAll(selectedController.layers, l => l.name);

                var layer = selectedController.layers[selectedLayerIndex];
                var states = layer.stateMachine.states;
                int modified = 0;

                foreach (var state in states)
                {
                    bool hasIt = false;
                    foreach (var behaviour in state.state.behaviours)
                    {
                        if (behaviour != null && behaviour.GetType() == typeof(OnStateMachine)) // 👈 ton type ici
                        {
                            hasIt = true;
                            break;
                        }
                    }

                    if (!hasIt)
                    {
                        state.state.AddStateMachineBehaviour<OnStateMachine>(); // 👈 ton type ici aussi
                        modified++;
                    }
                }

                Debug.Log($"Ajouté OnStateMachineLogger sur {modified} états du layer {layer.name}.");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}