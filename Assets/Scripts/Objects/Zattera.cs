﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zattera : MonoBehaviour {

    private PlayerController playerController;
    public bool with_player = false;
    private float m_horizontal = 0f;
    private Rigidbody2D rb;
    public float m_speed = 5f;
    private GameObject player;
    private FixedJoint2D joint;
    private Vector3 starting_position;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
        starting_position = transform.position;
    }
	
	void Update () {
        m_horizontal = Input.GetAxisRaw("Horizontal");
        

        if (with_player && Input.GetKeyDown(KeyCode.W)){
            joint.connectedBody = null;
            playerController.enabled = true;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerController.m_Jump_force, ForceMode2D.Impulse);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            SetUpPlayerVariables(collision);
        }
    }

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
}
