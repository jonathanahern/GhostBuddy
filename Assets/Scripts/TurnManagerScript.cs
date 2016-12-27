﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TurnManagerScript : NetworkBehaviour {

	public GameObject pinkGhost;
	public GameObject blueGhost;

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

	public bool forwardPinkGhost = false;
	public bool forwardBlueGhost = false;
	public bool backPinkGhost = false;
	public bool backBlueGhost = false;
	public bool gameLost = false;

	public float turnsLeft;
	public Text turnsText;
	public Image timerCircle;
	float totalTurns;

	public float iconsLeft = 10;
	public float iconsToStart = 10;
	public Image iconsLeftSquare;

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

	public GameObject waitScreen;
	public GameObject wheelfromBuddy;

	private Vector3 origPos;
	private Vector3 origRot;

	bool rotating = false;
	private int direction = 1;
	string networkNum = "10";

	public bool placedIcons = false;
	public bool receivedIcons = false;
	public GameObject readyToSeeSign;
	public GameObject loseScreen;
	public GameObject winScreen;

	public int[] fromPinkGhost = {0,0,0,0,0,0,0,0,0,0,0};
	public int[] fromBlueGhost = {0,0,0,0,0,0,0,0,0,0,0};
	public int[] fromPinkGhostColors = {0,0,0};
	public int[] fromBlueGhostColors = {0,0,0};

	public int[] colorArray = {95,95,95};


	public GameObject[] blueGhostIcon = new GameObject[11];
	public GameObject[] pinkGhostIcon = new GameObject[11];
	private GameObject[] iconList = new GameObject[20];
	public GameObject[] combinedIcons = new GameObject[14];


	public override void OnStartLocalPlayer() {

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

		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		blueGhost = Instantiate(Resources.Load("Blue Ghost", typeof(GameObject))) as GameObject;
		blueGhost.transform.position = playerTwoSpawn.transform.position;
		blueGhost.transform.rotation = playerTwoSpawn.transform.rotation;

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

		turnsText.text = turnsLeft.ToString ();

		if ((placedIcons == true) && (receivedIcons == true)) {
		
			TellOtherPlayerNotReady ();
			ReadyToSee ();
			placedIcons = false;
			receivedIcons = false;
		
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
	}

	public void PlaceWheelIcon () {
	
		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (wheelIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}

		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (wheelIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}


	
	}

	public void PlaceForwardGhost(){

		if (iconsLeft < 1) {
			return;
		}
	
		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);

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

		if (iconsLeft < 1) {
			return;
		}

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);

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

		if (iconsLeft < 1) {
			return;
		}

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (leftIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (leftIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
	
	}

	public void PlaceNaptIcon() {

		if (iconsLeft < 1) {
			return;
		}

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);

		if (networkNum == "1") {

			GameObject newTrigger = Instantiate (napIconPink, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}
		if (networkNum == "2") {

			GameObject newTrigger = Instantiate (napIconBlue, new Vector3 (0, 0, 0), Quaternion.identity);
			newTrigger.transform.SetParent (scrollList.transform, false);
		}

	}
		

	public void ForwardPink () {
		

		pinkStartPos = pinkGhost.transform.position;
		pinkEndPos = pinkGhost.transform.position - pinkGhost.transform.forward;
		forwardPinkGhost = true;
		StraightenGhost ();

	}

	public void RotateRightPink () {
		

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, 1f));
		StraightenGhost ();
	}

	public void RotateLeftPink () {
		
		
		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, 1f));
		StraightenGhost ();

	}

	public void ForwardBlue () {
		

		blueStartPos = blueGhost.transform.position;
		blueEndPos = blueGhost.transform.position - blueGhost.transform.forward;
		forwardBlueGhost = true;
		StraightenGhost ();

	}

	public void RotateRightBlue () {
		

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, 1f));
		StraightenGhost ();
	}

	public void RotateLeftBlue () {
		

		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, 1f));
		StraightenGhost ();

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

		if (gameLost == false) {
			pinkGhost.transform.position = origPos;
			pinkGhost.transform.eulerAngles = origRot;
		}

	}

	public void BackToOriginalPositionBlue () {

		if (gameLost == false) {
			blueGhost.transform.position = origPos;
			blueGhost.transform.eulerAngles = origRot;
		}

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
				seconds = seconds + 1.9f;
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
				seconds = seconds + 1.9f;
				if (i == (triggerCount - 1)) {
					Invoke ("BackToOriginalPositionBlue", seconds + 1);
				}
			}
		}

	}

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
	}

	//Finished placing moves
	public void TurnDone () {

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");

		waitScreen.SetActive (true);

		if (wheelPhase == false) {
		
			buttonPanel.MoveDown ();

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

//			if (i == fromBlueGhost.Length - 1) {
//			
//				receivedIcons = true;
//
//			}
		}
	}

	//Turns the blue int array into prefab array (blueGhostIcon)
	public void FillPinkArray () {

		receivedIcons = true;

		for(int i = 0; i < fromPinkGhost.Length; i++)
		{
			pinkGhostIcon [i] = iconList [fromPinkGhost [i]];

//			if (i == fromPinkGhost.Length - 1) {
//
//				receivedIcons = true;
//
//			}
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
			Debug.Log (diff);

			if (diff == 0) {
				Debug.Log ("equal happened");
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


		if (wheelPhase == true) {
			wheelPhase = false;
			if (turnsLeft > 0) {
				buttonPanel.MoveUp ();
			}
			for(int i = 0; i < colorArray.Length; i++)
			{
				colorArray[i] = 95;
			}
				
		} else {

			iconsLeft = iconsToStart;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);

			turnsLeft = turnsLeft - 1.0f;
			timerCircle.fillAmount = (turnsLeft / totalTurns);
		
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

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;

		for(int i = 0; i < triggerCount; i++)
		{
			Destroy (triggers[i]);
		}

		TellOtherPlayerReady ();
	}

	public void Delete(){
	
		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;

		Destroy (triggers[triggerCount - 1]);

		if (wheelPhase == true) {
		
			wheelPanel.MoveUp ();

		} else {
		
			iconsLeft++;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		
		}
	
	
	}

	public void LoseGame() {
	
		loseScreen.SetActive (true);
	
	}

	public void FillPinkColorArray () {

		if (networkNum == "2") {
			wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillPinkColorArray ();
		}

	}

	public void FillBlueColorArray () {

		if (networkNum == "1") {
			wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillBlueColorArray ();
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

		if (networkNum != "1") {
			GameObject localGameManager = GameObject.FindWithTag ("Game Manager Local");
			localGameManager.GetComponent<TurnManagerScript> ().fromPinkGhost = pinkArray;
			localGameManager.GetComponent<TurnManagerScript> ().FillPinkArray ();
		}
	}

	[Command]
	public void CmdBlueSendColorArray (int[] blueColorArray) {

		GameObject wheelManager = GameObject.FindWithTag ("Signal Circle Message");
		wheelManager.GetComponent<WheelFromBuddyScript> ().fromBlueGhostColors = blueColorArray;
	
	}

	//sends pink array to server game manager
	[ClientRpc]
	public void RpcPinkSendColorArray (int[] pinkColorArray){
		//fromBlueGhost = blueArray;

		if (networkNum != "1") {

			GameObject wheelManager = GameObject.FindWithTag ("Signal Circle Message");
			wheelManager.GetComponent<WheelFromBuddyScript> ().fromPinkGhostColors = pinkColorArray;

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

}