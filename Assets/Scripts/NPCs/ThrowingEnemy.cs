using UnityEngine;
using System.Collections;
using System;

public class ThrowingEnemy : MonoBehaviour
{

    public GameObject rock;
    bool shooting = false;
    Transform player;
    Transform tr;
    public float time = 1f;
    float radius;
    Animator animator;

    void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tr = transform;
        animator = gameObject.GetComponent<Animator>();
    }



    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !shooting)
        {
            shooting = true;
            StartCoroutine(Shot());
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player" && shooting)
        {
            shooting = false;
            //StopCoroutine(Shot());
        }
    }

    private IEnumerator Shot()
    {
        while (Vector2.Distance(new Vector2(tr.position.x, tr.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) < radius/2)
        {
            print(Vector2.Distance(new Vector2(tr.position.x, tr.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) < radius / 2);
            GameObject g = ObjectPoolingManager.Instance.GetObject(rock.name.ToString());
            //if(g != null) { } TODO 
            g.transform.position = tr.position;
            float v0x = (player.position.x - tr.position.x) / time;
            float v0y = (player.position.y + 0.5f * g.GetComponent<Rigidbody2D>().gravityScale*10 * time*time - tr.position.y) / time;
            if(animator!=null)
                animator.SetTrigger("Throw");
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f);
        }
        print(Vector2.Distance(new Vector2(tr.position.x, tr.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) < radius / 2);
        shooting = false;
        yield break;

    }
}
