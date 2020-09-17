using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmericaCartoonEffect : PostProcessBase
{
    public Shader AmToonShader;
    private Material AmToonMat = null;
    [Range(1, 100)]
    public int TileSize = 100;
    public float DotSize = 6;
    [Range(0,0.01f)]
    public float DotSmoothness = 0.002f;
    public Color BackColor;
    public Color DotColor;
    public Material Mat
    {
        get
        {
            AmToonMat = CheckShaderAndCreateMaterial(AmToonShader, AmToonMat);
            return AmToonMat;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Mat != null)
        {
            Mat.SetInt("_TileSize", TileSize);
            Mat.SetFloat("_DotSize", DotSize);
            Mat.SetFloat("_DotSmoothness", DotSmoothness);
            Mat.SetColor("_BackColor", BackColor);
            Mat.SetColor("_DotColor", DotColor);
            Graphics.Blit(source, destination, Mat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
