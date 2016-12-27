using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstrichController : MonoBehaviour
{

    public int m_scratch_layer = 8;
    public int m_player_layer = 9;
    GameController gc;
    public GameObject m_paint;

    [Range(0, 100)]
    public float m_speed = 20f;

    [Range(0, 200)]
    public float m_Jump_force = 10f;
    float normal_jump_force;
    public float diving_jump_force = 5f;

    int lay;
    Rigidbody2D rb;

    public bool jumping = false;
    private bool dead = false;

    private float m_horizontal = 0f;
    private float m_axis_jump = 0f;

    private bool facing_right = true;

    bool active = false;
    bool with_player = false;
    BoxCollider2D boxcoll;
    CircleCollider2D circlecoll;
    PolygonCollider2D polycoll;


    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        circlecoll = GetComponent<CircleCollider2D>();
        polycoll = GetComponent<PolygonCollider2D>();
        normal_jump_force = m_Jump_force;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.Pause && with_player)
        {
            m_horizontal = Input.GetAxisRaw("Horizontal");
            m_axis_jump = Input.GetAxisRaw("Jump");

            if (facing_right && m_horizontal < 0)
            {
                // TODO GIRARE STRUZZO E PLAYER sr.flipX = true;
                facing_right = false;
            }
            else if (!facing_right && m_horizontal > 0)
            {
                // TODO GIRARE STRUZZO E PLAYER sr.flipX = false;
                facing_right = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            //donothing
        }
        else
        {
            if (with_player)
            {
                rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);
                if (m_axis_jump > 0 && !jumping)
                {
                    rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                    jumping = true;
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = false;
        }
        if(coll.gameObject.tag == "Player" && active)
        {
            coll.gameObject.SetActive(false);
            coll.gameObject.transform.parent = gameObject.transform;
            //TODO start coroutine che fa animazione del player che sale sullo struzzo e poi fa enable dei comandi
            with_player = true;
        }
    }


    private void OnCollisionExit2D(Collision2D coll)
    {
        if (jumping == false && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !active)
        {
            boxcoll.enabled = false;
            circlecoll.enabled = true;
            polycoll.enabled = true;
            rb.isKinematic = false;
            gameObject.layer = m_player_layer;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            active = true;
        }
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_player_layer;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_player_layer;
        }
    }
}
