using UnityEngine;
using System.Collections;

public class BgSpawner : MonoBehaviour {

    public GameObject Obj;
    bool en = false;
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        print("Triggered");
        if(coll.gameObject.tag == "Scratch" && !en)
        {
            print("Triggered inside");
            Instantiate(Obj, transform.position, Quaternion.identity);
            en = true;
            StartCoroutine(Reset(10));
        }
    }

    IEnumerator Reset(int time)
    {
        yield return new WaitForSeconds(time);
        en = false;
    }
}
