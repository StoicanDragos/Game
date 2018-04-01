using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	private float timer = 5f;
	public GameObject enemy;
	private Vector3 spawnPoz;
	private float axisY = 0.0f;
	private float axisX = 0.0f;



	// Use this for initialization
	void Start () 
	{
		///enemyTransform = GetComponent <Transform> ();
		InvokeRepeating ("Spawner", timer, timer);

	}


	// Update is called once per frame
	

	void Update () 
	{
		
	}

	void Spawner () 
	{
		if (enemy.name == "Enemy") 
		{
			axisY = Random.Range (-30f, 30f);
			axisX = Random.Range (-62f, 62f);
			spawnPoz = new Vector3 (axisX, axisY, -1.5f);
			Object.Instantiate (enemy, spawnPoz, Quaternion.identity);
		}
	}
}
