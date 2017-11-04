using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour {

	public GameObject obstacle;
	public int obstaclePoolSize = 3;
	public float spawnRate = 7f;
	public float obstacleXBoundaryMin = -8f;
	public float obstacleXBoundaryMax = 8f;
	public float obstacleYBoundaryMin = 10.24f;
	public float obstacleYBoundaryMax = 13.27f;

	private GameObject[] obstaclePool;
	private Vector2 objectPoolPosition;
	private float timeSinceLastSpawn;
	private int currentObstacle = 0;


	// Use this for initialization
	void Start () {
		obstaclePool = new GameObject[obstaclePoolSize];

		//Create object pool offscreen
		objectPoolPosition = new Vector2 (25, 12);

		for (int i = 0; i < obstaclePoolSize; i++) {
			obstaclePool [i] = Instantiate (obstacle, objectPoolPosition, Quaternion.identity) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		SpawnObstalce ();

	}

	void SpawnObstalce(){
		timeSinceLastSpawn += Time.deltaTime;

		if(!GameController.instance.isGameOver && GameController.instance.spawnObstacles && timeSinceLastSpawn >= spawnRate){

			timeSinceLastSpawn = 0;
			float spawnYPosition = Random.Range(obstacleYBoundaryMin,obstacleYBoundaryMax);
			float spawnXPosition = Random.Range(obstacleXBoundaryMin,obstacleXBoundaryMax);
			obstaclePool [currentObstacle].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
			currentObstacle++;
			if(currentObstacle >= obstaclePoolSize){
				currentObstacle = 0;
			}
		}
	}
}
