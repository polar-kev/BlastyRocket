using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

	public float scrollMultiplier = 1f;

	private Rigidbody2D rgbd;
	//private float velocity;

	void Start () {
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		rgbd.velocity = new Vector2 (0, GameController.instance.getScrollVelocity () * scrollMultiplier);

		if(GameController.instance.isGameOver){
			rgbd.velocity = Vector2.zero;
		}
	}
}
