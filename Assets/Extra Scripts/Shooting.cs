using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {


	public GameObject projectile;
	private Transform playerTransform;

	// Use this for initialization
	void Start () 
	{
		playerTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate ()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			Instantiate (projectile, playerTransform.position, playerTransform.rotation);

		}
	}
}
