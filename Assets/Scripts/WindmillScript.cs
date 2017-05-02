﻿using UnityEngine;
using System.Collections;

public class WindmillScript : MonoBehaviour {

	bool rotating = false;
	public int direction = 1;

	public float rotateSpeed;
	private Vector3 rotationDirection;

	private bool rotateGhostOne = false;
	private bool rotateGhostTwo = false;
	private float startAngle;
//	private float endAngle;
	private float startAngle2;
//	private float endAngle2;

	public GameObject GhostBoxA1;
	public GameObject GhostBoxA2;
	public GameObject GhostBoxB1;
	public GameObject GhostBoxB2;

	public GameObject ghostOne;
	public GameObject ghostTwo;
	public GameObject[] players = new GameObject[2];
	public GameObject tester;


	public bool ghostOneInside = false;
	TurnManagerScript turnMan;
	GameObject robotBuddy;

	void Start () {


		rotationDirection = new Vector3 (0, 1 * direction, 0);

		if (direction < 0) {
		
			GhostBoxA1.SetActive (false);
			GhostBoxA2.SetActive (false);
		
		} else {
		
			GhostBoxB1.SetActive (false);
			GhostBoxB2.SetActive (false);
		
		}

	}


	void Update (){

		if (Input.GetKeyDown (KeyCode.Space) && rotateGhostOne == false) {

			RotateWindmill ();
			RotateGhostOne ();

		}

		if (rotateGhostOne == true) {

			ghostOne.transform.RotateAround (gameObject.transform.position, rotationDirection, rotateSpeed * Time.deltaTime);
		
			if (ghostOne.transform.eulerAngles.y < 0 && direction == -1) {

				rotateGhostOne = false;
				float newXPos = (Mathf.Round (ghostOne.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostOne.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostOne.transform.position.y, newZPos);
				ghostOne.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostOne.transform.eulerAngles.y / 10)) * 10;
				Vector3 newAngle = new Vector3 (ghostOne.transform.eulerAngles.x, roundedAngle, ghostOne.transform.eulerAngles.z);
				ghostOne.transform.eulerAngles = newAngle;
			}

			if (ghostOne.transform.eulerAngles.y > 0 && direction == 1) {

				rotateGhostOne = false;
				float newXPos = (Mathf.Round (ghostOne.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostOne.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostOne.transform.position.y, newZPos);
				ghostOne.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostOne.transform.eulerAngles.y / 10)) * 10;
				Vector3 newAngle = new Vector3 (ghostOne.transform.eulerAngles.x, roundedAngle, ghostOne.transform.eulerAngles.z);
				ghostOne.transform.eulerAngles = newAngle;
			}
		}

		if (rotateGhostTwo == true) {

			ghostTwo.transform.RotateAround (gameObject.transform.position, rotationDirection, rotateSpeed * Time.deltaTime);

			if (ghostTwo.transform.eulerAngles.y < 0 && direction == -1) {

				rotateGhostTwo = false;
				float newXPos = (Mathf.Round (ghostTwo.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostTwo.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostTwo.transform.position.y, newZPos);
				ghostTwo.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostOne.transform.eulerAngles.y / 90)) * 90;
				Vector3 newAngle = new Vector3 (ghostTwo.transform.eulerAngles.x, roundedAngle, ghostTwo.transform.eulerAngles.z);
				ghostTwo.transform.eulerAngles = newAngle;
			}

			if (ghostTwo.transform.eulerAngles.y > 0 && direction == 1) {

				rotateGhostTwo = false;
				float newXPos = (Mathf.Round (ghostTwo.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostTwo.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostTwo.transform.position.y, newZPos);
				ghostTwo.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostTwo.transform.eulerAngles.y / 90)) * 90;
				Vector3 newAngle = new Vector3 (ghostTwo.transform.eulerAngles.x, roundedAngle, ghostTwo.transform.eulerAngles.z);
				ghostTwo.transform.eulerAngles = newAngle;
			}
		}


	}
		
	public void RotateWindmill()
	{

		if (turnMan == null) {
		
			turnMan = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();
			players = GameObject.FindGameObjectsWithTag ("Player");

		}

//		if (turnMan.winning == true) {
//
//			if (ghostOne != null || ghostTwo != null) {
//				Debug.Log (ghostOne.name + "in the rotato");
//
//				for (int i = 0; i < players.Length; i++) {
//
//					if (players [i] != ghostOne && ghostTwo == null) {
//					
//						players[i] = ghostTwo;
//						players[i].transform.parent = gameObject.transform;
//
//					
//					}
//
//				}
//
//			//	players [0].transform.SetParent (players [1].transform, true);
//			//	Invoke ("UnhookBots", 1.0f);
//
//
//			}
//
//		}




		if (ghostOne != null) {
			RotateGhostOne ();
		}

		if (ghostTwo != null) {
			RotateGhostTwo ();
		}
		
		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(gameObject, rotation2, 1.0f));
	}


	public void AddGhost (GameObject ghost){
	
		Debug.Log (" square32");

		if (ghostTwo == null) {
			Debug.Log (" square323");
			ghostTwo = ghost;
		
		} else if (ghostOne == null) {
			Debug.Log (" square3233");
			ghostOne = ghost;

		}
	
	}

	public void GhostJumped (GameObject ghost) {

//		Debug.Log ("JUMPED1st" + ghost.name);

//		Debug.Log("it worked:" + ghostOne.name + ghostTwo.name);

//		turnMan.winning = false;

//		if (ghostOne == null || ghostTwo == null) {
//			Debug.Log ("GREY NULL HAPPENED");
//			return;
//		}

		//Debug.Log ("JUMPED" + ghost.name);


//		ghost.transform.parent = null;

//		if (ghost == ghostOne) {
//
//			ghostOne.transform.parent = null;
//
//		} else {
//
//			ghostTwo.transform.parent = null;
//
//		}

		if (ghost == ghostOne) {
			Debug.Log ("JUMPED" + ghost.name);
			ghostOne = null;
		
		}

		if (ghost == ghostTwo) {
			Debug.Log ("JUMPED" + ghost.name);
			ghostTwo = null;
		
		}

	}

	void RotateGhostOne () {

		ghostOne.transform.parent = gameObject.transform;


//		startAngle = ghostOne.transform.eulerAngles.y;
//		if (startAngle == 0 && direction == -1) {
//			startAngle = 360.0f;
//		}
//	
//		endAngle = startAngle + 90.0f * direction;
//
//		if (endAngle == 0 && direction == -1) {
//		
//			endAngle = 3f;
//		
//		}
//
//		if (endAngle == 360 && direction == 1) {
//
//			endAngle = 357f;
//
//		}
//
//		rotateGhostOne = true;
	
	}

	void RotateGhostTwo () {

		ghostTwo.transform.parent = gameObject.transform;

//		startAngle2 = ghostTwo.transform.eulerAngles.y;
//		if (startAngle2 == 0 && direction == -1) {
//			startAngle2 = 360.0f;
//		}
//
//		endAngle2 = startAngle2 + 90.0f * direction;
//
//		if (endAngle2 == 0 && direction == -1) {
//
//			endAngle2 = 3f;
//
//		}
//
//		if (endAngle2 == 360 && direction == 1) {
//
//			endAngle2 = 357f;
//
//		}
//
//		rotateGhostTwo = true;

	}



	IEnumerator RotateObject (GameObject gameObjectToMove, Quaternion newRot, float duration)
	{
		if (rotating) {
			yield break;
		}
		rotating = true;

		Quaternion currentRot = gameObjectToMove.transform.rotation;
		newRot = Quaternion.Euler (gameObjectToMove.transform.eulerAngles + newRot.eulerAngles);

		float counter = 0;
		while (counter < duration) {
			counter += Time.deltaTime;
			gameObjectToMove.transform.rotation = Quaternion.Lerp (currentRot, newRot, counter / duration);
			yield return null;
		}
		rotating = false;
	}
}
