using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {

    Transform tr;
    public float timetolive;
    float timer = 0f;
    bool vanishing = false;
    Vector3 scale;

	// Use this for initialization
	
    void Start()
    {
        tr = transform;
        scale = tr.localScale;
    }

	void OnEnable()
    {
        timer = 0f;
        vanishing = false;
        StartCoroutine(DisablePaint());
    }

	// Update is called once per frame
	void Update () {

        //timer += Time.deltaTime;
        //if (timer >= timetolive && !vanishing)
        //{
        //    timer = 0f;
        //    vanishing = true;
        //    //gameObject.SetActive(false);
        //}
        //if (vanishing)
        //{
        //    tr.localScale -= new Vector3(0.01f, 0.01f, 0);
        //    if (tr.localScale.x <= 0)
        //    {
        //        tr.localScale = scale;
        //        coll.enabled = false;
        //        gameObject.SetActive(false);
        //    }
        //}
	}

    IEnumerator DisablePaint()
    {
        yield return new WaitForSeconds(timetolive);
        gameObject.SetActive(false);
    }
}
