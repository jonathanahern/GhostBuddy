using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour {

	public bool moveSlime;

	public bool move1;
	public bool move2;
	public bool move3;
	public bool move4;
	public bool move5;
	public bool move6;
	public bool move7;
	public bool move8;

	private Vector3 slimePos;
	private Vector3 endPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
		
			RotateSlime ();
		
		}

		if (moveSlime == true) {

			slimePos = new Vector3 (gameObject.transform.position.x,
				gameObject.transform.position.y,
				gameObject.transform.position.z);

			gameObject.transform.position = Vector3.Lerp (slimePos, endPos, Time.deltaTime * 10);

			if (gameObject.transform.position == endPos) {

				moveSlime = false;

			}
		}
	}

	public void RotateSlime(){

		if (move1 == true) {
			MoveRight ();
			move1 = false;
			move2 = true;
			return;
		} else if (move2 == true) {
			MoveBack ();
			move2 = false;
			move3 = true;
			return;
		} else if (move3 == true) {
			MoveBack ();
			move3 = false;
			move4 = true;
			return;
		} else if (move4 == true) {
			MoveLeft ();
			move4 = false;
			move5 = true;
			return;
		} else if (move5 == true) {
			MoveLeft ();
			move5 = false;
			move6 = true;
			return;
		} else if (move6 == true) {
			MoveForward ();
			move6 = false;
			move7 = true;
			return;
		} else if (move7 == true) {
			MoveForward ();
			move7 = false;
			move8 = true;
			return;
		} else if (move8 == true) {
			MoveRight ();
			move8 = false;
			move1 = true;
			return;
		} 

		MoveForward ();

	}

	void MoveForward (){
	
		endPos = new Vector3 (gameObject.transform.position.x,
			gameObject.transform.position.y,
			gameObject.transform.position.z + 1.0f);

		moveSlime = true;
	
	}

	void MoveBack (){

		endPos = new Vector3 (gameObject.transform.position.x,
			gameObject.transform.position.y,
			gameObject.transform.position.z - 1.0f);

		moveSlime = true;

	}

	void MoveRight (){

		endPos = new Vector3 (gameObject.transform.position.x + 1.0f,
			gameObject.transform.position.y,
			gameObject.transform.position.z);

		moveSlime = true;

	}

	void MoveLeft (){

		endPos = new Vector3 (gameObject.transform.position.x - 1.0f,
			gameObject.transform.position.y,
			gameObject.transform.position.z);

		moveSlime = true;

	}
}
