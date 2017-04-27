using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
		//target = GameObject.FindGameObjectWithTag ("Player");


	}


	
	// Update is called once per frame
	void Update () {

		transform.rotation = target.transform.rotation;

	}
}
