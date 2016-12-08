using UnityEngine;
using System.Collections;

public class MakeChild : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.rotation = gameObject.GetComponentInParent<Transform>().rotation;
        collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.rotation = Quaternion.identity;
        collision.transform.parent = null;
    }
}
