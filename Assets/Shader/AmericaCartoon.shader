//美漫风格
Shader "Learn/PostProcess/AmericaCartoon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		
		[Header(Dots)]
		_TileSize("Tile大小", Range(1,100)) = 1000
		_DotSize("Dot Size", float) = 6
		_DotSmoothness("Smothness", Range(0, 0.01)) = 0.002

		[Header(Color)]
		_BackColor("BackColor", color) = (0.8,0.8,0.8,1)
		_DotColor("DotColor", color) = (0,0,0,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = 1 - v.uv;
                return o;
            }

            sampler2D _MainTex;
			int _TileSize,_DotSize,_DotSmoothness;
			fixed4 _BackColor, _DotColor;

            fixed4 frag (v2f i) : SV_Target
            {		

				 //Create Cells
				float cellWidth = _TileSize / _ScreenParams.x;
				float cellHeight = _TileSize / _ScreenParams.y;
				fixed2 roundedUV;
				roundedUV.x = round(i.uv.x / cellWidth) * cellWidth;
				roundedUV.y = round(i.uv.y / cellHeight) * cellHeight;

				//Calculate Distance From Cell Center
				float2 distanceVector = i.uv - roundedUV;
				distanceVector.x = (distanceVector.x / _ScreenParams.y) * _ScreenParams.x;
				float distanceFromCenter = length(distanceVector);

				//Calculate Dot Size
				float dotSize = _DotSize / _ScreenParams.x;
				fixed4 roundedCol = tex2D(_MainTex, i.uv); //use roundedUV instead of i.uv for different effect
				float luma = dot(roundedCol.rgb, float3(0.2126, 0.7152, 0.0722));
				dotSize *= (1 - luma);

				//Calculate Displayed Color
				float lerpAmount = smoothstep(dotSize, dotSize + _DotSmoothness, distanceFromCenter);
				fixed4 col = lerp(_BackColor, _DotColor, lerpAmount);
				return col;
            }
            ENDCG
        }
    }
}
