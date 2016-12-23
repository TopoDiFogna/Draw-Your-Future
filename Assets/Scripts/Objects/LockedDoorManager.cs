using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LockedDoorManager : MonoBehaviour {

    public SpearTrap speartrap;
    public GameObject Door;
    List<int> combination;
    public List<int> Solution;
    public TastoController[] tcs;
	// Use this for initialization
	void Start () {
        combination = new List<int>();
	}
	
    public void AddElement(int e)
    {
        
        combination.Add(e);
        //print(combination.Count);
        if (combination.Count == 3)
        {
            if (combination.SequenceEqual(Solution))
            {
                Door.SetActive(false);
            }
            else
            {
                combination = new List<int>();
                foreach (TastoController tc in tcs)
                {
                    tc.Reset();
                }
                speartrap.enabled = true;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
