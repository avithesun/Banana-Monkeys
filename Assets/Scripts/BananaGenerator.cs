﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaGenerator : MonoBehaviour {
    public GameObject banana;
    public string[] bananaString;
    public float bananaHeight;
    public List<GameObject> bananaList;

    private int m_rows;
    private int m_cols;
    private float m_offset;

	// Use this for initialization
	void Start () {
        m_rows = 2*GetComponent<gridRoot>().rows - 1;
        m_cols = GetComponent<gridRoot>().cols;
        m_offset = GetComponent<gridRoot>().offset;

        //There are twice the number of banana rows as there are tile rows.
        if (m_rows != bananaString.Length)
        {
            Debug.LogWarning("Warning! Banana string may not have the right number of rows (should have " + m_rows + "). Resizing now.");
            System.Array.Resize(ref bananaString, m_rows);
        }

        string s = "";
        for (int i = 0; i < bananaString.Length; i++)
        {
            s += bananaString[i];
        }
        //Debug.Log(s);
        //GenerateBananas(m_rows, m_cols, s);
    }

    public void RespawnBananas(string s)
        //respawn bananas of level - used for defeat
    {
        m_rows = 2 * GetComponent<gridRoot>().rows - 1;
        m_cols = GetComponent<gridRoot>().cols;
        m_offset = GetComponent<gridRoot>().offset;
        //Debug.Log(m_rows + " " + m_cols + "BAN");
        GenerateBananas(m_rows, m_cols, s);
    }

    public void GenerateBananas(int rows, int cols, string input)
    //detect locations based on grid information and then instantiate bananas based on input string
    {
        Debug.Log("GEN " + rows + " " + cols);
        float rowOneZ;
        float colOneX;
        int bananaCount = 0;
        Vector3 start = new Vector3(transform.position.x, transform.position.y + bananaHeight,transform.position.z);

  
        colOneX = start.x + (m_offset / 2);
        rowOneZ = start.z;

        float currentZ = rowOneZ;
        float currentX;
        int stringIndex = 0;

        bool isOddRow = true;

        for (int r = 0; r < 2 * rows; r++)
        {
            int numOfCols;
            if (isOddRow)
            {
                numOfCols = cols - 1;
                currentX = colOneX;

            }
            else
            {
                numOfCols = cols;
                currentX = colOneX - m_offset/2;

            }

            for (int c = 0; c < numOfCols; c++)
            {
                //Debug.Log(r + " " + c);
				if (stringIndex < input.Length && (input[stringIndex] == 'B' || input[stringIndex] == 'b'))
                {
                    //Debug.Log("BANNA");
                    GameObject current = Instantiate(banana, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    bananaList.Add(current);
                    bananaCount++;
                }

                currentX += m_offset;
                stringIndex++;
            }
            isOddRow = !isOddRow;
            currentZ -= m_offset / 2;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Monkey");
        player.GetComponent<Monkey>().bananaGoal = bananaCount;
        //Debug.Log("end");
    }



    public void generateTBananas(int rows, int cols, string input)
        //generate bananas for transposed grid
    {
        
        Debug.Log("GEN " + rows + " " + cols);
        float rowOneZ;
        float colOneX;
        int bananaCount = 0;
        Vector3 start = new Vector3(transform.position.x, transform.position.y + bananaHeight, transform.position.z);


        colOneX = start.x;
        rowOneZ = start.z - (m_offset / 2);

        float currentZ;
        float currentX = colOneX;
        int stringIndex = 0;

        bool isOddRow = true;

        for (int r = 0; r < 2 * rows; r++)
        {
            int numOfCols;
            if (isOddRow)
            {
                numOfCols = cols - 1;
                currentZ = rowOneZ;

            }
            else
            {
                numOfCols = cols;
                currentZ = rowOneZ + m_offset / 2;

            }

            for (int c = 0; c < numOfCols; c++)
            {
                //Debug.Log(r + " " + c);
                if (stringIndex < input.Length && (input[stringIndex] == 'B' || input[stringIndex] == 'b'))
                {
                    //Debug.Log("BANNA");
                    GameObject current = Instantiate(banana, new Vector3(currentX, start.y, currentZ), Quaternion.identity) as GameObject;
                    bananaList.Add(current);
                    bananaCount++;
                }

                currentZ -= m_offset;
                stringIndex++;
            }
            isOddRow = !isOddRow;
            currentX += m_offset / 2;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Monkey");
        player.GetComponent<Monkey>().bananaGoal = bananaCount;
        //Debug.Log("end");
    }
}
