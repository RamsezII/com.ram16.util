using UnityEngine;

namespace _CORE_
{
    public class CameraShader : MonoBehaviour
    {
        [SerializeField] Material shader;

        //--------------------------------------------------------------------------------------------------------------

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, shader);
        }
    }
}