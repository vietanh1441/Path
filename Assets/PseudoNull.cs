using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoNull : MonoBehaviour {

    private GameObject gm_obj;
    private GameManager gm_scr;
    public int x, y;
    private int gridWidth, gridHeight;
    private int xl, xr, yu, yd;
    public int color = 1000;


    // Use this for initialization
    protected virtual void Start () {
        
    }

    private void Init()
    {

        transform.tag = "Nu";
        gm_obj = GameObject.FindGameObjectWithTag("GM");
        gm_scr = gm_obj.GetComponent<GameManager>();
        gridWidth = gm_scr.GridWidth;
        gridHeight = gm_scr.GridHeight;
        xl = 15 - gridWidth;
        xr = 14 + 2*gridWidth;
        yu = 14 + 2*gridHeight;
        yd = 15 - gridHeight;

        //Debug.Log(" Check: " + xr + " " + xl + " " + yu + " " + yd);
    }

    private void ExtraRegister()
    {
        Register();
        //Debug.Log(" X :" + x);
        //Debug.Log(" Y : " + y);
        gm_scr.gems[x, y] = gameObject;
        gameObject.transform.parent = gm_obj.transform;
    }

    void Register()
    {
        Update();
        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);
        
            if (x < xl)
                x = xr;
            if (x > xr)
                x = xl;
            if (y < yd)
                y = yu;
            if (y > yu)
                y = yd;
        

        transform.position = new Vector2(x, y);
        //gm_scr.gems[x, y] = gameObject;
        //gameObject.transform.parent = gm_obj.transform;
    }

    // Update is called once per frame
    protected virtual void Update () {

        if (transform.position.x < xl - 1)
            transform.position = new Vector2((xr - xl + 1) + transform.position.x , transform.position.y);
        if (transform.position.x > xr + 1)
            transform.position = new Vector2(transform.position.x - (xr - xl + 1), transform.position.y);
        if (transform.position.y < yd -1)
            transform.position = new Vector2(transform.position.x,  transform.position.y + (yu - yd + 1));
        if (transform.position.y > yu + 1)
            transform.position = new Vector2(transform.position.x, transform.position.y - (yu - yd + 1));
    }
}
