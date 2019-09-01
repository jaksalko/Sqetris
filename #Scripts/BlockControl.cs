using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BlockControl : SingleTon<BlockControl> {
    public GameObject endPopup;
    private int blockNum2 = 0;
    private int blockNum = 0;
    private int X,Y = 0;
    public GameObject wall;
    private GameObject blck;
    public GameObject[] blockPrf;
    public Button leftButton;
    public Button rightButton;
    private List<GameObject> block = new List<GameObject>();
    public GameObject color;
    private GameObject[] test;
    private GameObject[,] Area = new GameObject[17,17];
    private GameObject[,] Area2 = new GameObject[17, 17];
    private int rotateNum = 0; // 0->1->2->3->0
    public GameObject tetArea;
    private int score = 0;
    private int line= 7;
    private int ch = 3;
    private int goalpoint = 12;
    private int life = 0;
    public TextMeshProUGUI text;
    public GameObject nextImg;
    public GameObject[] lifeOb;
    public GameObject popup;
    public GameObject exitPopup;
    private List<GameObject> destroyBlockList = new List<GameObject>();
    private GameObject destroyBlock;
    private GameObject destroyBlock2;
    public TextMeshProUGUI record;
    private int locker = 1;
    private void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 0);
        blockNum2 = Random.Range(0, 4);
        //color.GetComponent<Image>().color = Color.black;
        for (int a = 0; a < 17; a++)
        {
            for (int b = 0; b < 17; b++)
            {
                if (a == 0 || a == 16 || b == 0           )//*****
                {                                          //*   *
                    Area[a, b] = wall;                     //*   *
                    Area2[a, b] = wall; 
                }
                else {
                    Area[a, b] = null;
                    Area2[a, b] = null;
                }

            }
        }
        Area2[8, 8] = wall;
        Area[8, 8] = wall;
        //Debug.Log(Area[8, 8]);
        //color.GetComponent<Image>().color = Color.yellow;
        MakeBlock();
        //color.GetComponent<Image>().color = Color.white;
        StartCoroutine(MoveBlock());
        //color.GetComponent<Image>().color = Color.green;

    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))

            {
                blck.SetActive(false);
                Time.timeScale = 0;
                exitPopup.SetActive(true);
            }
        }

          
          
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void WallRotate()
    {
        if (locker == 1)
        {
            Debug.Log("RotateStart");
            locker = 0;
            if (Check_CanRotCClcok())
            {
                WallRotate_CClockWise();
            }
            locker = 1;
            Debug.Log("RotateEnd");
        }
        
    }
    public bool Check_CanRotCClcok() {
        for (int a = 1; a < 16; a++)
        {
            for (int b = 1; b < 16; b++)
            {
                if (Area[a, b] == wall)
                {
                    if (Area[b, 15 - a + 1] == blck)
                    {
                        return false;
                    }
                }
            }
        }
        return true;

    }
    public void WallRotate_CClockWise()//반시계회전90
    {
        
        
        for (int a = 1; a < 16; a++)
        {
            for (int b = 1; b < 16; b++)
            {
               
                    if (Area[a, b] == wall)
                    {
                        //Debug.Log(a + "," + b);
                        //Debug.Log((b) + "," + (15 - a + 1));

                        GameObject temp = tetArea.transform.Find("block" + a + "," + b).gameObject;
                        temp.name = "block" + (b) + "," + (15 - a + 1);
                        Area[a, b] = null;
                        Area2[b, 15 - a + 1] = wall;
                    }
                   
                

                
               
                    
            }
        }
        for (int a = 1; a < 16; a++)
        {
            for (int b = 1; b < 16; b++)
            {
                if (Area2[a, b] == wall)
                {
                    Area[a, b] = Area2[a, b];
                    Area2[a, b] = null;
                }
            }
        }
                /*for (int a = 1; a < 16; a++)
                {
                    for (int b = 1; b < 16; b++)
                    {
                        //Debug.Log("Area["+a+","+b+"]  "+Area[a, b]+ "Area2[" + a + "," + b + "]  "+ Area2[a, b]);
                        if (Area[a, b] != blck)
                        {
                            if (Area2[a, b] == wall)
                            {
                                //Debug.Log(a + "," + b + "  is wall");
                                Area[a, b] = Area2[a, b];
                            }
                            else {
                                //Debug.Log(a + "," + b + "  is null");
                                Area[a, b] = null;
                            }


                        }

                    }
                }

                for (int a = 0; a < 17; a++)
                {
                    for (int b = 0; b < 17; b++)
                    {
                        if (a == 0 || a == 16 || b == 0)//*****
                        {                                          //*   *
                                          //*   *
                            Area2[a, b] = wall;
                        }
                        else
                        {

                            Area2[a, b] = null;
                        }

                    }
                }
                Area2[8, 8] = wall;*/
                tetArea.transform.Rotate(new Vector3(0, 0, 90));
       
    }
  
    public void Rotation()
    {
        //Debug.Log("BlockNum : " +blockNum + "  RotNum : " +rotateNum +"  X: "+ X + "  Y:" + Y);
        Rotate();
        
    }
    public void Rotate() {
        switch (blockNum)
        {
            case 0:
                
                break;
            case 1:
                if (rotateNum == 0/* && Area[X + 1, Y + 1] == null*/ && Area[X + 1, Y] == null)
                {
                  
                    Area[X, Y + 1] = null;
                    Area[X + 1, Y] = blck;
                    rotateNum = 1;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 0 && Area[X + 1, Y] == wall && Area[X-1,Y] == null)
                {
                    Area[X, Y + 1] = null;
                    Area[X - 1, Y] = blck;
                    rotateNum = 1;
                   
                    Area[X, Y].transform.localPosition += new Vector3(-60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X--;
                }
                else if (rotateNum == 1 && Area[X, Y - 1] == null /*&& Area[X + 1, Y - 1] == null*/)
                {
                    Area[X + 1, Y] = null;
                    Area[X, Y - 1] = blck;
                    rotateNum = 2;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 2 /*&& Area[X - 1, Y - 1] == null*/ && Area[X - 1, Y] == null)
                {
                    Area[X, Y - 1] = null;
                    Area[X - 1, Y] = blck;
                    rotateNum = 3;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 2 && Area[X + 1, Y ] == null && Area[X - 1, Y] == wall)
                {
                    Area[X, Y - 1] = null;
                    Area[X + 1, Y] = blck;
                    rotateNum = 3;
                    Area[X, Y].transform.localPosition += new Vector3(60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X++;
                }
                else if (rotateNum == 3 /*&& Area[X - 1, Y + 1] == null*/ && Area[X, Y + 1] == null)
                {
                    Area[X - 1, Y] = null;
                    Area[X, Y + 1] = blck;
                    rotateNum = 0;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
            //    else { Debug.Log("Cant Rotate this"); }
                break;
            case 2:
                if (rotateNum == 0 && Area[X , Y - 1] == null /*&& Area[X + 1, Y - 1] == null && Area[X + 1, Y+1] == null*/)
                {
                    Area[X, Y + 1] = null;
                    Area[X , Y-1] = blck;
                    rotateNum = 1;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 1 /*&& Area[X+1, Y - 1] == null && Area[X - 1, Y - 1] == null*/ && Area[X - 1, Y] == null)
                {
                    Area[X + 1, Y] = null;
                    Area[X-1, Y] = blck;
                    rotateNum = 2;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 1 && Area[X + 1, Y + 1] == null && Area[X - 1, Y] == wall)
                {
                    Area[X, Y+1] = null;
                    Area[X+1, Y+1] = blck;
                    rotateNum = 2;
                    Area[X, Y].transform.localPosition += new Vector3(60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X++;
                }
                else if (rotateNum == 2 /*&& Area[X - 1, Y - 1] == null && Area[X - 1, Y+1] == null*/ && Area[X , Y+1] == null)
                {
                    Area[X, Y - 1] = null;
                    Area[X , Y+1] = blck;
                    rotateNum = 3;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 3/* && Area[X - 1, Y + 1] == null && Area[X+1, Y + 1] == null*/ &&Area[X + 1, Y] == null)
                {
                    Area[X - 1, Y] = null;
                    Area[X+1, Y ] = blck;
                    rotateNum = 0;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 3 &&Area[X-1,Y+1] ==null &&Area[X + 1, Y] ==wall)
                {
                    Area[X , Y+1] = null;
                    Area[X-1, Y+1 ] = blck;
                    rotateNum = 0;
                    Area[X, Y].transform.localPosition += new Vector3(-60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X--;
                }
             //   else { Debug.Log("Cant Rotate this"); }
                break;

            case 3:
                if (rotateNum == 0 && Area[X, Y - 1] == null)
                { Area[X - 1, Y] = null;
                    Area[X, Y - 1] = blck;
                    rotateNum = 1;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 1 && Area[X - 1, Y] == null)
                { Area[X, Y + 1] = null;
                    Area[X - 1, Y] = blck;
                    rotateNum = 2;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 1 && Area[X - 1, Y] == wall && Area[X + 2, Y] == null && Area[X + 1, Y - 1] == null) { Area[X, Y - 1] = null;
                    Area[X, Y + 1] = null;
                    Area[X, Y].transform.localPosition += new Vector3(60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X++;
                    Area[X + 1, Y] = blck;
                    Area[X, Y - 1] = blck;

                }
                else if (rotateNum == 2 && Area[X, Y + 1] == null) { Area[X + 1, Y] = null;
                    Area[X, Y + 1] = blck;
                    rotateNum = 3;
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (rotateNum == 3 && Area[X + 1, Y] == null) { Area[X, Y - 1] = null;
                    Area[X + 1, Y] = blck;
                    rotateNum = 0;
                    blck.transform.Rotate(new Vector3(0, 0, 90)); 

                }
                else if (rotateNum == 3 && Area[X + 1, Y] == wall && Area[X - 2 , Y] == null && Area[X-1,Y+1] == null){ Area[X, Y - 1] = null;
                    Area[X, Y + 1] = null;
                    Area[X, Y].transform.localPosition += new Vector3(-60, 0, 0);
                    blck.transform.Rotate(new Vector3(0, 0, 90));
                    X--;
                    Area[X - 1, Y] = blck;
                    Area[X, Y + 1] = blck;
                }
              //  else { Debug.Log("Cant Rotate this"); }
                break;

        }
    }
    public void Down() {
        switch (blockNum)
        {
            case 0:
                Area[X, Y] = null;
                Y++;
                Area[X, Y] = blck;
                break;
            case 1:
                if (rotateNum == 0)
                {
                    Area[X, Y] = null; 
                    Y++;
                    Area[X, Y + 1] = blck;
                }
                else if (rotateNum == 1)
                {
                    Area[X, Y] = null;
                    Area[X+1, Y] = null;
                    Y++;
                    Area[X, Y ] = blck;
                    Area[X + 1, Y] = blck;
                }
                else if (rotateNum == 2)
                {
                    Area[X, Y - 1] = null;
                    Y++;

                    Area[X, Y ] = blck;
                }
                else if (rotateNum == 3)
                {
                    Area[X, Y] = null;
                    Area[X - 1, Y] = null;
                    Y++;
                    Area[X, Y] = blck;
                    Area[X-1, Y] = blck;
                }

                  
                break;
            case 2:
                if (rotateNum == 0)
                {
                    Area[X, Y] = null;
                    Area[X + 1, Y] = null;
                    Y++;
                    Area[X + 1, Y] = blck;
                    Area[X, Y + 1] = blck;
                    
                }
                else if (rotateNum == 1)
                {
                    Area[X, Y-1] = null;
                    Area[X + 1, Y] = null;
                    Y++;
                    Area[X, Y] = blck;
                    Area[X + 1, Y] = blck;
                }
                else if (rotateNum == 2)
                {
                    Area[X, Y-1] = null;
                    Area[X - 1, Y] = null;
                    Y++;
                    Area[X - 1, Y] = blck;
                    Area[X, Y ] = blck;
                }
                else if (rotateNum == 3)
                {
                    Area[X, Y] = null;
                    Area[X - 1, Y] = null;
                    Y++;
                    Area[X, Y+1] = blck;
                    Area[X - 1, Y] = blck;
                }


                break;
            case 3:
                switch (rotateNum)
                {
                    case 0:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        Y++;

                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 1:
                        Area[X, Y] = null;
                        Area[X, Y - 1] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        Y++;
                        Area[X, Y] = blck;
                        Area[X, Y - 1] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 2:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y - 1] = null;
                        Area[X + 1, Y] = null;
                        Y++;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y - 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 3:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X, Y - 1] = null;
                        Y++;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X, Y - 1] = blck;
                        break;
                }
                        break;

        }
        
        
    }
    public void Right()
    {
        switch (blockNum)
        {
            case 0:
                Area[X, Y] = null;
                X++;
                Area[X, Y] = blck;
                break;
            case 1:
                if (rotateNum == 0)
                {
                    Area[X, Y] = null;
                    Area[X, Y + 1] = null;
                    X++;
                    Area[X, Y] = blck;
                    Area[X, Y + 1] = blck;
                }
                else if (rotateNum == 1)
                {
                    Area[X, Y] = null;
                    
                    X++;
                    Area[X+1, Y] = blck;
                    
                }
                else if (rotateNum == 2)
                {
                    Area[X, Y] = null;
                    Area[X, Y - 1] = null;
                    X++;
                    Area[X, Y] = blck;
                    Area[X, Y- 1] = blck;
                }
                else if (rotateNum == 3)
                {
                   
                    Area[X-1, Y ] = null;
                    X++;
                    Area[X, Y] = blck;
                   
                }


                break;
               
               
            case 2:
                if (rotateNum == 0)
                {
                    Area[X, Y] = null;
                    Area[X, Y + 1] = null;
                    X++;

                    Area[X, Y + 1] = blck;
                    Area[X + 1, Y] = blck;
                    
                }
                else if (rotateNum == 1)
                {
                    Area[X, Y] = null;
                    Area[X, Y - 1] = null;
                    X++;

                    Area[X, Y - 1] = blck;
                    Area[X + 1, Y] = blck;
                }
                else if (rotateNum == 2)
                {
                    Area[X-1, Y] = null;
                    Area[X, Y - 1] = null;
                    X++;

                    Area[X, Y - 1] = blck;
                    Area[X , Y] = blck;
                }
                else if (rotateNum == 3)
                {
                    Area[X-1, Y] = null;
                    Area[X, Y + 1] = null;
                    X++;

                    Area[X, Y + 1] = blck;
                    Area[X , Y] = blck;
                }


                break;

            case 3:
                switch (rotateNum)
                {
                    case 0:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        X++;

                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 1:
                        Area[X, Y] = null;
                        Area[X, Y - 1] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        X++;
                        Area[X, Y] = blck;
                        Area[X, Y - 1] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 2:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y - 1] = null;
                        Area[X + 1, Y] = null;
                        X++;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y - 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 3:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X, Y - 1] = null;
                        X++;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X, Y - 1] = blck;
                        break;

                }
                break;
        }
    }
    public void Left()
    {
        switch (blockNum)
        {
            case 0:
                Area[X, Y] = null;
                X--;
                Area[X, Y] = blck;
                break;
            case 1:
                if (rotateNum == 0)
                {
                   
                    Area[X, Y] = null;
                    Area[X, Y + 1] = null;
                    X--;
                    Area[X, Y] = blck;
                    Area[X, Y + 1] = blck;
                }
                else if (rotateNum == 1)
                {
                    
                    Area[X+1, Y] = null;
                    X--;
                    Area[X, Y] = blck;
                  

                }
                else if (rotateNum == 2)
                {
                    Area[X, Y] = null;
                    Area[X, Y - 1] = null;
                    X--;
                    Area[X, Y] = blck;
                    Area[X, Y - 1] = blck;
                }
                else if (rotateNum == 3)
                {

                    Area[X, Y] = null;
                    
                    X--;
                    
                    Area[X-1, Y] = blck;

                }


               
                break;
            case 2:
                if (rotateNum == 0)
                {
                    Area[X + 1, Y] = null;
                    Area[X, Y + 1] = null;
                    X--;

                    Area[X, Y + 1] = blck;
                    Area[X, Y] = blck;
                   
                }
                else if (rotateNum == 1)
                {
                    Area[X + 1, Y] = null;
                    Area[X, Y - 1] = null;
                    X--;

                    Area[X, Y - 1] = blck;
                    Area[X, Y] = blck;

                }
                else if (rotateNum == 2)
                {
                    Area[X , Y] = null;
                    Area[X, Y - 1] = null;
                    X--;

                    Area[X, Y - 1] = blck;
                    Area[X-1, Y] = blck;
                }
                else if (rotateNum == 3)
                {

                    Area[X , Y] = null;
                    Area[X, Y + 1] = null;
                    X--;

                    Area[X, Y + 1] = blck;
                    Area[X-1, Y] = blck;
                }



                break;
            case 3:
                switch (rotateNum)
                {
                    case 0:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        X--;

                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 1:
                        Area[X, Y] = null;
                        Area[X , Y-1] = null;
                        Area[X, Y + 1] = null;
                        Area[X + 1, Y] = null;
                        X--;
                        Area[X, Y] = blck;
                        Area[X , Y-1] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 2:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y - 1] = null;
                        Area[X + 1, Y] = null;
                        X--;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y - 1] = blck;
                        Area[X + 1, Y] = blck;
                        break;
                    case 3:
                        Area[X, Y] = null;
                        Area[X - 1, Y] = null;
                        Area[X, Y + 1] = null;
                        Area[X , Y-1] = null;
                        X--;
                        Area[X, Y] = blck;
                        Area[X - 1, Y] = blck;
                        Area[X, Y + 1] = blck;
                        Area[X , Y-1] = blck;
                        break;
                }
                break;

        }
    }
    public void MoveLeft()
    {
        if (Area[X - 1, Y] == wall)
        {

           // Debug.Log("Cant Left1");

        }
        else if (Area[X, Y + 1] == blck && Area[X - 1, Y + 1] == wall)
        {

           // Debug.Log("Cant Left3");

        }
        else if (Area[X, Y - 1] == blck && Area[X - 1, Y - 1] == wall)
        {

           // Debug.Log("Cant Left4");

        }
        else if (Area[X - 1, Y] == blck && Area[X - 2, Y] == wall)
        {

           // Debug.Log("Cant Leftt2");
        }
        else
        {
           // Debug.Log("Left");
            Area[X, Y].transform.localPosition += new Vector3(-60, 0, 0);
            Left();
        }
    }
    public void MoveRight()
    {
        if (Area[X + 1, Y] == wall)
        {

          //  Debug.Log("Cant Right1");

        }
        else if (Area[X , Y+1] == blck && Area[X+1,Y+1] == wall)
        {

          //  Debug.Log("Cant Right3");

        }
        else if (Area[X, Y - 1] == blck && Area[X + 1, Y - 1] == wall)
        {

          //  Debug.Log("Cant Right3");

        }
        else if (Area[X + 1, Y] == blck && Area[X + 2, Y] == wall)
        {

          //  Debug.Log("Cant Right2");
        }
        else
        {
          //    Debug.Log("Right"); 
            Area[X, Y].transform.localPosition += new Vector3(60, 0, 0);
            Right();
        }
    }
    IEnumerator MoveBlock()
    {
        while (locker == 1 )
        {
            Debug.Log("MoveBlockStart");
            locker = 0;
            if (Y == 15)
            {
                if (blockNum == 1 && rotateNum == 0)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 2 && rotateNum == 0)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 2 && rotateNum == 3)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 3 && rotateNum != 2)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }

            }
            else if (Y == 16)
            {
                Destroy(blck);
                MakeBlock();
                lifeOb[life].SetActive(false);
                life++;
            }
            if (life == 3)
            {

                if (PlayerPrefs.GetInt("HighScore", 0) < score)
                {
                    record.gameObject.SetActive(true);
                    record.text = "NEW RECORD" +
                        "\n" + score;
                    PlayerPrefs.SetInt("HighScore", score);
                }
                Debug.Log("endgame");
                endPopup.SetActive(true);
                for (int i = 1; i < 16; i++)
                {
                    for (int k = 1; k < 16; k++)
                    {
                        Area[i, k] = null;
                    }

                }
                for (int i = 0; i < block.Count; i++)
                {
                    Destroy(block[i]);
                }
                Time.timeScale = 0;
                StopCoroutine(MoveBlock());
            }
            if (Time.timeScale != 0)
            {
                if (Area[X, Y + 1] == wall)
                {

                    Debug.Log("Cant down1");
                    switch (blockNum)
                    {
                        case 0:
                            Area[X, Y] = wall;

                            break;
                        case 1:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                            }
                            else if (rotateNum == 3)
                            {

                                Area[X - 1, Y] = wall;
                                Area[X, Y] = wall;
                            }




                            break;
                        case 2:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;

                        case 3:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X - 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y + 1] = wall;
                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;


                    }
                    BlockManage();
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                    ClearLine();
                    MakeBlock();
                }
                else if (Area[X, Y + 1] == blck && Area[X, Y + 2] == wall)
                {
                    Debug.Log("x,y:" + X.ToString() + " " + Y.ToString());
                    Debug.Log("Cant down2");
                    switch (blockNum)
                    {
                        case 0:
                            Area[X, Y] = wall;

                            break;
                        case 1:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                            }
                            else if (rotateNum == 3)
                            {

                                Area[X - 1, Y] = wall;
                                Area[X, Y] = wall;
                            }




                            break;
                        case 2:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                        case 3:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X - 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y + 1] = wall;
                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                    }
                    BlockManage();
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                    ClearLine();
                    MakeBlock();

                }
                else if (Area[X + 1, Y] == blck && Area[X + 1, Y + 1] == wall)
                {
                    Debug.Log("Cant down3");
                    switch (blockNum)
                    {
                        case 0:
                            Area[X, Y] = wall;

                            break;
                        case 1:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                            }
                            else if (rotateNum == 3)
                            {

                                Area[X - 1, Y] = wall;
                                Area[X, Y] = wall;
                            }




                            break;
                        case 2:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                        case 3:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X - 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y + 1] = wall;
                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                    }
                    BlockManage();
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                    ClearLine();
                    MakeBlock();
                }
                else if (Area[X - 1, Y] == blck && Area[X - 1, Y + 1] == wall)
                {
                    Debug.Log("Cant down4");
                    switch (blockNum)
                    {
                        case 0:
                            Area[X, Y] = wall;

                            break;
                        case 1:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                            }
                            else if (rotateNum == 3)
                            {

                                Area[X - 1, Y] = wall;
                                Area[X, Y] = wall;
                            }




                            break;
                        case 2:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;

                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                        case 3:
                            if (rotateNum == 0)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X - 1, Y] = wall;

                            }
                            else if (rotateNum == 1)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y + 1] = wall;
                            }
                            else if (rotateNum == 2)
                            {
                                Area[X, Y] = wall;
                                Area[X + 1, Y] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }
                            else if (rotateNum == 3)
                            {
                                Area[X, Y] = wall;
                                Area[X, Y + 1] = wall;
                                Area[X, Y - 1] = wall;
                                Area[X - 1, Y] = wall;
                            }

                            break;
                    }
                    BlockManage();

                    ClearLine();
                    MakeBlock();
                }
                else
                {
                    Debug.Log("Down");
                    Down();
                    if (Area[X, Y] == null)
                    {
                        Debug.Log("BlockNum : " + blockNum + "  RotNum : " + rotateNum + "  X: " + X + "  Y:" + Y);
                        Debug.Log("nullerror");
                    }
                    else
                    {
                        Area[X, Y].transform.localPosition += new Vector3(0, -60, 0);
                    }



                }



                Debug.Log("MoveBlockEnd");
                locker = 1;
                yield return new WaitForSeconds(0.7f);
            }
        }
       
    }
    public void MakeBlock()//Make Block(0~3) randomly
    {
        
        rotateNum = 0;
        Debug.Log("MAke");
       
        blockNum = blockNum2;//0,1,2

        blockNum2 = Random.Range(0, 4);//0,1,2
        nextImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(blockNum2.ToString());
        int startX = 60 * Random.Range(0, 15);
        startX = 420;
        X = (7 + 1);
        Y = 1;
        if (Area[8, 1] == wall)
        {
            if (PlayerPrefs.GetInt("HighScore", 0) < score)
            {
                record.gameObject.SetActive(true);
                record.text = "NEW RECORD" +
                    "\n" + score;
                PlayerPrefs.SetInt("HighScore", score); 
            }
            Debug.Log("endgame");
            endPopup.SetActive(true);
            for (int i = 1; i < 16; i++)
            {
                for (int k = 1; k < 16; k++)
                {
                    Area[i, k] = null;
                }
                
            }
            for (int i = 0; i < block.Count; i++)
            {
                Destroy(block[i]);
            }
            Time.timeScale = 0;
            
        }
        if (Time.timeScale != 0)
        {
            switch (blockNum)
            {
                case 0://ㅁ
                    blck = Instantiate(blockPrf[blockNum], new Vector3(startX, 0, 0), Quaternion.identity);//Make GameObject
                                                                                                           // Debug.Log(blck.transform.childCount);

                    Area[X, Y] = blck;
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, false);
                    blck.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                    //StartCoroutine(MoveBlock(blck));
                    break;

                case 1://ㅁㅁ
                    blck = Instantiate(blockPrf[blockNum], new Vector3(startX, 0, 0), Quaternion.identity);//Make GameObject
                                                                                                           //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, false);
                    blck.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

                    Area[X, Y] = blck;//ㅁ
                    Area[X, Y + 1] = blck;//ㅁ


                    //Debug.Log(blck.transform.childCount);
                    //StartCoroutine(MoveBlock(blck));
                    break;

                case 2://ㅁㅁ
                       //ㅁ
                    blck = Instantiate(blockPrf[blockNum], new Vector3(startX, 0, 0), Quaternion.identity);//Make GameObject
                    blck.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, false);

                    Area[X, Y] = blck;//ㅁ
                    Area[X + 1, Y] = blck;//  ㅁ
                    Area[X, Y + 1] = blck;//
                                          //ㅁ

                    //Debug.Log(blck.transform.childCount);
                    //StartCoroutine(MoveBlock(blck));
                    break;
                case 3:
                    blck = Instantiate(blockPrf[blockNum], new Vector3(startX, 0, 0), Quaternion.identity);//Make GameObject
                    blck.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                    //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, false);

                    Area[X, Y] = blck;//ㅁ
                    Area[X + 1, Y] = blck;//  ㅁ
                    Area[X, Y + 1] = blck;//
                    Area[X - 1, Y] = blck;//  ㅁ
                    break;




            }
        }
    }
  
    public void ClearLine() {
        Debug.Log("ClearLineStart");
        int point = 0;
        if (Area[line, line] == wall)
        {
            
            for (int i = 0; i < ch; i++)
            {
                if (Area[line + i, line] == null)
                {
                    break;
                }
                Debug.Log("point++" + point);
                point++;
            }
            
            for (int i = 0; i < ch; i++)
            {
                if (Area[line , line+i] == null)
                {
                    break;
                }
                Debug.Log("point++" + point);
                point++;
            }
            for (int i = 0; i < ch; i++)
            {
                if (Area[line+ch-1, line+i] == null)
                {
                    break;
                }
                Debug.Log("point++" + point);
                point++;
            }
            for (int i = 0; i < ch; i++)
            {
                if (Area[line+i , line+ch-1 ] == null)
                {
                    break;
                }
                Debug.Log("point++" + point);
                point++;
            }
            Debug.Log("Point :" + point);
            //point 12
        }
        if (point != goalpoint)
        {
            //Debug.Log(point);
            if (line >1)
            {
                NextLine();
            }
            if (line == 1)
            {
                line = 7;
                ch = 3;
                goalpoint = 12;
            }
            
        }
        else {
            Debug.Log("Erase Line : " + line);
            EraseBlock();
            
        //    Debug.Log("clear line");
           
        }

        Debug.Log("ClearLineEnd");

    }
    public void NextLine()
    {
        line--;
        ch += 2;
        goalpoint += 8;
        ClearLine();
    }
    public void BlockManage()//childmove
    {
        switch (blockNum)
        {
            case 0:
                for (int k = 0; k < blck.transform.childCount; k++)
                {

                    blck.transform.GetChild(k).name = ("block" + X + "," + Y);
                    
               //     Debug.Log(blck.transform.GetChild(k));
                    block.Add(blck.transform.GetChild(k).gameObject);
                    blck.transform.GetChild(k).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                }

               /* for (int i = 0; i < block.Count; i++)
                {
                   Debug.Log(block[i]);
                }*/
                break;
            case 1:
                switch (rotateNum)
                {
                    case 0:
                        

                            blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                            block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + X + "," + (Y+1));
                       // blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        for (int i = 0; i < 2; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 1:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X+1) + "," + (Y));
                       // blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        for (int i = 0; i < 2; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 2:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + X + "," + (Y - 1));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        for (int i = 0; i < 2; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 3:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                       // blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X-1) + "," + (Y));
                       // blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        for (int i = 0; i < 2; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;

                }
                /*for (int i = 0; i < block.Count; i++)
                {
                    Debug.Log(block[i]);
                }*/
                break;
            case 2:
                switch (rotateNum)
                {
                    case 0:


                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + X + "," + (Y + 1));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X+1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        for (int i = 0; i < 3; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 1:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                       // blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X + 1) + "," + (Y));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + X + "," + (Y - 1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        for (int i = 0; i < 3; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 2:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + X + "," + (Y - 1));
                       // blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X-1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        for (int i = 0; i < 3; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 3:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X - 1) + "," + (Y));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + X + "," + (Y + 1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        for (int i = 0; i < 3; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;

                }
              /*  for (int i = 0; i < block.Count; i++)
                {
                    Debug.Log(block[i]);
                }*/
                break;
            case 3:
                switch (rotateNum)
                {
                    case 0:


                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X-1) + "," + (Y));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X) + "," + (Y+1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        blck.transform.GetChild(3).name = ("block" + (X + 1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(3));
                        block.Add(blck.transform.GetChild(3).gameObject);
                        for (int i = 0; i < 4; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 1:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X) + "," + (Y+1));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X+1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        blck.transform.GetChild(3).name = ("block" + (X) + "," + (Y-1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(3));
                        block.Add(blck.transform.GetChild(3).gameObject);
                        for (int i = 0; i < 4; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 2:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X + 1) + "," + (Y));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X) + "," + (Y - 1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        blck.transform.GetChild(3).name = ("block" + (X - 1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(3));
                        block.Add(blck.transform.GetChild(3).gameObject);
                        for (int i = 0; i < 4; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;
                    case 3:
                        blck.transform.GetChild(0).name = ("block" + X + "," + Y);
                        //blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(0));
                        block.Add(blck.transform.GetChild(0).gameObject);
                        blck.transform.GetChild(1).name = ("block" + (X) + "," + (Y-1));
                        //blck.transform.GetChild(1).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(1));
                        block.Add(blck.transform.GetChild(1).gameObject);
                        blck.transform.GetChild(2).name = ("block" + (X-1) + "," + (Y));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(2));
                        block.Add(blck.transform.GetChild(2).gameObject);
                        blck.transform.GetChild(3).name = ("block" + (X) + "," + (Y+1));
                        //blck.transform.GetChild(2).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        Debug.Log(blck.transform.GetChild(3));
                        block.Add(blck.transform.GetChild(3).gameObject);
                        for (int i = 0; i < 4; i++)
                        {
                            blck.transform.GetChild(0).SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                        }
                        break;

                }
              /*  for (int i = 0; i < block.Count; i++)
                {
                    Debug.Log(block[i]);
                }*/
                break;
        }
        Destroy(blck);
    }
    public void EraseBlock() {
        for (int i = line; i < (line + ch ); i++)
        {
            if (i == line || i == (line + ch-1))
            {
                for (int k = line; k < (line + ch ); k++)
                {
                    destroyBlock = tetArea.transform.Find("block" + k + "," + i).gameObject;
                    destroyBlockList.Add(destroyBlock);
                    iTween.ShakePosition(destroyBlock, new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
                    
                    //Debug.Log(k + "," + i);
                    Area[k, i] = null;
                    //Destroy(tetArea.transform.Find("block" + k + "," + i).gameObject);
                    score++;
                    text.text = "SCORE : " + score;
                }
            }
            else {
                destroyBlock = tetArea.transform.Find("block" + line + "," + i).gameObject;
                destroyBlockList.Add(destroyBlock);
                destroyBlock2 = tetArea.transform.Find("block" + (line + ch - 1) + "," + i).gameObject;
                destroyBlockList.Add(destroyBlock2);

                iTween.ShakePosition(destroyBlock, new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
                iTween.ShakePosition(destroyBlock2, new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
                
                //Debug.Log(line + "," + i);
                Area[line, i] = null;
                //Destroy(tetArea.transform.Find("block" + line + "," + i).gameObject);
                score++;
                text.text = "SCORE : " + score;
                //Debug.Log(line+ch-1 + "," + i);
                Area[line+ch-1, i] = null;
                //Destroy(tetArea.transform.Find("block" + (line+ch-1) + "," + i).gameObject);
                score++;
                text.text = "SCORE : " + score;
            }

        }
        Invoke("DestroyBlock", 0.7f);

        NextLine();
    
       
    }
    public void DestroyBlock() {
        for (int i = 0; i < destroyBlockList.Count; i++)
        {
            Destroy(destroyBlockList[i]);
        }
        destroyBlockList.Clear();
        Debug.Log(destroyBlockList.Count);
    }
    public void PauseGame()
    {
        popup.SetActive(true);
        blck.SetActive(false);
        Time.timeScale = 0;

    }
    public void ResumeGame()
    {
        popup.SetActive(false);
        exitPopup.SetActive(false);
        blck.SetActive(true);
        Time.timeScale = 1;
    }
    public void ResumeGame2()
    {
        blck.SetActive(true);
        exitPopup.SetActive(false);
        popup.SetActive(false);
        Time.timeScale = 1;
    }
    public void MoveDown()
    {
       
            if (Y == 15)
            {
                if (blockNum == 1 && rotateNum == 0)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 2 && rotateNum == 0)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 2 && rotateNum == 3)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }
                else if (blockNum == 3 && rotateNum != 2)
                {
                    Destroy(blck);
                    MakeBlock();
                    lifeOb[life].SetActive(false);
                    life++;
                }

            }
            else if (Y == 16)
            {
                Destroy(blck);
                MakeBlock();
                lifeOb[life].SetActive(false);
                life++;
            }
            if (life == 3)
            {

            if (PlayerPrefs.GetInt("HighScore", 0) < score)
            {
                record.gameObject.SetActive(true);
                record.text = "NEW RECORD" +
                    "\n" + score;
                PlayerPrefs.SetInt("HighScore", score);
            }
            Debug.Log("endgame");
            endPopup.SetActive(true);
            for (int i = 1; i < 16; i++)
            {
                for (int k = 1; k < 16; k++)
                {
                    Area[i, k] = null;
                }

            }
            for (int i = 0; i < block.Count; i++)
            {
                Destroy(block[i]);
            }
            Time.timeScale = 0;
            StopCoroutine(MoveBlock());
        }
            //Debug.Log(" Area[" + X.ToString() + "][" + Y.ToString() + "]" + Area[X, Y]);
            if (Area[X, Y + 1] == wall)
            {

                Debug.Log("Cant down1");
                switch (blockNum)
                {
                    case 0:
                        Area[X, Y] = wall;

                        break;
                    case 1:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                        }
                        else if (rotateNum == 3)
                        {

                            Area[X - 1, Y] = wall;
                            Area[X, Y] = wall;
                        }




                        break;
                    case 2:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;

                    case 3:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X - 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y + 1] = wall;
                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;


                }
                BlockManage();
                //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                ClearLine();
                MakeBlock();
            }
            else if (Area[X, Y + 1] == blck && Area[X, Y + 2] == wall)
            {
                Debug.Log("Cant down2");
                switch (blockNum)
                {
                    case 0:
                        Area[X, Y] = wall;

                        break;
                    case 1:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                        }
                        else if (rotateNum == 3)
                        {

                            Area[X - 1, Y] = wall;
                            Area[X, Y] = wall;
                        }




                        break;
                    case 2:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                    case 3:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X - 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y + 1] = wall;
                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                }
                BlockManage();
                //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                ClearLine();
                MakeBlock();

            }
            else if (Area[X + 1, Y] == blck && Area[X + 1, Y + 1] == wall)
            {
                Debug.Log("Cant down3");
                switch (blockNum)
                {
                    case 0:
                        Area[X, Y] = wall;

                        break;
                    case 1:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                        }
                        else if (rotateNum == 3)
                        {

                            Area[X - 1, Y] = wall;
                            Area[X, Y] = wall;
                        }




                        break;
                    case 2:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                    case 3:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X - 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y + 1] = wall;
                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                }
                BlockManage();
                //blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                ClearLine();
                MakeBlock();
            }
            else if (Area[X - 1, Y] == blck && Area[X - 1, Y + 1] == wall)
            {
                Debug.Log("Cant down4");
                switch (blockNum)
                {
                    case 0:
                        Area[X, Y] = wall;

                        break;
                    case 1:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                        }
                        else if (rotateNum == 3)
                        {

                            Area[X - 1, Y] = wall;
                            Area[X, Y] = wall;
                        }




                        break;
                    case 2:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;

                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                    case 3:
                        if (rotateNum == 0)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X - 1, Y] = wall;

                        }
                        else if (rotateNum == 1)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y + 1] = wall;
                        }
                        else if (rotateNum == 2)
                        {
                            Area[X, Y] = wall;
                            Area[X + 1, Y] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }
                        else if (rotateNum == 3)
                        {
                            Area[X, Y] = wall;
                            Area[X, Y + 1] = wall;
                            Area[X, Y - 1] = wall;
                            Area[X - 1, Y] = wall;
                        }

                        break;
                }
                BlockManage();
                // blck.transform.SetParent(GameObject.FindGameObjectWithTag("TetArea").transform, true);
                ClearLine();
                MakeBlock();
            }
            else
            {

                Down();
            if (Area[X, Y] == null)
            {
                Debug.Log("BlockNum : " + blockNum + "  RotNum : " + rotateNum + "  X: " + X + "  Y:" + Y);
                Debug.Log("nullerror");
            }
            else {
                Area[X, Y].transform.localPosition += new Vector3(0, -60, 0);
            }
                


            }





            
        

    }
    public void GoHome()
    {
        
        endPopup.SetActive(false);
        AutoFade.LoadLevel("Lobby", 1, 1, Color.black);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        
        endPopup.SetActive(false);
       
        Time.timeScale = 1;
        AutoFade.LoadLevel("Sqetris", 1, 1, Color.black);
    }
    
}
