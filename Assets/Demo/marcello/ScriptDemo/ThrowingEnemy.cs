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
            GameObject g = (GameObject)Instantiate(rock, transform.position, Quaternion.identity);
            float v0x = (player.position.x - tr.position.x) / time;
            float v0y = (player.position.y + 0.5f * g.GetComponent<Rigidbody2D>().gravityScale*10 * time*time - tr.position.y) / time;
            print(v0x + "  " + v0y);
            Debug.DrawRay(tr.position, new Vector3(v0x, v0y, 0).normalized, Color.red, 10000f);
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
            //g.GetComponent<Rigidbody2D>().velocity = new Vector2(v0x, v0y);
            yield return new WaitForSeconds(3f);
        }
        print(Vector2.Distance(tr.position, player.transform.position));
        shooting = false;
        yield break;

    }
}
