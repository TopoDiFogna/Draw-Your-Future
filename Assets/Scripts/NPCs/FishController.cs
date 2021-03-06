﻿using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform tr;
    private Vector3 startingPosition;
    public Transform endingGameObject;
    private Vector3 endingPoint;
    [Range(0.0f, 10.0f)]
    public float m_timeToJump;
    [Range(0.5f, 10.0f)]
    public float m_timeToWait;
    [Range(0.0f, 20.0f)]
    private FishController script;
    private SpriteRenderer rend;

    private void Awake()
    {
        tr = transform;
        startingPosition = new Vector3(tr.position.x, tr.position.y, tr.position.z);
        endingPoint = new Vector3(endingGameObject.position.x, endingGameObject.position.y, endingGameObject.position.z);
        script = GetComponent<FishController>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rend.enabled = true;
        tr.position = startingPosition;
        StartCoroutine(FishJump());
    }

    IEnumerator FishJump()
    {
        float v0x = (endingPoint.x- startingPosition.x) / m_timeToJump;
        float v0y = (endingPoint.y + 0.5f*rb.gravityScale*10 * m_timeToJump*m_timeToJump - startingPosition.y) / m_timeToJump;
        rb.isKinematic = false;
        rb.AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
        yield return new WaitForSeconds(m_timeToJump);
        script.enabled = false;
        rb.velocity = new Vector2();
        rend.enabled = false;
        yield return new WaitForSeconds(m_timeToWait);
        rb.isKinematic = true;
        tr.position = startingPosition;
        script.enabled = true;
    }
}
