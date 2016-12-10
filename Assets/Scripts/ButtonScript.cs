using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public GameObject signalWheel;
	private bool buttonBool = true;


	void Start (){


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
