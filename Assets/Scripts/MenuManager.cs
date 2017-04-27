using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject levelMap;
	public GameObject loading;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RevealLevelMap (){
	
		levelMap.SetActive (true);
		mainMenu.SetActive (false);

	}

	public void QuitGame () {
		Application.Quit ();
	}
}
