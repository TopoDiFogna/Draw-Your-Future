using UnityEngine;
using System.Collections;

public class MakeChild : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") {

            collision.transform.rotation = gameObject.GetComponentInParent<Transform>().rotation;
            collision.transform.parent = transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.rotation = Quaternion.identity;
            collision.transform.parent = null;
        }
    }
}
