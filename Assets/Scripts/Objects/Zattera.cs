using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zattera : MonoBehaviour {

    public int m_scratch_layer = 8;
    public int m_player_layer = 9;
    private PlayerController playerController;
    GameController gc;
    public bool with_player = false;
    private float m_horizontal = 0f;
    private Rigidbody2D rb;
    public float m_speed = 5f;
    private GameObject player;
    private FixedJoint2D joint;
    private Vector3 starting_position;
    public float m_Jump_force = 10;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
        starting_position = transform.position;
    }
	
	void Update () {
        if (!gc.paused)
        {
            m_horizontal = Input.GetAxisRaw("Horizontal");


            if (with_player && Input.GetAxisRaw("Jump")>0)
            {
                print("Mi stacco");
                joint.connectedBody = null;
                playerController.enabled = true;
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,m_Jump_force), ForceMode2D.Impulse);
                playerController.jumping = true;
                player.GetComponent<Animator>().SetBool("Jumping", true);
                with_player = false;
            }

            if (with_player && playerController.Dead)
            {
                joint.connectedBody = null;
                transform.position = starting_position;
                playerController.enabled = true;
                playerController = null;
                with_player = false;
            }
            if (playerController != null && playerController.Dead)
            {
                joint.connectedBody = null;
                transform.position = starting_position;
                playerController.enabled = true;
                playerController = null;
                with_player = false;
            }
            if (Input.GetKeyDown(KeyCode.F11) && with_player)
            {
                playerController.DieWithFade();
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        print("Mi attacco");
    //        SetUpPlayerVariables(collision);
    //    }
    //}

    private void FixedUpdate()
    {
        if (with_player)
        {
            rb.velocity = new Vector2(m_horizontal * m_speed, 0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SetUpPlayerVariables(collision);
        }
    }

    private void SetUpPlayerVariables(Collision2D collision)
    {
        player = collision.gameObject;
        playerController = collision.gameObject.GetComponent<PlayerController>();
        player.GetComponent<Animator>().SetFloat("Horizontal", 0f);
        joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerController.jumping = false;
        player.GetComponent<Animator>().SetBool("Jumping", false);
        playerController.enabled = false;
        with_player = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && with_player)
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_player_layer;
        }
    }
}
