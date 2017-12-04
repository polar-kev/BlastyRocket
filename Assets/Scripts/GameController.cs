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
	public float restartDelay = 0.8f;

	public GameObject baloon;

	public Vector2 baloonBoundary;
	public Vector2 obstacleBoundary;

	public bool isGameOver;
	public bool spawnObstacles;

	public int score;

	private AudioSource audioSource;
	private float scrollVelocity = 0;
	//private int difficultyThreshold = 2;
	private float randomizer;
	private float minBaloonDistance = 3f;
	private Vector2 oldBaloonPosition;
	bool gameOverSoundPlayed;

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
		audioSource = gameObject.GetComponent<AudioSource> ();

		score = 0;
		scoreText.text = "Score: 0";
		gameOverTextObject.SetActive(false);

		//Setup first baloon for player to collect
		Vector2 baloonPosition = new Vector2 (0, 3.30f);
		spawnBaloon (baloonPosition);
		spawnObstacles = false;
		gameOverSoundPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isGameOver && Input.anyKeyDown && gameOverSoundPlayed){
			SceneManager.LoadScene("BlastyRockey_Main");
		}
	}

	public void PlayerDied(){
		StopAllCoroutines ();
		isGameOver = true;
		audioSource.Play ();
		StartCoroutine ("RestartCountdown");
	}

	public void PlayerScored(){
		if(isGameOver){
			return;
		}

		score++;
		scoreText.text = "Score: " + score.ToString();

		/*Use this to increase difficulty
		 * if(score != 0 && score%2 == 0){
			scrollVelocity -= 1f;
		}
		*/
		spawnBaloon ();
	}

	public void spawnBaloon(){
		Vector2 newBaloonPosition;
		//Make sure new baloon spawns away from the old baloon
		do {
			newBaloonPosition = new Vector2 (Random.Range (0, baloonBoundary.x) * -Mathf.Sign (oldBaloonPosition.x), Random.Range (-1f, baloonBoundary.y));
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

	IEnumerator RestartCountdown(){
		yield return new WaitForSeconds (restartDelay);
		gameOverTextObject.SetActive (true);
		gameOverSoundPlayed = true;
	}
		
}
