using UnityEngine;
using System.Collections;

public class ColliderDetectionScript : MonoBehaviour {
	private WindmillScript windmill;

	// Use this for initialization
	void Start () {

		windmill = gameObject.transform.parent.GetComponent<WindmillScript> ();
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Ghost(Clone)") {
			windmill.ghostOne = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "Ghost(Clone)") {
			windmill.ghostOne = null;
		}
	}

}
