using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour {

    bool active = true;
    public float timeToDeactivate = 0.7f;

    // Use this for initialization
    private void OnEnable()
    {
        active = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain"){
            active = false;
            StartCoroutine("Deactivate");
        }
        if(collision.gameObject.tag == "Hyppo" && active)
        {
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timeToDeactivate);
        gameObject.SetActive(false);
    }
}
