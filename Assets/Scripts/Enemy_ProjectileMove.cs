using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ProjectileMove : MonoBehaviour {


    private Transform projectileTransform;
    public GameObject player;
    private Player playerscript;

    // Use this for initialization
    void Start()
    {
        projectileTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileTransform.position += projectileTransform.right;
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<Player> ();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            playerscript.LoseHP();
            Destroy(gameObject);
        }
    }
}
