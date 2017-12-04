using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour {

	private Animator animator;
	private AudioSource audioSource;
	private bool isHit;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		isHit = false;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(!GameController.instance.isGameOver && !isHit && other.gameObject.CompareTag("Player")){
			isHit = true;
			audioSource.Play ();
			animator.SetTrigger ("BaloonExplosion");
			GameController.instance.PlayerScored ();
		}
	}

	void DestroyGameObject(){
		Destroy (gameObject);
	}
}
