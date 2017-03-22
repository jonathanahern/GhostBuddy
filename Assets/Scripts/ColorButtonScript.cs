using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorButtonScript : MonoBehaviour {

	public Image firstTab;
	public Image secondTab;
	public Image thirdTab;

	public int tabCount = 0;

	public int[] wedge = {95,95,95};

	private Color pink;
	private Color blue;
	private Color green;
	private Color black;
	private Color purple;
	private Color white;

	private Color[] colorList = new Color[99];

	public TurnManagerScript turnManager;
	AudioSource source;
	public AudioClip buzz;


	void Start () {

		source = GetComponent<AudioSource> ();

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		//pink90,green91,purple92,blue93,black94,white95

		colorList [90] = pink;
		colorList [91] = green;
		colorList [92] = purple;
		colorList [93] = blue;
		colorList [94] = black;
		colorList [95] = white;


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
			firstTab.color = white;
			wedge [0] = 95;
			return;
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
			secondTab.color = white;
			wedge [1] = 95;
			return;
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
			thirdTab.color = white;
			wedge [2] = 95;
			return;
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
		
		SendWheelToTurnManager ();


		Color newColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		firstTab.color = newColor;
		secondTab.color = newColor;
		thirdTab.color = newColor;

	}

	public void SendWheelToTurnManager () {

		turnManager.colorArray = wedge; 
	
	}
}
