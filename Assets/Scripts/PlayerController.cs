using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int m_scratch_layer = 9;

    public GameObject m_paint;

    [Range(0, 100)]
    public float m_speed = 20f;

    [Range(0, 100)]
    public float m_climbing_speed = 15f;

    [Range(0, 200)]
    public float m_Jump_force = 10f;

    // Player Properties
    int lay;
    Transform tr;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    // Status variables
    bool jumping = false;
    bool climbing = false;
    bool facing_right = true;

    //Controls variables
    float m_horizontal = 0f;
    float m_vertical = 0f;

    // Use this for initialization
    void Start()
    {
        lay = gameObject.layer;
        tr = transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", Mathf.Abs(m_horizontal));

        if(facing_right && m_horizontal < 0)
        {
            sr.flipX = true;
            facing_right = false;
        }
        else if(!facing_right && m_horizontal > 0)
        {
            sr.flipX = false;
            facing_right = true;
        }

        if (climbing)
        {
            if (m_vertical != 0)
            {
                tr.position += new Vector3(0, m_climbing_speed * m_vertical * Time.deltaTime, 0);
            }
        }
        else
        {
            // TODO 
        }
 
        if (Input.GetKeyDown(KeyCode.W) && !jumping)
        {
            rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        if (!climbing)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * m_speed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
        }
    }


    void onCollisionExit2D(Collision2D coll)
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
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
            gameObject.layer = m_scratch_layer;
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
