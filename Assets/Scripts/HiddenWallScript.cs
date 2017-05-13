using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWallScript : MonoBehaviour {

	public Material transferMat;
	public Material wallMat;
	public Material hiddenMat;
	private bool colorUp;
	private MeshRenderer myMesh;

	public bool notAWall = false;
	public bool revealed = false;
	public bool playOneCantSee = false;
	public bool playTwoCantSee = false;
	public bool noOneCanSee = false;

	void Start(){
	
		myMesh = GetComponent<MeshRenderer> ();
	
	}


	void Update (){

//		if (Input.GetKeyDown (KeyCode.J)) {
//		
//			GetComponent<Renderer>().material = transferMat;
//			gameObject.layer = LayerMask.NameToLayer ("Default");
//			colorUp = true;
//		
//		}

		if (colorUp == true){

			Color currentColor = myMesh.material.color;
			Color lerpedColor = Color.Lerp(currentColor, Color.black, Time.deltaTime * 1.0f);
			myMesh.material.color = lerpedColor;

			if (lerpedColor.a < .1f) {
				GetComponent<Renderer>().material = wallMat;
				colorUp = false;
			}
		}
	}
		
	public void SwitchMat () {

		GetComponent<Renderer> ().material = transferMat;
		gameObject.layer = LayerMask.NameToLayer ("Default");
		colorUp = true;
		if (notAWall == false) {
			gameObject.tag = "New Wall";
			gameObject.name = "Wall";
		}
		revealed = true;

	}

	public void SwitchBackToHidden () {
		colorUp = false;
		if (playOneCantSee == true) {
			gameObject.layer = LayerMask.NameToLayer ("Player Two Can't See");
		} else if (playTwoCantSee ==true){
			gameObject.layer = LayerMask.NameToLayer ("Player One Can't See");
		} else if (noOneCanSee ==true){
			gameObject.layer = LayerMask.NameToLayer ("No One Can See");
		}

		if (notAWall == false) {
		
			gameObject.tag = "Hidden Wall";
			gameObject.name = "Hidden Wall";
		
		}

		GetComponent<Renderer> ().material = hiddenMat;
		revealed = false;

	}

}
