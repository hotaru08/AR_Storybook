// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Lit Shader/Textured with Bumpiness"
{
	/*
	How Shaders/Rendering works:
	- The CPU handles the Mesh, the mesh's Transform and the mesh's Material.
	- The GPU handles the Shader.

	Shaders require 2 programs - a Vertex program and a Fragment program.
	- The Vertex program is responsible for converting vertex points in a mesh from object space to display space.
	- The Fragment program is responsible for coloring the individual pixels that lie within the mesh's triangles.

	*/

	// Properties allow getting variables from the Unity material / external scripts
	Properties
	{
		_Tint("Tint", Color) = (1, 1, 1, 1)
		_MainTexture("Albedo", 2D) = "white" {}
		[NoScaleOffset] _NormalMap("Normal Map", 2D) = "bump" {}
		_BumpScale("Bump Scale", Float) = 1
		[Gamma] _Metallic("Metallic", Range(0, 1)) = 0
		_Smoothness("Smoothness", Range(0, 1)) = 0.5
		_DetailTexture("Detail", 2D) = "gray" {}
		[NoScaleOffset] _DetailNormalMap("Detail Normal Map", 2D) = "bump" {}
		_DetailBumpScale("Detail Bump Scale", Float) = 1
	}

	CGINCLUDE
		#define BINORMAL_PER_FRAGMENT
	ENDCG

	// SubShaders are used for different build platforms/ levels of detail.
	SubShader
	{
		// Multiple Passes are used to render an object multiple times,
		// which is used to create alot of visual effects.
		Pass
		{
			// Tags serve as a <key, value> pair for render settings.
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM // Start of Shader program

			#pragma target 3.0
			
			#pragma multi_compile _ VERTEXLIGHT_ON
			#pragma multi_compile _ SHADOWS_SCREEN

			#pragma vertex VertexProgram		// Defining the Vertex program
			#pragma fragment FragmentProgram	// Defining the Fragment program

			#define FORWARD_BASE_PASS // Refer to this as the Base pass

			#include "Lighting.cginc" // Personal lighting shader

			ENDCG // End of Shader program
		}

		Pass
		{
			// Tags serve as a <key, value> pair for render settings.
			Tags
			{
				"LightMode" = "ForwardAdd"
			}

			Blend One One	// Additive Blending
			ZWrite Off		// Write to the depth buffer once

			CGPROGRAM // Start of Shader program

			#pragma target 3.0
			#pragma vertex VertexProgram					// Defining the Vertex program
			#pragma fragment FragmentProgram				// Defining the Fragment program
			#pragma multi_compile_fwdadd_fullshadows

			#include "Lighting.cginc" // Personal lighting shader

			ENDCG // End of Shader program
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

			CGPROGRAM

			#pragma target 3.0

			#pragma multi_compile_shadowcaster

			#pragma vertex ShadowVertexProgram
			#pragma fragment ShadowFragmentProgram

			#include "Shadow.cginc"

			ENDCG
		}
	}
}