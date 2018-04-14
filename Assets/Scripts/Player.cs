using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private float moveHorizontal;
	private float moveVertical;
	private Vector3 movement;
	private Transform playerTransform;
	private Vector2 look;
	private float angle;
	Quaternion rotation;
	private float speed = 5f;
    public GameObject projectile;
    public static Player Instance;

    //Generate lifes
    public Image hearth;
    public Canvas parent;
    private Image[] images;
    private int lifes;


    // Use this for initialization
    void Start () 
	{
        Instance = this;
		playerTransform = GetComponent<Transform> ();
        images = new Image[6];
        for (lifes = 1 ; lifes <= 3 ; lifes++)
        {
            AddHP();
        }
        lifes--;

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
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, rotation, speed * Time.deltaTime);
        playerTransform.position += movement / 7;
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, playerTransform.position, playerTransform.rotation);
        }
    }

    public void AddHP()
    {
        images[lifes] = (Image) Instantiate(hearth, parent.transform);
        images[lifes].transform.localPosition = new Vector3(121 + 22 * (lifes - 1), 151, 0);
    }
    public void LoseHP()
    {
        Destroy(images[lifes]);
        lifes--;
        if (lifes < 0)
            Application.Quit();
    }

}
