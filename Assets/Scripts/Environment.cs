using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour {

    private Transform theEnvironment;
    public static Environment instance = null;
    private bool doingSetup = false;
    public GameObject theFloor;
    public GameObject theWall;
    private GameObject LevelImage;
    private Text LevelText;
    private bool playerMelee = false;

    public GameObject theDoor;

    //Game now restarts after 5 seconds
    public void gameOver()
    {
        doingSetup = true;
        LevelText.color = Color.red;
        LevelText.text = "SUBJECT BADDOG HAS BEEN SUBDUED.\nPREP THE MORGUE FOR AUTOPSY.";
        LevelImage.SetActive(true);
        Invoke("RestartGame", 5.0f);
    }

    //Method for restarting game
    void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Destroy(gameObject);
        SceneManager.LoadScene(scene.name);
    }

    public void doesPlayerMelee()
    {
        playerMelee = true;
        Invoke("playerMeleeOver", 1f);
    }

    private void playerMeleeOver()
    {
        playerMelee = false;
    }

    public bool getPlayerMelee()
    {
        return playerMelee;
    }

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        theEnvironment = new GameObject("Environment").transform;

        Room1();
        Hall1(6, 22);
        Room2(0, 64);
        Hall1(6, 86);
        SpecialHall(-30, 128);
        Room3(-52, 122);
        Room4(52, 122);
        Hall1(-46, 144);
        Hall1(58, 144);
        Room5(-52, 186);
        Hall2(-30, 192);
        Room6(12, 186);
        Room7(52, 186);
        Hall2(74, 192);
        Room6(116, 186);
        Hall2(-94, 192);
        Room8(-116, 186);
        Hall1(-110, 208);
        Hall2(-158, 192);
        Room9(-180, 186);
        Room7(-116, 250);
        Hall2(-94, 256);
        Room8(-52, 250);
        SpecialHall2(-30, 256);
        Room10(52, 250);
        Hall1(58, 208);
        Hall1(-110, 272);
        Room11(-116, 314);
        Hall1(-46, 272);
        BossRoom(-77, 314);
    }

    void Room1()
    {
        for (int i = -1; i < 21; i++)
            for (int j = -1; j < 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == -1 || i == 20 || j == -1 || j == 20)
                    toInstantiate = theWall;
                if (i == 20 && j == 10)
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room2(int bottom, int left)
    {
        for (int i = left-1; i < left+21; i++)
            for (int j = bottom-1; j < bottom+21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left-1) || i == (left+20) || j == (bottom-1) || j == (bottom+20))
                    toInstantiate = theWall;
                if ((i == (left+20) && j == (bottom+10)) || (i == (left-1) && j == (bottom+10)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room3(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left + 20) && j == (bottom + 10)) || (i == (left +10) && j == (bottom + 20)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room4(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left + 20) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room5(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left - 1) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1)) || (i == (left + 10) && j == (bottom + 20)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room6(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if (i == (left + 10) && j == (bottom - 1))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room7(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left + 10) && j == (bottom +20)) || (i == (left - 1) && j == (bottom + 10)) || (i == (left + 20) && j == (bottom + 10)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room8(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left +20) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1)) || (i == (left + 10) && j == (bottom + 20)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room9(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if (i == (left + 10) && j == (bottom + 20))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room10(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left -1) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1))) 
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Room11(int bottom, int left)
    {
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if (i == (left - 1) && j == (bottom + 10))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void BossRoom(int bottom, int left)
    {
        for (int i = left - 1; i < left + 71; i++)
            for (int j = bottom - 1; j < bottom + 71; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 70) || j == (bottom - 1) || j == (bottom + 70))
                    toInstantiate = theWall;
                if (i == (left - 1) && j == (bottom + 35))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Hall1(int bottom, int left)
    {
        for(int i = left - 1; i < left + 41; i++)
            for(int j = bottom - 1; j < bottom + 9; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left-1) || i == (left+40) || j == (bottom-1) || j == (bottom+8))
                    toInstantiate = theWall;
                if ((i == (left+40) && j == (bottom+4)) || (i == (left-1) && j == (bottom+4)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Hall2(int bottom, int left)
    {
        for (int i = left-1; i < left+9; i++)
            for (int j = bottom-1; j < bottom+41; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left-1) || i == (left+8) || j == (bottom-1) || j == (bottom+40))
                    toInstantiate = theWall;
                if ((i == (left+4) && j == (bottom-1)) || (i == (left+4) && j == (bottom+40)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void SpecialHall(int bottom, int left)
    {
        for(int i = left-1; i < left +9; i++)
            for(int j = bottom-1; j < bottom+81; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 8) || j == (bottom - 1) || j == (bottom + 80))
                    toInstantiate = theWall;
                if ((i == (left + 4) && j == (bottom - 1)) || (i == (left + 4) && j == (bottom + 80)) || (i == (left - 1) && j == (bottom + 40)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void SpecialHall2(int bottom, int left)
    {
        for (int i = left - 1; i < left + 9; i++)
            for (int j = bottom - 1; j < bottom + 81; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 8) || j == (bottom - 1) || j == (bottom + 80))
                    toInstantiate = theWall;
                if ((i == (left + 4) && j == (bottom - 1)) || (i == (left + 4) && j == (bottom + 80)))
                    toInstantiate = theDoor;
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
        MusicAndSounds.instance.musicSource.Play();
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
