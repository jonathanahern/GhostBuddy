using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetupScript : NetworkBehaviour {

	public GameObject eventSystem;
	private Color pink;
	private Color blue;


	public override void OnStartLocalPlayer(){
	
		string networkNum = netId.ToString();

		eventSystem.SetActive (true);
		if (networkNum == "1") {

			gameObject.name = "Pink Game Manager";
			gameObject.tag = "Game Manager Local";
			pink = new Color (1.0f, .8f, .808f, 1.0f);
			ButtonsToPink ();

		}

		if (networkNum == "2") {

			gameObject.name = "Blue Game Manager";
			gameObject.tag = "Game Manager Local";
			blue = new Color (.74f, .945f, 1.0f, 1.0f);
			ButtonsToBlue ();

		}
	
	
	}

	// Use this for initialization
	void Start () {


		if (!isLocalPlayer)
		{
			GameObject canvas = this.gameObject.transform.GetChild(0).gameObject;
			Destroy (canvas);
			//GameObject eventSystem = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;


		}

		if (isLocalPlayer)
		{
//			string networkNum = netId.ToString();
//
//			eventSystem.SetActive (true);
//			if (networkNum == "1") {
//
//				gameObject.name = "Pink Game Manager";
//				pink = new Color (1.0f, .8f, .808f, 1.0f);
//				ButtonsToPink ();
//			
//			}
//
//			if (networkNum == "2") {
//
//				gameObject.name = "Blue Game Manager";
//				blue = new Color (.74f, .945f, 1.0f, 1.0f);
//				ButtonsToBlue ();
//
//			}

		}


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ButtonsToPink () {
	
		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("Program Button");
		int buttonCount = buttons.Length;

		for(int i = 0; i < buttonCount; i++)
		{
			buttons[i].GetComponent<Image>().color = pink;

		}

	}

	void ButtonsToBlue () {

		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("Program Button");
		int buttonCount = buttons.Length;

		for(int i = 0; i < buttonCount; i++)
		{
			buttons[i].GetComponent<Image>().color = blue;

		}

	}


}
