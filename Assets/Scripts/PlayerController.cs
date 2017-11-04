using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float upForce = 100f;							
	public int score;
	public float torque = 2.0f;

	public float boostRate = 2f;

	private Rigidbody2D rgbd;								
	private Animator animator;	
	private bool isDead;
	private bool startedFlying;
	private bool hasBoost;
	private float timeTillnextBoost;

	// Use this for initialization
	void OnEnable () {
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponentInChildren<Animator> ();

		startedFlying = false;
		isDead = false;
		hasBoost = true;
		score = 0;
	}

	void Update(){
		if(!hasBoost){
			timeTillnextBoost += Time.deltaTime;
			if (timeTillnextBoost >= boostRate) {
				hasBoost = true;
			}
		}
				
		if(!isDead){
			//Get Input & move player
			GetInputAndMove();
		}
	}
		
	void GetInputAndMove(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			rgbd.velocity = Vector2.zero;
			animator.SetTrigger ("Rise");
			rgbd.AddRelativeForce(new Vector2 (0,upForce));
			startedFlying = true;
		}
		if (Input.GetKey (KeyCode.RightArrow) && startedFlying ) {
			animator.SetTrigger ("Rise");
			rgbd.AddTorque(-torque);

		}
		if (Input.GetKey (KeyCode.LeftArrow) && startedFlying) {
			animator.SetTrigger ("Rise");
			rgbd.AddTorque(torque);
		}
		//TODO: Add special boost animation
		//Boost
		if (Input.GetKeyDown (KeyCode.Space) && startedFlying && hasBoost) {
			rgbd.velocity = Vector2.zero;
			animator.SetTrigger ("Rise");
			rgbd.AddRelativeForce(new Vector2 (0,upForce*2));
			timeTillnextBoost = 0;
			hasBoost = false;
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
