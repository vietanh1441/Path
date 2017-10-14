using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int orientation;
    // 0: up
    // 1: right
    // 2: down
    // 3: left

    private GameObject gm_obj;
    private GameManager gm_scr;



    // Use this for initialization
    void Start () {
        gm_obj = GameObject.FindGameObjectWithTag("GM");
        gm_scr = gm_obj.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Char_Move()
    {
        //First check where is the next tile
        GameObject g = GetNextTile();
        
        //Then check if the tile will let the char to go there
        bool move = MoveCheck(g.GetComponent<Gem>().color);

        //if yes, move the char to the tile
        if(move)
        {
            transform.position = new Vector2(g.transform.position.x, g.transform.position.y);
        }

        //Then check where is the orientation will be based on the tile type

    }

    private bool MoveCheck(int c)
    {
        if(orientation == 0)
        {
            if (c == 0 || c == 2 || c == 5 || c == 6)
                return true;
            else
                return false;
        }
        if(orientation == 1)
        {
            if (c == 0 || c == 1 || c == 3 || c == 5)
                return true;
            else
                return false;
        }
        if (orientation == 2)
        {
            if (c == 0 || c == 2 || c == 3 || c == 4)
                return true;
            else
                return false;
        }
        if (orientation == 3)
        {
            if (c == 0 || c == 1 || c == 4 || c == 6)
                return true;
            else
                return false;
        }
        return false;
    }

    private GameObject GetNextTile()
    {
        int x = (int)Mathf.Round(transform.position.x);
        int y = (int)Mathf.Round(transform.position.y);
        if(orientation == 0)
        {
            y++;
        }
        if(orientation == 1)
        {
            x++;
        }
        if (orientation == 2)
        {
            y--;
        }
        if (orientation == 3)
        {
            x--;
        }
        return gm_scr.gems[x, y];
    }
}
