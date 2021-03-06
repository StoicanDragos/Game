﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectileMove : MonoBehaviour {

	///private Vector3 MousePoz;
	///private Vector3 movement;
	private Transform projectileTransform;
    ///public Transform playerTransform;
    ///private Vector3 target;
    ///private float speed= 20f;
    ///private GameObject objectt;

    // Use this for initialization
    void Start () 
	{
		projectileTransform = GetComponent <Transform> ();
		///projectileTransform.rotation = playerTransform.rotation;
		///objectt = GetComponent <GameObject> ();
		///target = Camera.main.ScreenToWorldPoint (Input.mousePosition) - playerTransform.position;
		///Physics.IgnoreCollision (playerTransform.GetComponent<Collider> (), GetComponent<Collider> ());
	}
	
	// Update is called once per frame
	void Update () 
	{
        projectileTransform.position += projectileTransform.right;
    }

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
        if (col.gameObject.tag == "Enemy")
        {
            GoldManagement.AddGold(10);
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
