Shader "Custom/Ground" 
{
	Properties 
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_LightMap ("Lightmap (RGB)", 2D) = "black" {}
	    _Light("Light",Range(0,10)) = 1  
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Stencil
		{
			Ref 1
			Comp NotEqual
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
uniform sampler2D _MainTex,_Specular_mask;			
			uniform fixed4 _Color;
            uniform fixed _Light;//_Glossness,_Specular_power

		struct Input {
			float2 uv_MainTex;
		};

		uniform half _Glossiness;
		uniform half _Metallic;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color*_Light;
			o.Albedo =  c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
