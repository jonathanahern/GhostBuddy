using UnityEngine;
using System.Collections;

public class ColliderDetectionScript : MonoBehaviour {
	
	private WindmillScript windmill;

	GameObject ghostOne;
	GameObject ghostTwo;

	// Use this for initialization
	void Start () {

		windmill = gameObject.transform.parent.GetComponent<WindmillScript> ();
	
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {

			if (windmill.ghostOne == null) {
				windmill.ghostOne = other.gameObject;
				ghostOne = other.gameObject;
			} else {
				windmill.ghostTwo = other.gameObject;
				ghostTwo = other.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			if (other.gameObject == ghostOne) {
				Debug.Log ("Out2");
				windmill.ghostOne = null;
				ghostOne = null;
			} else {
				Debug.Log ("Out3");
				windmill.ghostTwo = null;
				ghostTwo = null;
			}
		}
	}
}
