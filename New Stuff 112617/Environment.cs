using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour
{

    private string[] introLines = { "I meant only to create a basic potion. That's how it began.", "I knocked over a vial on the shelf, one meant to be added to the potion much later than it was. The results were catastrophic.", "I awoke surrounded by homunculi bent on my destruction...", "(Use W to move up, A to move left, S to move down, and D to move right. Use the mouse to aim your spells and the left mouse button to fire them.)", "(The red bar represents your health points. The blue bar represents your mana points. The yellow bar represents your experience points.)", "(When your mana points run out, you will have to wait until they recharge fully to cast spells again.)" };
    private Transform theEnvironment;
    public static Environment instance = null;
    public int rows = 16;
    public int columns = 64;
    private bool doingSetup = false;
    public GameObject theFloor;
    public GameObject theWall;
    private GameObject LevelImage;
    private Text LevelText;
    private float currentMp;
    private int whichSkill;
    private bool manaCharge = false;
    private float maxMp;
    public GameObject theDoor;
    public float RegenRate = .5f;
    public Slider expSlider;
    private float currentExp;
    private float toNextLevel;
    private bool levelUpReady = false;
    public Button nextButton;
    public Button restartButton;
    private int textIndex;

    // Use this for initialization
    void Awake()
    {
        nextButton.onClick.AddListener(delegate { NextLine(); });
        restartButton.onClick.AddListener(delegate { RestartGame(); });
        whichSkill = 1;
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        theEnvironment = new GameObject("Environment").transform;
        currentExp = 0f;
        toNextLevel = 15f;
        expSlider.value = currentExp / toNextLevel;
        
        /*
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
        BossRoom(-77, 314);*/
        
    }

    void Update()
    {
        Debug.Log("currentMp :" + currentMp);
        if (currentMp <= 0)
        {
            manaCharge = true;

        }

        if (currentMp != maxMp && manaCharge == true)
        {
            StartCoroutine(RegainMpOverTime());
        }

        checkExpLevel();
    }

    void checkExpLevel()
    {
        if(currentExp >= toNextLevel)
        {
            levelUpReady = true;
            currentExp -= toNextLevel;
            toNextLevel += 5;
        }
        expSlider.value = currentExp / toNextLevel;
    }

    public void giveEXP(int numPoints)
    {
        currentExp += (float)numPoints;
    }

    public void setLevelUpReady(bool theValue)
    {
        levelUpReady = theValue;
    }

    public bool getLevelUpReady()
    {
        return levelUpReady;       
    }

    void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Destroy(gameObject);
        SceneManager.LoadScene(scene.name);
    }

    private IEnumerator RegainMpOverTime()
    {
        manaCharge = true;
        while (currentMp < maxMp)
        {
            AdjustCurrentMp(0.1f);
            yield return new WaitForSeconds(RegenRate);
        }
        manaCharge = false;

    }
    public void AdjustCurrentMp(float adj)
    {
        currentMp += adj;

        
        if (currentMp < 0)
            currentMp = 0;

        if (currentMp > maxMp)
            currentMp = maxMp;

        if (maxMp < 1)
            maxMp = 1;
        


    }

    public bool getmanaChargeState()
    {
        return manaCharge;
    }


    public void gameOver()
    {
        MusicAndSounds.instance.musicSource.Stop();
        doingSetup = true;
        LevelText.text = "Game over.";
        restartButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        LevelImage.SetActive(true);
    }

    void StartScene()
    {
        textIndex = 0;
        restartButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        doingSetup = true;
        LevelImage = GameObject.Find("LevelImage");
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        LevelText.text = introLines[textIndex];
        LevelImage.SetActive(true);
    }

    private void NextLine()
    {
        if (textIndex < introLines.Length - 1)
        {
            textIndex++;
            LevelText.text = introLines[textIndex];
        }
        else
        {
            LevelImage.SetActive(false);
            Invoke("HideLevelImage", 0.5f);
        }
    }

    public int getWhichSkill()
    {
        return whichSkill;
    }

    public void mpOrbPickup(float mp)
    {
        currentMp += mp;
    }

    public float getCurrentMp()
    {
        return currentMp;
    }
    public void setIntCurrentMp(float mp)
    {
        maxMp = mp;
        currentMp = mp;
    }

    public void setCurrentMpAfterSkill(float usedMp)
    {
        currentMp = currentMp - usedMp;
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
        for (int i = left - 1; i < left + 21; i++)
            for (int j = bottom - 1; j < bottom + 21; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 20) || j == (bottom - 1) || j == (bottom + 20))
                    toInstantiate = theWall;
                if ((i == (left + 20) && j == (bottom + 10)) || (i == (left - 1) && j == (bottom + 10)))
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
                if ((i == (left + 20) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom + 20)))
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
                if ((i == (left + 10) && j == (bottom + 20)) || (i == (left - 1) && j == (bottom + 10)) || (i == (left + 20) && j == (bottom + 10)))
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
                if ((i == (left + 20) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1)) || (i == (left + 10) && j == (bottom + 20)))
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
                if ((i == (left - 1) && j == (bottom + 10)) || (i == (left + 10) && j == (bottom - 1)))
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
        for (int i = left - 1; i < left + 41; i++)
            for (int j = bottom - 1; j < bottom + 9; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 40) || j == (bottom - 1) || j == (bottom + 8))
                    toInstantiate = theWall;
                if ((i == (left + 40) && j == (bottom + 4)) || (i == (left - 1) && j == (bottom + 4)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void Hall2(int bottom, int left)
    {
        for (int i = left - 1; i < left + 9; i++)
            for (int j = bottom - 1; j < bottom + 41; j++)
            {
                GameObject toInstantiate = theFloor;
                if (i == (left - 1) || i == (left + 8) || j == (bottom - 1) || j == (bottom + 40))
                    toInstantiate = theWall;
                if ((i == (left + 4) && j == (bottom - 1)) || (i == (left + 4) && j == (bottom + 40)))
                    toInstantiate = theDoor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(theEnvironment);
            }
    }

    void SpecialHall(int bottom, int left)
    {
        for (int i = left - 1; i < left + 9; i++)
            for (int j = bottom - 1; j < bottom + 81; j++)
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
}
