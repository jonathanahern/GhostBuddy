using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TurnManagerScript : NetworkBehaviour {

	public GameObject pinkGhost;
	public GameObject blueGhost;

	private Vector3 pinkEndPos;
	private Vector3 pinkGhostPos;
	private Vector3 blueEndPos;
	private Vector3 blueGhostPos;

	private bool forwardPinkGhost = false;
	private bool forwardBlueGhost = false;

	public GameObject scrollList;
	public GameObject forwardIconPink;
	public GameObject leftIconPink;
	public GameObject rightIconPink;
	public GameObject forwardIconBlue;
	public GameObject leftIconBlue;
	public GameObject rightIconBlue;

	public GameObject wheelPanel;

	private Vector3 origPos;
	private Vector3 origRot;

	bool rotating = false;
	private int direction = 1;
	string networkNum;

	public int[] fromPinkGhost = {0,0,0,0,0,0,0};
	public int[] fromBlueGhost = {0,0,0,0,0,0,0};

	public GameObject[] blueGhostIcon = new GameObject[7];
	private GameObject[] iconList = new GameObject[3];


	public SyncListInt ghostOneList = new SyncListInt();
	public SyncListInt ghostTwoList = new SyncListInt();


	public override void OnStartLocalPlayer() {

		iconList [0] = forwardIconBlue;
		iconList [1] = rightIconBlue;
		iconList [2] = leftIconBlue;

		networkNum = netId.ToString();

		GameObject playerOneSpawn = GameObject.FindGameObjectWithTag ("Spawn One");
		pinkGhost = Instantiate(Resources.Load("Pink Ghost", typeof(GameObject))) as GameObject;
		pinkGhost.transform.position = playerOneSpawn.transform.position;
		pinkGhost.transform.rotation = playerOneSpawn.transform.rotation;

		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		blueGhost = Instantiate(Resources.Load("Blue Ghost", typeof(GameObject))) as GameObject;
		blueGhost.transform.position = playerTwoSpawn.transform.position;
		blueGhost.transform.rotation = playerTwoSpawn.transform.rotation;

	}

	void Update (){

		if (!isLocalPlayer)
		{
			return;
		}
			
		if (forwardPinkGhost == true) {
			
			pinkGhostPos = new Vector3 (pinkGhost.transform.position.x,
				pinkGhost.transform.position.y,
				pinkGhost.transform.position.z);

			pinkGhost.transform.position = Vector3.Lerp (pinkGhostPos, pinkEndPos, Time.deltaTime * 10);

			if (pinkGhost.transform.position == pinkEndPos) {
			
				forwardPinkGhost = false;
			
			}
		}

		if (forwardBlueGhost == true) {

			blueGhostPos = new Vector3 (blueGhost.transform.position.x,
				blueGhost.transform.position.y,
				blueGhost.transform.position.z);

			blueGhost.transform.position = Vector3.Lerp (blueGhostPos, blueEndPos, Time.deltaTime * 10);

			if (blueGhost.transform.position == blueEndPos) {

				forwardBlueGhost = false;

			}
		}

	}

	public void PlaceForwardGhost(){
	
		if (networkNum == "1") {
	
			GameObject newTrigger = Instantiate (forwardIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}

		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (forwardIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
	}

	public void PlaceRightIcon() {

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (rightIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);

		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (rightIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);

		}
	
	}

	public void PlaceLeftIcon() {

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (leftIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (leftIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
	
	}

	public void ForwardPink () {

		pinkEndPos = pinkGhost.transform.position - pinkGhost.transform.forward;
		forwardPinkGhost = true;

	}

	public void RotateRightPink () {

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, 1f));
	}

	public void RotateLeftPink () {
		
		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, 1f));

	}

	public void ForwardBlue () {

		blueEndPos = blueGhost.transform.position - blueGhost.transform.forward;
		forwardBlueGhost = true;

	}

	public void RotateRightBlue () {

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, 1f));
	}

	public void RotateLeftBlue () {

		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, 1f));

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

	public void BackToOriginalPositionPink () {

		pinkGhost.transform.position = origPos;
		pinkGhost.transform.eulerAngles = origRot;
	
	}

	public void BackToOriginalPositionBlue () {

		blueGhost.transform.position = origPos;
		blueGhost.transform.eulerAngles = origRot;

	}

	public void PlaybackButton () {

		if (networkNum == "1") {
			origPos = pinkGhost.transform.position;
			origRot = pinkGhost.transform.eulerAngles;
			GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
			int triggerCount = triggers.Length;
			float seconds = 0;
			for (int i = 0; i < triggerCount; i++) {
				triggers [i].GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
				seconds = seconds + 1.5f;
				if (i == (triggerCount - 1)) {
					Invoke ("BackToOriginalPositionPink", seconds + 1);
				}
			}
		}

		if (networkNum == "2") {
			origPos = blueGhost.transform.position;
			origRot = blueGhost.transform.eulerAngles;
			GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
			int triggerCount = triggers.Length;
			float seconds = 0;
			for (int i = 0; i < triggerCount; i++) {
				triggers [i].GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
				seconds = seconds + 1.5f;
				if (i == (triggerCount - 1)) {
					Invoke ("BackToOriginalPositionBlue", seconds + 1);
				}
			}
		}

	}


	public void TurnDone () {
	
		Debug.Log ("Turn Done");

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;
		int triggerOrder = 0;

		for(int i = 0; i < triggerCount; i++)
		{
			triggers[i].GetComponent<TriggerScript>().TriggerSend(triggerOrder);
			triggerOrder++;
			if (i == triggerCount - 1 && networkNum == "2") {
				CmdBlueSendArray (fromBlueGhost);
			}

		}
	}

	public void FillBlueArray () {

		for(int i = 0; i < fromBlueGhost.Length; i++)
		{
			blueGhostIcon [i] = iconList [fromBlueGhost [i]];

			if (i == fromBlueGhost.Length - 1) {
			
				CombineLists ();
			
			}
	
		}

	}

	void CombineLists (){

	for(int i = 0; i < blueGhostIcon.Length; i++)
	{
		GameObject newTrigger = Instantiate (blueGhostIcon[i], new Vector3 (0, 0, 0), Quaternion.identity);
		newTrigger.transform.SetParent (scrollList.transform, false);

		}
	}

	[Command]
	public void CmdBlueSendArray (int[] blueArray){
		//fromBlueGhost = blueArray;

		GameObject localGameManager = GameObject.FindWithTag ("Game Manager Local");
		localGameManager.GetComponent<TurnManagerScript> ().fromBlueGhost = blueArray;
		localGameManager.GetComponent<TurnManagerScript> ().FillBlueArray ();

	}


}


//pink up, blue up, pink up
//intantiate blue up in child 1
//instantiate 