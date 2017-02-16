using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeechBubbleScript : MonoBehaviour {

	public Transform bubbleHolder;
	public GameObject bubbleFab;
	public GameObject wheelPanel;

	private Vector3 onScreenPos;
	private Vector3 offScreenPos;
	private Vector3 currentPos;
	public bool moveUp;
	public bool moveDown;

	public List<string> bubbleList = new List<string>();

	// Use this for initialization
	void Start () {

		offScreenPos = gameObject.transform.position;
		onScreenPos = wheelPanel.transform.position;
		PopulateList ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F)) {
		
			LoadBubbles ();

		}

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
				DeleteBubbles ();

			}
		}


		
	}

	public void LoadBubbles (){
	
		moveUp = true;

		int bubbleCount = Random.Range (2, 5);

		for (int i = 0; i < bubbleCount; i++) {

			int listCount = Random.Range (0,bubbleList.Count);

			GameObject bubble = Instantiate(bubbleFab, new Vector3 (0, 0, 0), Quaternion.identity);
			bubble.transform.SetParent (bubbleHolder, false);
			Text bubText = bubble.transform.GetChild (0).gameObject.GetComponent<Text> ();
			bubText.text = bubbleList [listCount];
			bubbleList.RemoveAt (listCount);

		}
	
		PopulateList ();

	}

	void DeleteBubbles(){
	
		GameObject[] bubbles = GameObject.FindGameObjectsWithTag ("Bubble Button");

		for (int i = 0; i < bubbles.Length; i++) {

			Destroy (bubbles [i]);

		}
	
	}

	void PopulateList (){
		
		bubbleList.Clear ();

		bubbleList.Add ("Hey");
		bubbleList.Add ("Zzzzzz");
		bubbleList.Add ("Nice");
		bubbleList.Add ("Do not compute");
		bubbleList.Add ("My bad");
		bubbleList.Add ("Ugh");
		bubbleList.Add ("Beep Boop");
		bubbleList.Add ("Let's try again");
		bubbleList.Add ("Human error");
		bubbleList.Add ("Cool it");
		bubbleList.Add ("Whoops");
		bubbleList.Add ("Pretty");

	}
}
