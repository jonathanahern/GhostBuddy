using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostScript : MonoBehaviour {

	GameObject gameManager;
	TurnManagerScript turnManager;

	public GameObject canvas;
	public GameObject speechBubble;
	public Text speechText;

	public MeshRenderer body;
	public MeshRenderer leftArm;
	public MeshRenderer rightArm;

	private bool previewTime;


	// Use this for initialization
	void Start () {
		
		Invoke ("FindManager", 2.0f);
		
	}

	void Update (){
	
		if (previewTime == true) {

			float alpha = (Mathf.PingPong(Time.time * .5f, .5f)) + .3f;

			Color colorGoal = new Color (body.material.color.r,
										body.material.color.g,
				                		 body.material.color.b,
				                 		alpha);

			body.material.color = colorGoal;
			leftArm.material.color = colorGoal;
			rightArm.material.color = colorGoal;
		}
	
	
	}
		

	void FindManager () {

		gameManager = GameObject.FindGameObjectWithTag ("Game Manager Local");
		turnManager = gameManager.GetComponent <TurnManagerScript> ();

	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Slime") {

			Debug.Log ("SLIMED");

			turnManager.LoseGame ();
			turnManager.gameLost = true;
			turnManager.forwardBlueGhost = false;
			turnManager.forwardPinkGhost = false;

		}


		if (other.tag == "Player") {
		
			turnManager.winning = true;
		
		}
		
		if (other.tag == "Wall") {
			
			if (gameObject.name == "Pink Ghost(Clone)") {
				
				turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;

			}

			if (turnManager.preview == true) {
			
				GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
				int triggerCount = triggers.Length;
				triggers [triggerCount - 1].GetComponent<TriggerScript> ().hitWall = true;
			
			}


		}

		if (other.tag == "Hidden Wall" && turnManager.preview == false) {

			if (gameObject.name == "Pink Ghost(Clone)") {

				turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;

			}
		
			other.GetComponent<HiddenWallScript> ().SwitchMat ();

		}

		if (other.tag == "Border" && turnManager.preview == false) {

			if (gameObject.name == "Pink Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardPinkGhost = false;
				GetComponent<Rigidbody> ().useGravity = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardBlueGhost = false;
				GetComponent<Rigidbody> ().useGravity = true;
			}
		}

	}

	void OnTriggerExit (Collider other)
	{

		if (other.tag == "Player") {

			turnManager.winning = false;

		}

	}

	public void ShowBubble (string speech){
	
		speechText.text = speech;
		speechBubble.SetActive (true);
		Invoke ("HideBubble", 3.5f);
	}

	void HideBubble(){
	
		speechBubble.SetActive (false);

	}

	public void AssignCameras(string playerId){
	
		if (playerId == "1") {
			canvas.GetComponent<LookAtScript> ().target = GameObject.FindGameObjectWithTag ("Camera Two");

		} else {
			canvas.GetComponent<LookAtScript> ().target = GameObject.FindGameObjectWithTag ("Camera One");
		}
	
	}

	public void PreviewModeOn (){
	
//		body.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
//		leftArm.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
//		rightArm.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		previewTime = true;
	
	}

	public void PreviewModeOff (){

		previewTime = false;

		Color colorGoal = new Color (body.material.color.r,
			body.material.color.g,
			body.material.color.b,
			1.0f);

		body.material.color = colorGoal;
		leftArm.material.color = colorGoal;
		rightArm.material.color = colorGoal;


	}


}
