using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorButtonScript : MonoBehaviour {

	public Image firstTab;
	public Image secondTab;
	public Image thirdTab;

	public int tabCount = 0;

	public GameObject player;

	public GameObject gameManagerObject;

	private bool foundPlayer = false;


	// Use this for initialization
	void Start () {

	}


	public void TurnPink () {
		if (foundPlayer == false) {
		
			FindPlayer ();

		}

		Color newColor = new Color (.99f, .56f, .556f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOnePink ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoPink ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreePink ();
			tabCount = 0;
			return;
		}

	}

	public void TurnGreen () {

		if (foundPlayer == false) {

			FindPlayer ();

		}

		Color newColor = new Color (0.0f, .286f, .235f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOneGreen ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoGreen ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreeGreen ();
			tabCount = 0;
			return;
		}
	
	}

	public void TurnPurple (){

		if (foundPlayer == false) {

			FindPlayer ();

		}

		Color newColor = new Color (.4f, .255f, .51f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOnePurple ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoPurple ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreePurple ();
			tabCount = 0;
			return;
		}
	
	}

	public void TurnBlue (){

		if (foundPlayer == false) {

			FindPlayer ();

		}

		Color newColor = new Color (.09f, .31f, .447f, 1.0f);


		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOneBlue ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoBlue ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreeBlue ();
			tabCount = 0;
			return;
		}
	


	}

	public void TurnBlack (){

		if (foundPlayer == false) {

			FindPlayer ();

		}

		Color newColor = new Color (0.0f, 0.0f, 0.0f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOneBlack ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoBlack ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreeBlack ();
			tabCount = 0;
			return;
		}
	
	}


	public void TurnWhite (){

		if (foundPlayer == false) {

			FindPlayer ();

		}

		Color newColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeOneWhite ();
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			player.GetComponent<GhostMovementScript> ().CmdWedgeTwoWhite ();
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			player.GetComponent<GhostMovementScript> ().CmdWedgeThreeWhite ();
			tabCount = 0;
			return;
		}

	}

	public void EndTurnSignal (){
	
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		tabCount = 0;

		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = false;
		}

//		GameObject[] buttonHolders = GameObject.FindGameObjectsWithTag("Button Holder");

//		foreach (GameObject buttonHolder in buttonHolders) {
//			buttonHolder.GetComponent<ButtonHolderScript> ().Invoke ("MakeItTrue", 1.0f);
//		}

		player.GetComponent <GhostMovementScript> ().SwitchPlayers ();

	}

	void FindPlayer () {

		if (gameManagerObject.tag == "Game Manager One") {
		
			player = GameObject.FindGameObjectWithTag ("Player One");

		}

		else if (gameManagerObject.tag == "Game Manager Two") {

			player = GameObject.FindGameObjectWithTag ("Player Two");

		}

	}

	void TurnOffButtons () {

		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		tabCount = 0;

		foreach (GameObject button in buttons) {
			button.GetComponent<ButtonScript> ().buttonBool = false;
		}

	}

}
