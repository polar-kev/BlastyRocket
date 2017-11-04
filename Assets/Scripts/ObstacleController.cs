using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			GameController.instance.PlayerScored ();
		}
	}
}
