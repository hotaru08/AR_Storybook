#if !defined(LIGHTING)
#define LIGHTING

#include "AutoLight.cginc"
#include "UnityPBSLighting.cginc"

// Property variables
float4 _Tint;
sampler2D _MainTexture, _DetailTexture;
float4 _MainTexture_ST, _DetailTexture_ST;

sampler2D _NormalMap, _DetailNormalMap;
float _BumpScale, _DetailBumpScale;

float _Metallic;
float _Smoothness;

// Struct for containing Fragment program input values
struct Interpolators
{
	/*
	SV_POSITION:
	- SV stands for system value and POSITION for final vertex position

	TEXCOORD0:
	- Used for texture coordinates that are interpolated and NOT the actual vertex position
	*/

	float4 position : SV_POSITION;
	float4 uv : TEXCOORD0;
	float3 normal : TEXCOORD1;
	
	#if defined(BINORMAL_PER_FRAGMENT)
		float4 tangent : TEXCOORD2;
	#else
		float3 tangent : TEXCOORD2;
		float3 binormal : TEXCOORD3;
	#endif

	float3 worldPosition : TEXCOORD4;

	#if defined(VERTEXLIGHT_ON)
		float3 vertexLightColor : TEXCOORD5;
	#endif
};

// Struct for containing Vertex program input values
struct VertexData
{
	float4 position : POSITION;
	float2 uv : TEXCOORD0;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
};

// Creates light data based on vertex data.
UnityLight CreateLight(Interpolators i)
{
	UnityLight light;

	// Different types of light sources
	#if defined(POINT) || defined(SPOT) || defined(POINT_COOKIE)
		light.dir = normalize(_WorldSpaceLightPos0.xyz - i.worldPosition);
	#else
		light.dir = _WorldSpaceLightPos0.xyz;
	#endif

	UNITY_LIGHT_ATTENUATION(attenuation, 0, i.worldPosition);
	light.color = _LightColor0.rgb * attenuation;
	light.ndotl = DotClamped(i.normal, light.dir);

	return light;
}

// Creates indirect light data based on vertex data.
UnityIndirect CreateIndirectLight(Interpolators i)
{
	UnityIndirect indirectLight;
	indirectLight.diffuse = 0;
	indirectLight.specular = 0;

	#if defined(VERTEXLIGHT_ON)
		indirectLight.diffuse = i.vertexLightColor;
	#endif 

	#if defined(FORWARD_BASE_PASS)
		indirectLight.diffuse += max(0, ShadeSH9(float4(i.normal, 1)));
	#endif

	return indirectLight;
}

// Calculates the colors at each vertex that would be affected by the Vertex point light.
void ComputeVertexLightColor(inout Interpolators i)
{
	#if defined(VERTEXLIGHT_ON)
		i.vertexLightColor = Shade4PointLights	(
												unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
												unity_LightColor[0].rgb, unity_LightColor[1].rgb,
												unity_LightColor[2].rgb, unity_LightColor[3].rgb,
												unity_4LightAtten0, i.worldPos, i.normal
												);
	#endif
}

// Binormal calculations will be done here.
float3 CreateBinormal(float3 normal, float3 tangent, float binormalSign)
{
	return cross(normal, tangent.xyz) * (binormalSign * unity_WorldTransformParams.w);
}

// Converts vertex points in mesh's object space to display space.
Interpolators VertexProgram(VertexData v)
{
	Interpolators i;
	i.position = UnityObjectToClipPos(v.position);
	i.worldPosition = mul(unity_ObjectToWorld, v.position);
	i.normal = UnityObjectToWorldNormal(v.normal);
	
	#if defined(BINORMAL_PER_FRAGMENT)
		i.tangent = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);
	#else
		i.tangent = UnityObjectToWorldDir(v.tangent.xyz);
		i.binormal = CreateBinormal(i.normal, i.tangent, v.tangent.w);
	#endif

	i.uv.xy = TRANSFORM_TEX(v.uv, _MainTexture);
	i.uv.zw = TRANSFORM_TEX(v.uv, _DetailTexture);

	ComputeVertexLightColor(i);

	return i;
}

// Initialize normals of the vertex.
void InitializeFragmentNormal(inout Interpolators i)
{
	float3 mainNormal = UnpackScaleNormal(tex2D(_NormalMap, i.uv.xy), _BumpScale);
	float3 detailNormal = UnpackScaleNormal(tex2D(_DetailNormalMap, i.uv.zw), _DetailBumpScale);
	float3 tangentSpaceNormal = BlendNormals(mainNormal, detailNormal);
	
	#if defined(BINORMAL_PER_FRAGMENT)
		float3 binormal = CreateBinormal(i.normal, i.tangent.xyz, i.tangent.w);
	#else
		float3 binormal = i.binormal;
	#endif

	i.normal = normalize(tangentSpaceNormal.x * i.tangent + tangentSpaceNormal.y * binormal + tangentSpaceNormal.z * i.normal);
}

// Colors pixes within the mesh's triangles.
float4 FragmentProgram(Interpolators i) : SV_TARGET
{
	InitializeFragmentNormal(i);

	float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPosition);

	float3 albedo = tex2D(_MainTexture, i.uv.xy).rgb * _Tint.rgb;
	albedo *= tex2D(_DetailTexture, i.uv.zw) * unity_ColorSpaceDouble;

	float3 specularTint = albedo * _Metallic;
	float oneMinusReflectivity;
	albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

	return UNITY_BRDF_PBS(albedo, specularTint, oneMinusReflectivity, _Smoothness, i.normal, viewDirection, CreateLight(i), CreateIndirectLight(i));
}

#endif