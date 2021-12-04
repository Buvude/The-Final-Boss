using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerClass : MonoBehaviour
{
    
    private WouldMyProfessorFindThisFunny wmp;
    public GameObject self;
    private int tutorialSet, TittleSet=0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void returnToBasicTutorialScreen()
    {
        TittleSet = 2;
        SceneManager.LoadScene(0);
    }
    public void returnToAdvancedTutorialScreen()
    {
        TittleSet = 3;
        SceneManager.LoadScene(0);
       
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
    }
}
