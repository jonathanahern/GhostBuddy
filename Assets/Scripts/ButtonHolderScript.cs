using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonHolderScript : MonoBehaviour {

	public bool makeButton = true;
	public Canvas mainCanvas;

	public Button [] buttonArray = new Button[5];

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (makeButton == true){
			
			if (transform.childCount == 0) {

				Button buttonPrefab = buttonArray[Random.Range (0,buttonArray.GetLength (0))] as Button;
				Button newButton = (Button)Instantiate(buttonPrefab);
				newButton.transform.SetParent(gameObject.transform,false);
				makeButton = false;

			}
		}
	}
		
	public void MakeItTrue()

	{
		if (transform.childCount == 0) {

			makeButton = true;

		}
	}
}
