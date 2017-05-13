using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GhostScript : MonoBehaviour {

	GameObject gameManager;
	TurnManagerScript turnManager;

	public GameObject canvas;
	public GameObject speechBubble;
	public Text speechText;

	public MeshRenderer body;
	public MeshRenderer heart;
	public MeshRenderer heartRing;
	public Material previewMaterial;
	public Material solidMaterial;

	public MeshRenderer[] bodyParts;
//	public MeshRenderer leftArm;
//	public MeshRenderer rightArm;
//	public MeshRenderer head;
	public LineRenderer lineRend;

	private bool previewTime;
	public AudioClip wallHit;
	private AudioSource source;

	public GameObject walk;
	public GameObject blink;
	Animator blinkAnimation;
	public SpriteRenderer blinker;
	Color eyeColor;
	Color heartColor;
	Color heartRingColor;

	public GameObject detectors;

	public bool inWindMill = false;
	public GameObject myWindMill;


	// Use this for initialization
	void Start () {
		
		Invoke ("FindManager", 2.0f);
		source = GetComponent<AudioSource>();
		blinkAnimation = blink.GetComponent<Animator> ();
		InvokeRepeating("BlinkAnimation", 3.0f, 17.0f);
		
	}

	void Update (){

		if (Input.GetKeyDown(KeyCode.P)){
			CloseEyesAnimation();
		}
	
		if (Input.GetKeyDown(KeyCode.O)){
			previewTime = true;
		}

		if (previewTime == true) {

			float alpha = (Mathf.PingPong(Time.time * .5f, .5f)) + .15f;

			Color colorGoal = new Color (body.sharedMaterial.color.r,
				body.sharedMaterial.color.g,
				body.sharedMaterial.color.b,
				                 		alpha);

			body.sharedMaterial.color = colorGoal;

			eyeColor = new Color (blinker.color.r, blinker.color.g, blinker.color.b, alpha);
			blinker.color = eyeColor;

			heartColor = new Color (heart.material.color.r, heart.material.color.g, heart.material.color.b, alpha);
			heart.material.color = heartColor;

			heartRingColor = new Color (heartRing.material.color.r, heartRing.material.color.g, heartRing.material.color.b, alpha);
			heartRing.material.color = heartRingColor;


//			body.material.color = colorGoal;
//			leftArm.material.color = colorGoal;
//			rightArm.material.color = colorGoal;
//			head.material.color = colorGoal;
		}
	
	
	}

	public void DetectorsOn(){
	
		detectors.SetActive (true);
	
	}

	public void BlinkAnimation(){

		blinkAnimation.Play ("Blink");

	}

	public void CloseEyesAnimation(){
	
		blinkAnimation.Play ("Close Eyes");
	
	}

	public void HugAnimation(){

		walk.GetComponent<Animator>().Play ("Hug");
		CancelInvoke ();

	}

	public void WalkAnimation(){
	
		walk.GetComponent<Animator> ().Play ("Right Shoulder");
	
	}

	public void WobbleAnimation(){

		walk.GetComponent<Animator> ().Play ("Wobble");

	}

	public void StopAnimation(){

		walk.GetComponent<Animator> ().SetTrigger ("Stop");

	}
		

	void FindManager () {

		gameManager = GameObject.FindGameObjectWithTag ("Game Manager Local");
		turnManager = gameManager.GetComponent <TurnManagerScript> ();

	}

	void OnTriggerEnter(Collider other) {

		if (turnManager == null) {
			return;
		}

		if (turnManager.preview == true) {
			return;
		}

//		if (other.tag == "Slime") {
//
//			Debug.Log ("SLIMED");
//
//			turnManager.LoseGame ();
//			turnManager.gameLost = true;
//			turnManager.forwardBlueGhost = false;
//			turnManager.forwardPinkGhost = false;
//
//		}


//		if (other.tag == "Player") {
//		
//			turnManager.winning = true;
//		
//		}
		
		if (other.tag == "Wall" && turnManager.winning == false) {

			if (inWindMill == true)
				return;

			HitWallSound ();


			if (gameObject.name == "Pink Ghost(Clone)") {

				DOTween.Kill ("PinkForward", false);
				StopAnimation ();
				//turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;
				turnManager.CorrectPink ();

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				DOTween.Kill ("BlueForward", false);
				StopAnimation ();
				//turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;
				turnManager.CorrectBlue ();

			}

			if (turnManager.preview == true) {
			
				GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
				int triggerCount = triggers.Length;
				triggers [triggerCount - 1].GetComponent<TriggerScript> ().hitWall = true;
				turnManager.DeletePoint ();
			
			} else {

				turnManager.MoveTurnBar (5.0f);
				turnManager.TakeHit (5.0f);

			}

		}

		if (other.tag == "New Wall" && turnManager.winning == false) {

			if (inWindMill == true)
				return;

			HitWallSound ();


			if (gameObject.name == "Pink Ghost(Clone)") {

				DOTween.Kill ("PinkForward", false);
				StopAnimation ();
				//turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;
				turnManager.CorrectPink ();

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				DOTween.Kill ("BlueForward", false);
				StopAnimation ();
				//turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;
				turnManager.CorrectBlue ();

			}

			if (turnManager.preview == true) {

				GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
				int triggerCount = triggers.Length;
				triggers [triggerCount - 1].GetComponent<TriggerScript> ().hitWall = true;
				turnManager.DeletePoint ();

			} else {

				turnManager.MoveTurnBar (5.0f);
				turnManager.TakeHit (5.0f);

			}

		}

		if (other.tag == "Hidden Wall" && turnManager.preview == false && turnManager.winning == false) {

			if (inWindMill == true) {
				return;
			}

			//Debug.Log ("Hit hidden wall" + inWindMill + gameObject.name);

			if (gameObject.name == "Pink Ghost(Clone)") {

				DOTween.Kill ("PinkForward", false);
				StopAnimation ();
				//turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;
				turnManager.CorrectPink ();

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				DOTween.Kill ("BlueForward", false);
				StopAnimation ();
				//turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;
				turnManager.CorrectBlue ();

			}

			HitWallSound ();
			turnManager.MoveTurnBar (5.0f);
			turnManager.TakeHit (5.0f);
			other.GetComponent<HiddenWallScript> ().SwitchMat ();

		}

		if (other.tag == "Boulder" && turnManager.preview == false && turnManager.winning == false) {

			if (inWindMill == true) {
				return;
			}

			if (gameObject.name == "Pink Ghost(Clone)") {

				DOTween.Kill ("PinkForward", false);
				StopAnimation ();
				//turnManager.forwardPinkGhost = false;
				turnManager.backPinkGhost = true;
				turnManager.CorrectPink ();

			}

			if (gameObject.name == "Blue Ghost(Clone)") {

				DOTween.Kill ("BlueForward", false);
				StopAnimation ();
				//turnManager.forwardBlueGhost = false;
				turnManager.backBlueGhost = true;
				turnManager.CorrectBlue ();

			}

			HitWallSound ();
			turnManager.MoveTurnBar (5.0f);
			turnManager.TakeHit (5.0f);
			other.GetComponent<HiddenWallScript> ().SwitchMat ();

		}

		if (other.tag == "Border" && turnManager.preview == false && turnManager.winning == false) {

			turnManager.pointsTotal = 90.0f;
			turnManager.MoveTurnBar (90.0f);
			turnManager.CheckStarHit ();

			if (gameObject.name == "Pink Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardPinkGhost = false;
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;

			}

			if (gameObject.name == "Blue Ghost(Clone)") {
				turnManager.gameLost = true;
				turnManager.LoseGame ();
				turnManager.forwardBlueGhost = false;
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
			}
		}

	}

//	void OnTriggerExit (Collider other)
//	{
//
//		if (other.tag == "Player") {
//
//			turnManager.winning = false;
//
//		}
//
//	}

	void HitWallSound(){
	
		//Debug.Log ("hitWall");
		float vol = Random.Range (.6f, .75f);
		source.PlayOneShot(wallHit,vol);
	
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

		foreach (MeshRenderer bodyPart in bodyParts) {

			bodyPart.material = previewMaterial;

		}
			

		previewTime = true;
	
	}

	public void PreviewModeOff (){

		previewTime = false;


		foreach (MeshRenderer bodyPart in bodyParts) {

			bodyPart.material = solidMaterial;

		}

		eyeColor = new Color (blinker.color.r, blinker.color.g, blinker.color.b, 1.0f);
		blinker.color = eyeColor;

		heartColor = new Color (heart.material.color.r, heart.material.color.g, heart.material.color.b, 1.0f);
		heart.material.color = heartColor;

		heartRingColor = new Color (heartRing.material.color.r, heartRing.material.color.g, heartRing.material.color.b, 1.0f);
		heartRing.material.color = heartRingColor;


		Color colorGoal = new Color (body.sharedMaterial.color.r,
			body.sharedMaterial.color.g,
			body.sharedMaterial.color.b,
			1.0f);

		body.sharedMaterial.color = colorGoal;
//		head.material.color = colorGoal;
//		leftArm.material.color = colorGoal;
//		rightArm.material.color = colorGoal;


	}


}
