using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    //creating array to store color values.
    public Color[] cubeColor;
    public int points;

    public void Start()
    {
        //giving random color to the cube from the list of color array.
        GetComponent<Renderer>().material.color = cubeColor[Random.Range(0,cubeColor.Length)];

        //setting hit points according to the cube's color.
        if(GetComponent<Renderer>().material.color == cubeColor[0])
        {
            points = 15;
            this.gameObject.tag = "Red";
        }
        else
        {
            points = 20;
            this.gameObject.tag = "Blue";
        }
    }
}
