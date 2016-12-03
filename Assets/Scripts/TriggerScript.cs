using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public bool forwardTrigger;
	public bool rightTrigger;
	public bool leftTrigger;

	private TurnManagerScript gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<TurnManagerScript>();
		
	}

	public void InvokeTriggerAction (float seconds) {

		Invoke ("TriggerAction", seconds);

	}

	void TriggerAction () {
	
		if (forwardTrigger == true) {
		
			gameManager.ForwardGhost ();
		
		}

		else if (rightTrigger == true) {

			gameManager.RotateRightGhostOne ();

		}

		else if (leftTrigger == true) {

			gameManager.RotateLeftGhostOne ();

		}
	
	}

	public void TriggerSend () {

		if (forwardTrigger == true) {

			gameManager.ghostOneList.Add (1);

		}

		else if (rightTrigger == true) {

			gameManager.ghostOneList.Add (2);

		}

		else if (leftTrigger == true) {

			gameManager.ghostOneList.Add (3);

		}



	}

}
