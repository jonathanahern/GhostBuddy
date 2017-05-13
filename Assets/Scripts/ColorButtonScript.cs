using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorButtonScript : MonoBehaviour {

	public Image firstTab;
	public Image secondTab;
	public Image thirdTab;

	public int tabCount = 0;

	public int[] wedge = {96,96,96};

	private Color pink;
	private Color blue;
	private Color green;
	private Color black;
	private Color purple;
	private Color white;
	public Color grey;

	public bool noWhite;

	private Color[] colorList = new Color[99];

	public TurnManagerScript turnManager;
	public WheelPanelScript wheelPan;
	AudioSource source;
	public AudioClip buzz;
	public AudioClip bonk;


	void Start () {

		source = GetComponent<AudioSource> ();

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		//pink90,green91,purple92,blue93,black94,white95

		wedge [0] = 96;
		wedge [1] = 96;
		wedge [2] = 96;

		firstTab.color = grey;
		secondTab.color = grey;
		thirdTab.color = grey;

		colorList [90] = pink;
		colorList [91] = green;
		colorList [92] = purple;
		colorList [93] = blue;
		colorList [94] = black;
		colorList [95] = white;
		colorList [96] = grey;

	}

	public void ChangeWedgeOne () {
	
		source.PlayOneShot (buzz, .2f);

		if (firstTab.color == white) {
			firstTab.color = pink;
			wedge [0] = 90;
			return;
		} else if (firstTab.color == pink) {
			firstTab.color = blue;
			wedge [0] = 93;
			return;
		} else if (firstTab.color == blue) {
			firstTab.color = green;
			wedge [0] = 91;
			return;
		} else if (firstTab.color == green) {
			firstTab.color = purple;
			wedge [0] = 92;
			return;
		} else if (firstTab.color == purple) {
			if (noWhite == true) {
				firstTab.color = pink;
				wedge [0] = 90;
				return;
			} else {
				firstTab.color = white;
				wedge [0] = 95;
				return;
			}
		} else if (firstTab.color == grey) {

			if (noWhite == true) {
				firstTab.color = pink;
				wedge [0] = 90;
				return;
			} else {
				firstTab.color = white;
				wedge [0] = 95;
				return;
			}
		}

	}

	public void ChangeWedgeTwo () {

		source.PlayOneShot (buzz, .2f);

		if (secondTab.color == white) {
			secondTab.color = pink;
			wedge [1] = 90;
			return;
		} else if (secondTab.color == pink) {
			secondTab.color = blue;
			wedge [1] = 93;
			return;
		} else if (secondTab.color == blue) {
			secondTab.color = green;
			wedge [1] = 91;
			return;
		} else if (secondTab.color == green) {
			secondTab.color = purple;
			wedge [1] = 92;
			return;
		} else if (secondTab.color == purple) {
			if (noWhite == true) {
				secondTab.color = pink;
				wedge [1] = 90;
				return;
			} else {
				secondTab.color = white;
				wedge [1] = 95;
				return;
			}
		} else if (secondTab.color == grey) {
			if (noWhite == true) {
				secondTab.color = pink;
				wedge [1] = 90;
				return;
			} else {
				secondTab.color = white;
				wedge [1] = 95;
				return;
			}
		}

	}

	public void ChangeWedgeThree () {

		source.PlayOneShot (buzz, .2f);

		if (thirdTab.color == white) {
			thirdTab.color = pink;
			wedge [2] = 90;
			return;
		} else if (thirdTab.color == pink) {
			thirdTab.color = blue;
			wedge [2] = 93;
			return;
		} else if (thirdTab.color == blue) {
			thirdTab.color = green;
			wedge [2] = 91;
			return;
		} else if (thirdTab.color == green) {
			thirdTab.color = purple;
			wedge [2] = 92;
			return;
		} else if (thirdTab.color == purple) {
			if (noWhite == true) {
				thirdTab.color = pink;
				wedge [2] = 90;
				return;
			} else {
				thirdTab.color = white;
				wedge [2] = 95;
				return;
			}
		} else if (thirdTab.color == grey) {
			if (noWhite == true) {
				thirdTab.color = pink;
				wedge [2] = 90;
				return;
			} else {
				thirdTab.color = white;
				wedge [2] = 95;
				return;
			}
		}

	}


	//pink90,green91,purple92,blue93,black94,white95
	public void TurnPink () {

		Color newColor = new Color (.99f, .56f, .556f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 90;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 90;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 90;
			tabCount = 0;
			return;
		}

	}
	//pink90,green91,purple92,blue93,black94,white95
	public void TurnGreen () {


		Color newColor = new Color (0.0f, .286f, .235f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 91;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 91;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 91;
			tabCount = 0;
			return;
		}
	}
	//pink90,green91,purple92,blue93,black94,white95
	public void TurnPurple (){

		Color newColor = new Color (.4f, .255f, .51f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 92;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 92;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 92;
			tabCount = 0;
			return;
		}
	
	}
	//pink90,green91,purple92,blue93,black94,white95
	public void TurnBlue (){

		Color newColor = new Color (.09f, .31f, .447f, 1.0f);


		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 93;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 93;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 93;
			tabCount = 0;
			return;
		}

	}
	//pink90,green91,purple92,blue93,black94,white95
	public void TurnBlack (){

		Color newColor = new Color (0.0f, 0.0f, 0.0f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 94;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 94;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 94;
			tabCount = 0;
			return;
		}
	
	}
	//pink90,green91,purple92,blue93,black94,white95
	public void TurnWhite (){

		Color newColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		if (tabCount == 0){
			firstTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[0] = 95;
			return;
		}

		else if (tabCount == 1){
			secondTab.color = newColor;
			tabCount = tabCount + 1;
			wedge[1] = 95;
			return;
		}

		else if (tabCount == 2){
			thirdTab.color = newColor;
			wedge[2] = 95;
			tabCount = 0;
			return;
		}
			
	}

	public void	BackToWhite(){

		Debug.Log ("wedges: " + wedge [0] + wedge [1] + wedge [2]);

		if (wedge [0] == 96 || wedge [1] == 96 || wedge [2] == 96) {
			source.PlayOneShot (bonk, .15f);
			return;
		}

		SendWheelToTurnManager ();


	}

	public void SendWheelToTurnManager () {

		//Debug.Log ("Colors sent");
		turnManager.colorArray = wedge;
		turnManager.PlaceWheelIcon ();
		wheelPan.MoveDown ();

		firstTab.color = grey;
		secondTab.color = grey;
		thirdTab.color = grey;
	
	}

	public void BackToGrey(){

		wedge [0] = 96;
		wedge [1] = 96;
		wedge [2] = 96;

	}
}
