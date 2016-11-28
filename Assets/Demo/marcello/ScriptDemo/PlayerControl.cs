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
    [Range(0, 50)]
    public float LadderSpeed;

    [Range(0, 200)]
    public float Jump_force;

    public bool IsNearLadder = false;
    public bool Jumping = false;

	// Use this for initialization
	void Start () {
        lay = gameObject.layer;
        tr = transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) && !Jumping && !IsNearLadder)
        {
            rb.AddForce(Vector2.up*Jump_force,ForceMode2D.Impulse);
            Jumping = true;
        }
	}

    void FixedUpdate()
    {
        if (!IsNearLadder)
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * LadderSpeed);
        }

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
        string t = coll.gameObject.tag;
        if (t == "Scratch")
        {
            gameObject.layer = ScLayer;
        }
        else if(t == "Ladder")
        {
            IsNearLadder = true;
            rb.isKinematic = true;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        string t = coll.gameObject.tag;
        if (t == "Scratch")
        {
            gameObject.layer = ScLayer;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        string t = coll.gameObject.tag;
        if (t == "Scratch")
        {
            gameObject.layer = lay;
        }
        else if (t == "Ladder")
        {
            IsNearLadder = false;
            rb.isKinematic = false;
        }
    }
}
