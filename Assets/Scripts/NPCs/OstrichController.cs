using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstrichController : MonoBehaviour {

    public int m_scratch_layer = 8;
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
    private float m_vertical = 0f;
    private float m_axis_jump = 0f;

    private bool facing_right = true;

    // Use this for initialization
    void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        normal_jump_force = m_Jump_force;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gc.Pause)
        {
            m_horizontal = Input.GetAxisRaw("Horizontal");
            m_vertical = Input.GetAxisRaw("Vertical");
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
            rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);
            if (m_axis_jump > 0 && !jumping)
            {
                rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                jumping = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
        }
    }


    private void OnCollisionExit2D(Collision2D coll)
    {
        if (jumping == false && coll.gameObject.tag == "Terrain")
        {
            jumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
        }

    }
}
