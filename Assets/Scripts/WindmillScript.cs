using UnityEngine;
using System.Collections;

public class WindmillScript : MonoBehaviour {

	bool rotating = false;
	public int direction = 1;

	public float rotateSpeed;
	private Vector3 rotationDirection;

	private bool rotateGhost = false;
	private float startAngle;
	private float endAngle;

	public GameObject GhostBoxA1;
	public GameObject GhostBoxA2;
	public GameObject GhostBoxB1;
	public GameObject GhostBoxB2;

	public GameObject ghostOne;
	public GameObject ghostTwo;

	public bool ghostOneInside = false;

	void Start () {

		rotationDirection = new Vector3 (0, 1 * direction, 0);

		if (direction < 0) {
		
			GhostBoxA1.SetActive (false);
			GhostBoxA2.SetActive (false);
		
		} else {
		
			GhostBoxB1.SetActive (false);
			GhostBoxB2.SetActive (false);
		
		}

	}


	void Update (){

		if (Input.GetKeyDown (KeyCode.Space) && rotateGhost == false) {

			RotateWindmill ();
			RotateGhost ();

		}

		if (rotateGhost == true) {

			ghostOne.transform.RotateAround (gameObject.transform.position, rotationDirection, rotateSpeed * Time.deltaTime);
		
			if (ghostOne.transform.eulerAngles.y < endAngle && direction == -1) {

				rotateGhost = false;
				float newXPos = (Mathf.Round (ghostOne.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostOne.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostOne.transform.position.y, newZPos);
				ghostOne.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostOne.transform.eulerAngles.y / 10)) * 10;
				Vector3 newAngle = new Vector3 (ghostOne.transform.eulerAngles.x, roundedAngle, ghostOne.transform.eulerAngles.z);
				ghostOne.transform.eulerAngles = newAngle;
			}

			if (ghostOne.transform.eulerAngles.y > endAngle && direction == 1) {

				rotateGhost = false;
				float newXPos = (Mathf.Round (ghostOne.transform.position.x * 2)) / 2;
				float newZPos = (Mathf.Round (ghostOne.transform.position.z * 2)) / 2;
				Vector3 newPos = new Vector3 (newXPos, ghostOne.transform.position.y, newZPos);
				ghostOne.transform.position = newPos;

				float roundedAngle = (Mathf.Round (ghostOne.transform.eulerAngles.y / 10)) * 10;
				Vector3 newAngle = new Vector3 (ghostOne.transform.eulerAngles.x, roundedAngle, ghostOne.transform.eulerAngles.z);
				ghostOne.transform.eulerAngles = newAngle;
			}
		}

	}
		
	public void RotateWindmill()
	{
		if (ghostOne != null) {
			RotateGhost ();
		}
		
		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(gameObject, rotation2, 2f));
	}

	void RotateGhost () {

		startAngle = ghostOne.transform.eulerAngles.y;
		if (startAngle == 0 && direction == -1) {
			startAngle = 360.0f;
		}
	
		endAngle = startAngle + 90.0f * direction;

		if (endAngle == 0 && direction == -1) {
		
			endAngle = 3f;
		
		}

		if (endAngle == 360 && direction == 1) {

			endAngle = 357f;

		}

		Debug.Log ("start:" + startAngle);
		Debug.Log ("end:" + endAngle);
		rotateGhost = true;
	

	}
		


	IEnumerator RotateObject (GameObject gameObjectToMove, Quaternion newRot, float duration)
	{
		if (rotating) {
			yield break;
		}
		rotating = true;


		Quaternion currentRot = gameObjectToMove.transform.rotation;
		newRot = Quaternion.Euler (gameObjectToMove.transform.eulerAngles + newRot.eulerAngles);

		float counter = 0;
		while (counter < duration) {
			counter += Time.deltaTime;
			gameObjectToMove.transform.rotation = Quaternion.Lerp (currentRot, newRot, counter / duration);
			yield return null;
		}
		rotating = false;
	}
}
