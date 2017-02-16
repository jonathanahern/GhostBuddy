using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public bool forwardTriggerPink;
	public bool rightTriggerPink;
	public bool leftTriggerPink;
	public bool wheelTriggerPink;
	public bool napTriggerPink;
	public bool forwardTriggerBlue;
	public bool rightTriggerBlue;
	public bool leftTriggerBlue;
	public bool wheelTriggerBlue;
	public bool napTriggerBlue;
	public bool gearTrigger;

	public bool hitWall;

	private TurnManagerScript gameManager;
	private GameObject glow;
	private bool timerbool;
	private float timer;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript>();
		glow = transform.GetChild (0).gameObject;

		
	}

	void Update (){

		if (timerbool == true) {
			timer += Time.deltaTime;
			if (timer > 1.5f) {
			
				timerbool = false;
				timer = 0;
				TurnOffGlow ();

			}
		}
	}

	public void InvokeTriggerAction (float seconds) {

		Invoke ("TriggerAction", seconds);

	}

	void TurnOffGlow() {

		glow.SetActive (false);

	}

	void TurnOnGlow() {
	
		glow.SetActive (true);
		timerbool = true;
	
	}

	void TriggerAction () {

		TurnOnGlow ();
	
		if (gearTrigger == true) {

			gameManager.PlayGearItems ();

		}


		else if (forwardTriggerPink == true) {
		
			gameManager.ForwardPink ();
		
		}

		else if (rightTriggerPink == true) {

			gameManager.RotateRightPink ();

		}

		else if (leftTriggerPink == true) {

			gameManager.RotateLeftPink ();

		}
		else if (wheelTriggerPink == true) {

			gameManager.GetComponent<TurnManagerScript> ().FillPinkColorArray ();

		}

		else if (napTriggerPink == true) {

			Debug.Log ("take a nap pink");

		}

		else if (forwardTriggerBlue == true) {

			gameManager.ForwardBlue ();

		}

		else if (rightTriggerBlue == true) {

			gameManager.RotateRightBlue ();

		}

		else if (leftTriggerBlue == true) {

			gameManager.RotateLeftBlue ();

		}

		else if (wheelTriggerBlue == true) {

			gameManager.GetComponent<TurnManagerScript> ().FillBlueColorArray ();

		}
	
		else if (napTriggerBlue == true) {

			Debug.Log ("take a nap blue");

		}
	}

	public void TriggerSend (int arrayOrder) {

		if (forwardTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 11;

		}

		else if (rightTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 12;

		}

		else if (leftTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 13;

		}

		else if (wheelTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 14;

		}

		else if (napTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 15;

		}	


		else if (forwardTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 1;

		}

		else if (rightTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 2;

		}

		else if (leftTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 3;

		}

		else if (wheelTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 4;

		}

		else if (napTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 5;

		}
	}

	public void UndoAction () {

		if (forwardTriggerPink == true && hitWall == false) {

			gameManager.BackwardPink ();

		}

		else if (rightTriggerPink == true) {

			gameManager.RotateLeftPink ();

		}

		else if (leftTriggerPink == true) {

			gameManager.RotateRightPink ();

		}

		else if (forwardTriggerBlue == true  && hitWall == false) {

			gameManager.BackwardBlue ();

		}

		else if (rightTriggerBlue == true) {

			gameManager.RotateLeftBlue ();

		}

		else if (leftTriggerBlue == true) {

			gameManager.RotateRightBlue ();

		}
	}
}
