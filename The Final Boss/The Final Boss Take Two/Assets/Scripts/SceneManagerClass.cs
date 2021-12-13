using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerClass : MonoBehaviour
{
    public List<int> presetHSList;
    public List<int> highScores;
    public List<bool> PlayerHeld;
    public bool score1, score2, score3, killTheBoss;
    private WouldMyProfessorFindThisFunny wmp;
    public GameObject self, pausemenu;
    private int tutorialSet, TittleSet=0;
    public PlayerMovement pm;
    public Text PauseText;
    private bool paused=false;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            LoadOnOpen();
        }
        catch (System.Exception)
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                highScores[i] = presetHSList[i];
                PlayerHeld[i] = false;
            }
            throw;
        }
        for (int i = 0; i < PlayerHeld.Count; i++)
        {
            if (PlayerHeld[i]) 
            {
                if (i >= 0 && !score1 )
                {
                    score1 = true;
                }
                else if (i >= 1 &&!score2)
                {
                    score2 = true;
                }
                else if (i == 2 && !score3)
                {
                    score3 = true;
                }
            }
        }

        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
        /*score1 = PlayerHeld[0];
        score2 = PlayerHeld[1];
        score3 = PlayerHeld[2];*/
        if (score1 && score2 && score3)
        {
            killTheBoss = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(0)))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
        if (Time.timeScale==0&&!pm.dead)
        {
            if (!paused)
            {
                paused = true;
            }
            pausemenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                returnToTittle();
            }
        }
        if (Time.timeScale == 1 && paused)
        {
            paused = false;
            pausemenu.SetActive(false);
        }
    }
    public void ResetSaveData()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            highScores[i] = presetHSList[i];
            PlayerHeld[i] = false;
        }
        Save();
        score1 = false;
        score2 = false;
        score3 = false;
    }
    public void skipTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void loadBasicMovementTutorial()
    {
        tutorialSet = 0;
        SceneManager.LoadScene(2);
    }
    public void loadBasicAndSpecialAbilites()
    {
        tutorialSet = 1;
        SceneManager.LoadScene(2);
    }
    public void loadBossPhaseTutorial()
    {
        tutorialSet = 2;
        SceneManager.LoadScene(2);
    }
    public void loadSandbox()
    {
        tutorialSet = 3;
        SceneManager.LoadScene(2);
    }
    public void returnToTittle()
    {
        TittleSet = 0;
        SceneManager.LoadScene(0);
        Destroy(self);
    }
    public void returnToBasicTutorialScreen()
    {
        TittleSet = 2;
        SceneManager.LoadScene(0);
        Destroy(self);
    }
    public void returnToAdvancedTutorialScreen()
    {
        TittleSet = 3;
        SceneManager.LoadScene(0);
        Destroy(self);

    }
    public void tutorialLoaded()
    {
        switch (tutorialSet)
        {
            case 0:
                wmp = GameObject.Find("EventSystem").GetComponent<ALocalForTutorials>().wmp;
                wmp.tutorialLevel = 0;
                wmp.readyToStart = true;
                break;
            case 1:
                wmp = GameObject.Find("EventSystem").GetComponent<ALocalForTutorials>().wmp;
                wmp.tutorialLevel = 1;
                wmp.readyToStart = true;
                break;
            case 2:
                wmp = GameObject.Find("EventSystem").GetComponent<ALocalForTutorials>().wmp;
                wmp.tutorialLevel = 2;
                wmp.readyToStart = true;
                break;
            case 3:
                wmp = GameObject.Find("EventSystem").GetComponent<ALocalForTutorials>().wmp;
                wmp.tutorialLevel = 3;
                wmp.readyToStart = true;
                break;
        }
    }
    public void TittleLoaded()
    {
        switch (TittleSet)
        {
            case 0:
                //nothing because this is just the default position
                break;
            case 2:
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().tittle.SetActive(false);
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().tuturialBasic.SetActive(true);
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().curse.newMenu();

                break;
            case 3:
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().tittle.SetActive(false);
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().tutorialAdvanced.SetActive(true);
                GameObject.Find("EventSystem").GetComponent<TittleScreenScript>().curse.newMenu();
                break;

        }
        Time.timeScale = 1;
        PauseText.enabled = true;
        pausemenu.SetActive(false);
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Save()
    {
        PlayerPrefs.SetInt("HS1", highScores[0]);
        PlayerPrefs.SetInt("HS2", highScores[1]);
        PlayerPrefs.SetInt("HS3", highScores[2]);
        PlayerPrefs.SetString("PH1", PlayerHeld[0].ToString());
        PlayerPrefs.SetString("PH2", PlayerHeld[1].ToString());
        PlayerPrefs.SetString("PH3", PlayerHeld[2].ToString());
        PlayerPrefs.Save();
    }
    public void LoadOnOpen()
    {
        bool truth = true;
        highScores[0] = PlayerPrefs.GetInt("HS1");
        highScores[1] = PlayerPrefs.GetInt("HS2");
        highScores[2] = PlayerPrefs.GetInt("HS3");
        if(truth.ToString()== PlayerPrefs.GetString("PH1"))
        {
            PlayerHeld[0] = true;
        }
        else
        {
            PlayerHeld[0] = false;
        }
        if (truth.ToString() == PlayerPrefs.GetString("PH2"))
        {
            PlayerHeld[1] = true;
        }
        else
        {
            PlayerHeld[1] = false;
        }
        if (truth.ToString() == PlayerPrefs.GetString("PH3"))
        {
            PlayerHeld[2] = true;
        }
        else
        {
            PlayerHeld[2] = false;
        }
    }
}
