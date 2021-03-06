﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridRoot : MonoBehaviour {
	public List<List<GameObject>> objectGrid = new List<List<GameObject>>();

	public int rows = 3;
	public int cols = 3;
    public float scalar;



    public float offset = 0.2f;

    public GameObject playField;

	// Use this for initialization
	void Start () 
	{
		//actualGenerateGrid (3, 3, "11111c11c", 3, 1, 1, 3);
        GameObject player = GameObject.FindGameObjectWithTag("Monkey");
        //player.GetComponent<Monkey>().setGrid(objectGrid);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void actualGenerateGrid( int rows, int cols, string input, int startRow, int startCol, int endRow, int endCol)
	{

        Debug.Log("ACTUAL");

        //resize terrain to fit new proposed grid
        playField.transform.localScale = new Vector3(cols, transform.localScale.y, rows)*scalar;

        //load gameobjects 
		GameObject oneBranch = Resources.Load ("oneBranch") as GameObject;
		GameObject twoBranch = Resources.Load ("twoBranch") as GameObject;
		GameObject fourBranch = Resources.Load ("fourBranch") as GameObject;
		GameObject Cliff = Resources.Load ("Cliff") as GameObject;
		GameObject blue = Resources.Load ("dummyBird") as GameObject;

        //set initial values
		Vector3 start = this.transform.position;
        Debug.Log(start);
		float rowOneZ;
		float colOneX;

		colOneX = start.x;
		rowOneZ = start.z;

		float currentZ = rowOneZ;
		float currentX;
		int stringIndex = 0;

		//for (int i = 0; i < rows; i++) {
		//	objectGrid.Add(new List<GameObject>());
		//	objectGrid.Add(new List<GameObject>());
			//objectGrid.Add(new List<GameObject>());
		//	objectGrid.Add(new List<GameObject>());
		//}


        //loop through rows and columns while instantiating and adding the specified gameobjects to objectGrid 
		for (int r = 0; r < rows; r++) 
		{
			objectGrid.Add(new List<GameObject>());
			currentX = colOneX;
			for (int c = 0; c < cols; c++) {
				if (input [stringIndex] == '1') {
					GameObject current = Instantiate (oneBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					objectGrid [r].Add (current);
				} else if (input [stringIndex] == '2') {
					GameObject current = Instantiate (twoBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					objectGrid [r].Add (current);
				} else if (input [stringIndex] == '4') {
					GameObject current = Instantiate (fourBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					objectGrid [r].Add (current);
				} else if (input [stringIndex] == 'c') {
					GameObject current = Instantiate (Cliff, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					objectGrid [r].Add (current);
				}
					else if (input [stringIndex] == 'b') {
					//GameObject current = Instantiate (oneBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					GameObject current2 = Instantiate (blue, new Vector3 (currentX, start.y + 2.5f, currentZ), Quaternion.identity) as GameObject;
					//objectGrid [r].Add (current);
					objectGrid [r].Add (current2);
					current2.transform.GetChild(0).GetComponent<BlueJay> ().latentMoveStock = 4;
				}
					else if (input [stringIndex] == 'n') {
					//GameObject current = Instantiate (oneBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
					GameObject current2 = Instantiate (blue, new Vector3 (currentX, start.y + 2.5f, currentZ), Quaternion.identity) as GameObject;
					//objectGrid [r].Add (current);
					objectGrid [r].Add (current2);
					current2.transform.GetChild(0).GetComponent<BlueJay> ().latentMoveStock = 2;
				}

				currentX += offset;
				stringIndex++;
			}
			currentZ -= offset;
		}


        //specify starting location
		if (objectGrid [startRow - 1] [startCol - 1].name.Contains ("oneB")) {
			objectGrid [startRow - 1] [startCol - 1].GetComponent<oneBranch> ().start = true;
		} else if (objectGrid [startRow - 1] [startCol - 1].name.Contains ("twoB")) {
			objectGrid [startRow - 1] [startCol - 1].GetComponent<twoBranch> ().start = true;
		}

        //specify ending location
		if (objectGrid [endRow - 1] [endCol - 1].name.Contains ("oneB")) {
			objectGrid [endRow - 1] [endCol - 1].GetComponent<oneBranch> ().end = true;
			objectGrid [endRow - 1] [endCol - 1].GetComponent<oneBranch> ().myRow = endRow;
			objectGrid [endRow - 1] [endCol - 1].GetComponent<oneBranch> ().myCol = endCol;
		} else if (objectGrid [endRow - 1] [endCol - 1].name.Contains ("twoB")) {
			objectGrid [endRow - 1] [endCol - 1].GetComponent<twoBranch> ().start = true;
			objectGrid [endRow - 1] [endCol - 1].GetComponent<oneBranch> ().myRow = endRow;
			objectGrid [endRow - 1] [endCol - 1].GetComponent<oneBranch> ().myCol = endCol;
		}

        
        //set key player values
        GameObject player = GameObject.FindGameObjectWithTag ("Monkey");
		//Debug.Log (objectGrid);
		player.GetComponent<Monkey> ().setGrid (objectGrid);
        player.GetComponent<Monkey>().setCurrentPosition(startRow,startCol);
        player.GetComponent<Monkey> ().bananaGoal = 0;

	}

   public void generateTransposedGrid(int rows, int cols, string input, int startRow, int startCol, int endRow, int endCol)
    {
        //generate grid modified to transpose the original input string
        playField.transform.localScale = new Vector3(rows, transform.localScale.y, cols) * scalar;

        GameObject oneBranch = Resources.Load("oneBranch") as GameObject;
        GameObject twoBranch = Resources.Load("twoBranch") as GameObject;
        GameObject fourBranch = Resources.Load("fourBranch") as GameObject;
        GameObject Cliff = Resources.Load("Cliff") as GameObject;
        GameObject blue = Resources.Load("dummyBird") as GameObject;

        Vector3 start = this.transform.position;
        Debug.Log(start);
        float rowOneZ;
        float colOneX;

        colOneX = start.x;
        rowOneZ = start.z;

        float currentZ;
        float currentX = colOneX;
        int stringIndex = 0;

        //for (int i = 0; i < rows; i++) {
        //	objectGrid.Add(new List<GameObject>());
        //	objectGrid.Add(new List<GameObject>());
        //objectGrid.Add(new List<GameObject>());
        //	objectGrid.Add(new List<GameObject>());
        //}


        for (int r = 0; r < rows; r++)
        {
            objectGrid.Add(new List<GameObject>());
            currentZ = rowOneZ;
            
            for (int c = 0; c < cols; c++)
            {
                Debug.Log("row");
                if (input[stringIndex] == '1')
                {
                    Debug.Log("column");
                    GameObject current = Instantiate(oneBranch, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    objectGrid[r].Add(current);
                }
                else if (input[stringIndex] == '2')
                {
                    GameObject current = Instantiate(twoBranch, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    objectGrid[r].Add(current);
                }
                else if (input[stringIndex] == '4')
                {
                    GameObject current = Instantiate(fourBranch, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    objectGrid[r].Add(current);
                }
                else if (input[stringIndex] == 'c')
                {
                    GameObject current = Instantiate(Cliff, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    objectGrid[r].Add(current);
                }
                else if (input[stringIndex] == 'b')
                {
                    //GameObject current = Instantiate (oneBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    GameObject current2 = Instantiate(blue, new Vector3(currentX, start.y + 2.5f, currentZ), Quaternion.identity) as GameObject;
                    //objectGrid [r].Add (current);
                    objectGrid[r].Add(current2);
                    current2.transform.GetChild(0).GetComponent<BlueJay>().latentMoveStock = 4;
                }
                else if (input[stringIndex] == 'n')
                {
                    //GameObject current = Instantiate (oneBranch, new Vector3 (currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    GameObject current2 = Instantiate(blue, new Vector3(currentX, start.y + 2.5f, currentZ), Quaternion.identity) as GameObject;
                    //objectGrid [r].Add (current);
                    objectGrid[r].Add(current2);
                    current2.transform.GetChild(0).GetComponent<BlueJay>().latentMoveStock = 2;
                }

                currentZ -= offset;
                stringIndex++;
            }
            currentX += offset;
        }

        if (objectGrid[startRow - 1][startCol - 1].name.Contains("oneB"))
        {
            objectGrid[startRow - 1][startCol - 1].GetComponent<oneBranch>().start = true;
        }
        else if (objectGrid[startRow - 1][startCol - 1].name.Contains("twoB"))
        {
            objectGrid[startRow - 1][startCol - 1].GetComponent<twoBranch>().start = true;
        }

        if (objectGrid[endRow - 1][endCol - 1].name.Contains("oneB"))
        {
            objectGrid[endRow - 1][endCol - 1].GetComponent<oneBranch>().end = true;
            objectGrid[endRow - 1][endCol - 1].GetComponent<oneBranch>().myRow = endCol;
            objectGrid[endRow - 1][endCol - 1].GetComponent<oneBranch>().myCol = endRow;
        }
        else if (objectGrid[endRow - 1][endCol - 1].name.Contains("twoB"))
        {
            objectGrid[endRow - 1][endCol - 1].GetComponent<twoBranch>().end = true;
            objectGrid[endRow - 1][endCol - 1].GetComponent<oneBranch>().myRow = endCol;
            objectGrid[endRow - 1][endCol - 1].GetComponent<oneBranch>().myCol = endRow;
        }



        List<List<GameObject>> tGrid = new List<List<GameObject>>();
        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                tGrid.Add(new List<GameObject>());
                tGrid[c].Add(objectGrid[r][c]);
            }

        }
        string s = "";
        foreach(List<GameObject> l in tGrid)
        {
            foreach(GameObject g in l)
            {
                s += g.name;
            }
            s += "\n";
        }
        Debug.Log(s);



        GameObject player = GameObject.FindGameObjectWithTag("Monkey");
        //Debug.Log (objectGrid);
        player.GetComponent<Monkey>().setGrid(tGrid);
        player.GetComponent<Monkey>().setCurrentPosition(startCol, startRow);
        player.GetComponent<Monkey>().bananaGoal = 0;

    }
}
