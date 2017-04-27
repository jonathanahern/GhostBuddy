using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour {

	public bool north;
	public bool east;
	public bool south;
	public bool west;

	public int id;
	private int dir;

	public bool alreadyHit = false;

	public GameObject parent;
	TurnManagerScript turnMan;

	// Use this for initialization
	void Start () {

		Invoke ("FindTurnMan", 2.0f);

		if (north == true) {
			dir = 1;
		} else if (east == true) {
			dir = 2;
		} else if (south == true) {
			dir = 3;
		} else if (west == true) {
			dir = 4;
		}

	}

	void FindTurnMan(){
	
		turnMan = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (turnMan.preview == true) {
			return;
		}
	
		if (other.tag == "Detector") {
				
			if (alreadyHit == true) {
				return;
			}


			alreadyHit = true;
			turnMan.InSameSquare (id, dir);
			parent.SetActive (false);
			//Invoke ("HitBack", 0.35f);
		
		}
	
	}

	void HitBack (){
	
		alreadyHit = false;

	}

}
