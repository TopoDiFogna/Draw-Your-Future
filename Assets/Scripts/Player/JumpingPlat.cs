using UnityEngine;
using System.Collections;

public class JumpingPlat : MonoBehaviour {

    public float force = 10;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);

        }
    }
}
