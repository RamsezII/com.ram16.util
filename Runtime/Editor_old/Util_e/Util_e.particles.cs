#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static partial class Util_e_OLD
{
    [MenuItem("CONTEXT/" + nameof(ParticleSystem) + "/" + nameof(_EDITOR_) + "/" + nameof(PlayParticles))]
    static void PlayParticles(MenuCommand menuCommand) => ((ParticleSystem)menuCommand.context).Play();
}
#endif