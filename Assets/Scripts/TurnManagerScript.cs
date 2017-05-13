using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class TurnManagerScript : NetworkBehaviour {

	private float actTime;

	public GameObject pinkGhost;
	public GameObject blueGhost;
	private GhostScript myGhostScript;
	public GameObject pinkShell;
	public GameObject blueShell;
	private GameObject myGhost;
	private GameObject theirGhost;
	private GhostScript pinkScript;
	private GhostScript blueScript;


	bool bluePlayerThere = false;
	public bool otherPlayerReady = true;

	public GameObject[] ghosts = new GameObject[2];

	public bool winning;
	private CameraRotation camerRot;

	private Vector3 pinkEndPos;
	private Vector3 pinkGhostPos;
	private Vector3 blueEndPos;
	private Vector3 blueGhostPos;
	private Vector3 pinkStartPos;
	private Vector3 blueStartPos;
	private Vector3 pinkBackOnePos;
	private Vector3 blueBackOnePos;
	Vector3 blueMidPos;
	Vector3 pinkMidPos;

	public GameObject speechPanel;

	public bool forwardPinkGhost = false;
	public bool forwardBlueGhost = false;
	public bool backPinkGhost = false;
	public bool backBlueGhost = false;
	bool backwardPinkGhost = false;
	bool backwardBlueGhost = false;
	public bool gameLost = false;
	bool buttonCushion = false;

	public float pointsTotal;
	public Text pointsText;
	public Image timerCircle;
	//float totalTurns;
	float turnPoints = 10.0f;

	public GameObject deleteNull;
	public GameObject turnBar;
	public GameObject turnBarEnd;
	private RectTransform barRect;
	private RectTransform barRectEnd;
	public bool moveLeft;
	private Vector2 nextTurnPos;
	private float startBarPosX;
	private Vector2 startPosBar;

	public float iconsLeft = 7;
	public float iconsToStart = 7;
	public Image iconsLeftSquare;
	public Image iconsLeftSquare2;

	bool wheelPhase = true;
	public WheelPanelScript buttonPanel;
	public WheelPanelScript wheelPanel;
	public ColorButtonScript colorBut;
	public RestartScript restartGame;

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
	GameObject readyToSeeSign;
	public GameObject pinkPlay;
	public GameObject bluePlay;
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

	public AudioSource source;
	public AudioSource source2;
	public AudioClip changeBackground;
	public AudioClip sendBackground;
	public AudioClip leftTurn;
	public AudioClip rightTurn;
	public AudioClip forwardSound;
	public AudioClip napSound;
	public AudioClip deleteSound;
	public AudioClip gear;
	public AudioClip doneSound;
	public AudioClip blip;
	public AudioClip allDone;
	public AudioClip beepTimer;

	private float pitchMin = .65f;
	private float pitchMax = .85f;

	public float toneVol;
	public AudioClip[] tones; 

	float timer;
	float timerStart;
	private int timerInt;
	public Text timerText;
	private bool timerGo = false;
	public Image timerOutline;
	bool timerSoundTime;

	public RectTransform starGold;
	public RectTransform starSilver;
	public RectTransform starBronze;

	public RectTransform startPoint;
	public RectTransform endPoint;

	public float starGoldPoint;
	public float starSilverPoint;
	public float starBronzePoint;

	bool goldHit;
	bool silverHit;
	bool bronzeHit;

	float timerTurn = 30.0f;
	bool timerTurnGo;

	private int idOne = 0;
	private int idTwo = 0;

//	GameObject[] gears;

	public override void OnStartLocalPlayer() {

		actTime = 1.0f;

		timerText.text = "-";
		timerStart = timer;
		barRect = turnBar.GetComponent<RectTransform> ();
		barRectEnd = turnBarEnd.GetComponent<RectTransform> ();
		startBarPosX = barRect.anchoredPosition.x;
		startPosBar = barRect.anchoredPosition;

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
		//totalTurns = turnsLeft;

		GameObject playerOneSpawn = GameObject.FindGameObjectWithTag ("Spawn One");
		pinkGhost = Instantiate(Resources.Load("Pink Ghost", typeof(GameObject))) as GameObject;
		pinkGhost.transform.position = playerOneSpawn.transform.position;
		pinkGhost.transform.rotation = playerOneSpawn.transform.rotation;
		pinkGhost.GetComponent<GhostScript> ().AssignCameras (networkNum);

		camerRot = GameObject.FindGameObjectWithTag ("Camera Center").GetComponent<CameraRotation> ();

		pinkLine = pinkGhost.GetComponent<GhostScript> ().lineRend;
//		Vector3 pinkPoint = new Vector3 (pinkGhost.transform.position.x, .15f, pinkGhost.transform.position.z);
//		pinkPoints = 1;
//		pinkLine.numPositions = pinkPoints;
//		pinkLine.SetPosition (pinkPoints - 1, pinkPoint);

		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		blueGhost = Instantiate(Resources.Load("Blue Ghost", typeof(GameObject))) as GameObject;
		blueGhost.transform.position = playerTwoSpawn.transform.position;
		blueGhost.transform.rotation = playerTwoSpawn.transform.rotation;
		blueGhost.GetComponent<GhostScript> ().AssignCameras (networkNum);

		blueLine = blueGhost.GetComponent<GhostScript> ().lineRend;

		pinkScript = pinkGhost.GetComponent<GhostScript> ();
		blueScript = blueGhost.GetComponent<GhostScript> ();
//		Vector3 bluePoint = new Vector3 (blueGhost.transform.position.x, .15f, blueGhost.transform.position.z);
//		bluePoints = 1;
//		blueLine.numPositions = bluePoints;
//		blueLine.SetPosition (bluePoints - 1, bluePoint);

		if (networkNum == "1") {
			myGhostScript = pinkGhost.GetComponent<GhostScript> ();
			myGhost = pinkGhost;
			theirGhost = blueGhost;
			readyToSeeSign = pinkPlay;
		}

		if (networkNum == "2") {
			myGhostScript = blueGhost.GetComponent<GhostScript> ();
			myGhost = blueGhost;
			theirGhost = pinkGhost;
			readyToSeeSign = bluePlay;
		}

//		gears = GameObject.FindGameObjectsWithTag ("Gear Item");
//		foreach (GameObject gear in gears) {
//		
//			gear.GetComponent<WindmillScript> ().pinkGhost = pinkGhost;
//			gear.GetComponent<WindmillScript> ().blueGhost = blueGhost;
//		
//		}

		pinkEndPos = pinkGhost.transform.position;
		blueEndPos = blueGhost.transform.position;
	//	Debug.Log ("b1"  + blueEndPos);

		Invoke("DelayedStart", 2.0f);

		ghosts [0] = blueGhost;
		ghosts [1] = pinkGhost;

		for(int i = 0; i < colorArray.Length; i++)
		{
			colorArray[i] = 95;
		}

		PlaceStars ();

	}

	void DelayedStart(){

		camerRot.CameraRotate (theirGhost);

	}

	void Update (){
//
//		if (Input.GetKeyDown(KeyCode.I)) {
//		
//			//source2.PlayOneShot (leftTurn, .2f);
//			StartTimer ("1");
//
//		}
//
//		if (Input.GetKeyDown(KeyCode.O)) {
//
//			source2.PlayOneShot (rightTurn, .2f);
//
//		}
//
//		if (Input.GetKeyDown(KeyCode.P)) {
//
//			source2.PlayOneShot (forwardSound, .2f);
//
//		}


		if (!isLocalPlayer)
		{
			return;
		}

		if (timerGo == true) {

			timer -= Time.deltaTime;
			timerInt = Mathf.RoundToInt (timer);
			timerText.text = timerInt.ToString ();
			timerOutline.fillAmount = timer / timerStart;

			if (timerInt == 5 && timerSoundTime == true) {
			
				timerSoundTime = false;
				source.PlayOneShot (beepTimer, .1f);
			
			}

			if (timer < .1) {
				timerGo = false;
				timerOutline.fillAmount = 0;
				timerText.text = "-";
				//Debug.Log ("timer done");
				if (wheelPhase == false) {
				
					TurnDone ();

				} else {

					if (colorBut.wedge [0] == 96) {
						colorBut.wedge [0] = 95;
					}

					if (colorBut.wedge [1] == 96) {
						colorBut.wedge [1] = 95;
					}

					if (colorBut.wedge [2] == 96) {
						colorBut.wedge [2] = 95;
					}
				
					//wheelPanel.MoveDown ();
					colorBut.BackToWhite ();
					//PlaceWheelIcon ();
				
				}
			}
		}

		if (timerTurnGo == true) {

			timerTurn -= Time.deltaTime;

			if (timerTurn < 1) {
				timerTurnGo = false;
				timerTurn = 0;
			}
		}


		if (moveLeft == true) {

			Vector2 pos = new Vector2 (barRect.anchoredPosition.x,
				barRect.anchoredPosition.y);

			barRect.anchoredPosition = Vector2.Lerp (pos, nextTurnPos, Time.deltaTime * 2.0f);

			if (nextTurnPos.x + pos.x > -5.0f) {
				moveLeft = false;
				barRect.anchoredPosition = nextTurnPos;
			}

		}

		pointsText.text = pointsTotal.ToString ();

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
				blueScript.StopAnimation ();

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
		

	public void StartTimer (string playerNumTime){
	
		//Debug.Log ("starttime");

		source.PlayOneShot (allDone, .1f);

		if (networkNum == playerNumTime) {
			timerSoundTime = true;
			float timeAmt;
			timeAmt = (timerTurn / 60.0f) * 15;
			timer = timeAmt + 10;
			timerStart = timeAmt + 10;
			//Debug.Log ("starttimeinside" + timer);

			//Invoke ("PlayTimerSound", timeAmt + 5.0f);

			timerGo = true;
		}
	}

//	void PlayTimerSound (){
//	
//		source.PlayOneShot (beepTimer, .1f);
//
//	}

	public void PlaceWheelIcon () {
	
		DeleteEmpty ();

		if (timerGo == true) {

			timerGo = false;
			timerOutline.fillAmount = 0;
			timerText.text = "-";

		}



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

		PlayTone ();
		DeleteEmpty ();
	
		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", 1.1f);

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

		PlayTone ();
		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", 1.1f);

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

		PlayTone ();
		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", 1.1f);

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

	public void PlaceNapIcon() {

		//.Log ("NAP: " + iconsLeft);

		if (iconsLeft < 1 || buttonCushion == true) {
			return;
		}

		PlayTone ();

		DeleteEmpty ();

		iconsLeft--;
		iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
		iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		buttonCushion = true;
		Invoke ("ButtonCushion", .2f);

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

		if (winning == true) {
			winning = false;
			pinkStartPos = new Vector3 ((pinkGhost.transform.position.x + blueGhost.transform.position.x) / 2, blueGhost.transform.position.y, (pinkGhost.transform.position.z + blueGhost.transform.position.z) / 2);
			pinkEndPos = pinkStartPos - pinkGhost.transform.forward;
			pinkGhost.transform.DOJump (pinkEndPos, 1.0f, 0, 1.0f, false).SetEase(Ease.OutBounce).OnComplete (PinkGoIdle).SetId ("PinkForward");
			pinkScript.WalkAnimation ();
			Invoke ("BackToNormal", .8f);
			blueGhost.transform.DOMove (pinkStartPos, .5f).SetEase (Ease.InOutSine).OnComplete (BlueGoIdle).SetId ("BlueForward");
			blueScript.WalkAnimation ();

			if (pinkScript.inWindMill == true) {
				pinkScript.myWindMill.GetComponent<WindmillScript> ().GhostJumped (pinkGhost);
				pinkGhost.transform.parent = null;
				pinkScript.inWindMill = false;
				pinkScript.myWindMill = null;


			}
				

			return;
		}


		pinkStartPos = pinkGhost.transform.position;
		pinkEndPos = pinkGhost.transform.position - pinkGhost.transform.forward;
		//forwardPinkGhost = true;
		pinkGhost.transform.DOMove (pinkEndPos, actTime).SetEase (Ease.InOutSine).OnComplete (PinkGoIdle).SetId ("PinkForward");
		pinkScript.WalkAnimation ();

		if (winning == false){
			StraightenGhost ();
		}


		if (preview == true) {
			
			AddLinePointsPink (pinkEndPos);
			if (pinkShell.activeInHierarchy == false) {
				PinkShellAppear ();
			}
		} else {
		
			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (forwardSound, .2f);
		
		}

	}

	public void CorrectPink (){

		pinkEndPos = pinkStartPos;
	}

	public void BackwardPink (){
		
		if (winning == false){
			StraightenGhost ();
		}
		pinkStartPos = pinkGhost.transform.position;
		pinkBackOnePos = pinkGhost.transform.position + pinkGhost.transform.forward;
		pinkScript.WalkAnimation ();
		pinkGhost.transform.DOMove (pinkBackOnePos, actTime/1.5f).SetEase (Ease.InOutSine).OnComplete (PinkGoIdle).SetId ("PinkForward");

		//backwardPinkGhost = true;
		DeletePoint ();

	}

	public void RotateRightPink () {

		if (preview == false) {
			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (rightTurn, .2f);
		}

		direction = 1;

		pinkScript.WobbleAnimation ();
		Invoke ("PinkGoIdle", 0.95f);

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, actTime));
		if (winning == false){
			StraightenGhost ();
		}

	}

	public void RotateLeftPink () {

		if (preview == false) {
			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (leftTurn, .2f);
		}
		direction = -1;

		pinkScript.WobbleAnimation ();
		Invoke ("PinkGoIdle", 0.95f);

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(pinkGhost, rotation2, actTime));
		if (winning == false){
			StraightenGhost ();
		}

	}

	void BlueGoIdle (){
	
		blueScript.StopAnimation ();
	
	}

	void PinkGoIdle (){

		pinkScript.StopAnimation ();

	}

	public void ForwardBlue () {

		if (winning == true) {
			winning = false;
			blueStartPos = new Vector3 ((pinkGhost.transform.position.x + blueGhost.transform.position.x) / 2, blueGhost.transform.position.y, (pinkGhost.transform.position.z + blueGhost.transform.position.z) / 2);
			blueEndPos = blueStartPos - blueGhost.transform.forward;
			//Debug.Log ("b2"  + blueEndPos);
			blueGhost.transform.DOJump (blueEndPos, 1.0f, 0, 1.0f, false).SetEase(Ease.OutBounce).OnComplete (BlueGoIdle).SetId ("BlueForward");
			blueScript.WalkAnimation ();
			Invoke ("BackToNormal", .8f);
			pinkGhost.transform.DOMove (blueStartPos, .5f).SetEase (Ease.InOutSine).OnComplete (PinkGoIdle).SetId ("PinkForward");
			pinkScript.WalkAnimation ();
			if (blueScript.inWindMill == true) {
				blueScript.myWindMill.GetComponent<WindmillScript> ().GhostJumped (blueGhost);
				blueGhost.transform.parent = null;
				blueScript.inWindMill = false;
				blueScript.myWindMill = null;


			}
				
			return;
		}

		blueStartPos = blueGhost.transform.position;

		blueEndPos = blueGhost.transform.position - blueGhost.transform.forward;
		//Debug.Log ("b3"  + blueEndPos);
		blueGhost.transform.DOMove (blueEndPos, actTime).SetEase (Ease.InOutSine).OnComplete (BlueGoIdle).SetId ("BlueForward");
		//forwardBlueGhost = true;
		blueScript.WalkAnimation ();
		if (winning == false){
			StraightenGhost ();
		}

		if (preview == true) {
			AddLinePointsBlue (blueEndPos);
			if (blueShell.activeInHierarchy == false) {
				BlueSheelAppear ();
			}
		} else {

			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (forwardSound, .2f);

		}

	}

	public void CorrectBlue (){
		
		blueEndPos = blueStartPos;
		//Debug.Log ("b4" + blueEndPos);
	}

	void BackToNormal (){

		pinkGhost.GetComponent<CapsuleCollider> ().enabled = true;
		blueGhost.GetComponent<CapsuleCollider> ().enabled = true;
		pinkScript.detectors.SetActive (true);
		blueScript.detectors.SetActive (true);

		GameObject[] detectors = GameObject.FindGameObjectsWithTag ("Detector");

		foreach (GameObject detector in detectors) {
		
			detector.GetComponent<DetectorScript> ().alreadyHit = false;
		
		}

	}

	public void BackwardBlue () {

		if (winning == false){
			StraightenGhost ();
		}
		blueStartPos = blueGhost.transform.position;
		blueBackOnePos = blueGhost.transform.position + blueGhost.transform.forward;
		blueScript.WalkAnimation ();
		blueGhost.transform.DOMove (blueBackOnePos, actTime/1.5f).SetEase (Ease.InOutSine).OnComplete (BlueGoIdle).SetId ("BlueForward");

		//backwardBlueGhost = true;
		DeletePoint ();

	}

	public void RotateRightBlue () {

		if (preview == false) {
			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (rightTurn, .2f);
		}
		direction = 1;

		blueScript.WobbleAnimation ();
		Invoke ("BlueGoIdle", actTime - .05f);

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, actTime));
		if (winning == false){
			StraightenGhost ();
		}
	}

	public void RotateLeftBlue () {

		if (preview == false) {
			source2.pitch = Random.Range (pitchMin, pitchMax);
			source2.PlayOneShot (leftTurn, .2f);
		}
		direction = -1;

		blueScript.WobbleAnimation ();
		Invoke ("BlueGoIdle", actTime - .05f);

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(blueGhost, rotation2, actTime));
		if (winning == false){
			StraightenGhost ();
		}

	}

	public void Nap (){
	
		source.PlayOneShot (napSound, .2f);
	
	}

	void ResetIds(){
		idOne = 0;
		idTwo = 0;
	
	}

	//0= North - Z, 1= east - X, 2= south + Z, 3= west + X
	public void InSameSquare (int id, int dir) {

		if (id == 1 && idOne == 0) {
			idOne = 1;
		} else if (id == 2 && idTwo == 0) {
			idTwo = 2;
		} else if (id == 1 && idOne == 1) {
			return;
		} else if (id == 2 && idTwo == 2) {
			return;
		}

		Invoke ("ResetIds", .8f);

		//Debug.Log (dir + "InSameSquare" + id + Time.time);
		//Debug.Log ("Pink:" + pinkEndPos);
		//Debug.Log ("Blue:" + blueEndPos);
		winning = true;
		pinkGhost.GetComponent<CapsuleCollider> ().enabled = false;
		blueGhost.GetComponent<CapsuleCollider> ().enabled = false;

		if (id == 1) {
			DOTween.Kill ("PinkForward", false);
			pinkScript.StopAnimation ();
			pinkScript.WobbleAnimation ();
			int pinkFace = Mathf.RoundToInt(pinkGhost.transform.eulerAngles.y/90);

			if (pinkScript.inWindMill == true) {
				pinkEndPos = blueEndPos;
			}

			if (dir == 1) {
				
				if (pinkFace == 0) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z + .21f);
				} else if (pinkFace == 1) {
					pinkMidPos = new Vector3 (pinkEndPos.x + .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 2) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z - .21f);
				} else if (pinkFace == 3) {
					pinkMidPos = new Vector3 (pinkEndPos.x - .21f, pinkEndPos.y, pinkEndPos.z);
				}
					
			} else if (dir == 2) {

				if (pinkFace == 0) {
					pinkMidPos = new Vector3 (pinkEndPos.x + .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 1) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z - .21f);
				} else if (pinkFace == 2) {
					pinkMidPos = new Vector3 (pinkEndPos.x - .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 3) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z + .21f);
				}
					
			} else if (dir == 3) {
				
				if (pinkFace == 0) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z - .21f);
				} else if (pinkFace == 1) {
					pinkMidPos = new Vector3 (pinkEndPos.x - .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 2) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z + .21f);
				} else if (pinkFace == 3) {
					pinkMidPos = new Vector3 (pinkEndPos.x + .21f, pinkEndPos.y, pinkEndPos.z);
				}

			} else if (dir == 4) {
				
				if (pinkFace == 0) {
					pinkMidPos = new Vector3 (pinkEndPos.x - .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 1) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z + .21f);
				} else if (pinkFace == 2) {
					pinkMidPos = new Vector3 (pinkEndPos.x + .21f, pinkEndPos.y, pinkEndPos.z);
				} else if (pinkFace == 3) {
					pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z - .21f);
				}

			}
			//Debug.Log ("square1");
			pinkGhost.transform.DOMove (pinkMidPos, .5f).SetEase (Ease.InOutSine).OnComplete (PinkGoIdle);
			if (blueScript.inWindMill == true) {
				//Debug.Log (blueScript.myWindMill.name + " square");
				pinkScript.myWindMill = blueScript.myWindMill;
				pinkScript.inWindMill = true;
				//pinkGhost.transform.parent = blueScript.myWindMill.transform;
				blueScript.myWindMill.GetComponent<WindmillScript> ().AddGhost (pinkGhost);

			}
		}

		if (id == 2) {
			DOTween.Kill ("BlueForward", false);
			blueScript.StopAnimation ();
			blueScript.WobbleAnimation ();
			int blueFace = Mathf.RoundToInt(blueGhost.transform.eulerAngles.y/90);

			if (blueScript.inWindMill == true) {
				blueEndPos = pinkEndPos;
			}

			if (dir == 1) {
				
				if (blueFace == 0) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z + .26f);
				} else if (blueFace == 1) {
					blueMidPos = new Vector3 (blueEndPos.x + .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 2) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z - .26f);
				} else if (blueFace == 3) {
					blueMidPos = new Vector3 (blueEndPos.x - .26f, blueEndPos.y, blueEndPos.z);
				}

			} else if (dir == 2) {
				
				if (blueFace == 0) {
					blueMidPos = new Vector3 (blueEndPos.x + .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 1) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z - .26f);
				} else if (blueFace == 2) {
					blueMidPos = new Vector3 (blueEndPos.x - .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 3) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z + .26f);
				}

			} else if (dir == 3) {
				
				if (blueFace == 0) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z - .26f);
				} else if (blueFace == 1) {
					blueMidPos = new Vector3 (blueEndPos.x - .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 2) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z + .26f);
				} else if (blueFace == 3) {
					blueMidPos = new Vector3 (blueEndPos.x + .26f, blueEndPos.y, blueEndPos.z);
				}

			} else if (dir == 4) {
				
				if (blueFace == 0) {
					blueMidPos = new Vector3 (blueEndPos.x - .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 1) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z + .26f);
				} else if (blueFace == 2) {
					blueMidPos = new Vector3 (blueEndPos.x + .26f, blueEndPos.y, blueEndPos.z);
				} else if (blueFace == 3) {
					blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z - .26f);
				}

			}

			blueGhost.transform.DOMove (blueMidPos, .5f).SetEase (Ease.InOutSine).OnComplete (BlueGoIdle);
			if (pinkScript.inWindMill == true) {
			
				blueScript.myWindMill = pinkScript.myWindMill;
				blueScript.inWindMill = true;
				//blueGhost.transform.parent = pinkScript.myWindMill.transform;
				pinkScript.myWindMill.GetComponent<WindmillScript> ().AddGhost (blueGhost);
			
			}


		}

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

		//Debug.Log ("DeletePoint");

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
			pinkEndPos = origPos;
			pinkGhost.transform.eulerAngles = origRot;
			pinkShell.SetActive (false);
			myGhostScript.PreviewModeOff ();
		}

		if (networkNum == "2") {
			blueGhost.transform.position = origPos;
			blueEndPos = origPos;
			blueGhost.transform.eulerAngles = origRot;
			blueShell.SetActive (false);
			myGhostScript.PreviewModeOff ();
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

	void PlayCombinedIcons () {

		//Debug.Log ("PinkC:" + pinkEndPos);
		//Debug.Log ("BlueC:" + blueEndPos);


		GameObject[] triggersPlusNull = GameObject.FindGameObjectsWithTag ("Trigger");

		for (int i = 0; i < triggersPlusNull.Length; i++) {

			if (triggersPlusNull [i].name == "NullObject(Clone)") {

				Destroy (triggersPlusNull [i]);
				}
			}

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		float seconds = 0;

		for (int i = 0; i < triggers.Length; i++) {
			seconds = seconds + actTime + .25f;
			scrollList.transform.GetChild(i).gameObject.GetComponent<TriggerScript> ().InvokeTriggerAction (seconds);
			if (i == triggers.Length - 1) {
			
				Invoke ("BeginNewTurn", seconds + 2.0f);
			
			}
		}
	}

	public void PlayGearItems () {

		source2.PlayOneShot (gear, .2f);

		GameObject[] gearItems = GameObject.FindGameObjectsWithTag ("Gear Item");
		for (int i = 0; i < gearItems.Length; i++) {
			gearItems [i].GetComponent<WindmillScript> ().RotateWindmill ();
		}

		GameObject[] boulders = GameObject.FindGameObjectsWithTag ("Boulder");
		for (int i = 0; i < boulders.Length; i++) {
			boulders [i].GetComponent<BoulderScript> ().RotateBoulder ();
		}

//		GameObject[] evilGhosts = GameObject.FindGameObjectsWithTag ("Slime");
//		for (int i = 0; i < evilGhosts.Length; i++) {
//			evilGhosts [i].GetComponent<SlimeScript> ().RotateSlime ();
//		}


	}

	//Finished placing moves
	public void TurnDone () {

		source.Stop ();
		source.PlayOneShot (doneSound, .31f);



		if (timerGo == true) {
		
			timerGo = false;
			timerOutline.fillAmount = 0;
			timerText.text = "-";
		
		}

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");

		waitScreen.SetActive (true);

		if (wheelPhase == false) {

			myGhostScript.StopAnimation ();
			buttonPanel.MoveDown ();
			preview = false;
			DOTween.Kill ("PinkForward", false);
			DOTween.Kill ("BlueForward", false);
			Invoke ("BackToOriginalPosition", .5f);

			RestartLineRender ();

		} else if (wheelPhase == true && triggers.Length == 0) {
		
			wheelPanel.MoveDown ();

		}

		if (networkNum == "2" && receivedIcons == false) {
			CmdHurryBuddy ("1");
		} else if (networkNum == "1" && receivedIcons == false){
			CmdHurryBuddy ("2");
		}

		GameObject[] empties = GameObject.FindGameObjectsWithTag ("Empty");

		if (empties.Length > 0) {

			for (int i = 0; i < empties.Length; i++) {
				Destroy (empties [i]);
			}

		}


		if (networkNum == "2" && otherPlayerReady == true) {

			SendArray ();

		} else if (networkNum == "2" && otherPlayerReady == false) {

			//CmdHurryBuddy ("1");
			InvokeRepeating ("WaitingForOtherPlayer", 1.0f, 1.0f);

		} else if (networkNum == "1" && bluePlayerThere == true && otherPlayerReady == true) {
		
			SendArray ();

		} else if (networkNum == "1" && bluePlayerThere == true && otherPlayerReady == false) {

			//CmdHurryBuddy ("2");
			InvokeRepeating ("WaitingForOtherPlayer", 1.0f, 1.0f);

		} else if (networkNum == "1" && bluePlayerThere == false) {
		
			InvokeRepeating ("LookingForBlue", 1.0f, 1.0f);

		}
	}

	[Command]
	public void CmdHurryBuddy(string playerNumTime){
	
		//Debug.Log ("THIS: " + playerNumTime);
		RpcHurryBuddy (playerNumTime);

	}

	[ClientRpc]
	public void RpcHurryBuddy(string playerNumTime){

		GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ().StartTimer (playerNumTime);

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

	//	Debug.Log ("Send Array Happened");

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
				//Debug.Log ("Send Array1: " + triggerCount);
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
				//Debug.Log ("Send Array2: " + triggerCount);
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

		//Debug.Log ("FillPinkArray");

		receivedIcons = true;

		for(int i = 0; i < fromPinkGhost.Length; i++)
		{
			pinkGhostIcon [i] = iconList [fromPinkGhost [i]];

		}
	}


	void ReadyToSee () {

		source.PlayOneShot (blip, .25f);
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

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;

		for(int i = 0; i < triggerCount; i++)
		{
			Destroy (triggers[i]);
		}

		timerTurn = 60.0f;
		timerTurnGo = true;

		if (wheelPhase == true) {
			wheelPhase = false;
			pinkEndPos = pinkGhost.transform.position;
			blueEndPos = blueGhost.transform.position;
			//Debug.Log ("b5" + blueEndPos);
			if (pointsTotal < 100) {
				deleteNull.SetActive (false);
				buttonPanel.MoveUp ();
				camerRot.CameraRotate (myGhost);
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

			colorBut.BackToGrey ();
				
		} else {

			GameObject empty = Instantiate (emptyPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			empty.transform.SetParent (scrollList.transform, false);

			iconsLeft = iconsToStart;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
			iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);

			pointsTotal = pointsTotal + turnPoints;
			CheckStarHit ();

			MoveTurnBar (turnPoints);

			//timerCircle.fillAmount = (turnsLeft / totalTurns);
		
			wheelPhase = true;
			if (pointsTotal < 100 && winning == false) {
				wheelPanel.MoveUp ();
				camerRot.CameraRotate (theirGhost);

			}
			if (pointsTotal > 99 && winning == false) {

				loseScreen.SetActive (true);
				restartGame.moveUp = true;


			}

			if (winning == true) {
				ActivateWin ();
			}
				
		}
			
		ClearArray ();

		//Debug.Log ("turnsLeft: " + turnsLeft + "totalTurns: " + totalTurns + " fillAmount " + (turnsLeft / totalTurns));

		TellOtherPlayerReady ();
	}

	void ActivateWin (){



		Vector3 northWest = GameObject.FindGameObjectWithTag ("North West").transform.position;
		//Vector3 southEast = GameObject.FindGameObjectWithTag ("South East").transform.position;

		float pinkToN = Vector3.Distance (northWest, pinkGhost.transform.position);
		//float pinkToS = Vector3.Distance (southEast, pinkGhost.transform.position);
		float blueToN = Vector3.Distance (northWest, blueGhost.transform.position);
		//float blueToS = Vector3.Distance (southEast, blueGhost.transform.position);

		pinkScript.WobbleAnimation ();
		blueScript.WobbleAnimation ();

		if (Mathf.Abs((pinkGhost.transform.position.z - Mathf.RoundToInt(pinkGhost.transform.position.z))) > .1f){
			
		if (pinkToN > blueToN) {
				Vector3 blueAngle = new Vector3 (blueGhost.transform.eulerAngles.x, 180, blueGhost.transform.eulerAngles.z);
				Vector3 pinkAngle = new Vector3 (pinkGhost.transform.eulerAngles.x, 0, pinkGhost.transform.eulerAngles.z);
				blueGhost.transform.DORotate (blueAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (BlueGoIdle);
				pinkGhost.transform.DORotate (pinkAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (PinkGoIdle);

		} else {
				Vector3 blueAngle = new Vector3 (blueGhost.transform.eulerAngles.x, 0, blueGhost.transform.eulerAngles.z);
				Vector3 pinkAngle = new Vector3 (pinkGhost.transform.eulerAngles.x, 180, pinkGhost.transform.eulerAngles.z);
				blueGhost.transform.DORotate (blueAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (BlueGoIdle);
				pinkGhost.transform.DORotate (pinkAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (PinkGoIdle);
			}
		}

		else {

		if (pinkToN > blueToN) {
				Vector3 blueAngle = new Vector3 (blueGhost.transform.eulerAngles.x, 90, blueGhost.transform.eulerAngles.z);
				Vector3 pinkAngle = new Vector3 (pinkGhost.transform.eulerAngles.x, 270, pinkGhost.transform.eulerAngles.z);
				blueGhost.transform.DORotate (blueAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (BlueGoIdle);
				pinkGhost.transform.DORotate (pinkAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (PinkGoIdle);
		} else {
				Vector3 blueAngle = new Vector3 (blueGhost.transform.eulerAngles.x, 270, blueGhost.transform.eulerAngles.z);
				Vector3 pinkAngle = new Vector3 (pinkGhost.transform.eulerAngles.x, 90, pinkGhost.transform.eulerAngles.z);
				blueGhost.transform.DORotate (blueAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (BlueGoIdle);
				pinkGhost.transform.DORotate (pinkAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (PinkGoIdle);
			}
				
		}


//		Vector3 blueAngle = new Vector3 (blueGhost.transform.eulerAngles.x, 0, blueGhost.transform.eulerAngles.z);
//		Vector3 pinkAngle = new Vector3 (pinkGhost.transform.eulerAngles.x, 180, pinkGhost.transform.eulerAngles.z);
//		blueGhost.transform.DORotate (blueAngle, 2.0f).SetEase (Ease.OutCirc).OnComplete (BlueGoIdle);
//		pinkGhost.transform.DORotate (pinkAngle, 2.0f).SetEase(Ease.OutCirc).OnComplete (PinkGoIdle);

//		pinkMidPos = new Vector3 (pinkEndPos.x, pinkEndPos.y, pinkEndPos.z - .26f);
//		pinkGhost.transform.DOMove (pinkMidPos, 1.6f).SetEase (Ease.InOutSine);
//		blueMidPos = new Vector3 (blueEndPos.x, blueEndPos.y, blueEndPos.z + .26f);
//		blueGhost.transform.DOMove (blueMidPos, 1.6f).SetEase (Ease.InOutSine);


		Invoke ("Hug", 2.3f);

		if (goldHit == false) {
			camerRot.starColor = 1;
			return;
		} else if (silverHit == false) {
			camerRot.starColor = 2;
			return;
		} else if (bronzeHit == false) {
			camerRot.starColor = 3;
			return;
		}
	
	}

	void Hug () {
		
		blueScript.HugAnimation ();
		pinkScript.HugAnimation ();
		camerRot.EndGameRotion ();
		Invoke ("CloseEyes", 4.0f);

	}

	void CloseEyes () {

		blueScript.CloseEyesAnimation ();
		pinkScript.CloseEyesAnimation ();
	}

	public void MoveTurnBar(float pointsTaken){

		if (moveLeft == true) {

			moveLeft = false;
			barRect.anchoredPosition = nextTurnPos;

		}
			
		float xPos;

		xPos = barRect.anchoredPosition.x + ((barRectEnd.anchoredPosition.x - startBarPosX)*(pointsTaken/100.0f));
	
		nextTurnPos = new Vector2 (xPos ,barRect.anchoredPosition.y);

		moveLeft = true;

	}

	void PlaceStars(){

		float goldRatio = starGoldPoint / 100;
		float xPos;
		xPos = startPoint.anchoredPosition.x + ((Mathf.Abs(startPoint.anchoredPosition.x - endPoint.anchoredPosition.x))*goldRatio); // -700
		Vector2 goldPos = new Vector2 (xPos, starGold.anchoredPosition.y);
		starGold.anchoredPosition = goldPos;

		float silverRatio = starSilverPoint / 100;
		xPos = startPoint.anchoredPosition.x + ((Mathf.Abs(startPoint.anchoredPosition.x - endPoint.anchoredPosition.x))*silverRatio); // -700
		Vector2 silverPos = new Vector2 (xPos, starSilver.anchoredPosition.y);
		starSilver.anchoredPosition = silverPos;

		float bronzeRatio = starBronzePoint / 100;
		xPos = startPoint.anchoredPosition.x + ((Mathf.Abs(startPoint.anchoredPosition.x - endPoint.anchoredPosition.x))*bronzeRatio); // -700
		Vector2 bronzePos = new Vector2 (xPos, starBronze.anchoredPosition.y);
		starBronze.anchoredPosition = bronzePos;

	}

	public void TakeHit(float pointsAdded){
	
		pointsTotal = pointsTotal + pointsAdded;
		CheckStarHit ();

		if (pointsTotal > 99) {
			LoseGame ();
		}
	
	}

	public void CheckStarHit () {
		
		if (goldHit == false) {
			if (starGoldPoint < pointsTotal) {
				starGold.gameObject.SetActive (false);
				goldHit = true;
			}
		} 

		if (silverHit == false) {
			if (starSilverPoint < pointsTotal) {
				starSilver.gameObject.SetActive (false);
				silverHit = true;
			}

		} 

		if (bronzeHit == false) {
			if (starBronzePoint < pointsTotal) {
				starBronze.gameObject.SetActive (false);
				bronzeHit = true;
			}
		}
	}

	public void Delete(){
	
		if (wheelPhase == true || placedIcons == true || buttonCushion == true) {
			return;
		}

		PlayTone ();

//		source2.pitch = Random.Range (pitchMin, pitchMax);
//		source2.PlayOneShot (deleteSound, .75f);

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;
		triggers [triggerCount - 1].GetComponent<TriggerScript> ().UndoAction ();
		Destroy (triggers[triggerCount - 1]);

		if (wheelPhase == true) {
		
			wheelPanel.MoveUp ();
			camerRot.CameraRotate (theirGhost);

		} else {
		
			iconsLeft++;
			iconsLeftSquare.fillAmount = (iconsLeft / iconsToStart);
			iconsLeftSquare2.fillAmount = (iconsLeft / iconsToStart);
		
		}

		GameObject empty = Instantiate (emptyPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		empty.transform.SetParent (scrollList.transform, false);
		buttonCushion = true;
		Invoke ("ButtonCushion", 1.1f);

	}

	public void LoseGame() {
	
		loseScreen.SetActive (true);
	
	}

	public void FillPinkColorArray () {

		if (networkNum == "2") {
			//wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillPinkColorArray ();
			myBackground.GetComponent<ColorCodeScript> ().FillPinkColorArray ();
			source.PlayOneShot (changeBackground, .2f);
		} else {
			source.PlayOneShot (sendBackground, .2f);
		}

	}

	public void FillBlueColorArray () {

		if (networkNum == "1") {
			//wheelfromBuddy.GetComponent<WheelFromBuddyScript> ().FillBlueColorArray ();
			myBackground.GetComponent<ColorCodeScript> ().FillBlueColorArray ();
			source.PlayOneShot(changeBackground,.2f);
		} else {
			source.PlayOneShot (sendBackground, .2f);
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
		//Debug.Log ("RpcPinkSendArray: " + networkNum);
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

		//Debug.Log ("RpcPinkSendColorArray: " + networkNum);

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
	
	//	Debug.Log ("told them i'm ready");

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
		//Debug.Log ("Got to CMD bubble: " + phrase + ghostNum);

	}

	[ClientRpc]
	public void RpcSendBubble(string phrase, string ghostNum) {

		//Debug.Log ("Got to RPC bubble: " + phrase + ghostNum);

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

	void PlayTone () {

		int toneNum = Random.Range (0, 4);
		source.PlayOneShot (tones [toneNum], toneVol);

	}

	public void RestartGame(){

		loseScreen.SetActive (false);
		wheelPanel.MoveUp ();
	
		pointsTotal = 0;
		barRect.anchoredPosition = startPosBar;
		goldHit = false;
		silverHit = false;
		bronzeHit = false;
		starGold.gameObject.SetActive (true);
		starSilver.gameObject.SetActive (true);
		starBronze.gameObject.SetActive (true);
		PlaceStars ();

		pinkGhost.GetComponent<Rigidbody> ().isKinematic = true;
		pinkGhost.GetComponent<Rigidbody> ().useGravity = false;
		blueGhost.GetComponent<Rigidbody> ().isKinematic = true;
		blueGhost.GetComponent<Rigidbody> ().useGravity = false;

		GameObject playerOneSpawn = GameObject.FindGameObjectWithTag ("Spawn One");
		pinkGhost.transform.position = playerOneSpawn.transform.position;
		pinkGhost.transform.rotation = playerOneSpawn.transform.rotation;
		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		blueGhost.transform.position = playerTwoSpawn.transform.position;
		blueGhost.transform.rotation = playerTwoSpawn.transform.rotation;

		GameObject[] hiddenStuff = GameObject.FindGameObjectsWithTag ("New Wall");
		foreach (GameObject hiddenStuffItem in hiddenStuff) {
			hiddenStuffItem.GetComponent<HiddenWallScript> ().SwitchBackToHidden ();
		}

		GameObject[] hiddenBoulders = GameObject.FindGameObjectsWithTag ("Boulder");
		foreach (GameObject hiddenBoulder in hiddenBoulders) {
			hiddenBoulder.GetComponent<HiddenWallScript> ().SwitchBackToHidden ();
		}

		camerRot.CameraRotate (theirGhost);
	
	}

}