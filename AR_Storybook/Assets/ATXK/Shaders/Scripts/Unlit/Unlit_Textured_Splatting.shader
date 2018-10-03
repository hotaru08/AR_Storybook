// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Unlit Shader/Texture Splatting"
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
		_MainTexture("Texture", 2D) = "white" {}
		[NoScaleOffset] _Texture1("Texture 1", 2D) = "white" {}
		[NoScaleOffset] _Texture2("Texture 2", 2D) = "white" {}
		[NoScaleOffset] _Texture3("Texture 3", 2D) = "white" {}
		[NoScaleOffset] _Texture4("Texture 4", 2D) = "white" {}
	}

		// SubShaders are used for different build platforms/ levels of detail.
		SubShader
	{
		// Multiple Passes are used to render an object multiple times,
		// which is used to create alot of visual effects.
		Pass
		{
			CGPROGRAM // Start of Shader program

			#pragma vertex VertexProgram		// Defining the Vertex program
			#pragma fragment FragmentProgram	// Defining the Fragment program

			#include "UnityCG.cginc"

			// Property variables
			sampler2D _MainTexture;
			float4 _MainTexture_ST;
			sampler2D _Texture1, _Texture2, _Texture3, _Texture4;

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
				float2 uv : TEXCOORD0;
				float2 uvSplat : TEXCOORD1;
			};

			// Struct for containing Vertex program input values
			struct VertexData
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			// Converts vertex points in mesh's object space to display space.
			Interpolators VertexProgram(VertexData v)
			{
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);
				i.uv = TRANSFORM_TEX(v.uv, _MainTexture);
				i.uvSplat = v.uv;

				return i;
			}

			/*
			SV_TARGET:
			- SV stands for system value and TARGET for shader target
			*/

			// Colors pixes within the mesh's triangles
			float4 FragmentProgram(	Interpolators i) : SV_TARGET 
			{
				float4 splat = tex2D(_MainTexture, i.uvSplat);

				return	tex2D(_Texture1, i.uv) * splat.r + 
						tex2D(_Texture2, i.uv) * splat.g + 
						tex2D(_Texture3, i.uv) * splat.b +
						tex2D(_Texture4, i.uv) * (1 - splat.r - splat.g - splat.b);
			}

			ENDCG // End of Shader program
		}
	}
}