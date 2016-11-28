using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public Sprite m_bomb_active;
    public AudioClip m_explosion_sound;

    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
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
        gameObject.SetActive(false);
    }

    void ExplodeBomb()
    {
        //TODO effetti esplosione BOOOM!
    }
}
