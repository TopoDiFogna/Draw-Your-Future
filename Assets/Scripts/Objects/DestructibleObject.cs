using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour {

    int m_counter;
    public int m_hitlimit;
    public Sprite[] breaking_sprites;
    SpriteRenderer sr;

    void Start()
    {
        m_counter = 0;
        sr = GetComponent<SpriteRenderer>();
    }

	void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Projectile")
        {
            m_counter++;
            coll.gameObject.SetActive(false);
            if (m_counter >= m_hitlimit)
            {
                //TODO animazione di rottura
                gameObject.SetActive(false);
            }
            else
            {
                sr.sprite = breaking_sprites[m_counter];
            }

        }
    }
}
