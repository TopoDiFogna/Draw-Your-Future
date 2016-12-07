using UnityEngine;
using System.Collections;

public class JumpingPlat : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().jumping = false;
        }
    }
}
