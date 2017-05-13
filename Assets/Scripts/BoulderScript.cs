using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoulderScript : MonoBehaviour {

	Vector3 origPos;
	Vector3 nextPos;
	Vector3 currentPos;
	float direction = 1.0f;

	public bool startLeft;
	public bool perp;

	// Use this for initialization
	void Start () {

		origPos = gameObject.transform.position;
		currentPos = origPos;
		if (startLeft == true) {

			if (perp == false) {
				nextPos = new Vector3 (origPos.x + 1.0f, origPos.y, origPos.z);
			} else {
				nextPos = new Vector3 (origPos.x, origPos.y, origPos.z - 1.0f);
			}
				
		} else {

			if (perp == false) {
				nextPos = new Vector3 (origPos.x - 1.0f, origPos.y, origPos.z);
				direction = -1.0f;
			} else {
				nextPos = new Vector3 (origPos.x , origPos.y, origPos.z + 1.0f);
				direction = -1.0f;
			
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.B)){

			RotateBoulder();

		}
		
	}

	public void RotateBoulder(){

		Vector3 newAngle;
		if (currentPos == origPos) {
			newAngle = new Vector3 (gameObject.transform.eulerAngles.x + 180.0f * direction, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
			gameObject.transform.DORotate (newAngle, 1.0f);
			gameObject.transform.DOMove (nextPos, 1.0f);
			currentPos = nextPos;
		} else {
			newAngle = new Vector3 (gameObject.transform.eulerAngles.x - 180.0f * direction, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
			gameObject.transform.DORotate (newAngle, 1.0f);
			gameObject.transform.DOMove (origPos, 1.0f);
			currentPos = origPos;
		}
	}
}
