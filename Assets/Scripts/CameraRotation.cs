using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour {

	public GameObject cameraOne;
	public GameObject cameraTwo;
	public AnimationCurve curve;
	private GameObject cameraCentEnd;

	public GameObject quadOne;
	public GameObject quadTwo;
	public GameObject quadThree;
	public GameObject quadFour;

	GameObject target;
	bool startLooking;
	bool startRotating;

	public GameObject angleFinder;

	public Color gold;
	public Color silver;
	public Color bronze;

	GameObject starImage;
	public int starColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.T)) {
			EndGameRotion ();
		}

		if (startLooking == true) {
			cameraOne.transform.LookAt(target.transform);
			cameraTwo.transform.LookAt(target.transform);
		}

		if (startRotating == true) {
			cameraOne.transform.RotateAround (cameraCentEnd.transform.position, Vector3.up, 35 * Time.deltaTime);
			cameraTwo.transform.RotateAround (cameraCentEnd.transform.position, Vector3.up, 35 * Time.deltaTime);
		}
		
	}
		

	public void CameraRotate (GameObject robotGoal) {
		
		float goalAngle;
		float oneDist = Vector3.Distance(robotGoal.transform.position, quadOne.transform.position);
		float twoDist = Vector3.Distance(robotGoal.transform.position, quadTwo.transform.position);
		float threeDist = Vector3.Distance(robotGoal.transform.position, quadThree.transform.position);
		float fourDist = Vector3.Distance(robotGoal.transform.position, quadFour.transform.position);

		if (oneDist < twoDist && oneDist < threeDist && oneDist < fourDist) {
			goalAngle = 0;
		} else if (twoDist < oneDist && twoDist < threeDist && twoDist < fourDist) {
			goalAngle = 90;
		} else if (threeDist < twoDist && threeDist < oneDist && threeDist < fourDist) {
			goalAngle = 180;
		} else if (fourDist < twoDist && fourDist < threeDist && fourDist < oneDist) {
			goalAngle = 270;
		} else {
			goalAngle = 90;
		}
		 
		Vector3 goalRotation = new Vector3 (gameObject.transform.eulerAngles.x, goalAngle, gameObject.transform.eulerAngles.z);

		gameObject.transform.DORotate (goalRotation, 2.0f).SetEase(Ease.OutCirc);


//		float number = 3;
//		DOTween.To (() => number, (c) => number = c, 5, 2).SetEase(Ease.InOutElastic).OnComplete(()=>{
//			Debug.Log("Whatever");
//			Debug.Log("alskdsd");
//			LLL();
//		});
	
	}

//	void LLL(){
//	}

	public void EndGameRotion () {

		target = GameObject.FindGameObjectWithTag ("Player");
		angleFinder.transform.LookAt(target.transform);
		cameraOne.transform.DORotate (angleFinder.transform.eulerAngles, 2.0f).SetEase (Ease.InQuad).OnComplete(EndRotation2);
		cameraTwo.transform.DORotate (angleFinder.transform.eulerAngles, 2.0f).SetEase (Ease.InQuad);
	}

	void EndRotation2(){
		startLooking = true;
		cameraCentEnd = GameObject.FindGameObjectWithTag ("Camera Center End");
		Vector3 startCamPos = new Vector3 (cameraCentEnd.transform.position.x + 1.5f, cameraCentEnd.transform.position.y, cameraCentEnd.transform.position.z);

		cameraOne.transform.DOMove (startCamPos, 4.0f).OnComplete(RotateAroundHuggers);
		cameraTwo.transform.DOMove (startCamPos, 4.0f);
		SendStar ();
	
	}

	void RotateAroundHuggers(){
		
		startRotating = true;

	}

	void SendStar(){
	
		starImage = GameObject.FindGameObjectWithTag ("Star");

		if (starColor == 1) {
			starImage.GetComponent<Image> ().DOColor (gold, 2.0f);
		
		} else if (starColor == 2) {
			starImage.GetComponent<Image> ().DOColor (silver, 2.0f);

		} else if (starColor == 3) {
			starImage.GetComponent<Image> ().DOColor (bronze, 2.0f);

		}
	
	}

}
