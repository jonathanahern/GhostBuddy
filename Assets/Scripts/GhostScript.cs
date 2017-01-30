using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

	GameObject gameManager;
	TurnManagerScript turnManager;

	// Use this for initialization
	void Start () {
		
		Invoke ("FindManager", 2.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void FindManager () {

		gameManager = GameObject.FindGameObjectWithTag ("Game Manager Local");
		turnManager = gameManager.GetComponent <TurnManagerScript> ();

	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Slime") {

			Debug.Log ("SLIMED");

			turnManager.LoseGame ();
			turnManager.gameLost = true;
			turnManager.forwardBlueGhost = false;
			turnManager.forwardPinkGhost = false;

		}


		if (other.tag == "Player") {
		
			turnManager.winning = true;
		
		}
		
		if (other.tag == "Wall") {
			
			if (gameObject.name == "Pink Ghost(Clone)") {
				
				turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;

			}



		}

		if (other.tag == "Border") {

			if (gameObject.name == "Pink Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardPinkGhost = false;
				GetComponent<Rigidbody> ().useGravity = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardBlueGhost = false;
				GetComponent<Rigidbody> ().useGravity = true;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{

		if (other.tag == "Player") {

			turnManager.winning = false;

		}

	}
}
