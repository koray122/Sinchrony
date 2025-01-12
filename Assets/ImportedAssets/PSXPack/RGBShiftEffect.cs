using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RGBShiftEffect : MonoBehaviour
{
    [Header("RGB Shift Settings")]
    [Range(0f, 0.05f)] public float shiftAmount = 0.005f;

    private Material rgbShiftMaterial;

    private void Start()
    {
        Shader shader = Shader.Find("Custom/RGBShiftShader");
        if (shader == null)
        {
            Debug.LogError("RGB Shift Shader not found. Make sure you have added the shader.");
            enabled = false;
            return;
        }
        rgbShiftMaterial = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (rgbShiftMaterial != null)
        {
            rgbShiftMaterial.SetFloat("_Amount", shiftAmount);
            Graphics.Blit(source, destination, rgbShiftMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void OnDestroy()
    {
        if (rgbShiftMaterial != null)
        {
            DestroyImmediate(rgbShiftMaterial);
        }
    }
}
