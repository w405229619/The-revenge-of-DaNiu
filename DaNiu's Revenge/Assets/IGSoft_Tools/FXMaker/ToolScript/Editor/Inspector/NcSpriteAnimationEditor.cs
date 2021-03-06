// ----------------------------------------------------------------------------------
//
// FXMaker
// Created by ismoon - 2012 - ismoonto@gmail.com
//
// ----------------------------------------------------------------------------------

// --------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

[CustomEditor(typeof(NcSpriteAnimation))]

public class NcSpriteAnimationEditor : FXMakerEditor
{
	// Attribute ------------------------------------------------------------------------
	protected	NcSpriteAnimation		m_Sel;
	protected	FxmPopupManager			m_FxmPopupManager;

	// Property -------------------------------------------------------------------------
	// Event Function -------------------------------------------------------------------
    void OnEnable()
    {
 		m_Sel = target as NcSpriteAnimation;
 		m_UndoManager	= new FXMakerUndoManager(m_Sel, "NcSpriteAnimation");
   }

    void OnDisable()
    {
		if (m_FxmPopupManager != null && m_FxmPopupManager.IsShowByInspector())
			m_FxmPopupManager.CloseNcPrefabPopup();
    }

	public override void OnInspectorGUI()
	{
		Rect rect;
		AddScriptNameField(m_Sel);
		m_UndoManager.CheckUndo();
		m_FxmPopupManager = GetFxmPopupManager();

//		test code
// 		if (GUILayout.Button("Pause"))
// 			FxmInfoIndexing.FindInstanceIndexing(m_Sel.transform, false).GetComponent<NcSpriteAnimation>().PauseAnimation();
// 		if (GUILayout.Button("Resume"))
// 			FxmInfoIndexing.FindInstanceIndexing(m_Sel.transform, false).GetComponent<NcSpriteAnimation>().ResumeAnimation();

		// --------------------------------------------------------------
		bool bClickButton = false;
		EditorGUI.BeginChangeCheck();
		{
//			DrawDefaultInspector();
			m_Sel.m_fUserTag = EditorGUILayout.FloatField(GetCommonContent("m_fUserTag"), m_Sel.m_fUserTag);

			m_Sel.m_TextureType		= (NcSpriteAnimation.TEXTURE_TYPE)EditorGUILayout.EnumPopup(GetHelpContent("m_TextureType"), m_Sel.m_TextureType);

			EditorGUILayout.Space();

			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.TileTexture)
			{
				if (GUILayout.Button(GetHelpContent("ConvertTo : TrimTexture")))
				{
					m_Sel.m_NcSpriteFrameInfos = NgAtlas.TileToTrimTexture(m_Sel.GetComponent<Renderer>().sharedMaterial, m_Sel.m_nTilingX, m_Sel.m_nTilingY, 0, m_Sel.m_nFrameCount, 4096);
					if (m_Sel.m_NcSpriteFrameInfos != null)
						m_Sel.m_TextureType = NcSpriteAnimation.TEXTURE_TYPE.TrimTexture;
				}
				if (GUILayout.Button(GetHelpContent("ExportTo : SplitTexture")))
				{
					string path = FXMakerCapture.GetExportSlitDir();
					path = NgAtlas.ExportSplitTexture(path, m_Sel.GetComponent<Renderer>().sharedMaterial.mainTexture, m_Sel.m_nTilingX, m_Sel.m_nTilingY, 0, m_Sel.m_nFrameCount);
					if (path != "")
					{
						Debug.Log(path);
						EditorUtility.OpenWithDefaultApp(path);
					}
				}
			} else
			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.TrimTexture)
			{
				if (GUILayout.Button(GetHelpContent("ExportTo : SplitTexture")))
				{
					string path = FXMakerCapture.GetExportSlitDir();
					path = NgAtlas.ExportSplitTexture(path, m_Sel.GetComponent<Renderer>().sharedMaterial.mainTexture, m_Sel.m_NcSpriteFrameInfos);
					if (path != "")
					{
						Debug.Log(path);
						EditorUtility.OpenWithDefaultApp(path);
					}
				}
			} else
			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.SpriteFactory)
			{
			}
			EditorGUILayout.Space();

			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.TileTexture)
			{
				m_Sel.m_nTilingX	= EditorGUILayout.IntField	(GetHelpContent("m_nTilingX")		, m_Sel.m_nTilingX);
				m_Sel.m_nTilingY	= EditorGUILayout.IntField	(GetHelpContent("m_nTilingY")		, m_Sel.m_nTilingY);
			} else
			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.TrimTexture)
			{
				m_Sel.m_MeshType	= (NcSpriteFactory.MESH_TYPE)EditorGUILayout.EnumPopup(GetHelpContent("m_MeshType"), m_Sel.m_MeshType);
				m_Sel.m_AlignType	= (NcSpriteFactory.ALIGN_TYPE)EditorGUILayout.EnumPopup(GetHelpContent("m_AlignType"), m_Sel.m_AlignType);
			} else
			if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.SpriteFactory)
			{
				m_Sel.m_NcSpriteFactoryPrefab	= (GameObject)EditorGUILayout.ObjectField(GetHelpContent("m_NcSpriteFactoryPrefab"), m_Sel.m_NcSpriteFactoryPrefab, typeof(GameObject), false, null);
				// --------------------------------------------------------------
				EditorGUILayout.Space();
				rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(m_fButtonHeight));
				{
					if (FXMakerLayout.GUIButton(FXMakerLayout.GetInnerHorizontalRect(rect, 2, 0, 1), GetHelpContent("Select SpriteFactory"), (m_FxmPopupManager != null)))
						m_FxmPopupManager.ShowSelectPrefabPopup(m_Sel, true, 0);
					if (FXMakerLayout.GUIButton(FXMakerLayout.GetInnerHorizontalRect(rect, 2, 1, 1), GetHelpContent("Clear SpriteFactory"), (m_Sel.m_NcSpriteFactoryPrefab != null)))
					{
						bClickButton = true;
						m_Sel.m_NcSpriteFactoryPrefab = null;
					}
					GUILayout.Label("");
				}
				EditorGUILayout.EndHorizontal();

				// --------------------------------------------------------------
				if (m_Sel.m_NcSpriteFactoryPrefab != null && m_Sel.m_NcSpriteFactoryPrefab.GetComponent<Renderer>() != null && m_Sel.GetComponent<Renderer>())
					if (m_Sel.m_NcSpriteFactoryPrefab.GetComponent<Renderer>().sharedMaterial != m_Sel.GetComponent<Renderer>().sharedMaterial)
						m_Sel.UpdateFactoryMaterial();

				// --------------------------------------------------------------
				NcSpriteFactory ncSpriteFactory	= (m_Sel.m_NcSpriteFactoryPrefab == null ? null : m_Sel.m_NcSpriteFactoryPrefab.GetComponent<NcSpriteFactory>());
				if (ncSpriteFactory != null)
				{
					int nSelIndex	= EditorGUILayout.IntSlider(GetHelpContent("m_nSpriteFactoryIndex")	, m_Sel.m_nSpriteFactoryIndex, 0, ncSpriteFactory.GetSpriteNodeCount()-1);
					if (m_Sel.m_nSpriteFactoryIndex != nSelIndex)
						m_Sel.SetSpriteFactoryIndex(nSelIndex, false);
				}

				m_Sel.m_MeshType	= (NcSpriteFactory.MESH_TYPE)EditorGUILayout.EnumPopup(GetHelpContent("m_MeshType"), m_Sel.m_MeshType);
				m_Sel.m_AlignType	= (NcSpriteFactory.ALIGN_TYPE)EditorGUILayout.EnumPopup(GetHelpContent("m_AlignType"), m_Sel.m_AlignType);
			}

			m_Sel.m_PlayMode				= (NcSpriteAnimation.PLAYMODE)EditorGUILayout.EnumPopup (GetHelpContent("m_PlayMode")		, m_Sel.m_PlayMode, GUILayout.MaxWidth(Screen.width));
			if (m_Sel.m_PlayMode != NcSpriteAnimation.PLAYMODE.SELECT)
				m_Sel.m_fDelayTime			= EditorGUILayout.FloatField(GetHelpContent("m_fDelayTime")		, m_Sel.m_fDelayTime);

			if (m_Sel.m_PlayMode != NcSpriteAnimation.PLAYMODE.RANDOM && m_Sel.m_PlayMode != NcSpriteAnimation.PLAYMODE.SELECT)
			{
				m_Sel.m_bLoop				= EditorGUILayout.Toggle	(GetHelpContent("m_bLoop")			, m_Sel.m_bLoop);
				if (m_Sel.m_bLoop == false)
					m_Sel.m_bAutoDestruct	= EditorGUILayout.Toggle	(GetHelpContent("m_bAutoDestruct")	, m_Sel.m_bAutoDestruct);
				m_Sel.m_fFps				= EditorGUILayout.FloatField(GetHelpContent("m_fFps")			, m_Sel.m_fFps);
			}

			m_Sel.m_nStartFrame	= EditorGUILayout.IntField	(GetHelpContent("m_nStartFrame")	, m_Sel.m_nStartFrame);
			m_Sel.m_nFrameCount	= EditorGUILayout.IntField	(GetHelpContent("m_nFrameCount")	, m_Sel.m_nFrameCount);

			if (m_Sel.m_PlayMode == NcSpriteAnimation.PLAYMODE.SELECT)
				m_Sel.m_nSelectFrame		= EditorGUILayout.IntField	(GetHelpContent("m_nSelectFrame")	, m_Sel.m_nSelectFrame);

			// check
			SetMinValue(ref m_Sel.m_nTilingX, 1);
			SetMinValue(ref m_Sel.m_nTilingY, 1);
			SetMinValue(ref m_Sel.m_fFps, 0.1f);
			SetMinValue(ref m_Sel.m_fDelayTime, 0);
			SetMinValue(ref m_Sel.m_nStartFrame, 0);
			SetMaxValue(ref m_Sel.m_nStartFrame, m_Sel.GetMaxFrameCount()-1);
			SetMinValue(ref m_Sel.m_nFrameCount, 1);
			SetMaxValue(ref m_Sel.m_nFrameCount, m_Sel.GetValidFrameCount());
			SetMinValue(ref m_Sel.m_nSelectFrame, 0);
			SetMaxValue(ref m_Sel.m_nSelectFrame, (0 < m_Sel.m_nFrameCount ? m_Sel.m_nFrameCount-1 : m_Sel.m_nTilingX*m_Sel.m_nTilingY-1));

			if (m_Sel.m_PlayMode != NcSpriteAnimation.PLAYMODE.RANDOM && m_Sel.m_PlayMode != NcSpriteAnimation.PLAYMODE.SELECT)
				EditorGUILayout.TextField(GetHelpContent("DurationTime"), m_Sel.GetDurationTime().ToString());


			// Texture --------------------------------------------------------------
			rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(150));
			{
				GUI.Box(rect, "");
				GUILayout.Label("");

				Rect subRect = rect;

				// draw texture
				if (m_Sel.GetComponent<Renderer>() != null && m_Sel.GetComponent<Renderer>().sharedMaterial != null && m_Sel.GetComponent<Renderer>().sharedMaterial.mainTexture != null)
				{
					int nClickFrameIndex;
					if (m_Sel.m_TextureType == NcSpriteAnimation.TEXTURE_TYPE.TileTexture)
					{
						if (DrawTileTexture(subRect, (m_Sel.m_PlayMode == NcSpriteAnimation.PLAYMODE.SELECT), m_Sel.GetComponent<Renderer>().sharedMaterial, m_Sel.m_nTilingX, m_Sel.m_nTilingY, m_Sel.m_nStartFrame, m_Sel.m_nFrameCount, m_Sel.m_nSelectFrame, out nClickFrameIndex))
						{
							bClickButton	= true;
							if (bClickButton && m_Sel.m_PlayMode == NcSpriteAnimation.PLAYMODE.SELECT)
								m_Sel.m_nSelectFrame = nClickFrameIndex;
						}
					}

					if (m_Sel.m_TextureType != NcSpriteAnimation.TEXTURE_TYPE.TileTexture)
					{
						if (DrawTrimTexture(subRect, (m_Sel.m_PlayMode == NcSpriteAnimation.PLAYMODE.SELECT), m_Sel.GetComponent<Renderer>().sharedMaterial, m_Sel.m_NcSpriteFrameInfos, m_Sel.m_nStartFrame, m_Sel.m_nFrameCount, m_Sel.m_nSelectFrame, out nClickFrameIndex))
						{
							bClickButton	= true;
							if (bClickButton && m_Sel.m_PlayMode == NcSpriteAnimation.PLAYMODE.SELECT)
								m_Sel.m_nSelectFrame = nClickFrameIndex;
						}
					}
				}
			}
			EditorGUILayout.EndHorizontal();
			m_UndoManager.CheckDirty();

			EditorGUILayout.Space();
			// Remove AlphaChannel
			if (GUILayout.Button(GetHelpContent("Remove AlphaChannel")))
				NgAtlas.ConvertAlphaTexture(m_Sel.GetComponent<Renderer>().sharedMaterial, false, m_Sel.m_curveAlphaWeight, 1, 1, 1);
			// AlphaWeight
			if ((m_Sel.m_curveAlphaWeight == null || m_Sel.m_curveAlphaWeight.length <= 0) && FXMakerOption.inst != null)
				m_Sel.m_curveAlphaWeight = FXMakerOption.inst.m_AlphaWeightCurve;
			if (m_Sel.m_curveAlphaWeight != null)
			{
				bool bHighLight = m_Sel.m_bNeedRebuildAlphaChannel;
				if (bHighLight)
	 				FXMakerLayout.GUIColorBackup(FXMakerLayout.m_ColorHelpBox);
				if (GUILayout.Button(GetHelpContent("Adjust the alpha channel with AlphaWeight")))
				{
					m_Sel.m_bNeedRebuildAlphaChannel = false;
					NgAtlas.ConvertAlphaTexture(m_Sel.GetComponent<Renderer>().sharedMaterial, true, m_Sel.m_curveAlphaWeight, 1, 1, 1);
//					NgAtlas.ConvertAlphaTexture(m_Sel.renderer.sharedMaterial, m_Sel.m_curveAlphaWeight, m_Sel.m_fRedAlphaWeight, m_Sel.m_fGreenAlphaWeight, m_Sel.m_fBlueAlphaWeight);
				}
				if (bHighLight)
					FXMakerLayout.GUIColorRestore();

				EditorGUI.BeginChangeCheck();
				m_Sel.m_curveAlphaWeight	= EditorGUILayout.CurveField(GetHelpContent("m_curveAlphaWeight"), m_Sel.m_curveAlphaWeight);
				if (EditorGUI.EndChangeCheck())
					m_Sel.m_bNeedRebuildAlphaChannel = true;
// 				m_Sel.m_fRedAlphaWeight		= EditorGUILayout.Slider("", m_Sel.m_fRedAlphaWeight	, 0, 1.0f);
// 				m_Sel.m_fGreenAlphaWeight	= EditorGUILayout.Slider("", m_Sel.m_fGreenAlphaWeight	, 0, 1.0f);
//	 			m_Sel.m_fBlueAlphaWeight	= EditorGUILayout.Slider("", m_Sel.m_fBlueAlphaWeight	, 0, 1.0f);
			}

			EditorGUILayout.Space();

		}
		// --------------------------------------------------------------
		if ((EditorGUI.EndChangeCheck() || bClickButton) && GetFXMakerMain())
			GetFXMakerMain().CreateCurrentInstanceEffect(true);
		// ---------------------------------------------------------------------
		if (GUI.tooltip != "")
			m_LastTooltip	= GUI.tooltip;
		HelpBox(m_LastTooltip);
	}

	// ----------------------------------------------------------------------------------
	protected GUIContent GetHelpContent(string tooltip)
	{
		string caption	= tooltip;
		string text		= FXMakerTooltip.GetHsEditor_NcSpriteAnimation(tooltip);
		return GetHelpContent(caption, text);
	}

	protected override void HelpBox(string caption)
	{
		string	str	= caption;
		if (caption == "" || caption == "Script")
			str = FXMakerTooltip.GetHsEditor_NcSpriteAnimation("");
		base.HelpBox(str);
	}
}
