#if !defined(LIGHTING)
#define LIGHTING

#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"

// Property variables
float4 _Tint;
sampler2D _MainTexture, _DetailTexture, _DetailMask;
float4 _MainTexture_ST, _DetailTexture_ST;

sampler2D _NormalMap, _DetailNormalMap;
float _BumpScale, _DetailBumpScale;

sampler2D _MetalMap;
float _Metallic;

sampler2D _SmoothMap;
float _Smoothness;

sampler2D _EmissionMap;
float3 _Emission;

sampler2D _OcclusionMap;
float _Occlusion;

// Struct for containing Fragment program input values
struct Interpolators
{
	/*
	SV_POSITION:
	- SV stands for system value and POSITION for final vertex position

	TEXCOORD0:
	- Used for texture coordinates that are interpolated and NOT the actual vertex position
	*/

	float4 pos : SV_POSITION;
	float4 uv : TEXCOORD0;
	float3 normal : TEXCOORD1;
	
	#if defined(BINORMAL_PER_FRAGMENT)
		float4 tangent : TEXCOORD2;
	#else
		float3 tangent : TEXCOORD2;
		float3 binormal : TEXCOORD3;
	#endif

	float3 worldPosition : TEXCOORD4;

	SHADOW_COORDS(5)

	#if defined(VERTEXLIGHT_ON)
		float3 vertexLightColor : TEXCOORD6;
	#endif
};

// Struct for containing Vertex program input values
struct VertexData
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
};

// Creates reflection projection data for lighting.
float3 BoxProjection(float3 direction, float3 position, float4 cubemapPosition, float3 boxMin, float3 boxMax)
{
	#if UNITY_SPECCUBE_BOX_PROJECTION
	UNITY_BRANCH
	if (cubemapPosition.w > 0)
	{
		float3 factors = ((direction > 0 ? boxMax : boxMin) - position) / direction;
		float scalar = min(min(factors.x, factors.y), factors.z);
		direction = direction * scalar + (position - cubemapPosition);
	}
	#endif

	return direction;
}

// Calculates the colors at each vertex that would be affected by the Vertex point light.
void ComputeVertexLightColor(inout Interpolators i)
{
	#if defined(VERTEXLIGHT_ON)
		i.vertexLightColor = Shade4PointLights	(
												unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
												unity_LightColor[0].rgb, unity_LightColor[1].rgb,
												unity_LightColor[2].rgb, unity_LightColor[3].rgb,
												unity_4LightAtten0, i.worldPosition, i.normal
												);
	#endif
}

// Calculates the metallic value of a fragment based on the metallic map.
float ComputeMetallic(Interpolators i)
{
	return tex2D(_MetalMap, i.uv.xy).r * _Metallic;
}

// Calculates the smoothness value of a fragment based on the smoothness map.
float ComputeSmoothness(Interpolators i)
{
	return tex2D(_SmoothMap, i.uv.xy).r * _Smoothness;
}

// Calculates the emission value of a fragment based on the emission map.
float3 ComputeEmission(Interpolators i)
{
	#if defined(FORWARD_BASE_PASS)
		#if defined(_EMISSION_MAP)
			return tex2D(_EmissionMap, i.uv.xy) * _Emission;
		#else
			return _Emission;
		#endif
	#else
			return 0;
	#endif
}

// Calculates the occlusion value of a fragment based on the occlusion map.
float ComputeOcclusion(Interpolators i)
{
	#if defined(_OCCLUSION_MAP)
		return lerp(1, tex2D(_OcclusionMap, i.uv.xy).g, _Occlusion);
	#else
		return 1;
	#endif
}

// Calculates the masking value of a fragment based on the detail mask.
float ComputeDetailMask(Interpolators i)
{
	#if defined (_DETAIL_MASK)
		return tex2D(_DetailMask, i.uv.xy).a;
	#else
		return 1;
	#endif
}

// Calculates the albedo value of a fragment.
float3 ComputeAlbedo(Interpolators i)
{
	float3 albedo = tex2D(_MainTexture, i.uv.xy).rgb * _Tint.rgb;
	float3 details = tex2D(_DetailTexture, i.uv.zw) * unity_ColorSpaceDouble;
	albedo = lerp(albedo, albedo * details, ComputeDetailMask(i));
	return albedo;
}

