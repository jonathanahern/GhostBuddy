using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBackground : MonoBehaviour {

	private Color currentColor;
	private Color pink;
	private Color green;
	private Color purple;
	private Color blue;
	private Color white;
	private Color black;

	private Color colorOneGoal;
	private Color colorTwoGoal;
	private Color colorThreeGoal;

	private Color currentColorOne;
	private Color currentColorTwo;
	private Color currentColorThree;

	private Color lastColorOne;
	private Color lastColorTwo;
	private Color lastColorThree;

	bool changeColorOne;
	bool changeColorTwo;
	bool changeColorThree;

	private Color colorGoal;

	MeshRenderer background;
	public float rotateSpeed;

//	public int[] fromPinkGhostColors = {0,0,0};
//	public int[] fromBlueGhostColors = {0,0,0};

	//private Color[] colorList = new Color[99];
	public List<Color> colorList = new List<Color>();

	// Use this for initialization
	void Start () {

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		colorList.Add (pink);
		colorList.Add (green);
		colorList.Add (purple);
		colorList.Add (blue);

//		colorOne = pink;
//		colorTwo = blue;
//		colorThree = white;
//		colorGoal = colorOne;

		background = GetComponent<MeshRenderer> ();
		background.sharedMaterial.SetColor ("_TopColor", white);
		background.sharedMaterial.SetColor ("_MidColor", black);
		background.sharedMaterial.SetColor ("_BottomColor", white);

		lastColorOne = white;
		lastColorTwo = black;
		lastColorThree = white;

		InvokeRepeating ("ChangeTopColor", 2.0f, 12.0f);
		InvokeRepeating ("ChangeMidColor", 6.0f, 12.0f);
		InvokeRepeating ("ChangeBottomColor", 10.0f, 12.0f);

	}
	
	// Update is called once per frame
	void Update () {

		background.sharedMaterial.SetFloat ("_Rotate", Mathf.Repeat (Time.time * rotateSpeed, 1.0f));

		if (changeColorOne == true) {

			currentColorOne = Color.Lerp(currentColor, colorOneGoal, Time.deltaTime * 1.5f);
			currentColor = currentColorOne;

			background.sharedMaterial.SetColor ("_TopColor", currentColorOne);

			float dif = Mathf.Abs (currentColor.g - colorOneGoal.g);

			if (dif < .01f) {
			
				background.sharedMaterial.SetColor ("_TopColor", colorOneGoal);
				changeColorOne = false;
			
			}
		
		}

		if (changeColorTwo == true) {

			currentColorTwo = Color.Lerp(currentColor, colorTwoGoal, Time.deltaTime * 1.5f);
			currentColor = currentColorTwo;

			background.sharedMaterial.SetColor ("_MidColor", currentColorTwo);

			float dif = Mathf.Abs (currentColor.g - colorTwoGoal.g);

			if (dif < .01f) {

				background.sharedMaterial.SetColor ("_MidColor", colorTwoGoal);
				changeColorTwo = false;

			}

		}

		if (changeColorThree == true) {

			currentColorThree = Color.Lerp(currentColor, colorThreeGoal, Time.deltaTime * 1.5f);
			currentColor = currentColorThree;

			background.sharedMaterial.SetColor ("_BottomColor", currentColorThree);

			float dif = Mathf.Abs (currentColor.g - colorThreeGoal.g);

			if (dif < .01f) {

				background.sharedMaterial.SetColor ("_BottomColor", colorThreeGoal);
				changeColorThree = false;

			}

		}

	}

	void PopulateColorList () {

		colorList.Add (pink);
		colorList.Add (green);
		colorList.Add (purple);
		colorList.Add (blue);
		colorList.Add (black);
		colorList.Add (white);

	}

//	void ChangeColors () {
//
//		if (colorGoal == colorOne) {
//		
//			colorGoal = colorTwo;
//
//		} else if (colorGoal == colorTwo) {
//		
//			colorGoal = colorThree;
//
//		} else if (colorGoal == colorThree) {
//		
//			colorGoal = colorOne;
//
//		}
//	}

	void ChangeTopColor () {

		if (colorList.Count == 0) {
		
			PopulateColorList ();

		}

		int colorNum = Random.Range (0, colorList.Count);
		colorOneGoal = colorList [colorNum];
		currentColor = lastColorOne;
		lastColorOne = colorList [colorNum];
		colorList.RemoveAt (colorNum);

		changeColorOne = true;

	}

	void ChangeMidColor () {

		if (colorList.Count == 0) {

			PopulateColorList ();

		}

		int colorNum = Random.Range (0, colorList.Count);
		colorTwoGoal = colorList [colorNum];
		currentColor = lastColorTwo;
		lastColorTwo = colorList [colorNum];
		colorList.RemoveAt (colorNum);

		changeColorTwo = true;

	}

	void ChangeBottomColor () {

		if (colorList.Count == 0) {

			PopulateColorList ();

		}

		int colorNum = Random.Range (0, colorList.Count);
		colorThreeGoal = colorList [colorNum];
		currentColor = lastColorThree;
		lastColorThree = colorList [colorNum];
		colorList.RemoveAt (colorNum);

		changeColorThree = true;

	}
		
}
