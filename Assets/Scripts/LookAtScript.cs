using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
		target = GameObject.FindGameObjectWithTag ("MainCamera");

	}
	
	// Update is called once per frame
	void Update () {
	
		transform.LookAt(target.transform);

	}
}
