using UnityEngine;

partial class Util
{
    public static void Clear(this RenderTexture renderTexture, in Color clearColor = default)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, clearColor);
        RenderTexture.active = rt;
    }

    public static Texture2D GetCopy(this RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D texture = new(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = rt;
        return texture;
    }
}