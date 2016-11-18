using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    int lay;

    public int ScLayer;


    Transform tr;
    Rigidbody2D rb;
    public GameObject paint;

    [Range(0, 100)]
    public float Speed;

    [Range(0, 200)]
    public float Jump_force;

    public bool Jumping = false;

	// Use this for initialization
	void Start () {
        lay = gameObject.layer;
        tr = transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) && !Jumping)
        {
            rb.AddForce(Vector2.up*Jump_force,ForceMode2D.Impulse);
            Jumping = true;
        }
	}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*Speed,rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (Jumping == true && coll.gameObject.tag == "Terrain")
        {
            Jumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = ScLayer;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = ScLayer;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = lay;
        }
    }
}
