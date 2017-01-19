using UnityEngine;
using System.Collections;

public class StopPlayerControlsX : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().sliding = true;
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, coll.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().sliding = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().sliding = false;
        }
    }
}