// Calculates normals in tangent space for each fragment.
float3 ComputeTangentSpaceNormal(Interpolators i)
{
	float3 mainNormal = UnpackScaleNormal(tex2D(_NormalMap, i.uv.xy), _BumpScale);
	float3 detailNormal = UnpackScaleNormal(tex2D(_DetailNormalMap, i.uv.zw), _DetailBumpScale);
	detailNormal = lerp(float3(0, 0, 1), detailNormal, ComputeDetailMask(i));
	return BlendNormals(mainNormal, detailNormal);
}

// Binormal calculations will be done here.
float3 CreateBinormal(float3 normal, float3 tangent, float binormalSign)
{
	return cross(normal, tangent.xyz) * (binormalSign * unity_WorldTransformParams.w);
}

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

	// Attenuation if shadows are enabled
	UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPosition);

	light.color = _LightColor0.rgb * attenuation;
	light.ndotl = DotClamped(i.normal, light.dir);

	return light;
}

// Creates indirect light data based on vertex data.
UnityIndirect CreateIndirectLight(Interpolators i, float3 viewDir)
{
	UnityIndirect indirectLight;
	indirectLight.diffuse = 0;
	indirectLight.specular = 0;

#if defined(VERTEXLIGHT_ON)
	indirectLight.diffuse = i.vertexLightColor;
#endif 

#if defined(FORWARD_BASE_PASS)
	indirectLight.diffuse += max(0, ShadeSH9(float4(i.normal, 1)));

	float3 reflectionDir = reflect(-viewDir, i.normal);
	Unity_GlossyEnvironmentData envData;
	envData.roughness = 1 - _Smoothness;
	envData.reflUVW = BoxProjection(reflectionDir, i.worldPosition, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);

	float3 probe0 = Unity_GlossyEnvironment(UNITY_PASS_TEXCUBE(unity_SpecCube0), unity_SpecCube0_HDR, envData);
	envData.reflUVW = BoxProjection(reflectionDir, i.worldPosition, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);

#if UNITY_SPECCUBE_BLENDING
	float interpolator = unity_SpecCube0_BoxMin.w;
	UNITY_BRANCH
		if (interpolator < 0.99999)
		{
			float3 probe1 = Unity_GlossyEnvironment(UNITY_PASS_TEXCUBE_SAMPLER(unity_SpecCube1, unity_SpecCube0), unity_SpecCube0_HDR, envData);
			indirectLight.specular = lerp(probe1, probe0, unity_SpecCube0_BoxMin.w);
		}
		else
		{
			indirectLight.specular = probe0;
		}
#else
	indirectLight.specular = probe0;
#endif

	float occlusion = ComputeOcclusion(i);
	indirectLight.diffuse *= occlusion;
	indirectLight.specular *= occlusion;
#endif

	return indirectLight;
}

// Converts vertex points in mesh's object space to display space.
Interpolators VertexProgram(VertexData v)
{
	Interpolators i;
	i.pos = UnityObjectToClipPos(v.vertex);
	i.worldPosition = mul(unity_ObjectToWorld, v.vertex);
	i.normal = UnityObjectToWorldNormal(v.normal);
	
	#if defined(BINORMAL_PER_FRAGMENT)
		i.tangent = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);
	#else
		i.tangent = UnityObjectToWorldDir(v.tangent.xyz);
		i.binormal = CreateBinormal(i.normal, i.tangent, v.tangent.w);
	#endif

	i.uv.xy = TRANSFORM_TEX(v.uv, _MainTexture);
	i.uv.zw = TRANSFORM_TEX(v.uv, _DetailTexture);

	TRANSFER_SHADOW(i);

	ComputeVertexLightColor(i);

	return i;
}

// Initialize normals of the vertex.
void InitializeFragmentNormal(inout Interpolators i)
{
	float3 tangentSpaceNormal = ComputeTangentSpaceNormal(i);
	
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

	float3 specularTint;
	float oneMinusReflectivity;
	float3 albedo = DiffuseAndSpecularFromMetallic(ComputeAlbedo(i), ComputeMetallic(i), specularTint, oneMinusReflectivity);

	float4 color = UNITY_BRDF_PBS(albedo, specularTint, oneMinusReflectivity, ComputeSmoothness(i), i.normal, viewDirection, CreateLight(i), CreateIndirectLight(i, viewDirection));
	color.rgb += ComputeEmission(i);
	return color;	
}

#endif