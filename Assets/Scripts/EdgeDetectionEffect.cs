using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 边缘检测 描边
/// </summary>
public class EdgeDetectionEffect : PostProcessBase
{
    public Shader EdgeDetectShader;
    private Material EdgeDetectMat = null;
    public  Material Mat {
        get {
            EdgeDetectMat = CheckShaderAndCreateMaterial(EdgeDetectShader, EdgeDetectMat);
            return EdgeDetectMat;
        }
    }

    [Range(0.0f, 1.0f)]
    public float edgesOnly = 0.0f;

    public Color edgeColor = Color.black;

    public Color backgroundColor = Color.white;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(Mat != null)
        {
            Mat.SetFloat("_EdgeOnly", edgesOnly);
            Mat.SetColor("_EdgeColor", edgeColor);
            Mat.SetColor("_BackgroundColor", backgroundColor);

            Graphics.Blit(source, destination, Mat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
