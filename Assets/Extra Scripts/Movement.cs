﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float moveHorizontal;
	private float moveVertical;
	private Vector3 movement;
	private Transform playerTransform;
	private Vector2 look;
	private float angle;
	Quaternion rotation;
	private float speed = 5f;


	// Use this for initialization
	void Start () 
	{
		playerTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveHorizontal = Input.GetAxisRaw("Horizontal");
		moveVertical = Input.GetAxisRaw("Vertical");

		movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		look = Camera.main.ScreenToWorldPoint (Input.mousePosition) - playerTransform.position;
		angle = Mathf.Atan2 (look.y, look.x) * Mathf.Rad2Deg;
		rotation = Quaternion.AngleAxis (angle, Vector3.forward);


	}

	void FixedUpdate ()
	{
		playerTransform.position += movement/3;
		playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, rotation, speed * Time.deltaTime);
	}


}
