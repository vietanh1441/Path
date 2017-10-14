using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[,] gems = new GameObject[41, 41];
    public GameObject[,] floors = new GameObject[40, 40];
    public GameObject gemPrefab;
    public GameObject floorPrefab;
    public GameObject tempPrefab;
    public GameObject nullPrefab;

    public GameObject[] character;

    public GameObject library;

    private Vector2 originalMouse;

    private bool horizontal = false;
    private bool vertical = false;
    private GameObject temp;

    public int GridWidth = 5;
    public int GridHeight = 5;
    public int max_element = 4;

    private int x_move, y_move;

    private bool selected;

    public List<int> pre_x = new List<int>();
    public List<int> pre_y = new List<int>();
    public List<int> pre_cor = new List<int>();

    public enum Status { Ready, Move, Wait };
    public Status status;

    // Use this for initialization
    void Start () {
        selected = false;
        temp = Instantiate(tempPrefab, new Vector3(0,0,0),Quaternion.identity);
        CustomStart(GridWidth, GridHeight, 4);
        //CustomStart(5, 5, 4);
    }
	
	// Update is called once per frame
	void Update () {


        /*if (Input.GetMouseButtonDown(0) && status == Status.Ready)
        {
            Collider2D hit = Physics2D.OverlapPoint(GetMousePosition());
            if (hit != null)
            {
                selected = true;
                //Get the hit coordinate
                x_move = hit.gameObject.GetComponent<Gem>().x;
                y_move = hit.gameObject.GetComponent<Gem>().y;
                //highlight the selected floor
                floors[x_move, y_move].GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            GoLeft();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            GoRight();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            GoUp();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            GoDown();
        }*/

        if (Input.GetMouseButtonDown(0) && status == Status.Ready)
        {
            originalMouse = GetMousePosition();
            Collider2D hit = Physics2D.OverlapPoint(GetMousePosition());
            if (hit != null)
            {
                
                status = Status.Move;
                x_move = (int)hit.gameObject.transform.position.x;
                y_move = (int)hit.gameObject.transform.position.y;
            }
        }
        if (status == Status.Move && Vector2.Distance(GetMousePosition(), originalMouse) > 0.1f && selected == false)
        {
            if (Mathf.Abs(GetMousePosition().x - originalMouse.x) > Mathf.Abs(GetMousePosition().y -originalMouse.y))
            {
                selected = true;
                GoHorizontal();
            } else
            {
                selected = true;
                GoVertical();
            }
        }
        else if (horizontal)
        { 
            if (selected == true)
            {
                temp.transform.position = new Vector2(GetMousePosition().x - originalMouse.x, 0);
            }
            if (selected && Input.GetMouseButtonUp(0))
            {
                temp.transform.position = new Vector2(Mathf.Round(GetMousePosition().x - originalMouse.x), 0);
                selected = false;
                status = Status.Ready;
                horizontal = false;
                AbandonChildren(temp);
            }
        }
        else if (vertical)
        {
            if (selected == true)
            {
                temp.transform.position = new Vector2(0, GetMousePosition().y - originalMouse.y);
            }
            if (selected && Input.GetMouseButtonUp(0))
            {
                temp.transform.position = new Vector2(0, Mathf.Round(GetMousePosition().y -originalMouse.y));
                selected = false;
                status = Status.Ready;
                vertical = false;
                AbandonChildren(temp);
            }
        }



     }

    private void GoHorizontal()
    {
        for (int i = 5; i < 35; i++)
        {
            if (gems[i, y_move] != null)
                gems[i, y_move].transform.parent = temp.transform;
        }
        horizontal = true;
    }

    private void GoVertical()
    {
        for (int i = 5; i < 35; i++)
        {
            if (gems[x_move, i] != null)
                gems[x_move, i].transform.parent = temp.transform;
        }
        vertical = true;
    }

    private void AbandonChildren(GameObject temp)
    {
        temp.BroadcastMessage("ExtraRegister");
        temp.transform.position = new Vector2(0, 0);
        MakingIllusion();


    }

    private void GoLeft()
    {
        GameObject temp = Instantiate(tempPrefab);
        for(int i = 0; i < 40; i++)
        {
            if(gems[i,y_move] != null)
            gems[i, y_move].transform.parent = temp.transform;
        }
        temp.transform.position = new Vector2(temp.transform.position.x - 1, temp.transform.position.y);
        AbandonChildren(temp);
    }

    private void GoRight()
    {
        GameObject temp = Instantiate(tempPrefab);
        for (int i = 0; i < 40; i++)
        {
            if (gems[i, y_move] != null)
                gems[i, y_move].transform.parent = temp.transform;
        }
        temp.transform.position = new Vector2(temp.transform.position.x + 1, temp.transform.position.y);
        AbandonChildren(temp);
    }

    private void GoUp()
    {
        GameObject temp = Instantiate(tempPrefab);
        for (int i = 0; i < 40; i++)
        {
            if (gems[x_move, i] != null)
                gems[x_move, i].transform.parent = temp.transform;
        }
        temp.transform.position = new Vector2(temp.transform.position.x , temp.transform.position.y-1);
        AbandonChildren(temp);
    }

    private void GoDown()
    {
        GameObject temp = Instantiate(tempPrefab);
        for (int i = 0; i < 40; i++)
        {
            if (gems[x_move, i] != null)
                gems[x_move, i].transform.parent = temp.transform;
        }
        temp.transform.position = new Vector2(temp.transform.position.x , temp.transform.position.y+1);
        AbandonChildren(temp);
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void CustomStart(int g = 10, int h = 10, int e = 4)
    {

        GridHeight = h;
        GridWidth = g;
        max_element = e;

        //library.SendMessage("SetCurrentPic", 0);

        status = Status.Wait;

        //Populate everything with null
        for (int y = 15; y < GridHeight + 15; y++)
        {
            for (int x = 15; x < GridWidth + 15; x++)
            {
                CreateNull(x, y);
                CreateIllusion(x, y, 1000);
            }
        }



                //currentStatus = (int)Status.NewTurn;

        for (int y = 15; y < GridHeight + 15; y++)
        {
            for (int x = 15; x < GridWidth + 15; x++)
            {
                
                
                //Create floor
                CreateNewFloor(x, y);
                //	mark_to_create[x,y] = false;
                //				gems.Add(g.GetComponent<Gem>());
            }
        }

        Predetermined();

        //RandomizedBoard();
        //Create illusion gems
        

        //Match Checking

        //caching central


        //Invoke("BoardChecking", 0.2f);
        //Display();
        status = Status.Ready;
    }

    private void RandomizedBoard()
    {
        //Pick a rando point x or y;
        //Set them at x_move or y_move

        for (int i = 0; i < 50; i++)
        {
            x_move = Random.Range(15, 15 + GridWidth);
            y_move = Random.Range(15, 15 + GridHeight);


            GoRandomDir(Random.Range(0, 2));

        }
        //Set them Move horizontaly or vertically
        
        //Set the temp to move by random step
        
        
    }

    private void GoRandomDir(int r)
    {
        if(r == 0)
        {
            GoVertical();
            temp.transform.position = new Vector2(temp.transform.position.x, temp.transform.position.y - Random.Range(0,GridHeight));
        }
        else
        {
            GoHorizontal();
            temp.transform.position = new Vector2(temp.transform.position.x - Random.Range(0, GridWidth), temp.transform.position.y);
        }
        AbandonChildren(temp);
    }

    private void Predetermined()
    {
       for(int i = 0; i < pre_x.Count; i++)
        {
            CreateGem(pre_x[i], pre_y[i], pre_cor[i]);
            CreateIllusion(pre_x[i], pre_y[i], pre_cor[i]);
        }
       
    }

    private void MakingIllusion()
    {
        for (int y = 15; y < GridHeight + 15; y++)
        {
            for (int x = 15; x < GridWidth + 15; x++)
            {
                GameObject obj = gems[x, y];
                if (gems[x, y].tag == "Nu")
                {
                    CreateIllusion(x, y, 1000);
                }
                else
                {
                    CreateIllusion(x, y, obj.GetComponent<Gem>().color);
                }
            }
        }

        MoveChar();
    }

    private void MoveChar()
    {
        character[0].SendMessage("Char_Move");
    }

    //Create a random gem at x,y;
    private GameObject CreateNewGem(int x, int y)
    {
        //Debug.Log(" " + x + " " + y);
        GameObject g = Instantiate(gemPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        g.transform.parent = gameObject.transform;
        g.SendMessage("Init");
        g.SendMessage("Init_color");
        g.SendMessage("Register");
        Destroy(gems[x, y]);
        gems[x, y] = g;

        // match_flag = false;
        /* while(CheckMatch(gems[x,y]))
         {
             gems[x, y].SendMessage("Init_color");
             match_flag = false;
         }*/

        return g;

    }

    //CreateIllusion create predetermine gem at 4 spot
    //if g_type == 1000, that means the obj is a pseudoNull
    private void CreateIllusion(int x, int y, int g_type)
    {
        if (g_type == 1000)
        {
            CreateNull(x - GridWidth, y);
            CreateNull(x + GridWidth, y);
            CreateNull(x, y + GridHeight);
            CreateNull(x, y - GridHeight);
        }
        else
        {
            CreateGem(x - GridWidth, y, g_type);
            CreateGem(x + GridWidth, y, g_type);
            CreateGem(x, y + GridHeight, g_type);
            CreateGem(x, y - GridHeight, g_type);
        }
    }

    //Create a predetermine gem at x,y
    private GameObject CreateGem(int x, int y, int g_type)
    {
        
        GameObject g = Instantiate(gemPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        g.transform.parent = gameObject.transform;
        g.SendMessage("Init");
        g.SendMessage("SetColor", g_type);
        g.SendMessage("Register");
        Destroy(gems[x, y]);
        gems[x, y] = g;

        return g;
    }

    public void CreateNull(int x, int y)
    {
        //Debug.Log(" " + x + " " + y);
        GameObject g = Instantiate(nullPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        g.transform.parent = gameObject.transform;
        g.SendMessage("Init");
        g.SendMessage("Register");
        Destroy(gems[x, y]);
        gems[x, y] = g;

        // match_flag = false;
        /* while(CheckMatch(gems[x,y]))
         {
             gems[x, y].SendMessage("Init_color");
             match_flag = false;
         }*/

    }

    public void CreateNewFloor(int x, int y)
    {
        //Debug.Log(" " + x + " " + y);
        GameObject g = Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        g.transform.parent = gameObject.transform;
        //g.SendMessage("Register");

       floors[x, y] = g;

        // match_flag = false;
        /* while(CheckMatch(gems[x,y]))
         {
             gems[x, y].SendMessage("Init_color");
             match_flag = false;
         }*/

    }

}
