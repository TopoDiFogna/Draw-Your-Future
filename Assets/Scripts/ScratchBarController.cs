using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScratchBarController : MonoBehaviour {

    GameObject fullBar;
    public float max_pool_size = 200;
    private float current_pool_size;

	// Use this for initialization
	void Start () {
        current_pool_size = max_pool_size;
        fullBar = GameObject.Find("FullBar");
	}
	
	// Update is called once per frame
	void Update () {
        print(fullBar);
	    if(fullBar==null)
            fullBar = GameObject.Find("FullBar");
    }

    public void ChangeSize(int increment)
    {
        current_pool_size += increment;
        float width_percentage = (current_pool_size / max_pool_size);
        print(fullBar);
        fullBar.transform.localScale = new Vector3(width_percentage, fullBar.transform.localScale.y);


        /*fullBar.rectTransform.rect.Set(fullBar.rectTransform.rect.position.x,
            fullBar.rectTransform.rect.position.y,
            basic_width * width_percentage,
            fullBar.rectTransform.rect.height);*/
            
    }
}
