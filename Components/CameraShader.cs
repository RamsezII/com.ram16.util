using UnityEngine;

namespace _CORE_
{
    public class CameraShader : MonoBehaviour
    {
        [SerializeField] Material[] shaders;

        //--------------------------------------------------------------------------------------------------------------

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            for (int i = 0; i < shaders.Length; i++)
                Graphics.Blit(source, destination, shaders[i]);
        }
    }
}