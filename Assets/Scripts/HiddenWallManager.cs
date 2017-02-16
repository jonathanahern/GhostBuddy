using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWallManager : MonoBehaviour {

	MeshRenderer wall;
	float maxA = .6f;
	float minA = .5f;
	public float speed = 9;

	// Use this for initialization
	void Start () {

		wall = GetComponent<MeshRenderer> ();

	}

	// Update is called once per frame
	void Update () {

		float currentA = wall.sharedMaterial.color.a;
		float randomA = Random.Range (currentA - .1f, currentA + .1f);

		float alpha = Mathf.Lerp (currentA, randomA, Time.deltaTime * speed);

		if (alpha<minA)
			alpha=minA;

		if (alpha>maxA)
			alpha=maxA;	

		Color colorGoal = new Color (wall.sharedMaterial.color.r,
			wall.sharedMaterial.color.g,
			wall.sharedMaterial.color.b,
			alpha);

		wall.sharedMaterial.color = colorGoal;

	}

}
