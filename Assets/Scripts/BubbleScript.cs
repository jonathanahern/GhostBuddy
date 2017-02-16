using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleScript : MonoBehaviour {

	private string phrase;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendBubble(){
	
		GameObject localMan = GameObject.FindGameObjectWithTag ("Game Manager Local");

		phrase = GetComponent<Text> ().text;
		string ghostNum = localMan.GetComponent<TurnManagerScript> ().networkNum;
		localMan.GetComponent<TurnManagerScript> ().CmdSendBubble (phrase, ghostNum);
		localMan.GetComponent<TurnManagerScript> ().speechPanel.GetComponent<SpeechBubbleScript> ().moveDown = true;

		Debug.Log ("Bubble: " + phrase + ghostNum + localMan.name);


	}
}
