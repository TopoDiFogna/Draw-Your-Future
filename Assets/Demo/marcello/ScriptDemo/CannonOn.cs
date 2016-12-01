using UnityEngine;
using System.Collections;
using System;

public class CannonOn : MonoBehaviour {

    public GameObject CBSpawn;
    public GameObject CannonBall;
    public GameObject Spark;
    public GameObject Direction;
    public float BallPower;

    Vector3 CBSPosition;
    Vector2 BallDir;

    public bool activated = false;

	// Use this for initialization
	void Start () {

	}

    private void OnEnable()
    {
        CBSPosition = CBSpawn.transform.position;
        BallDir = (Direction.transform.position - CBSpawn.transform.position).normalized;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            StartCoroutine(CannonShoot());
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            activated = false;
        }
    }

    private IEnumerator CannonShoot()
    {
        GameObject g = ObjectPoolingManager.Instance.GetObject(Spark.name.ToString());
        g.transform.position = transform.position;
        g.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(2);
        g.SetActive(false);
        GameObject cb = ObjectPoolingManager.Instance.GetObject(CannonBall.name.ToString());
        cb.transform.position = CBSPosition;
        cb.GetComponent<Rigidbody2D>().AddForce(BallDir * BallPower, ForceMode2D.Impulse);
    }
}
