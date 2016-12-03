using UnityEngine;
using System.Collections;
using System;

public class MortalObject : MonoBehaviour {

    String objtag;

    [Range(1f, 10f)]
    public float m_time_to_despawn = 5f;

    private void Start()
    {
        objtag = gameObject.tag;
        print(gameObject.tag);
    }

    private void OnEnable()
    {
        gameObject.tag = objtag;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().DieWithFade();
            gameObject.SetActive(false);
        }
        if (coll.tag == "Terrain")
        {
            gameObject.tag = "Untagged";
            StartCoroutine(DisableObject());
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(m_time_to_despawn);
        gameObject.SetActive(false);
    }
}
