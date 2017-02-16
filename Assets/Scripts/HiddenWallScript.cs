using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWallScript : MonoBehaviour {

	public Material transferMat;
	public Material wallMat;
	private bool colorUp;
	private MeshRenderer myMesh;

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

			if (lerpedColor.a < .08f) {
				GetComponent<Renderer>().material = wallMat;
				colorUp = false;
			}


		}


	}
		
	public void SwitchMat () {

		GetComponent<Renderer> ().material = transferMat;
		gameObject.layer = LayerMask.NameToLayer ("Default");
		colorUp = true;
		gameObject.tag = "Wall";
		gameObject.name = "Wall";

	}

}
