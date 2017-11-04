using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour {

	private Animator animator;
	private bool isHit;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		isHit = false;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(!GameController.instance.isGameOver && !isHit && other.gameObject.CompareTag("Player")){
			isHit = true;
			animator.SetTrigger ("BaloonExplosion");
			GameController.instance.PlayerScored ();
		}
	}

	void DestroyGameObject(){
		Destroy (gameObject);
	}
}
