﻿using UnityEngine;
using System.Collections;

public class MortalObject : MonoBehaviour {


    public bool CanDisable = true;

    [Range(1f, 10f)]
    public float m_time_to_despawn = 5f;

    bool deadly = true;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && deadly)
        {
            coll.GetComponent<PlayerController>().DieWithFade();
            if(CanDisable)
                gameObject.SetActive(false);
        }
        if (coll.tag == "Terrain")
        {
            deadly = false;
            if(CanDisable)
                StartCoroutine(DisableObject());
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(m_time_to_despawn);
        gameObject.SetActive(false);
        deadly = true;
    }
}