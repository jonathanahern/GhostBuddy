using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TurnManagerScript : NetworkBehaviour {

	public GameObject pinkGhost;
	public GameObject blueGhost;
	private GhostScript myGhostScript;
	public GameObject pinkShell;
	public GameObject blueShell;


	bool bluePlayerThere = false;
	public bool otherPlayerReady = true;

	public GameObject[] ghosts = new GameObject[2];

	public bool winning;

	private Vector3 pinkEndPos;
	private Vector3 pinkGhostPos;
	private Vector3 blueEndPos;
	private Vector3 blueGhostPos;
	private Vector3 pinkStartPos;
	private Vector3 blueStartPos;
	private Vector3 pinkBackOnePos;
	private Vector3 blueBackOnePos;

	public GameObject speechPanel;

	public bool forwardPinkGhost = false;
	public bool forwardBlueGhost = false;
	public bool backPinkGhost = false;
	public bool backBlueGhost = false;
	bool backwardPinkGhost = false;
	bool backwardBlueGhost = false;
	public bool gameLost = false;
	bool buttonCushion = false;

	public float turnsLeft;
	public Text turnsText;
	//public Image timerCircle;
	float totalTurns;

	public GameObject deleteNull;
	public GameObject turnBar;
	public GameObject turnBarEnd;
	private RectTransform barRect;
	private RectTransform barRectEnd;
	public bool moveLeft;
	private Vector2 nextTurnPos;
	private float startBarPosX;

	public float iconsLeft = 7;
	public float iconsToStart = 7;
	public Image iconsLeftSquare;
	public Image iconsLeftSquare2;

	bool wheelPhase = true;
	public WheelPanelScript buttonPanel;
	public WheelPanelScript wheelPanel;

	public GameObject scrollList;

	public GameObject gearIcon;
	public GameObject forwardIconPink;
	public GameObject leftIconPink;
	public GameObject rightIconPink;
	public GameObject forwardIconBlue;
	public GameObject leftIconBlue;
	public GameObject rightIconBlue;
	public GameObject wheelIconPink;
	public GameObject wheelIconBlue;
	public GameObject napIconPink;
	public GameObject napIconBlue;
	public GameObject emptyPrefab;

	public GameObject waitScreen;
	//public GameObject wheelfromBuddy;

	private Vector3 origPos;
	private Vector3 origRot;

	bool rotating = false;
	private int direction = 1;
	public string networkNum;

	public bool placedIcons = false;
	public bool receivedIcons = false;
	public bool doneWithBubble = false;
	public bool preview = false;
	public GameObject readyToSeeSign;
	public GameObject loseScreen;
	public GameObject winScreen;

	private LineRenderer pinkLine;
	private LineRenderer blueLine;
	private int pinkPoints;
	private int bluePoints;

	private GameObject myCamera;

	public int[] fromPinkGhost = {0,0,0,0,0,0,0,0,0,0,0};
	public int[] fromBlueGhost = {0,0,0,0,0,0,0,0,0,0,0};
	public int[] fromPinkGhostColors = {0,0,0};
	public int[] fromBlueGhostColors = {0,0,0};

	public int[] colorArray = {95,95,95};


	public GameObject[] blueGhostIcon = new GameObject[11];
	public GameObject[] pinkGhostIcon = new GameObject[11];
	private GameObject[] iconList = new GameObject[20];
	public GameObject[] combinedIcons = new GameObject[14];

	public GameObject myBackground;

	//placed icons, if 


	public override void OnStartLocalPlayer() {

		barRect = turnBar.GetComponent<RectTransform> ();
		barRectEnd = turnBarEnd.GetComponent<RectTransform> ();
		startBarPosX = barRect.anchoredPosition.x;

		iconList [0] = null;
		iconList [1] = forwardIconBlue;
		iconList [2] = rightIconBlue;
		iconList [3] = leftIconBlue;
		iconList [4] = wheelIconBlue;
		iconList [5] = napIconBlue;

		iconList [10] = gearIcon;

		iconList [11] = forwardIconPink;
		iconList [12] = rightIconPink;
		iconList [13] = leftIconPink;
		iconList [14] = wheelIconPink;
		iconList [15] = napIconPink;

		networkNum = netId.ToString();
		totalTurns = turnsLeft;

		GameObject playerOneSpawn = GameObject.FindGameObjectWithTag ("Spawn One");
		pinkGhost = Instantiate(Resources.Load("Pink Ghost", typeof(GameObject))) as GameObject;
		pinkGhost.transform.position = playerOneSpawn.transform.position;
		pinkGhost.transform.rotation = playerOneSpawn.transform.rotation;
		pinkGhost.GetComponent<GhostScript> ().AssignCameras (networkNum);

		pinkLine = pinkGhost.GetComponent<GhostScript> ().lineRend;
//		Vector3 pinkPoint = new Vector3 (pinkGhost.transform.position.x, .15f, pinkGhost.transform.position.z);
//		pinkPoints = 1;
//		pinkLine.numPositions = pinkPoints;
//		pinkLine.SetPosition (pinkPoints - 1, pinkPoint);

		if (networkNum == "1") {
			myGhostScript = pinkGhost.GetComponent<GhostScript> ();
		}

		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		blueGhost = Instantiate(Resources.Load("Blue Ghost", typeof(GameObject))) as GameObject;
		blueGhost.transform.position = playerTwoSpawn.transform.position;
		blueGhost.transform.rotation = playerTwoSpawn.transform.rotation;
		blueGhost.GetComponent<GhostScript> ().AssignCameras (networkNum);

		blueLine = blueGhost.GetComponent<GhostScript> ().lineRend;
//		Vector3 bluePoint = new Vector3 (blueGhost.transform.position.x, .15f, blueGhost.transform.position.z);
//		bluePoints = 1;
//		blueLine.numPositions = bluePoints;
//		blueLine.SetPosition (bluePoints - 1, bluePoint);

		if (networkNum == "2") {
			myGhostScript = blueGhost.GetComponent<GhostScript> ();
		}

		ghosts [0] = blueGhost;
		ghosts [1] = pinkGhost;

		for(int i = 0; i < colorArray.Length; i++)
		{
			colorArray[i] = 95;
		}

	}

	void Update (){

		if (!isLocalPlayer)
		{
			return;
		}

		if (moveLeft == true) {

			Vector2 pos = new Vector2 (barRect.anchoredPosition.x,
				barRect.anchoredPosition.y);

			barRect.anchoredPosition = Vector2.Lerp (pos, nextTurnPos, Time.deltaTime * 2.0f);

			if (pos.x - nextTurnPos.x < 3) {
				moveLeft = false;
				barRect.anchoredPosition = nextTurnPos;
			}

		}

		turnsText.text = turnsLeft.ToString ();

		if ((placedIcons == true) && (receivedIcons == true) && (doneWithBubble == true)) {
		
			TellOtherPlayerNotReady ();
			ReadyToSee ();
			placedIcons = false;
			receivedIcons = false;
			doneWithBubble = false;
		
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

		if (backPinkGhost == true) {

			pinkGhostPos = new Vector3 (pinkGhost.transform.position.x,
				pinkGhost.transform.position.y,
				pinkGhost.transform.position.z);

			pinkGhost.transform.position = Vector3.Lerp (pinkGhostPos, pinkStartPos, Time.deltaTime * 14);

			if (pinkGhost.transform.position == pinkStartPos) {

				backPinkGhost = false;

			}
		}

		if (backwardPinkGhost == true) {

			pinkGhostPos = new Vector3 (pinkGhost.transform.position.x,
				pinkGhost.transform.position.y,
				pinkGhost.transform.position.z);

			pinkGhost.transform.position = Vector3.Lerp (pinkGhostPos, pinkBackOnePos, Time.deltaTime * 14);

			if (pinkGhost.transform.position == pinkBackOnePos) {

				backwardPinkGhost = false;

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

		if (backBlueGhost == true) {

			blueGhostPos = new Vector3 (blueGhost.transform.position.x,
				blueGhost.transform.position.y,
				blueGhost.transform.position.z);

			blueGhost.transform.position = Vector3.Lerp (blueGhostPos, blueStartPos, Time.deltaTime * 14);

			if (blueGhost.transform.position == blueStartPos) {

				backBlueGhost = false;

			}
		}

		if (backwardBlueGhost == true) {

			blueGhostPos = new Vector3 (blueGhost.transform.position.x,
				blueGhost.transform.position.y,
				blueGhost.transform.position.z);

			blueGhost.transform.position = Vector3.Lerp (blueGhostPos, blueBackOnePos, Time.deltaTime * 14);

			if (blueGhost.transform.position == blueBackOnePos) {

				backwardBlueGhost = false;

			}
		}
	}

	public void PlaceWheelIcon () {
	
		DeleteEmpty ();

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (wheelIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			newTrigger.transform.SetAsFirstSibling ();
			Invoke ("TurnDone", 0.5f);
		}

		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (wheelIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			newTrigger.transform.SetAsFirstSibling ();
			Invoke ("TurnDone", 0.5f);
		}
	
	}

	void ButtonCushion () {
	
		buttonCushion = false;

	}

	public void PlaceForwardGhost(){

		if (iconsLeft < 1 || buttonCushion == true) {
			return;
		}

		DeleteEmpty ();
	
		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", .6f);

		if (networkNum == "1") {
	
			GameObject newTrigger = Instantiate (forwardIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			ForwardPink ();

		}

		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (forwardIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			ForwardBlue ();
		}
	}

	public void PlaceRightIcon() {

		if (iconsLeft < 1 || buttonCushion == true) {
			return;
		}

		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", .6f);

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (rightIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			RotateRightPink ();

		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (rightIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			RotateRightBlue ();

		}
	
	}

	public void PlaceLeftIcon() {

		if (iconsLeft < 1 || buttonCushion == true) {
			return;
		}

		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", .6f);

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (leftIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			RotateLeftPink ();
		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (leftIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);

			RotateLeftBlue ();
		}
	
	}

	public void PlaceNaptIcon() {

		if (iconsLeft < 1 || buttonCushion == true) {
			return;
		}

		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", .6f);

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (napIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);
		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (napIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
			float sibCount = iconsToStart - (iconsLeft + 1);
			newTrigger.transform.SetSiblingIndex ((int)sibCount);
		}

	}

	void DeleteEmpty(){

		GameObject empty = GameObject.FindGameObjectWithTag ("Empty");
		Destroy (empty);
	}
		

	public void ForwardPink () {

		pinkStartPos = pinkGhost.transform.position;
		pinkEndPos = pinkGhost.transform.position - pinkGhost.transform.forward;
		forwardPinkGhost = true;
		StraightenGhost ();

		if (preview == true) {
			
			AddLinePointsPink (pinkEndPos);
			if (pinkShell.activeInHierarchy == false) {
				PinkShellAppear ();
			}
		}

	}

	public void BackwardPink (){
		
		StraightenGhost ();
		pinkStartPos = pinkGhost.transform.position;
		pinkBackOnePos = pinkGhost.transform.position + pinkGhost.transform.forward;
		backwardPinkGhost = true;
		DeletePoint ();

	}

	public void RotateRightPink () {

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, .5f));
		StraightenGhost ();

	}

	public void RotateLeftPink () {
		
		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, .5f));
		StraightenGhost ();

	}

	public void ForwardBlue () {

		blueStartPos = blueGhost.transform.position;
		blueEndPos = blueGhost.transform.position - blueGhost.transform.forward;
		forwardBlueGhost = true;
		StraightenGhost ();

		if (preview == true) {
			AddLinePointsBlue (blueEndPos);
			if (blueShell.activeInHierarchy == false) {
				BlueSheelAppear ();
			}
		}

	}

	public void BackwardBlue () {

		StraightenGhost ();
		blueStartPos = blueGhost.transform.position;
		blueBackOnePos = blueGhost.transform.position + blueGhost.transform.forward;
		backwardBlueGhost = true;
		DeletePoint ();

	}

	public void RotateRightBlue () {
		
		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, .5f));
		StraightenGhost ();
	}

	public void RotateLeftBlue () {
		
		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, .5f));
		StraightenGhost ();

	}

	void PinkShellAppear () {

		pinkShell.SetActive (true);
		pinkShell.transform.position = origPos;
		pinkShell.transform.eulerAngles = origRot;

	}

	void BlueSheelAppear () {

		blueShell.SetActive (true);
		blueShell.transform.position = origPos;
		blueShell.transform.eulerAngles = origRot;

	}

	void AddLinePointsPink (Vector3 newPoint){

		Vector3 pinkPoint = new Vector3 (newPoint.x, .15f, newPoint.z);

		pinkPoints++;
		pinkLine.numPositions = pinkPoints;
		pinkLine.SetPosition (pinkPoints - 1, pinkPoint);
	
	}

	void AddLinePointsBlue (Vector3 newPoint){

		Vector3 bluePoint = new Vector3 (newPoint.x, .15f, newPoint.z);

		bluePoints++;
		blueLine.numPositions = bluePoints;
		blueLine.SetPosition (bluePoints - 1, bluePoint);

	}

	public void DeletePoint (){

		Debug.Log ("DeletePoint");

		if (networkNum == "2") {
			bluePoints--;
			blueLine.numPositions = bluePoints;
		} else if (networkNum == "1") {
			pinkPoints--;
			pinkLine.numPositions = pinkPoints;
		}

	}

	void RestartLineRender (){
	
		bluePoints = 0;
		pinkPoints = 0;
		pinkLine.numPositions = pinkPoints;
		blueLine.numPositions = bluePoints;
	
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

//	public void BackToOriginalPositionPink () {
//
//		if (gameLost == false) {
//			pinkGhost.transform.position = origPos;
//			pinkGhost.transform.eulerAngles = origRot;
//		}
//
//	}
//
//	public void BackToOriginalPositionBlue () {
//
//		if (gameLost == false) {
//			blueGhost.transform.position = origPos;
//			blueGhost.transform.eulerAngles = origRot;
//		}
//
//	}

	void BackToOriginalPosition(){

		if (networkNum == "1") {
			pinkGhost.transform.position = origPos;
			pinkGhost.transform.eulerAngles = origRot;
			pinkShell.SetActive (false);
		}

		if (networkNum == "2") {
			blueGhost.transform.position = origPos;
			blueGhost.transform.eulerAngles = origRot;
			blueShell.SetActive (false);
		}
			
	}

	void RecordPosition(){

		if (networkNum == "1") {
			origPos = pinkGhost.transform.position;
			origRot = pinkGhost.transform.eulerAngles;
		}

		if (networkNum == "2") {
			origPos = blueGhost.transform.position;
			origRot = blueGhost.transform.eulerAngles;
		}

	}
		
//	public void PlaybackButton () {
//
//		if (networkNum == "1") {
//			origPos = pinkGhost.transform.position;
//			origRot = pinkGhost.transform.eulerAngles;
//			GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
//			int triggerCount = triggers.Length;
//			float seconds = 0;
//			for (int i = 0; i < triggerCount; i++) {
//				triggers [i].GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
//				seconds = seconds + 1.9f;
//				if (i == (triggerCount - 1)) {
//					Invoke ("BackToOriginalPositionPink", seconds + 1);
//				}
//			}
//		}
//
//		if (networkNum == "2") {
//			origPos = blueGhost.transform.position;
//			origRot = blueGhost.transform.eulerAngles;
//			GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
//			int triggerCount = triggers.Length;
//			float seconds = 0;
//			for (int i = 0; i < triggerCount; i++) {
//				triggers [i].GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
//				seconds = seconds + 1.9f;
//				if (i == (triggerCount - 1)) {
//					Invoke ("BackToOriginalPositionBlue", seconds + 1);
//				}
//			}
//		}
//
//	}

	void PlayCombinedIcons () {

		GameObject[] triggersPlusNull = GameObject.FindGameObjectsWithTag ("Trigger");

		for (int i = 0; i < triggersPlusNull.Length; i++) {

			if (triggersPlusNull [i].name == "NullObject(Clone)") {

				Destroy (triggersPlusNull [i]);
				}
			}

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		float seconds = 0;

		for (int i = 0; i < triggers.Length; i++) {
			seconds = seconds + 1.9f;
			scrollList.transform.GetChild(i).gameObject.GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
			if (i == triggers.Length - 1) {
			
				Invoke ("BeginNewTurn", seconds + 2.0f);
			
			}
		}
	}

	public void PlayGearItems () {

		GameObject[] gearItems = GameObject.FindGameObjectsWithTag ("Gear Item");
		for (int i = 0; i < gearItems.Length; i++) {
			gearItems [i].GetComponent<WindmillScript> ().RotateWindmill ();
		}

		GameObject[] evilGhosts = GameObject.FindGameObjectsWithTag ("Slime");
		for (int i = 0; i < evilGhosts.Length; i++) {
			evilGhosts [i].GetComponent<SlimeScript> ().RotateSlime ();
		}


	}

	//Finished placing moves
	public void TurnDone () {

		GameObject[] empties = GameObject.FindGameObjectsWithTag ("Empty");

		if (empties.Length > 0) {
		
			for (int i = 0; i < empties.Length; i++) {
				Destroy (empties [i]);
			}
		
		}
			
		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");

		waitScreen.SetActive (true);

		if (wheelPhase == false) {
		
			buttonPanel.MoveDown ();
			preview = false;
			BackToOriginalPosition ();
			myGhostScript.PreviewModeOff ();
			RestartLineRender ();

		} else if (wheelPhase == true && triggers.Length == 0) {
		
			wheelPanel.MoveDown ();

		}

		if (networkNum == "2" && otherPlayerReady == true) {

			SendArray ();

		} else if (networkNum == "2" && otherPlayerReady == false) {

			InvokeRepeating ("WaitingForOtherPlayer", 1.0f, 1.0f);

		} else if (networkNum == "1" && bluePlayerThere == true && otherPlayerReady == true) {
		
			SendArray ();

		} else if (networkNum == "1" && bluePlayerThere == true && otherPlayerReady == false) {

			InvokeRepeating ("WaitingForOtherPlayer", 1.0f, 1.0f);

		} else if (networkNum == "1" && bluePlayerThere == false) {
		
			InvokeRepeating ("LookingForBlue", 1.0f, 1.0f);

		}
	}

	void LookingForBlue () {
	
		if (bluePlayerThere == true) {

			CancelInvoke ();
			SendArray ();
		}
	
	}

	void WaitingForOtherPlayer () {

		if (otherPlayerReady == true) {

			CancelInvoke ();
			SendArray ();
		}

	}

	void SendArray () {

		Debug.Log ("Send Array Happened");

		if (receivedIcons == false) {
		
			speechPanel.GetComponent<SpeechBubbleScript> ().LoadBubbles ();
		
		}

		if (wheelPhase == false) {
			deleteNull.SetActive (true);
		}

		placedIcons = true;
		//Counts number of icons
		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;
		int triggerOrder = 0;

		//in case blank is sent
		if (triggerCount == 0) {

			if (networkNum == "2") {
				CmdBlueSendArray (fromBlueGhost);

				CmdBlueSendColorArray (colorArray);
			}

			if (networkNum == "1") {
				RpcPinkSendColorArray (colorArray);
				Debug.Log ("Send Array1: " + triggerCount);
				RpcPinkSendArray (fromPinkGhost);
			}
		}

		//Places int icons into an array (fromBlueGhost) along with the order
		for(int i = 0; i < triggerCount; i++)
		{
			triggers[i].GetComponent<TriggerScript>().TriggerSend(triggerOrder);
			triggerOrder++;
			if (i == triggerCount - 1 && networkNum == "2") {
				//Sends the array the other self on server
				CmdBlueSendArray (fromBlueGhost);
				CmdBlueSendColorArray (colorArray);
			}

			if (i == triggerCount - 1 && networkNum == "1") {
				RpcPinkSendColorArray (colorArray);
				Debug.Log ("Send Array2: " + triggerCount);
				//Sends the array the other self on server
				RpcPinkSendArray (fromPinkGhost);

			}
		}
	}

	//Turns the blue int array into prefab array (blueGhostIcon)
	public void FillBlueArray () {

		receivedIcons = true;

		for(int i = 0; i < fromBlueGhost.Length; i++)
		{
			blueGhostIcon [i] = iconList [fromBlueGhost [i]];

		}
	}

	//Turns the blue int array into prefab array (blueGhostIcon)
	public void FillPinkArray () {

		Debug.Log ("FillPinkArray");

		receivedIcons = true;

		for(int i = 0; i < fromPinkGhost.Length; i++)
		{
			pinkGhostIcon [i] = iconList [fromPinkGhost [i]];

		}
	}


	void ReadyToSee () {
		
		readyToSeeSign.SetActive (true);

	}

	public void CombineLists (){
		
		waitScreen.SetActive (false);
		readyToSeeSign.SetActive (false);
			
		if (networkNum == "1") {
			int sibDex = 1;
			for (int i = 0; i < blueGhostIcon.Length; i++) {
				if (blueGhostIcon [i] == null) {
					PlaceGearIcons ();
					return;
				} 
				GameObject newTrigger = Instantiate (blueGhostIcon [i], new Vector3 (0, 0, 0), Quaternion.identity);
				newTrigger.transform.SetParent (scrollList.transform, false);
				newTrigger.transform.SetSiblingIndex (i + sibDex);
				sibDex++;
			}
		}

		if (networkNum == "2") {
			int sibDex = 0;
			for (int i = 0; i < pinkGhostIcon.Length; i++) {
				if (pinkGhostIcon [i] == null) {
					PlaceGearIcons ();
					return;
				} 

				GameObject newTrigger = Instantiate (pinkGhostIcon [i], new Vector3 (0, 0, 0), Quaternion.identity);
				newTrigger.transform.SetParent (scrollList.transform, false);
				newTrigger.transform.SetSiblingIndex (i + sibDex);
				sibDex++;

			}
		}
	}

	void PlaceGearIcons () {

		if (wheelPhase == true) {
		
			PlayCombinedIcons ();

			int pinkTotWheel = 0;
			int blueTotWheel = 0;

			for (int i = 0; i < fromBlueGhost.Length; i++) {

				if (fromBlueGhost [i] > 0) {
					blueTotWheel++;
				}
			}

			for (int i = 0; i < fromPinkGhost.Length; i++) {

				if (fromPinkGhost [i] > 0) {
					pinkTotWheel++;
				}
			}

			if (blueTotWheel == 0 && pinkTotWheel == 0) {

				BeginNewTurn ();

			}
		
		} else {

			int pinkTot = 0;
			int blueTot = 0;

			for (int i = 0; i < fromBlueGhost.Length; i++) {

				if (fromBlueGhost [i] > 0) {
					blueTot++;
				}
			}

			for (int i = 0; i < fromPinkGhost.Length; i++) {

				if (fromPinkGhost [i] > 0) {
					pinkTot++;
				}
			}

			if (blueTot == 0 && pinkTot == 0) {
			
				BeginNewTurn ();

			}
				
			int diff = blueTot - pinkTot;

			if (diff == 0) {
				
				GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
				int gearCount = Mathf.CeilToInt ((triggers.Length + 1) / 2); 

				int sibDex = 2;

				for (int i = 0; i < gearCount; i++) {

					GameObject newGear = Instantiate (gearIcon, new Vector3 (0, 0, 0), Quaternion.identity);
					newGear.transform.SetParent (scrollList.transform, false);
					newGear.transform.SetSiblingIndex (i + sibDex);
					sibDex = sibDex + 2;

//					if (i == gearCount - 1) {
//
//						PlayCombinedIcons ();
//
//					}

				}
			}

			//more blue than pink
				if (diff > 0) {
				
				int sibDex = 2;

				for (int i = 0; i < pinkTot; i++) {

					GameObject newGear = Instantiate (gearIcon, new Vector3 (0, 0, 0), Quaternion.identity);
					newGear.transform.SetParent (scrollList.transform, false);
					newGear.transform.SetSiblingIndex (i + sibDex);
					sibDex = sibDex + 2;

				}

				sibDex = sibDex - 1;

				for (int i = pinkTot; i < blueTot; i++) {

					GameObject newGear = Instantiate (gearIcon, new Vector3 (0, 0, 0), Quaternion.identity);
					newGear.transform.SetParent (scrollList.transform, false);
					newGear.transform.SetSiblingIndex (i + sibDex);
					sibDex = sibDex + 1;



				}

			}

			//more pink
			if (diff < 0) {

				int sibDex = 2;

				for (int i = 0; i < blueTot; i++) {

					GameObject newGear = Instantiate (gearIcon, new Vector3 (0, 0, 0), Quaternion.identity);
					newGear.transform.SetParent (scrollList.transform, false);
					newGear.transform.SetSiblingIndex (i + sibDex);
					sibDex = sibDex + 2;

				}

				sibDex = sibDex - 1;

				for (int i = blueTot; i < pinkTot; i++) {

					GameObject newGear = Instantiate (gearIcon, new Vector3 (0, 0, 0), Quaternion.identity);
					newGear.transform.SetParent (scrollList.transform, false);
					newGear.transform.SetSiblingIndex (i + sibDex);
					sibDex = sibDex + 1;

				}
			}

			pinkTot = 0;
			blueTot = 0;
			PlayCombinedIcons ();


		}
	}

	void BeginNewTurn () {
		//Debug.Log ("winningbool: " + winning);

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;

		for(int i = 0; i < triggerCount; i++)
		{
			Destroy (triggers[i]);
		}


		if (wheelPhase == true) {
			wheelPhase = false;
			if (turnsLeft > 0) {
				deleteNull.SetActive (false);
				buttonPanel.MoveUp ();
				preview = true;
				RecordPosition ();
				myGhostScript.PreviewModeOn ();
				AddLinePointsBlue (blueGhost.transform.position);
				AddLinePointsPink (pinkGhost.transform.position);

				for (int i = 0; i < 7; i++) {

					GameObject empty = Instantiate (emptyPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
					empty.transform.SetParent (scrollList.transform, false);

				}

			}

			for(int i = 0; i < colorArray.Length; i++)
			{
				colorArray[i] = 95;
			}
				
		} else {

			GameObject empty = Instantiate (emptyPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			empty.transform.SetParent (scrollList.transform, false);

			iconsLeft = iconsToStart;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
			iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);

			turnsLeft = turnsLeft - 1.0f;

			MoveTurnBar ();

			//timerCircle.fillAmount = (turnsLeft / totalTurns);
		
			wheelPhase = true;
			if (turnsLeft > 0) {
				wheelPanel.MoveUp ();
			}
			if (turnsLeft == 0 && winning == false) {

				loseScreen.SetActive (true);

			}

			if (winning == true) {
				winScreen.SetActive (true);
			}


		}
			
		ClearArray ();

		//Debug.Log ("turnsLeft: " + turnsLeft + "totalTurns: " + totalTurns + " fillAmount " + (turnsLeft / totalTurns));




		TellOtherPlayerReady ();
	}

	void MoveTurnBar(){

		if (moveLeft == true) {
			return;
		}
			
		float xPos;
		xPos = barRect.anchoredPosition.x + ((startBarPosX + barRectEnd.anchoredPosition.x)/totalTurns);

		nextTurnPos = new Vector2 (xPos ,barRect.anchoredPosition.y);

		moveLeft = true;

	}

	public void Delete(){
	
		if (wheelPhase == true || placedIcons == true || buttonCushion == true) {
			return;
		}

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;
		triggers [triggerCount - 1].GetComponent<TriggerScript> ().UndoAction ();
		Destroy (triggers[triggerCount - 1]);

		if (wheelPhase == true) {
		
			wheelPanel.MoveUp ();

		} else {
		
			iconsLeft++;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
			iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		
		}

		GameObject empty = Instantiate (emptyPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		empty.transform.SetParent (scrollList.transform, false);
		buttonCushion = true;
		Invoke ("ButtonCushion", .6f);

	}

	public void LoseGame() {
	
		loseScreen.SetActive (true);
	
	}

	public void FillPinkColorArray () {

		if (networkNum == "2") {
			//wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillPinkColorArray ();
			myBackground.GetComponent<ColorCodeScript> ().FillPinkColorArray ();
		}

	}

	public void FillBlueColorArray () {

		if (networkNum == "1") {
			//wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillBlueColorArray ();
			myBackground.GetComponent<ColorCodeScript> ().FillBlueColorArray ();
		}
	}

	//sends blue array to server game manager
	[Command]
	public void CmdBlueSendArray (int[] blueArray){
		//fromBlueGhost = blueArray;

		GameObject localGameManager = GameObject.FindWithTag ("Game Manager Local");
		localGameManager.GetComponent<TurnManagerScript> ().fromBlueGhost = blueArray;
		localGameManager.GetComponent<TurnManagerScript> ().FillBlueArray ();

	}

	//sends pink array to server game manager
	[ClientRpc]
	public void RpcPinkSendArray (int[] pinkArray){
		//fromBlueGhost = blueArray;
		Debug.Log ("RpcPinkSendArray: " + networkNum);
		if (networkNum != "1") {
			GameObject localGameManager = GameObject.FindWithTag ("Game Manager Local");
			localGameManager.GetComponent<TurnManagerScript> ().fromPinkGhost = pinkArray;
			localGameManager.GetComponent<TurnManagerScript> ().FillPinkArray ();
		}
	}

	[Command]
	public void CmdBlueSendColorArray (int[] blueColorArray) {

		//GameObject wheelManager = GameObject.FindWithTag ("Signal Circle Message");
		//wheelManager.GetComponent<WheelFromBuddyScript> ().fromBlueGhostColors = blueColorArray;
		GameObject theirBackground = GameObject.FindGameObjectWithTag ("Color Spiral 1");
		theirBackground.GetComponent<ColorCodeScript> ().fromBlueGhostColors = blueColorArray;
	
	}

	//sends pink array to server game manager
	[ClientRpc]
	public void RpcPinkSendColorArray (int[] pinkColorArray){
		//fromBlueGhost = blueArray;

		Debug.Log ("RpcPinkSendColorArray: " + networkNum);

		if (networkNum != "1") {

			//GameObject wheelManager = GameObject.FindWithTag ("Signal Circle Message");
			//wheelManager.GetComponent<WheelFromBuddyScript> ().fromPinkGhostColors = pinkColorArray;
			GameObject theirBackground = GameObject.FindGameObjectWithTag ("Color Spiral 2");
			theirBackground.GetComponent<ColorCodeScript> ().fromPinkGhostColors = pinkColorArray;

		}
	}

	void StraightenGhost () {

		for (int i = 0; i < 2; i++) {
			
			float newXPos = Mathf.Round (ghosts[i].transform.position.x);
			float newZPos = Mathf.Round (ghosts[i].transform.position.z);
			Vector3 newPos = new Vector3 (newXPos, ghosts[i].transform.position.y, newZPos);
			ghosts[i].transform.position = newPos;

			float roundedAngle = (Mathf.Round (ghosts[i].transform.eulerAngles.y / 90)) * 90;
			Vector3 newAngle = new Vector3 (ghosts[i].transform.eulerAngles.x, roundedAngle, ghosts[i].transform.eulerAngles.z);
			ghosts[i].transform.eulerAngles = newAngle;
		}
	
	
	}

	void ClearArray (){

		for (int i = 0; i < fromPinkGhost.Length; i++) {

			fromPinkGhost [i] = 0;
			fromBlueGhost [i] = 0;
			blueGhostIcon [i] = null;
			pinkGhostIcon [i] = null;

		}
				
	}

	public void BluePlayerThere (){

		bluePlayerThere = true;

	}


	void TellOtherPlayerReady () {
	
		Debug.Log ("told them i'm ready");

		if (networkNum == "1") {
			RpcTellOtherPlayerReady ();
		} else 
		
		{
		
			CmdTellOtherPlayerReady ();
		
		}
			
	}
	[ClientRpc]
	public void RpcTellOtherPlayerReady (){

		TurnManagerScript localManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();

		if (localManager.networkNum == "2") { 
				localManager.otherPlayerReady = true;
		}

	}

	[Command]
	public void CmdTellOtherPlayerReady (){

		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().otherPlayerReady = true;
	}

	void TellOtherPlayerNotReady () {

		if (networkNum == "1") {
			RpcTellOtherPlayerNotReady ();
		} else 

		{

			CmdTellOtherPlayerNotReady ();

		}

	}
	[ClientRpc]
	public void RpcTellOtherPlayerNotReady (){

		TurnManagerScript localManager = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();

		if (localManager.networkNum == "2") { 
			localManager.otherPlayerReady = false;
		}

	}

	[Command]
	public void CmdTellOtherPlayerNotReady (){

		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().otherPlayerReady = false;
	}

	[Command]
	public void CmdSendBubble(string phrase, string ghostNum) {
	
		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript>().RpcSendBubble (phrase, ghostNum);
		Debug.Log ("Got to CMD bubble: " + phrase + ghostNum);

	}

	[ClientRpc]
	public void RpcSendBubble(string phrase, string ghostNum) {

		Debug.Log ("Got to RPC bubble: " + phrase + ghostNum);

		Invoke ("BubblesDone", 4.5f);

		if (ghostNum == "1") {
			GameObject ghost = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().pinkGhost;
			ghost.GetComponent<GhostScript> ().ShowBubble (phrase);
		} else {
			GameObject ghost = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().blueGhost;
			ghost.GetComponent<GhostScript> ().ShowBubble (phrase);
		}

	}

	public void BubblesDone(){
	
		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().doneWithBubble = true;
	
	}
}