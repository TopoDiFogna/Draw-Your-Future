using UnityEngine;
using System.Collections;

public class BackgroundSound : MonoBehaviour {

    private static volatile BackgroundSound istance;

	// Use this for initialization
	void Start () {
        if (istance == null)
        {
            istance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
