using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	private GameObject signalWheel;
	public bool buttonBool = false;


	void Start (){

		signalWheel = GameObject.Find ("Signal Circle Original");

	}

	public void PinkButtonClicked (){

		if (buttonBool == true) {
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
			signalWheel.GetComponent<ColorButtonScript> ().TurnPink ();
		}
	}

	public void GreenButtonClicked (){

		if (buttonBool == true){
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
			signalWheel.GetComponent<ColorButtonScript>().TurnGreen ();
		}
	}

	public void BlueButtonClicked (){

		if (buttonBool == true) {
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
			signalWheel.GetComponent<ColorButtonScript> ().TurnBlue ();
		}
	}

	public void PurpleButtonClicked (){

		if (buttonBool == true){
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
		signalWheel.GetComponent<ColorButtonScript>().TurnPurple ();
		}
	}

	public void BlackButtonClicked (){

		if (buttonBool == true) {
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
			signalWheel.GetComponent<ColorButtonScript> ().TurnBlack ();
		}
	}

	public void WhiteButtonClicked (){

		if (buttonBool == true) {
			//signalWheel = GameObject.FindGameObjectWithTag ("Signal Wheel");
			signalWheel.GetComponent<ColorButtonScript> ().TurnWhite ();
		}
	}



}
