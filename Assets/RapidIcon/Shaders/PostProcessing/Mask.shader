Shader "RapidIcon/PostProcessing/Mask" {
	Properties{
		[NoScaleOffset]
		[MainTexture]
		_MainTex("Tex", 2D) = "black" {}

		[NoScaleOffset]
		_maskTex("Mask", 2D) = "white" {}
	}
		SubShader
		{
			Tags {  "Queue" = "Transparent" "RenderType" = "Transparent" }
			Blend One OneMinusSrcAlpha
			Pass
			{
				CGPROGRAM
				#pragma vertex vert_img
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

				sampler2D _MainTex;
				sampler2D _maskTex;

				fixed4 frag(v2f_img i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);

					fixed3 maskCol = tex2D(_maskTex, i.uv).rgb;
					fixed maskLum = Luminance(maskCol).r;
					col *= maskLum;
					
					return col;

				}
				ENDCG
			}
		}
			FallBack "Diffuse"
}
