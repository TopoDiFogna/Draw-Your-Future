using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    int lay;
    public int ScLayer;

    Transform tr;
    Rigidbody2D rb;
    public GameObject paint;

    [Range(0, 100)]
    public float Speed;
    [Range(0, 100)]
    public float ClimbingSpeed;

    [Range(0, 200)]
    public float Jump_force;

    bool jumping = false;
    bool climbing = false;

    // Use this for initialization
    void Start()
    {
        lay = gameObject.layer;
        tr = transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (climbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0){
                tr.position += new Vector3(0, ClimbingSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime, 0);
            }
        }
        else
        {
            //TODO 
        }
 
        if (Input.GetKeyDown(KeyCode.W) && !jumping)
        {
            rb.AddForce(Vector2.up * Jump_force, ForceMode2D.Impulse);
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        if (!climbing)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, rb.velocity.y);
        }
    }






    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
        }


    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = ScLayer;
        }

        if (coll.gameObject.tag == "Climbable")
        {
            jumping = false;
            climbing = true;
            rb.isKinematic = true;
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

        if (coll.gameObject.tag == "Climbable")
        {
            climbing = false;
            rb.isKinematic = true;
        }
    }
}
