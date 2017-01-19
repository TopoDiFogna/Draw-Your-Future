using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBreak : MonoBehaviour {

    public GameObject m_particles;

	// Use this for initialization
	void Start () {
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Terrain")
        {
            collision.gameObject.SetActive(false);
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<Rigidbody2D>().isKinematic = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch")
        {
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    public void DisableCrate()
    {
        StartCoroutine(CreateParticles());
    }

    IEnumerator CreateParticles()
    {
        m_particles.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}
