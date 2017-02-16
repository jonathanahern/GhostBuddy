using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWallScript : MonoBehaviour {

	public Material transferMat;
	public Material wallMat;
	private bool colorUp;
	private bool alreadyHit = false;
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
			gameObject.name = "Wall";

			if (lerpedColor.a < .05f) {
				GetComponent<Renderer>().material = wallMat;
				gameObject.tag = "Wall";
				colorUp = false;
			}


		}


	}

	void OnTriggerEnter(Collider other) {
	
		if (other.tag == "Player") {

			if (alreadyHit == false) {
				
				GetComponent<Renderer> ().material = transferMat;
				gameObject.layer = LayerMask.NameToLayer ("Default");
				colorUp = true;
				alreadyHit = true;
			}

		}

	}

	public void SwitchTag () {





	}

}
