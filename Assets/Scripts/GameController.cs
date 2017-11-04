using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Text scoreText;
	public GameObject gameOverTextObject;


	public float startWait = 10.0f;
	public float obstacleWait = 1.5f;

	public GameObject obstacle;
	public GameObject baloon;
	public Transform spawnLocation;

	public float obstacleBoundaryMin = -1.0f;
	public float obstacleBoundaryMax = 3.3f;

	public bool isGameOver;
	public bool spawnObstacles;

	public int score;

	private float scrollVelocity = 0;
	private int difficultyThreshold = 2;
	private float obstacleWaitReduction = 0.1f;
	private float randomizer;
	private float minBaloonDistance = 3f;
	private Vector2 oldBaloonPosition;

	//Game controller Singleton pattern
	void Awake(){
		if(instance == null){
			instance = this;
		} else if(instance != this){
			Destroy (gameObject);
		}
	}

	//Initializations
	void Start () {
		score = 0;
		scoreText.text = "Score: 0";
		gameOverTextObject.SetActive(false);

		//Setup first baloon for player to collect
		Vector2 baloonPosition = new Vector2 (0, 3.30f);
		spawnBaloon (baloonPosition);
		spawnObstacles = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isGameOver && Input.anyKeyDown){
			SceneManager.LoadScene("BlastyRockey_Main");
		}
	}

	public void PlayerDied(){
		StopAllCoroutines ();
		isGameOver = true;
		gameOverTextObject.SetActive (true);
	}

	public void PlayerScored(){
		if(isGameOver){
			return;
		}

		score++;
		scoreText.text = "Score: " + score.ToString();

		spawnBaloon ();

		//For Flappy bird game
		//Incrementally increase difficulty by spawning obstacles sooner as the score inscreases
		if(score > difficultyThreshold && score % difficultyThreshold == 0 && obstacleWait >= 0.7f){
			obstacleWait -= obstacleWaitReduction;
		}
	}

	public void spawnBaloon(){
		Vector2 newBaloonPosition;
		//TODO create a variable for baloon position spawn parameters instead of hardcoding
		do {
			newBaloonPosition = new Vector2 (Random.Range (0, 8.2f) * -Mathf.Sign (oldBaloonPosition.x), Random.Range (-1f, 2f));
		} while(Vector2.Distance(oldBaloonPosition,newBaloonPosition) <= minBaloonDistance);

		Instantiate(baloon, new Vector3(newBaloonPosition.x, newBaloonPosition.y, 0), Quaternion.identity);
		oldBaloonPosition = newBaloonPosition;

	}

	public void spawnBaloon(Vector2 position){
		Instantiate(baloon, new Vector3(position.x,position.y, 0), Quaternion.identity);
		oldBaloonPosition = position;
	}

	public float getScrollVelocity(){
		return scrollVelocity;
	}

	public void setScrollVelocity(float vel){
		scrollVelocity = vel;
	}
		
}
