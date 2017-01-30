using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

	public bool playerOneCanCollect = false;
	public bool playerTwoCanCollect = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		
	
	}

	void OnTriggerEnter(Collider other){


		if (other.gameObject.name == "Pink Ghost(Clone)" && playerOneCanCollect == true) {

			GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent <GameCountScript> ().keyCount++;
			Destroy (gameObject);
			return;
		}

		else if (other.gameObject.name == "Blue Ghost(Clone)" && playerTwoCanCollect == true) {

			GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent <GameCountScript> ().keyCount++;
			Destroy (gameObject);
			return;
		}

		else if (other.gameObject.name == "Pink Ghost(Clone)" && playerOneCanCollect == false) {
		
			TurnManagerScript turnManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent <TurnManagerScript> ();
			turnManager.gameLost = true;
			turnManager.LoseGame ();
			return;
		}

		else if (other.gameObject.name == "Blue Ghost(Clone)" && playerTwoCanCollect == false) {

			TurnManagerScript turnManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent <TurnManagerScript> ();
			turnManager.gameLost = true;
			turnManager.LoseGame ();

			return;
		}

	}

}
