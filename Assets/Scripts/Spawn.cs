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
			axisY = Random.Range (-4f, 3f);
			axisX = Random.Range (-7f, 8f);
			spawnPoz = new Vector3 (axisX, axisY, -1f);
			Instantiate (enemy, spawnPoz, Quaternion.identity);
		}
	}
}
