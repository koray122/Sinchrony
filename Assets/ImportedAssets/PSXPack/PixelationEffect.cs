using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelationEffect : MonoBehaviour
{
    [Range(64, 1024)] public int pixelResolution = 256;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
        int width = pixelResolution;
        int height = pixelResolution * source.height / source.width;

        RenderTexture lowRes = RenderTexture.GetTemporary(width, height, 0);
        lowRes.filterMode = FilterMode.Point;

        
        Graphics.Blit(source, lowRes);

       
        Graphics.Blit(lowRes, destination);

        
        RenderTexture.ReleaseTemporary(lowRes);
    }
}
