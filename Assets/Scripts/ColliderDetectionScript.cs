using UnityEngine;
using System.Collections;

public class ColliderDetectionScript : MonoBehaviour {

	private GameObject windmill;
	private WindmillScript windmillScript;

	GameObject ghostOne;
	GameObject ghostTwo;

	// Use this for initialization
	void Start () {

		windmill = gameObject.transform.parent.gameObject;
		windmillScript = windmill.GetComponent<WindmillScript> ();
	
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {

			if (windmillScript.ghostOne == null) {
				windmillScript.ghostOne = other.gameObject;
				ghostOne = other.gameObject;
			} else {
				windmillScript.ghostTwo = other.gameObject;
				ghostTwo = other.gameObject;

			}
		}

	}

	void OnTriggerExit(Collider other) {
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			if (other.gameObject == ghostOne) {
				windmillScript.ghostOne = null;
				ghostOne.transform.parent = null;
				ghostOne = null;
			} else {
				windmillScript.ghostTwo = null;
				ghostTwo.transform.parent = null;
				ghostTwo = null;
			}
		}
	}
}
