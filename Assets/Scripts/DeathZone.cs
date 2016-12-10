using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().DieWithFade();
        }
    }
}