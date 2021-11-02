//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2019 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class BCG_EnterExitInitLoad : MonoBehaviour {

	[InitializeOnLoad]
	public class InitOnLoad {

		static InitOnLoad(){
			
			SetEnabled("BCG_ENTEREXIT", true);

			if(!EditorPrefs.HasKey("BCG" + "0.02b" + "Installed")){
				
				EditorPrefs.SetInt("BCG" + "0.02b" + "Installed", 1);
				Selection.activeObject = RCC_Settings.Instance;
				EditorUtility.DisplayDialog("Standard Assets Needed For FPS/TPS Demo Scenes", "FPS/TPS Enter exit scenes are using Standard Assets Character Controllers. You have to import Standard Assets by Unity to your project to use demo scenes.", "Close");

				if(EditorUtility.DisplayDialog("Importing BoneCracker Games Shared Assets", "Do you want to import ''BoneCracker Games Shared Assets'' to your project? It will be used for enter / exit on all vehicles created by BoneCracker Games in future.", "Import it", "No")){
					
					string url = "https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351";
					Application.OpenURL (url);

				}

			}

		}

	}

	private static BuildTargetGroup[] buildTargetGroups = new BuildTargetGroup[]
	{

		BuildTargetGroup.Standalone,
		BuildTargetGroup.Android,
		BuildTargetGroup.iOS,
		BuildTargetGroup.WebGL,
		BuildTargetGroup.Facebook,
		BuildTargetGroup.XboxOne,
		BuildTargetGroup.PS4,
		BuildTargetGroup.tvOS,
		BuildTargetGroup.Switch,
		BuildTargetGroup.WSA

	};

	private static void SetEnabled(string defineName, bool enable)
	{
		//Debug.Log("setting "+defineName+" to "+enable);
		foreach (var group in buildTargetGroups)
		{
			var defines = GetDefinesList(group);
			if (enable)
			{
				if (defines.Contains(defineName))
				{
					return;
				}
				defines.Add(defineName);
			}
			else
			{
				if (!defines.Contains(defineName))
				{
					return;
				}
				while (defines.Contains(defineName))
				{
					defines.Remove(defineName);
				}
			}
			string definesString = string.Join(";", defines.ToArray());
			PlayerSettings.SetScriptingDefineSymbolsForGroup(group, definesString);
		}
	}

	private static List<string> GetDefinesList(BuildTargetGroup group)
	{
		return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));
	}

}
