using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public bool forwardTriggerPink;
	public bool rightTriggerPink;
	public bool leftTriggerPink;
	public bool forwardTriggerBlue;
	public bool rightTriggerBlue;
	public bool leftTriggerBlue;

	private TurnManagerScript gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript>();
		
	}

	public void InvokeTriggerAction (float seconds) {

		Invoke ("TriggerAction", seconds);

	}

	void TriggerAction () {
	
		if (forwardTriggerPink == true) {
		
			gameManager.ForwardPink ();
		
		}

		else if (rightTriggerPink == true) {

			gameManager.RotateRightPink ();

		}

		else if (leftTriggerPink == true) {

			gameManager.RotateLeftPink ();

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
	
	}

	public void TriggerSend (int arrayOrder) {

		if (forwardTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 3;

		}

		else if (rightTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 4;

		}

		else if (leftTriggerPink == true) {

			gameManager.fromPinkGhost [arrayOrder] = 5;

		}		
		else if (forwardTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 0;

		}

		else if (rightTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 1;

		}

		else if (leftTriggerBlue == true) {

			gameManager.fromBlueGhost [arrayOrder] = 2;

		}



	}

}
