using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LockedDoorManager : MonoBehaviour {

    public SpearTrap speartrap;
    public GameObject Door;
    List<int> combination;
    public List<int> Solution;
    public LeverCostellation[] lcs;
	// Use this for initialization
	void Start () {
        combination = new List<int>();
	}
	
    public void Reset()
    {
        combination = new List<int>();
        foreach (LeverCostellation tc in lcs)
        {
            tc.Reset();
        }
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
                foreach (LeverCostellation lc in lcs)
                {
                    lc.Reset();
                }
                speartrap.enabled = true;
            }
        }
    }


    public void PopElement(int element)
    {
        combination.Remove(element);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
