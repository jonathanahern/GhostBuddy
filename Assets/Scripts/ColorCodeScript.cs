using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCodeScript : MonoBehaviour {

	private Color currentColor;
	private Color pink;
	private Color green;
	private Color purple;
	private Color blue;
	private Color white;
	private Color black;

	private Color colorOne;
	private Color colorTwo;
	private Color colorThree;

	private Color colorGoal;

	MeshRenderer background;
	public float rotateSpeed;

	public int[] fromPinkGhostColors = {0,0,0};
	public int[] fromBlueGhostColors = {0,0,0};

	private Color[] colorList = new Color[99];

	// Use this for initialization
	void Start () {

		blue = new Color (.09f, .31f, .447f, 1.0f);
		green = new Color (0.0f, .286f, .235f, 1.0f);
		pink = new Color (.99f, .56f, .556f, 1.0f);
		purple = new Color (.4f, .255f, .51f, 1.0f);
		black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		white = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		colorList [90] = pink;
		colorList [91] = green;
		colorList [92] = purple;
		colorList [93] = blue;
		colorList [94] = black;
		colorList [95] = white;


		colorOne = pink;
		colorTwo = blue;
		colorThree = white;
		colorGoal = colorOne;

		background = GetComponent<MeshRenderer> ();
		background.sharedMaterial.SetColor ("_TopColor", colorList [90]);
		background.sharedMaterial.SetColor ("_MidColor", colorList [92]);
		background.sharedMaterial.SetColor ("_BottomColor", colorList [93]);

	}
	
	// Update is called once per frame
	void Update () {

		background.sharedMaterial.SetFloat ("_Rotate", Mathf.Repeat (Time.time * rotateSpeed, 1.0f));


	}

	void ChangeColors () {

		if (colorGoal == colorOne) {
		
			colorGoal = colorTwo;

		} else if (colorGoal == colorTwo) {
		
			colorGoal = colorThree;

		} else if (colorGoal == colorThree) {
		
			colorGoal = colorOne;

		}
	}

	public void FillPinkColorArray () {

		background.sharedMaterial.SetColor ("_TopColor", colorList [fromPinkGhostColors [0]]);
		background.sharedMaterial.SetColor ("_MidColor", colorList [fromPinkGhostColors [1]]);
		background.sharedMaterial.SetColor ("_BottomColor", colorList [fromPinkGhostColors [2]]);

	}

	public void FillBlueColorArray () {

		background.sharedMaterial.SetColor ("_TopColor", colorList [fromBlueGhostColors [0]]);
		background.sharedMaterial.SetColor ("_MidColor", colorList [fromBlueGhostColors [1]]);
		background.sharedMaterial.SetColor ("_BottomColor", colorList [fromBlueGhostColors [2]]);
	}

}
