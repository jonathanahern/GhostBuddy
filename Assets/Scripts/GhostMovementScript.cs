using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostMovementScript : NetworkBehaviour {

	GameObject hitSquare;
	Vector3 squarePos;

	Camera myCamera;
	private Vector3 ghostPos;
	public bool moving = false;
	public float speed = 2.0F;

	public GameObject glowSquare;
	public bool moveTurn = false;
	private int moveCount;
	public int movesLeft = 3;

	GameManagerScript gameManager;
	private Transform signalSpawn;
	private GameObject canvas;

	[SyncVar]
	public int playerTurn;

	[SyncVar]
	public int hitCount;

	public Color wedge1;
	public Color wedge2;
	public Color wedge3;

	public Color pink;
	public Color blue;
	public Color green;
	public Color black;
	public Color purple;
	public Color white;


	public GameObject playerTurnText;
	public Text playerText;

	public int playerId = 0;

	public GameObject gameManagerObject;

	public RectTransform rectTran;

	public GameObject signalCircle;
	private GameObject winScreen;

	private Vector3 lastPos;

	int layer_mask;


public override void OnStartLocalPlayer()
	{	

		GameObject gameManagerObjectOther = GameObject.FindGameObjectWithTag ("Game Manager");
		gameManager = gameManagerObjectOther.GetComponent<GameManagerScript> ();

		Material pinkghost = (Material)Resources.Load("Pink Ghost", typeof(Material));
		GetComponent<Renderer>().material = pinkghost;

		Transform ghost = gameObject.transform;

//		signalSpawn = canvas.transform;

		Material pinkMat = (Material)Resources.Load("Pink", typeof(Material));
		foreach (Transform child in ghost)
			if (child.CompareTag ("Arm")) {
				child.gameObject.GetComponent<Renderer> ().material = pinkMat;
			}

		glowSquare = GameObject.FindGameObjectWithTag ("Glow Square");

		GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
		int playerCount = players.Length;

		playerTurnText = GameObject.FindGameObjectWithTag ("Turn Text");
		playerText = playerTurnText.GetComponent<Text> ();

		signalCircle = GameObject.FindGameObjectWithTag ("Signal Wheel");
		signalCircle.GetComponent <ColorButtonScript> ().player = gameObject;

		GameObject cameraOne = GameObject.FindGameObjectWithTag ("Camera One");
		GameObject cameraTwo = GameObject.FindGameObjectWithTag ("Camera Two");

		layer_mask = LayerMask.GetMask("Tile");




		if (playerCount == 1) {

			gameObject.tag = "Player One";
			gameManager.tag = "Game Manager One";
			signalCircle.tag = "Signal Wheel One";
			gameManager.playerTurn = 1;
			gameManager.AssignId ();
			playerId = 1;
			myCamera = cameraOne.GetComponent<Camera>();
			cameraTwo.SetActive (false);
			gameManager.player = this.gameObject;
			gameManager.TurnOn ();
			gameManager.moveCount = 3;
			moveTurn = true;
			//CmdSpawnObjectsP1 ();

		}

		if (playerCount == 2) {

			gameObject.tag = "Player Two";
			gameManager.tag = "Game Manager Two";
			signalCircle.tag = "Signal Wheel Two";
			gameManager.AssignId ();
			playerId = 2;
			myCamera = cameraTwo.GetComponent<Camera>();
			cameraOne.SetActive (false);
			gameManager.player = this.gameObject;
			gameManager.TurnOff ();
			gameManager.moveCount = 0;
			moveTurn = false;

			Scene currentScene = SceneManager.GetActiveScene ();
			string sceneName = currentScene.name;

			if (sceneName == "Ghost Prototype") {
				CmdSpawnObjectsP2 ();
			}

		}

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		lastPos = transform.position;
			
	}
	
	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

			if (moveTurn == true) {
			
				if (Input.GetMouseButtonDown (0) && moving == false) {
					RaycastHit hitInfo = new RaycastHit ();
				bool hit = Physics.Raycast (myCamera.ScreenPointToRay (Input.mousePosition), out hitInfo, 100.0f, layer_mask);
					if (hit) {
						if (hitInfo.transform.gameObject.tag == "Tile") {

							hitSquare = hitInfo.transform.gameObject;
							squarePos = new Vector3 (hitSquare.transform.position.x,
								hitSquare.transform.position.y + 0.875f,
								hitSquare.transform.position.z);
							ghostPos = new Vector3 (gameObject.transform.position.x,
								gameObject.transform.position.y,
								gameObject.transform.position.z);

							float distance = Vector3.Distance (ghostPos, squarePos);

						if (distance < 1.3f && distance > .5f) {
							
								moving = true;
								movesLeft = movesLeft - 1;
								gameManager.moveCount = movesLeft;
								glowSquare.SetActive (true);
								glowSquare.transform.position = new Vector3 (hitSquare.transform.position.x,
									hitSquare.transform.position.y + .05f,
									hitSquare.transform.position.z);
							}
						} 
					} 
				}

				if (moving == true) {

					ghostPos = new Vector3 (gameObject.transform.position.x,
						gameObject.transform.position.y,
						gameObject.transform.position.z);

					transform.position = Vector3.Lerp (ghostPos, squarePos, Time.deltaTime * speed);
					float distance = Vector3.Distance (ghostPos, squarePos);
					if (distance < .02f) {
						moveCount = moveCount + 1;
						moving = false;
						glowSquare.SetActive (false);
						transform.position = squarePos;
						lastPos = transform.position;
						
					if (moveCount > 2 && playerId == 1) {
							//gameManager.TurnOver ();
							//CmdSwitchToPlayerTwo ();
							movesLeft = 0;
							gameManager.moveCount = movesLeft;
							moveTurn = false;
							moveCount = 0;
							movesLeft = 3;

					} else if (moveCount > 2 && playerId == 2) {
							//gameManager.TurnOver ();
							//CmdSwitchToPlayerOne ();
							moveTurn = false;
							moveCount = 0;
							movesLeft = 0;
							gameManager.moveCount = movesLeft;
							movesLeft = 3;

						}
					}
				}
			}
		}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Wall") {
			transform.position = lastPos;
			moving = false;
			moveCount = moveCount + 1;
			glowSquare.SetActive (false);

			if (moveCount > 2 && playerId == 1) {
				//gameManager.TurnOver ();
				//CmdSwitchToPlayerTwo ();
				movesLeft = 0;
				gameManager.moveCount = movesLeft;
				moveTurn = false;
				moveCount = 0;
				movesLeft = 3;

			} else if (moveCount > 2 && playerId == 2) {
				//gameManager.TurnOver ();
				//CmdSwitchToPlayerOne ();
				moveTurn = false;
				moveCount = 0;
				movesLeft = 0;
				gameManager.moveCount = movesLeft;
				movesLeft = 3;

			}
		}

		if (other.name == "Ghost(Clone)") {

			Color fullAColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			GameObject winScreen = GameObject.FindWithTag ("Win Screen");
			winScreen.GetComponent<Image> ().color = fullAColor;
		
		}
	}


	public void SwitchPlayers (){
		
		if (playerId == 1) {
			moveTurn = false;
			CmdSwitchToPlayerTwo ();

		} else if (playerId == 2) {
			moveTurn = false;
			CmdSwitchToPlayerOne ();
		}
	}

	[Command]
	void CmdSwitchToPlayerTwo () {
		
			gameManager.playerTurn = 2;
			RpcSwitchOnPlayerTwo ();
	
	}

	[Command]
	void CmdSwitchToPlayerOne () {
		
		if (gameManager == null) {
			GameObject gameManagerObjectOther = GameObject.Find ("Game Manager");
			gameManager = gameManagerObjectOther.GetComponent<GameManagerScript> ();
			gameManager.playerTurn = 1;
			RpcSwitchOnPlayerOne ();

		} else {
		
			gameManager.playerTurn = 1;
			RpcSwitchOnPlayerOne ();
		}
			
	}

	[ClientRpc]
	void RpcSwitchOnPlayerTwo (){
	
		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
			gameManager.TurnOnPlayerTwo ();
		} else {

			gameManager.TurnOnPlayerTwo ();

		}
	}

	[ClientRpc]
	void RpcSwitchOnPlayerOne (){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
			gameManager.TurnOnPlayerOne ();

		} else {

			gameManager.TurnOnPlayerOne ();

		}
	}
		

