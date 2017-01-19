using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySnakeController : MonoBehaviour
{
    Animator anim;
    public float JumpImpulse = 10;
    Rigidbody2D rb;
    bool jumping;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        jumping = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Jump");
        jumping = true;
        rb.AddForce(Vector2.up * JumpImpulse, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Terrain" && jumping)
        {
            anim.SetTrigger("Land");
            jumping = false;
            //anim.SetTrigger("Land");
            StartCoroutine(Jump());
        }
    }
}
