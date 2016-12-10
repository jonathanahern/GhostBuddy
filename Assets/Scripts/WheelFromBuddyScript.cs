using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WheelFromBuddyScript : NetworkBehaviour {

	public Image[] wedge;

	public TurnManagerScript turnManager;
	public int networkNum;

	public int[] fromPinkGhostColors = {0,0,0};
	public int[] fromBlueGhostColors = {0,0,0};

	private Color pink;
	private Color blue;
	private Color green;
	private Color black;
	private Color purple;
	private Color white;

	private Color[] colorList = new Color[99];

	// Use this for initialization
	void Start () {

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

	public void FillPinkColorArray () {

			for (int i = 0; i < fromPinkGhostColors.Length; i++) {
				wedge [i].color = colorList [fromPinkGhostColors [i]];

		}
	}

	public void FillBlueColorArray () {
		
			for (int i = 0; i < fromBlueGhostColors.Length; i++) {
				wedge [i].color = colorList [fromBlueGhostColors [i]];
		}
	}
}