//	[Command]
//	public void CmdUpdateColors () {
//		
//
//		GameManagerScript gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript>();
//		gameManagerScript.wedge1 = wedge1;
//		gameManagerScript.wedge2 = wedge2;
//		gameManagerScript.wedge3 = wedge3;
//		gameManagerScript.UpdateColors ();
//
//	}
//
//	[ClientRpc]
//	void RpcUpdateColors () {
//	
//		if (gameManager == null) {
//
//			GameObject gameManagerObject = GameObject.Find ("Game Manager");
//			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
//
//		}
//
//		Debug.Log (playerId);
//		Debug.Log (wedge1);
//
//		gameManager.wedge1 = wedge1;
//		gameManager.wedge2 = wedge2;
//		gameManager.wedge3 = wedge3;
//
//	}
//		
//	public void UpdateWedgeOne() {
//
//		if (gameManager == null) {
//
//			GameObject gameManagerObject = GameObject.Find ("Game Manager");
//			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
//
//		}
//
//		gameManager.wedge1 = wedge1;
//
//	}
//
//	public void UpdateWedgeTwo() {
//
//		if (gameManager == null) {
//
//			GameObject gameManagerObject = GameObject.Find ("Game Manager");
//			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
//
//		}
//
//		gameManager.wedge2 = wedge2;
//
//	}
//
//	public void UpdateWedgeThree() {
//
//		if (gameManager == null) {
//
//			GameObject gameManagerObject = GameObject.Find ("Game Manager");
//			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();
//
//		}
//
//		gameManager.wedge3 = wedge3;
//
//	}

	[Command]
	public void CmdSendButton (){

			RpcSendButton ();

	}


	[ClientRpc]
	public void RpcSendButton(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		GameObject signalCircleOrig = GameObject.Find ("Signal Circle Original");

		GameObject signalMessageFind = GameObject.Find ("Signal Circle From Partner");
		GameManagerScript gameManagerFind = GameObject.Find ("Game Manager").GetComponent <GameManagerScript> ();


		if (signalCircleOrig.tag == "Signal Wheel One" && gameManagerFind.playerTurn == 2) {
			Image wedgeWheel1 = signalMessageFind.transform.GetChild (0).gameObject.GetComponent <Image> ();
			Image wedgeWheel2 = signalMessageFind.transform.GetChild (1).gameObject.GetComponent <Image> ();
			Image wedgeWheel3 = signalMessageFind.transform.GetChild (2).gameObject.GetComponent <Image> ();
		
			wedgeWheel1.color = gameManagerFind.wedge1;
			wedgeWheel2.color = gameManagerFind.wedge2;
			wedgeWheel3.color = gameManagerFind.wedge3;

		}

		if (signalCircleOrig.tag == "Signal Wheel Two" && gameManagerFind.playerTurn == 1) {
			Image wedgeWheel1 = signalMessageFind.transform.GetChild (0).gameObject.GetComponent <Image> ();
			Image wedgeWheel2 = signalMessageFind.transform.GetChild (1).gameObject.GetComponent <Image> ();
			Image wedgeWheel3 = signalMessageFind.transform.GetChild (2).gameObject.GetComponent <Image> ();

			wedgeWheel1.color = gameManagerFind.wedge1;
			wedgeWheel2.color = gameManagerFind.wedge2;
			wedgeWheel3.color = gameManagerFind.wedge3;

		}

		if (signalCircleOrig.tag == "Signal Wheel One" && gameManagerFind.playerTurn == 1) {
		
			Image wheel1 = signalCircleOrig.transform.GetChild(0).gameObject.GetComponent <Image>();
			Image wheel2 = signalCircleOrig.transform.GetChild(1).gameObject.GetComponent <Image>();
			Image wheel3 = signalCircleOrig.transform.GetChild(2).gameObject.GetComponent <Image>();

			wheel1.color = white;
			wheel2.color = white;
			wheel3.color = white;

			gameManager.BackToWhite ();
		
		}

		if (signalCircleOrig.tag == "Signal Wheel Two" && gameManagerFind.playerTurn == 2) {

			Image wheel1 = signalCircleOrig.transform.GetChild(0).gameObject.GetComponent <Image>();
			Image wheel2 = signalCircleOrig.transform.GetChild(1).gameObject.GetComponent <Image>();
			Image wheel3 = signalCircleOrig.transform.GetChild(2).gameObject.GetComponent <Image>();

			wheel1.color = white;
			wheel2.color = white;
			wheel3.color = white;

			gameManager.BackToWhite ();

		}
	
	}

	[Command]
	public void CmdAddHit () {
		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.hitCount = gameManager.hitCount + 1;
	
	}

	[Command]
	public void CmdAddKey () {
		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.keyCount = gameManager.keyCount + 1;
		gameManager.CheckPortal ();

	}

	[Command]
	public void CmdSpawnPortals(){

		GameObject P1Portal = GameObject.Find ("First Portal Locale");
		GameObject portal1 = Instantiate(Resources.Load("Portal P1", typeof(GameObject))) as GameObject;
		portal1.transform.position = P1Portal.transform.position;
		NetworkServer.Spawn(portal1);

		GameObject P2Portal = GameObject.Find ("Second Portal Locale");
		GameObject portal2 = Instantiate(Resources.Load("Portal P2", typeof(GameObject))) as GameObject;
		portal2.transform.position = P2Portal.transform.position;
		NetworkServer.Spawn(portal2);

		GameObject exitPortalLocale = GameObject.Find ("Exit Portal Locale");
		GameObject exitPortal = Instantiate(Resources.Load("Exit Portal", typeof(GameObject))) as GameObject;
		exitPortal.transform.position = exitPortalLocale.transform.position;
		NetworkServer.Spawn(exitPortal);

	}



	[Command]
	public void CmdWedgeOnePink(){
		RpcWedgeOnePink ();
	}

	[ClientRpc]
	void RpcWedgeOnePink(){
		
		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.pink;
	
	}

	[Command]
	public void CmdWedgeOneBlue(){
		RpcWedgeOneBlue ();
	}

	[ClientRpc]
	void RpcWedgeOneBlue(){
		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.blue;

	}

	[Command]
	public void CmdWedgeOneGreen(){
		RpcWedgeOneGreen ();
	}

	[ClientRpc]
	void RpcWedgeOneGreen(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.green;

	}

	[Command]
	public void CmdWedgeOnePurple(){
		RpcWedgeOnePurple ();
	}

	[ClientRpc]
	void RpcWedgeOnePurple(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.purple;

	}

	[Command]
	public void CmdWedgeOneBlack(){
		RpcWedgeOneBlack();
	}

	[ClientRpc]
	void RpcWedgeOneBlack(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.black;

	}

	[Command]
	public void CmdWedgeOneWhite(){
		RpcWedgeOneWhite ();
	}

	[ClientRpc]
	void RpcWedgeOneWhite(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge1 = gameManager.white;

	}

	[Command]
	public void CmdWedgeTwoPink(){
		RpcWedgeTwoPink ();
	}

	[ClientRpc]
	void RpcWedgeTwoPink(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.pink;

	}

	[Command]
	public void CmdWedgeTwoBlue(){
		RpcWedgeTwoBlue ();
	}

	[ClientRpc]
	void RpcWedgeTwoBlue(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.blue;

	}

	[Command]
	public void CmdWedgeTwoGreen(){
		RpcWedgeTwoGreen ();
	}

	[ClientRpc]
	void RpcWedgeTwoGreen(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.green;

	}

	[Command]
	public void CmdWedgeTwoPurple(){
		RpcWedgeTwoPurple ();
	}

	[ClientRpc]
	void RpcWedgeTwoPurple(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.purple;

	}

	[Command]
	public void CmdWedgeTwoBlack(){
		RpcWedgeTwoBlack ();
	}

	[ClientRpc]
	void RpcWedgeTwoBlack(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.black;

	}

	[Command]
	public void CmdWedgeTwoWhite(){
		RpcWedgeTwoWhite ();
	}

	[ClientRpc]
	void RpcWedgeTwoWhite(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge2 = gameManager.white;

	}

	[Command]
	public void CmdWedgeThreePink(){
		RpcWedgeThreePink ();
	}

	[ClientRpc]
	void RpcWedgeThreePink(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.pink;

	}

	[Command]
	public void CmdWedgeThreeBlue(){
		RpcWedgeThreeBlue ();
	}

	[ClientRpc]
	void RpcWedgeThreeBlue(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.blue;

	}

	[Command]
	public void CmdWedgeThreeGreen(){
		RpcWedgeThreeGreen ();
	}

	[ClientRpc]
	void RpcWedgeThreeGreen(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.green;

	}

	[Command]
	public void CmdWedgeThreePurple(){
		RpcWedgeThreePurple ();
	}

	[ClientRpc]
	void RpcWedgeThreePurple(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.purple;

	}

	[Command]
	public void CmdWedgeThreeBlack(){
		RpcWedgeThreeBlack ();
	}

	[ClientRpc]
	void RpcWedgeThreeBlack(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.black;

	}

	[Command]
	public void CmdWedgeThreeWhite(){
		RpcWedgeThreeWhite ();
	}

	[ClientRpc]
	void RpcWedgeThreeWhite(){

		if (gameManager == null) {

			GameObject gameManagerObject = GameObject.Find ("Game Manager");
			gameManager = gameManagerObject.GetComponent<GameManagerScript> ();

		}

		gameManager.wedge3 = gameManager.white;

	}
	[Command]
	void CmdSpawnObjectsP1 (){
		GameObject firstKey = GameObject.Find ("First Key");
		GameObject keyP1 = Instantiate(Resources.Load("Key P1", typeof(GameObject))) as GameObject;
		keyP1.transform.position = firstKey.transform.position;
		NetworkServer.Spawn(keyP1);

		GameObject secondKey = GameObject.Find ("Second Key");
		GameObject keyP12 = Instantiate(Resources.Load("Key P1", typeof(GameObject))) as GameObject;
		keyP12.transform.position = secondKey.transform.position;
		NetworkServer.Spawn(keyP12);
	}

	[Command]
	void CmdSpawnObjectsP2 (){
		GameObject firstKey = GameObject.Find ("First Key");
		GameObject keyP1 = Instantiate(Resources.Load("Key P1", typeof(GameObject))) as GameObject;
		keyP1.transform.position = firstKey.transform.position;
		NetworkServer.Spawn(keyP1);

		GameObject secondKey = GameObject.Find ("Second Key");
		GameObject keyP12 = Instantiate(Resources.Load("Key P1", typeof(GameObject))) as GameObject;
		keyP12.transform.position = secondKey.transform.position;
		NetworkServer.Spawn(keyP12);

		GameObject thirdKey = GameObject.Find ("Third Key");
		GameObject keyP2 = Instantiate(Resources.Load("Key P2", typeof(GameObject))) as GameObject;
		keyP2.transform.position = thirdKey.transform.position;
		NetworkServer.Spawn(keyP2);

		GameObject fourthKey = GameObject.Find ("Fourth Key");
		GameObject keyP22 = Instantiate(Resources.Load("Key P2", typeof(GameObject))) as GameObject;
		keyP22.transform.position = fourthKey.transform.position;
		NetworkServer.Spawn(keyP22);
	}

	[ClientRpc]
	public void RpcWinScreen (){
		winScreen = GameObject.FindGameObjectWithTag ("Win Screen");
		winScreen.GetComponent<Image>().color = new Color(1.0f,1.0f,1.0f,1.0f);

	}
}