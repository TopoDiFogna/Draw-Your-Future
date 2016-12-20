using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySnakeController : MonoBehaviour
{

    public float JumpImpulse = 10;
    Rigidbody2D rb;
    bool jumping;

    // Use this for initialization
    void Start()
    {
        jumping = false;
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(1);
        rb.AddForce(Vector2.up * JumpImpulse, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Terrain")
        {
            StartCoroutine(Jump());
        }
    }
}
