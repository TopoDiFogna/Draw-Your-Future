using UnityEngine;
using System.Collections;
using System;

public class ThrowingEnemyKey : MonoBehaviour
{

    public GameObject m_key;
    bool can_shoot = true;
    Transform player;
    Transform tr;
    public float time = 1f;
    Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tr = transform;
        animator = gameObject.GetComponent<Animator>();
    }



    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && can_shoot)
        {
            can_shoot = false;
            StartCoroutine(Shot());
        }
    }


    private IEnumerator Shot()
    {
        GameObject g = Instantiate(m_key);
        g.transform.position = tr.position;
        float v0x = (player.position.x - tr.position.x) / time;
        float v0y = (player.position.y + 0.5f * g.GetComponent<Rigidbody2D>().gravityScale*10 * time*time - tr.position.y) / time;
        animator.SetTrigger("Throw");
        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
        yield return new WaitForSeconds(3f);
    }
}
