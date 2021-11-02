//----------------------------------------------
//           	 BCG Shared Assets
//
// Copyright © 2014 - 2019 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BCG_UIInteractionButton : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData){
		
		#if BCG_ENTEREXIT

		if (BCG_EnterExitManager.Instance.BCGCharacterPlayer != null) 
			BCG_EnterExitManager.Instance.Interact();
		
		#endif

	}

}
