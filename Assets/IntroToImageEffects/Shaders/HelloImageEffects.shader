Shader "ImageEffects/HelloImageEffects"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplacementTex("Displacement texture", 2D) = "white" {}
		_Intensity("Intensity", Range(0, 1)) = 0
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
				o.uv = v.uv;
				return o;
			}
			// About data types https://docs.unity3d.com/560/Documentation/Manual/SL-DataTypesAndPrecision.html
			sampler2D _MainTex;
			sampler2D _DisplacementTex;
			half _Intensity;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 disp = (tex2D(_DisplacementTex, i.uv + _Time.x) * 2. - 1.) * _Intensity;

				fixed4 col = tex2D(_MainTex, i.uv + disp);
				// just invert the colors
				col = col;
				return col;
			}
			ENDCG
		}
	}
}
