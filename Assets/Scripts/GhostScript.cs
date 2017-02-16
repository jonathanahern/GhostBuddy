using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostScript : MonoBehaviour {

	GameObject gameManager;
	TurnManagerScript turnManager;

	public GameObject canvas;
	public GameObject speechBubble;
	public Text speechText;

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

		if (other.tag == "Hidden Wall" && turnManager.preview == false) {

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

	public void ShowBubble (string speech){
	
		speechText.text = speech;
		speechBubble.SetActive (true);
		Invoke ("HideBubble", 3.5f);
	}

	void HideBubble(){
	
		speechBubble.SetActive (false);

	}

	public void AssignCameras(string playerId){
	
		if (playerId == "1") {
			canvas.GetComponent<LookAtScript> ().target = GameObject.FindGameObjectWithTag ("Camera Two");

		} else {
			canvas.GetComponent<LookAtScript> ().target = GameObject.FindGameObjectWithTag ("Camera One");
		}
	
	}


}
