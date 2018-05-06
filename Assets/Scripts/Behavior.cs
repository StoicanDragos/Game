using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour {

    public GameObject enemy_projectile;
    private GameObject player;
    private float dir = 11f;
    private float timerMove = 1f;
    private float timerShoot = 1f;
    private Vector2 look;
	private float angle;
    Quaternion rotation;
    private float speed = 5f;
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Movement", 0, timerMove);
        timerShoot += Random.Range(-0.3f, 0.3f);
        InvokeRepeating("Shooting", 0, timerShoot);
        player = Player.Instance.gameObject;
	}

    void Update()
    {
        look = player.transform.position - transform.position;
        angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        ///transform.rotation = Quaternion.Inverse(player.transform.rotation);
        if (dir < 0.2f)
        {
            transform.position += -transform.right/7;
        }
        else if (dir < 0.55f)
        {
            transform.position += transform.up/7;
        }
        else if (dir < 0.9f)
        {
            transform.position += -transform.up/7;
        }
        else if (dir < 0.1f)
            transform.position += transform.right/7;

    }

    void Movement()
    {
        dir = Random.value;
    }

   /* void Shooting()
    {
        Instantiate(enemy_projectile, transform.position, transform.rotation);
    }*/
	
}
