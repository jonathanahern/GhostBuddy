using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPanelScript : MonoBehaviour {

	public GameObject buttonPanel;
	public GameObject wheelPanel;

	private Vector3 onScreenPos;
	private Vector3 offScreenPos;
	private Vector3 currentPos;

	public bool moveUp;
	public bool moveDown;

	// Use this for initialization
	void Start () {

		offScreenPos = buttonPanel.transform.position;
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

	public void MoveUp (){

		moveUp = true;

	}

	public void MoveDown (){

		moveDown = true;

	}
		
}
