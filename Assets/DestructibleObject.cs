using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
