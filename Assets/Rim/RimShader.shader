Shader "Custom/RimShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_RimIntensity ("Rim intensity", Range(-1,1)) = 0
		[NoScaleOffset] _MainTex("Main Texture", 2D) = "white" {}
	}
	SubShader {
		Tags{ 
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}
		LOD 200
		Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:fade nolighting 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
		};
		fixed4 _Color;
		sampler2D _MainTex;
		fixed _RimIntensity;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//fixed4 c = _Color;

			fixed border = 1.0 - abs(dot(IN.viewDir, IN.worldNormal));

			// Cuando border tiene valores < 0.5 alpha toma valores negativos
			// https://www.desmos.com/calculator/2fui1exlmk
			// Interpolacion lineal entre border y 1 (cuando _RimIntensity esta entre 0 y 1-)
			fixed alpha = (border*(1 - _RimIntensity)) + _RimIntensity;
			// 1*(1 - -1) + -1
			// 1*(1 + 1) - 1
			// 1*(2) - 1
			// 2 - 1
			// 1


			o.Albedo = c.rgb;
			o.Alpha = c.a * alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
