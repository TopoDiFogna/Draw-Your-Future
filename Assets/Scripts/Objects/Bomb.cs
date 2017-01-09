using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public GameObject Spark;
    public GameObject Boom;
    public AudioClip m_explosion_sound;
    public GameObject m_explosion;
    private bool active = false;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scratch" && !active)
        {
            active = true;
            StartCoroutine(explode_bomb());
            Spark.SetActive(true);
        }
    }

    IEnumerator explode_bomb()
    {
        yield return new WaitForSeconds(5f);
        Spark.SetActive(false);
        ExplodeBomb();
        sr.enabled = false;
        foreach(CircleCollider2D collider in GetComponents<CircleCollider2D>())
        {
            collider.enabled = false;
        }
        StartCoroutine(RestoreStatus());
    }

    IEnumerator RestoreStatus()
    {
        yield return new WaitForSeconds(1);
        Boom.SetActive(false);
        m_explosion.SetActive(false);
        yield return new WaitForSeconds(9);
        foreach (CircleCollider2D collider in GetComponents<CircleCollider2D>())
        {
            collider.enabled = true;
            sr.enabled = true;
        }
        active = false;
    }

    void ExplodeBomb()
    {
        Boom.SetActive(true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(m_explosion_sound);
        m_explosion.SetActive(true);
    }
}
