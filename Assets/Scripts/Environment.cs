using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour {

    private Transform theEnvironment;
    public static Environment instance = null;
    public int rows = 16;
    public int columns = 64;
    private bool doingSetup = false;
    public GameObject theFloor;
    public GameObject theWall;
    private GameObject LevelImage;
    private Text LevelText;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        theEnvironment = new GameObject("Environment").transform;
        for(int i = -1; i < columns + 1; i++)
            for(int j = -1; j < rows + 1; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == -1 || i == columns || j == -1 || j == rows || (((i % 8) == 0) && ((j % 4) == 0)))
                    toInstantiate = theWall;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void StartScene()
    {
        doingSetup = true;
        LevelImage = GameObject.Find("LevelImage");
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        LevelText.text = "I awoke surrounded by homunculi bent on my destruction...";
        LevelImage.SetActive(true);
        Invoke("HideLevelImage", 2f);
    }

    void HideLevelImage()
    {
        LevelImage.SetActive(false);
        doingSetup = false;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartScene();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public bool isDoingSetup()
    {
        return doingSetup;
    }
}
