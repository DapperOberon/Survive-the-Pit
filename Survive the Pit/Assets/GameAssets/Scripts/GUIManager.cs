using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {

	public TMP_Text TimeDateText;
	
	// Update is called once per frame
	void Update () {
		TimeDateText.text = TimeManager.instance.toString(); // TODO Possibly add static instance and call toString in an updateText function in TimeManager
	}
}
