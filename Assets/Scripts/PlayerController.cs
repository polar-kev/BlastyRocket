using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float upForce = 100f;							
	public int score;
	public float torque = 2.0f;
	public float thrustRate = 2f;

	public Transform upTransform;

	public GameObject inputTextObject;

	private AudioSource audioSource;
	private Rigidbody2D rgbd;								
	private Animator animator;	
	private bool isDead;
	private bool startedFlying;
	private bool hasThrust;
	private float timeTillnextThrust;

	void OnEnable () {
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponentInChildren<Animator> ();

		audioSource = gameObject.GetComponent<AudioSource> ();

		startedFlying = false;
		isDead = false;
		hasThrust = true;
		score = 0;
	}

	void FixedUpdate(){
		if (!hasThrust) {
			timeTillnextThrust += Time.deltaTime;
			if (timeTillnextThrust >= thrustRate) {
				hasThrust = true;
			}
		}else{
			timeTillnextThrust = 0;
		}
	}

	void Update(){
		if(!isDead){
			//Get Input & move player
			GetInputAndMove();
		}
	}
		
	void GetInputAndMove(){
		if (hasThrust && Input.GetKeyDown (KeyCode.UpArrow)) {
			rgbd.velocity = Vector2.zero;
			animator.SetTrigger ("Rise");
			rgbd.AddRelativeForce(new Vector2 (0,upForce));
			audioSource.Play ();
			startedFlying = true;
			hasThrust = false;
			inputTextObject.SetActive (false);
		}
		if (Input.GetKey (KeyCode.RightArrow) && startedFlying ) {
			animator.SetTrigger ("Rise");
			rgbd.AddTorque(-torque);
			//rgbd.angularVelocity -= torque*50;

		}
		if (Input.GetKey (KeyCode.LeftArrow) && startedFlying) {
			animator.SetTrigger ("Rise");
			rgbd.AddTorque(torque);
			//rgbd.angularVelocity += torque*50;
		}
		//TODO: Add special boost animation
		//Boost
		if (Input.GetKeyDown (KeyCode.Space) && startedFlying && hasThrust) {
			rgbd.velocity = Vector2.zero;
			animator.SetTrigger ("Rise");
			//rgbd.AddRelativeForce(new Vector2 (0,upForce*2));
			timeTillnextThrust = 0;
			hasThrust = false;
		}
	}

	//Check collisions. Baloon collision handled in BaloonController
	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.CompareTag("Safe")){
			return;
		}
		bool isVertical = false;
		/*/Check for a vertical landing
		Vector3 playerRotation = rgbd.transform.rotation.eulerAngles;
		//Debug.Log (playerRotation.ToString ());

		


		//TODO: force a proper landing if the landing is somewhat vertically
		if(playerRotation.z <= 17f || playerRotation.z >= 343f){
			//isVertical = true;
		}*/


		if(startedFlying && !isVertical){
			isDead = true;
			animator.SetTrigger ("Die");
			rgbd.velocity = Vector2.zero;
			GameController.instance.PlayerDied ();
		}


	}

	//Start upwards scroll of the game
	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.CompareTag("StartScroll")){
			GameController.instance.setScrollVelocity(-1);
			GameController.instance.spawnObstacles = true;
			Destroy (other.gameObject);
		}
	}

}
