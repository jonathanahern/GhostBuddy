using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float rotateSpeed;

	public GameObject tile;
	private Vector3 tilePos;
	public bool forwardRotation;

	// Use this for initialization
	void Start () {
	
		tilePos = new Vector3 (tile.transform.position.x,
			tile.transform.position.y,
			tile.transform.position.z);
			

	}
	
	// Update is called once per frame
	void Update () {

		if (forwardRotation == true) {

			transform.RotateAround (gameObject.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
	
		} else {
		
			transform.RotateAround (tilePos, Vector3.up, rotateSpeed * Time.deltaTime);

		}
	}
}
