namespace ATXK.Shaders
{
	using UnityEngine;
	using UnityEditor;
	
	public class ShaderEditor : ShaderGUI
	{
		Material target;
		MaterialEditor editor;
		MaterialProperty[] properties;

		static GUIContent staticLabel = new GUIContent();
		static ColorPickerHDRConfig emissionConfig = new ColorPickerHDRConfig(0f, 99f, 1f / 99f, 3f);

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] materialProperties)
		{
			target = materialEditor.target as Material;
			editor = materialEditor;
			properties = materialProperties;

			MainMapsGUI();
			MetallicGUI();
			SmoothnessGUI();
			DetailMapsGUI();
			MetallicMapsGUI();
			SmoothnessMapGUI();
		}

		private void MainMapsGUI()
		{
			GUILayout.Label("Main Maps", EditorStyles.boldLabel);

			MaterialProperty mainTexture = FindProperty("_MainTexture");
			MaterialProperty tint = FindProperty("_Tint");

			editor.TexturePropertySingleLine(CreateLabel(mainTexture, "Albedo (RGB)"), mainTexture, tint);

			NormalMapsGUI("_NormalMap", "_BumpScale");
			EmissionMapGUI();
			OcclusionMapGUI();
			DetailMaskGUI();

			editor.TextureScaleOffsetProperty(mainTexture);
		}

		private void NormalMapsGUI(string mapProperty, string bumpProperty)
		{
			MaterialProperty map = FindProperty(mapProperty);
			MaterialProperty bump = FindProperty(bumpProperty);

			editor.TexturePropertySingleLine(CreateLabel(map, "Normals"), map, map.textureValue ? bump : null);
		}

		private void MetallicGUI()
		{
			MaterialProperty slider = FindProperty("_Metallic");
			editor.ShaderProperty(slider, CreateLabel(slider));
		}

		private void SmoothnessGUI()
		{
			MaterialProperty slider = FindProperty("_Smoothness");
			editor.ShaderProperty(slider, CreateLabel(slider));
		}

		private void DetailMapsGUI()
		{
			GUILayout.Label("Detail Maps", EditorStyles.boldLabel);

			MaterialProperty detailTex = FindProperty("_DetailTexture");

			editor.TexturePropertySingleLine(CreateLabel(detailTex, "Albedo (RGB) multiplied by 2"), detailTex);

			NormalMapsGUI("_DetailNormalMap", "_DetailBumpScale");

			editor.TextureScaleOffsetProperty(detailTex);
		}

		private void MetallicMapsGUI()
		{
			GUILayout.Label("Metallic Maps", EditorStyles.boldLabel);

			MaterialProperty metalMap = FindProperty("_MetalMap");
			MaterialProperty metallic = FindProperty("_Metallic");

			editor.TexturePropertySingleLine(CreateLabel(metalMap, "Metallic (R)"), metalMap, null);
		}

		private void SmoothnessMapGUI()
		{
			GUILayout.Label("Smoothness Maps", EditorStyles.boldLabel);

			MaterialProperty smoothMap = FindProperty("_SmoothMap");
			MaterialProperty smoothness = FindProperty("_Smoothness");

			editor.TexturePropertySingleLine(CreateLabel(smoothMap, "Smoothness (R)"), smoothMap, null);
		}

		private void EmissionMapGUI()
		{
			MaterialProperty emissionMap = FindProperty("_EmissionMap");
			MaterialProperty emission = FindProperty("_Emission");

			EditorGUI.BeginChangeCheck();

			editor.TexturePropertyWithHDRColor(CreateLabel(emissionMap, "Emission (RGB)"), emissionMap, emission, emissionConfig, false);

			if (EditorGUI.EndChangeCheck())
			{
				SetKeyword("_EMISSION_MAP", emissionMap.textureValue);
			}
		}

		private void OcclusionMapGUI()
		{
			MaterialProperty occlusionMap = FindProperty("_OcclusionMap");
			MaterialProperty occlusion = FindProperty("_Occlusion");

			EditorGUI.BeginChangeCheck();

			editor.TexturePropertySingleLine(CreateLabel(occlusionMap, "Occlusion (G)"), occlusionMap, occlusionMap.textureValue ? occlusion : null);

			if (EditorGUI.EndChangeCheck())
			{
				SetKeyword("_OCCLUSION_MAP", occlusionMap.textureValue);
			}
		}

		private void DetailMaskGUI()
		{
			MaterialProperty detailMask = FindProperty("_DetailMask");

			EditorGUI.BeginChangeCheck();

			editor.TexturePropertySingleLine(CreateLabel(detailMask, "Detail Mask (A)"), detailMask);

			if (EditorGUI.EndChangeCheck())
			{
				SetKeyword("_DETAIL_MASK", detailMask.textureValue);
			}
		}

		private void SetKeyword(string keyword, bool state)
		{
			if (state)
			{
				foreach(Material m in editor.targets)
					m.EnableKeyword(keyword);
			}
			else
			{
				foreach (Material m in editor.targets)
					m.DisableKeyword(keyword);
			}
		}

		private MaterialProperty FindProperty(string name)
		{
			return FindProperty(name, properties);
		}

		private GUIContent CreateLabel(string text, string tooltip = null)
		{
			staticLabel.text = text;
			staticLabel.tooltip = tooltip;

			return staticLabel;
		}

		private GUIContent CreateLabel(MaterialProperty property, string tooltip = null)
		{
			staticLabel.text = property.displayName;
			staticLabel.tooltip = tooltip;

			return staticLabel;
		}
	}
}