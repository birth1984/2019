Shader "CC2/Character_Metal_NonFlow" {
Properties{
	 _Diff_Map("Diff_Map", 2D) = "white" {}
	 _Light_size("Light_size", Range(0, 1)) = 1
	 _Specular_Map("Specular_Map", 2D) = "white" {}
	 _Specular_size("Specular_size", Range(0, 20)) = 0.70
	 _Gloss_size("Gloss_size", Range(0, 1)) = 0.50
	 _Rim_Color("Rim_Color", Color) = (1,1,1,1)
	 _Rim_size("Rim_size", Range(0, 8)) = 1.7
	 _Sharpening_size("Sharpening_size", Range(0, 30)) = 16.0
	 _Emission_Map("Emission_Map", 2D) = "white" {}
	 _Emission_size("Emission_size", Range(0, 2)) = 0.30
	 _Normal_Map("Normal_Map", 2D) = "bump" {}
	 _ShadowColor("Shadow Color",Color) = (1,1,0,1)
	//Material Capture纹理  
    _MatCap("MatCap", 2D) = "white" {}  
    //反射颜色  
    _ReflectionColor("Reflection Color", Color) = (0.2, 0.2, 0.2, 1.0)  
    //反射立方体贴图  
    _ReflectionMap("Reflection Cube Map", Cube) = "" {}  
    //反射强度  
    _ReflectionStrength("Reflection Strength", Range(0.0, 1.0)) = 0.5  
	}
	SubShader{
	Tags{"IgnoreProjector" = "True""Queue" = "Geometry+200""RenderType" = "Opaque"}
		LOD 200
	Pass
	{
		Name "FORWARD" Tags{"LightMode" = "ForwardBase"}
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#define UNITY_PASS_FORWARDBASE
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "autolight.cginc"
#pragma multi_compile_fwdbase
	uniform sampler2D _Normal_Map; 
	uniform sampler2D _Diff_Map; 
	uniform float4 _Diff_Map_ST;
	uniform sampler2D _Specular_Map; 
	uniform float4 _Rim_Color;
	uniform fixed _Rim_size;
	uniform sampler2D _Emission_Map; 
	uniform fixed _Emission_size;
	uniform fixed _Light_size;
	
	uniform fixed _Sharpening_size;
	uniform fixed _Gloss_size;
	uniform fixed _Specular_size;
	uniform fixed4 _ShadowColor;

	uniform sampler2D _MatCap;
    uniform fixed4 _ReflectionColor;  
    uniform samplerCUBE _ReflectionMap;  
    uniform fixed _ReflectionStrength;  
	struct VertexInput {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float4 tangent : TANGENT;
		float2 texcoord0 : TEXCOORD0;
	};
	struct VertexOutput {
		float4 pos : SV_POSITION;
		float4 uv0 : TEXCOORD0;
		float4 posWorld : TEXCOORD1;
		float3 normalDir : TEXCOORD2;
		float3 tangentDir : TEXCOORD3;
		float3 bitangentDir : TEXCOORD4;
		float3 refDir : TEXCOORD5;
		LIGHTING_COORDS(6,1)
	};
	VertexOutput vert(VertexInput v) {
		VertexOutput o = (VertexOutput)0;		
		//漫反射UV坐标准备：存储于TEXCOORD0的前两个坐标xy。  
        o.uv0.xy = TRANSFORM_TEX(v.texcoord0, _Diff_Map);
		//MatCap坐标准备：将法线从模型空间转换到观察空间，存储于TEXCOORD0的后两个纹理坐标zw  
        o.uv0.z = dot(normalize(UNITY_MATRIX_IT_MV[0].xyz), normalize(v.normal));  
        o.uv0.w = dot(normalize(UNITY_MATRIX_IT_MV[1].xyz), normalize(v.normal));  
        //归一化的法线值区间[-1,1]转换到适用于纹理的区间[0,1]  
        o.uv0.zw = o.uv0.zw * 0.5 + 0.5;  
		//世界空间法线 
		o.normalDir = UnityObjectToWorldNormal(v.normal);
		o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
		o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
		//世界空间位置  
		o.posWorld = mul(unity_ObjectToWorld, v.vertex);	
		//世界空间反射向量
		//o.refDir = reflect(o.posWorld-_WorldSpaceCameraPos.xyz,o.normalDir);
		o.refDir = reflect(-WorldSpaceViewDir(v.vertex),o.normalDir);
		//坐标变换  
		o.pos = UnityObjectToClipPos(v.vertex);
		TRANSFER_VERTEX_TO_FRAGMENT(o);
		TRANSFER_SHADOW(o)
		return o;
	}
	fixed4 frag(VertexOutput i) : COLOR{
		i.normalDir = normalize(i.normalDir);
	float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
	fixed3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
	fixed3 _Normal_Map_var = UnpackNormal(tex2D(_Normal_Map,i.uv0));
	fixed3 normalLocal = _Normal_Map_var.rgb;
	fixed3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals
	fixed3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);	
	fixed3 halfDirection = normalize(viewDirection + lightDirection);
	////// Lighting:
	fixed attenuation = LIGHT_ATTENUATION(i);
	fixed3 attenColor = attenuation * _LightColor0.xyz;
	///////// Gloss:
	fixed gloss = _Gloss_size;
	fixed specPow = exp2(gloss * 10.0 + 1.0);	
	////// Diffuse:
	fixed NdotL = max(0, dot(normalDirection, lightDirection));
	fixed4 _Specular_Map_var = tex2D(_Specular_Map,i.uv0);
	fixed4 matcap = tex2D(_MatCap, i.refDir);
	fixed3 directDiffuse = max(0.0, NdotL) * attenColor;
	//fixed3 indirectDiffuse = float3(0,0,0);
	fixed4 _Diff_Map_var = tex2D(_Diff_Map,i.uv0);	
	fixed3 specularColor = _Specular_Map_var.rgb*_Specular_size;
	//镜面反射颜色  
    fixed3 reflectionColor = texCUBE(_ReflectionMap, i.refDir).rgb * _ReflectionColor.rgb; 
	//从提供的MatCap纹理中，提取出对应光照信息  
    fixed3 matCapColor = tex2D(_MatCap, i.uv0.zw).rgb;	
	fixed3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
	fixed3 specular = directSpecular;
	fixed3 diffuseColor = _Diff_Map_var.rgb*_Light_size;
	//indirectDiffuse += diffuseColor; // Diffuse Ambient Light
	fixed3 diffuse =  diffuseColor;
	fixed3 mainColor = lerp(diffuse, reflectionColor*_Specular_Map_var.a, _ReflectionStrength);  
	////// Emissive:
	fixed4 _Emission_Map_var = tex2D(_Emission_Map,i.uv0);
	fixed _normalDirection = i.normalDir.r;
	fixed3 _rim = (_Diff_Map_var.rgb + ((_Rim_Color.rgb*(pow(1.0 - max(0,dot(i.normalDir, viewDirection)),_Sharpening_size)*(_normalDirection + ((1.0 - _normalDirection)*0.48))))*_Rim_size));
	fixed3 emissive = (_Emission_size*_Emission_Map_var.rgb) + _rim;

		fixed3 finalColor = mainColor*matCapColor + emissive+specular;

	fixed shadow_attenuation = SHADOW_ATTENUATION(i);
	finalColor = lerp((finalColor*_ShadowColor),finalColor, shadow_attenuation);
	return fixed4(finalColor,1);
	}
		ENDCG
	}
		}
		 FallBack "Mobile/Diffuse"
}
