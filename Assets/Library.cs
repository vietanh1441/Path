using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour {
    public List<Sprite[]> list = new List<Sprite[]>();
    public Sprite[] pic0 = new Sprite[9];
    private int current_pic;

	// Use this for initialization
	void Start () {
        list.Add(pic0);
		
	}

    private void SetCurrentPic(int p)
    {
        current_pic = p;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite GetSprite(int s)
    {
        return list[current_pic][s];
    }
}
