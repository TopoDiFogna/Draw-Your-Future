using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public Sprite m_bomb_active;
    private Sprite bomb_normal;
    public AudioClip m_explosion_sound;

    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bomb_normal = sr.sprite;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scratch")
        {
            sr.sprite = m_bomb_active;
            StartCoroutine(explode_bomb());
        }
    }

    IEnumerator explode_bomb()
    {
        yield return new WaitForSeconds(5f);
        // Missing audio effect
        ExplodeBomb();
        sr.sprite = null;
        foreach(CircleCollider2D collider in GetComponents<CircleCollider2D>())
        {
            collider.enabled = false;
        }
        StartCoroutine(RestoreStatus());
    }

    IEnumerator RestoreStatus()
    {
        yield return new WaitForSeconds(10);
        sr.sprite = bomb_normal;
        foreach (CircleCollider2D collider in GetComponents<CircleCollider2D>())
        {
            collider.enabled = true;
        }
    }

    void ExplodeBomb()
    {
        //TODO effetti esplosione BOOOM!
    }
}
