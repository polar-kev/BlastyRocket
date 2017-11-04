using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

	public float backgroundHeight;

	void Update () {
		//If the background gets to a point out of view of the camera, reposition the background
			if(transform.position.y <= -backgroundHeight){
			RepositionBackground ();
		}
	}

	void RepositionBackground() {
		Vector2 newPosition = new Vector2 (0, backgroundHeight * 2f);
		gameObject.transform.position = (Vector2)gameObject.transform.position + newPosition;
	}
}
