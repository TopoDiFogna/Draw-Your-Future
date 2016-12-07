using UnityEngine;

public class ScratchBarController : MonoBehaviour {

    GameObject fullBar;
    float max_pool_size;
    float current_pool_size;
    MouseSpawn ms;

	// Use this for initialization
	void Start () {
        ms = gameObject.GetComponent<MouseSpawn>();
        max_pool_size = ms.NumberOfScratches;
        current_pool_size = max_pool_size;
        fullBar = GameObject.Find("FullBar");
	}
	
	// Update is called once per frame
	void Update () {
	    if(fullBar==null)
            fullBar = GameObject.Find("FullBar");
    }

    public void ChangeSize(int increment)
    {
        current_pool_size += increment;
        float width_percentage = (current_pool_size / max_pool_size);
        fullBar.transform.localScale = new Vector3(width_percentage, fullBar.transform.localScale.y);         
    }
}
