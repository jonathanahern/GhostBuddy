using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour {

	public bool moveUp;
	public bool moveDown;
	public GameObject wheelPanel;
	Vector3 currentPos;
	Vector3 onScreenPos;
	Vector3 offScreenPos;
	public TurnManagerScript turnMan;

	// Use this for initialization
	void Start () {

		offScreenPos = gameObject.transform.position;
		onScreenPos = wheelPanel.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (moveUp == true) {
			currentPos = new Vector3 (gameObject.transform.position.x,
				gameObject.transform.position.y,
				gameObject.transform.position.z);

			gameObject.transform.position = Vector3.Lerp (currentPos, onScreenPos, Time.deltaTime * 10);

			if (Vector3.Distance(currentPos,onScreenPos) < .05f) {

				moveUp = false;
				gameObject.transform.position = onScreenPos;

			}

		}

		if (moveDown == true) {

			currentPos = new Vector3 (gameObject.transform.position.x,
				gameObject.transform.position.y,
				gameObject.transform.position.z);

			gameObject.transform.position = Vector3.Lerp (currentPos, offScreenPos, Time.deltaTime * 10);

			if (Vector3.Distance(currentPos,offScreenPos) < .1f) {

				moveDown = false;
				gameObject.transform.position = offScreenPos;

			}
		}
	}

	public void QuitGame (){
	
		Application.Quit();
	
	}


	public void RestartGame (){
	
		moveDown = true;
		turnMan.RestartGame ();
	
	}

}
