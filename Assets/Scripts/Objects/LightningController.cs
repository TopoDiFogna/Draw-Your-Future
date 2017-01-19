using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour {

    public float timeVisible;

	// Use this for initialization
	void Start () {
        
    }

    void OnEnable()
    {
        StartCoroutine("Lightning");
    }
	
	// Update is called once per frame

    IEnumerator Lightning()
    {
        yield return new WaitForSeconds(timeVisible);
        gameObject.SetActive(false);
    }
}
