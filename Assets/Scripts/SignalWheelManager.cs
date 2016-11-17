using UnityEngine;
using System.Collections;

public class SignalWheelManager : MonoBehaviour {

	private bool buttonBool;

	public GameObject[] buttons;



	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SignalClicked () {

		buttons = GameObject.FindGameObjectsWithTag("Button");

		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = true;
		}

	}
		
}
