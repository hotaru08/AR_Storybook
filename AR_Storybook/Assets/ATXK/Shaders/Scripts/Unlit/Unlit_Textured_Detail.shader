// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Unlit Shader/Textured with Detail"
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
		_MainTexture("Texture", 2D) = "white" {}
		_DetailTexture("Detail Texture", 2D) = "gray" {}
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
			float4 _Tint;
			sampler2D _MainTexture, _DetailTexture;
			float4 _MainTexture_ST, _DetailTexture_ST;

			// Struct for containing Fragment program input values
			struct Interpolators
			{
				/*
				SV_POSITION:
				- SV stands for system value and POSITION for final vertex position

				TEXCOORD0/TEXCOORD1/etc:
				- Used for texture coordinates that are interpolated and NOT the actual vertex position
				*/

				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uvDetail : TEXCOORD1;
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
				i.uvDetail = TRANSFORM_TEX(v.uv, _DetailTexture);

				return i;
			}

			/*
			SV_TARGET:
			- SV stands for system value and TARGET for shader target
			*/

			// Colors pixes within the mesh's triangles
			float4 FragmentProgram(	Interpolators i) : SV_TARGET 
			{
				float4 color = tex2D(_MainTexture, i.uv) * _Tint;
				color *= tex2D(_DetailTexture, i.uvDetail) * unity_ColorSpaceDouble;

				return color;
			}

			ENDCG // End of Shader program
		}
	}
}