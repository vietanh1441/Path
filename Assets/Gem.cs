using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : PseudoNull {

    public Sprite[] color_sprite = new Sprite[5];
    private SpriteRenderer spriteRenderer;
    
 

    //public int x, y;

    protected override void Start()
    {
        base.Start();
       
        
        //Init_color();
    }

   
    void Init_color()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //transform.tag = "Unready";
        int color_num = 4;
        color = Random.Range(0, color_num);
        spriteRenderer.sprite = color_sprite[color];
        transform.tag = "Gem";
        //transform.tag = color.ToString();
    }

    private void SetColor(int c)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = color_sprite[c];
        color = c;
        transform.tag = "Gem";
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
    }
}
