using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

    private Transform theEnvironment;
    public int rows = 8;
    public int columns = 8;
    public GameObject theFloor;
    public GameObject theWall;

    // Use this for initialization
    void Awake()
    {
        theEnvironment = new GameObject("Environment").transform;
        for(int i = -1; i < columns + 1; i++)
            for(int j = -1; j < rows + 1; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == -1 || i == columns || j == -1 || j == rows)
                    toInstantiate = theWall;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }
}
