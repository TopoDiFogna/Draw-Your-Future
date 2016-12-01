using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {

    int m_counter;
    public int m_hitlimit;

    void Start()
    {
        m_counter = 0;
    }

	void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Projectile")
        {
            m_counter++;
            coll.gameObject.SetActive(false);
            if (m_counter>=m_hitlimit)
                Destroy(gameObject);
        }
    }
}
