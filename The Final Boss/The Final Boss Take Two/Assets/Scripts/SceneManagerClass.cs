using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerClass : MonoBehaviour
{
    public bool score1, score2, score3, killTheBoss;
    private WouldMyProfessorFindThisFunny wmp;
    public GameObject self, pausemenu;
    private int tutorialSet, TittleSet=0;
    public PlayerMovement pm;
    private bool paused=false;
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
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
        pausemenu.SetActive(false);
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
