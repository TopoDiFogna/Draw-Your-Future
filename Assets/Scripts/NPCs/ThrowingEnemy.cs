using UnityEngine;
using System.Collections;
using System;

public class ThrowingEnemy : MonoBehaviour
{

    public GameObject rock;
    bool shooting = false;
    Transform player;
    Transform tr;
    public float maxDist;
    public float time = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tr = transform;
    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !shooting)
        {
            shooting = true;
            StartCoroutine(Shot());
        }
    }

    private IEnumerator Shot()
    {
        while (Vector2.Distance(tr.position, player.transform.position) < maxDist)
        {
            GameObject g = ObjectPoolingManager.Instance.GetObject(rock.name.ToString());
            g.transform.position = transform.position;
            float v0x = (player.position.x - tr.position.x) / time;
            float v0y = (player.position.y + 0.5f * g.GetComponent<Rigidbody2D>().gravityScale*10 * time*time - tr.position.y) / time;
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f);
        }
        shooting = false;
        yield break;

    }
}
