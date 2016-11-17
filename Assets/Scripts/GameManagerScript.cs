using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManagerScript : NetworkBehaviour {

	private bool move;
	private GameObject waitText;

	public Color pink;
	public Color blue;
	public Color green;
	public Color black;
	public Color purple;
	public Color white;
	public Color grey;

	private Image frisbeeWedge1;
	private Image frisbeeWedge2;
	private Image frisbeeWedge3;

	public GameObject signalWheel;

	public GameObject player;

	public bool notMyTurn = false;

	[SyncVar]
	public int playerTurn;

	[SyncVar]
	public int hitCount = 0;

	[SyncVar]
	public int keyCount = 0;

	public int moveCount = 0;

	[SyncVar]
	public Color wedge1;
	[SyncVar]
	public Color wedge2;
	[SyncVar]
	public Color wedge3;

	public int playerId;

	public Text hitCountText;
	public Text keyCountText;
	public Text moveCountText;


	public int gameManagerId = 0;

	private bool buttonBool;

	public GameObject[] buttons;
	public GameObject sendButton;

	public Sprite nextTurnGhost;
	public Sprite sleepingGhost;
	public Image nextTurnImage;

	public GameObject screenFade;


	// Use this for initialization
	void Start () {
	
		hitCount = 0;

		waitText = GameObject.FindGameObjectWithTag ("Wait Text");
		waitText.SetActive (false);

		GameObject frisbeeObject = GameObject.FindGameObjectWithTag ("Signal Circle Message");
		frisbeeWedge1 = frisbeeObject.transform.GetChild(0).gameObject.GetComponent <Image>();
		frisbeeWedge2 = frisbeeObject.transform.GetChild(1).gameObject.GetComponent <Image>();
		frisbeeWedge3 = frisbeeObject.transform.GetChild(2).gameObject.GetComponent <Image>();

		wedge1 = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		wedge2 = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		wedge3 = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		grey = new Color (.62f, .62f, .62f, 1.0f);


	}
	
	// Update is called once per frame
	void Update () {


		//playerTurnText.text = playerTurn.ToString ();
		hitCountText.text = "Hits " + hitCount.ToString () + "/2";
		keyCountText.text = "Keys " + keyCount.ToString () + "/4";
		moveCountText.text = "Moves " + moveCount.ToString () + "/3";

		if (gameManagerId != playerTurn) {
			TurnOver ();
		} else {
		
			waitText.SetActive (false);

		}


	
	}

	public void MoveAction (){

		if (gameManagerId != playerTurn) {
			return;
		}

			player.GetComponent<GhostMovementScript> ().moveTurn = true;	


	}

	public void SignalClicked () {


		if (gameManagerId != playerTurn) {
			return;
		}

		buttons = GameObject.FindGameObjectsWithTag("Button");

		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = true;
		}
	}



	public void TurnOver () {

		waitText.SetActive (true);
	
	}
		
	public void AssignId () {

		if (gameObject.tag == "Game Manager One") {
		
			gameManagerId = 1;
		
		}

		if (gameObject.tag == "Game Manager Two") {

			gameManagerId = 2;

		}
	}

		

	public void SendWheel() {

		if (gameManagerId == playerTurn) {

			if (gameManagerId == 1) {
				GameObject playerLocal = GameObject.FindGameObjectWithTag ("Player One");
				playerLocal.GetComponent<GhostMovementScript> ().CmdSendButton ();
				sendButton.SetActive (false);
				
			}
		
			if (gameManagerId == 2) {
				GameObject playerLocal = GameObject.FindGameObjectWithTag ("Player Two");
				playerLocal.GetComponent<GhostMovementScript> ().CmdSendButton ();
				sendButton.SetActive (false);
		
			}
		} else {
			return;
		}
	}

	public void UpdateColors () {

				if (gameManagerId != playerTurn) {
		
					frisbeeWedge1.color = wedge1;
					frisbeeWedge2.color = wedge2;
					frisbeeWedge3.color = wedge3;
		
				}
		
				if (gameManagerId == playerTurn) {
		
					signalWheel.GetComponent<ColorButtonScript> ().EndTurnSignal ();
		
				}
	}

	public void BackToWhite () {
	
		wedge1 = white;
		wedge2 = white;
		wedge3 = white;
	
	}

	public void EndTurnButton (){
		
		if (gameManagerId == playerTurn) {

			if (gameManagerId == 1) {

				GameObject playerLocal = GameObject.FindGameObjectWithTag ("Player One");
				TurnOff ();
				moveCount = 0;
				playerLocal.GetComponent<GhostMovementScript> ().SwitchPlayers ();

			}

			if (gameManagerId == 2) {

				GameObject playerLocal = GameObject.FindGameObjectWithTag ("Player Two");
				TurnOff ();
				moveCount = 0;
				playerLocal.GetComponent<GhostMovementScript> ().SwitchPlayers ();

			}
		} else {
			return;
		}
	
	}

	public void TurnOn () {

		Debug.Log (gameManagerId);
		buttons = GameObject.FindGameObjectsWithTag("Button");
		moveCount = 3;
		nextTurnImage.sprite = nextTurnGhost; 
		sendButton.SetActive (true);
		screenFade.SetActive (false);
		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = true;
		}

	}

	public void TurnOff (){

		screenFade.SetActive (true);
		buttons = GameObject.FindGameObjectsWithTag("Button");
		nextTurnImage.sprite = sleepingGhost; 
		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = false;
		}
	
	}

	public void TurnOnPlayerOne (){

		if (gameManagerId != 1) {
			return;
		}

		TurnOn ();
		player.GetComponent<GhostMovementScript> ().moveTurn = true;

	
	}

	public void TurnOnPlayerTwo (){

		if (gameManagerId != 2) {
			return;
		}

		TurnOn ();
		player.GetComponent<GhostMovementScript> ().moveTurn = true;

	}
		
	public void CheckPortal (){
	
		if (keyCount > 3) {

			player.GetComponent<GhostMovementScript> ().CmdSpawnPortals ();
		}
	}

}
