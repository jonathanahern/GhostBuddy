using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManagerScript : NetworkBehaviour {

	GameObject ghostOne;
	GameObject ghostTwo;

	private Vector3 endPos;
	private Vector3 ghostOnePos;

	private bool forwardGhostOne = false;

	private GameObject scrollList;
	public GameObject forwardIcon;
	public GameObject leftIcon;
	public GameObject rightIcon;

	private Vector3 origPos;

	bool rotating = false;
	private int direction = 1;

	public SyncListInt ghostOneList = new SyncListInt();

	public override void OnStartLocalPlayer() {

		GameObject playerOneSpawn = GameObject.FindGameObjectWithTag ("Spawn One");
		ghostOne = Instantiate(Resources.Load("Pink Ghost", typeof(GameObject))) as GameObject;
		ghostOne.transform.position = playerOneSpawn.transform.position;
		ghostOne.transform.rotation = playerOneSpawn.transform.rotation;

		GameObject playerTwoSpawn = GameObject.FindGameObjectWithTag ("Spawn Two");
		ghostTwo = Instantiate(Resources.Load("Blue Ghost", typeof(GameObject))) as GameObject;
		ghostTwo.transform.position = playerTwoSpawn.transform.position;
		ghostTwo.transform.rotation = playerTwoSpawn.transform.rotation;

		scrollList = GameObject.FindGameObjectWithTag ("Playback Scroll");

	}

	void Update (){

		if (!isLocalPlayer)
		{
			return;
		}
			
		if (forwardGhostOne == true) {
			
			ghostOnePos = new Vector3 (ghostOne.transform.position.x,
				ghostOne.transform.position.y,
				ghostOne.transform.position.z);

			ghostOne.transform.position = Vector3.Lerp (ghostOnePos, endPos, Time.deltaTime * 10);

			if (ghostOne.transform.position == endPos) {
			
				forwardGhostOne = false;
			
			}
		}
	}

	public void ForwardGhost () {

		endPos = ghostOne.transform.position - ghostOne.transform.forward;
		forwardGhostOne = true;
	
	}

	public void PlaceForwardGhost(){
	
		GameObject newTrigger = Instantiate(forwardIcon, new Vector3(0, 0, 0), Quaternion.identity);
		newTrigger.transform.SetParent (scrollList.transform, false);

	}

	public void RotateLeftGhostOne () {
		direction = -1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(ghostOne, rotation2, 1f));
	}

	public void PlaceRightIcon() {
		GameObject newTrigger = Instantiate(rightIcon, new Vector3(0, 0, 0), Quaternion.identity);
		newTrigger.transform.SetParent (scrollList.transform, false);
	
	}

	public void PlaceLeftIcon() {
		GameObject newTrigger = Instantiate(leftIcon, new Vector3(0, 0, 0), Quaternion.identity);
		newTrigger.transform.SetParent (scrollList.transform, false);
	
	}

	public void RotateRightGhostOne () {

		direction = 1;

		Quaternion rotation2 = Quaternion.Euler(new Vector3(0, 90 * direction, 0));
		StartCoroutine(RotateObject(ghostOne, rotation2, 1f));
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

	public void BackToOriginalPosition () {
	
		ghostOne.transform.position = origPos;
	
	}

	public void PlaybackButton () {
		origPos = ghostOne.transform.position;
		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;
		float seconds = 0;
		for(int i = 0; i < triggerCount; i++)
		{
			triggers[i].GetComponent<TriggerScript>().InvokeTriggerAction(seconds);
			seconds = seconds + 1.5f;
			if (i == (triggerCount-1)) {
				Invoke ("BackToOriginalPosition", seconds + 1);
			}
		}
	}

	public void TurnDone () {
	
		Debug.Log ("Turn Done");

		GameObject[] triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		int triggerCount = triggers.Length;

		for(int i = 0; i < triggerCount; i++)
		{
			triggers[i].GetComponent<TriggerScript>().TriggerSend();

		}
	}

}
