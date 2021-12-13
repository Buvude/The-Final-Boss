using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleScreenScript : MonoBehaviour
{
    public Text Score1, Score2, Score3;
    public GameObject tittle, scoreboard, tutorialStart, tuturialBasic, tutorialAdvanced;//different pages
    private SceneManagerClass smc;
    public cursor curse;
    public Text Explanation;
    public bool unlock1, unlock2, finalUnlock;
    public Text unlock1txt, unlock2txt, unlock3txt;
    public Button unlock1btn, unlock2btn, unlock3btn, WhoKnows, WhoActuallyKnowstho, reallyWhoKnows, reallyIDontEvenKnow;
    // Start is called before the first frame update
    void Start()
    {
        
        smc = GameObject.Find("SceneManager").GetComponent<SceneManagerClass>();
        smc.TittleLoaded();
    }

    // Update is called once per frame
    void Update()
    {
        unlock1 = smc.score1;
        unlock2 = smc.score2;
        finalUnlock = smc.score3;
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Save Data Deleted");
            smc.ResetSaveData();
        }
        if (smc.PlayerHeld[0])
        {
            Score1.text = "1st) Player held: " + smc.highScores[0];
        }
        else
        {
            Score1.text = "1st) Boss held: " + smc.highScores[0];
        }
        if (smc.PlayerHeld[1])
        {
            Score2.text = "2nd) Player held: " + smc.highScores[1];
        }
        else
        {
            Score2.text = "2nd) Boss held: " + smc.highScores[1];
        }
        if (smc.PlayerHeld[2])
        {
            Score3.text = "3rd) Player held: " + smc.highScores[2];
        }
        else
        {
            Score3.text = "3rd) Boss held: " + smc.highScores[2];
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (unlock1)
        {
            unlock1txt.text = "Lore 1/3 unlocked\n" +
                "Basic Lore outline is active";
            WhoKnows.gameObject.SetActive(false);
            WhoActuallyKnowstho.gameObject.SetActive(true);
        }
        if (unlock2)
        {
            unlock2btn.interactable = true;
            unlock2btn.GetComponent<BoxCollider2D>().enabled = true;
            unlock2txt.text = "Lore 2/3 unlocked\n" +
                "??? is now active";
        }
        if (finalUnlock)
        {
            unlock3txt.text = "Lore 3/3 unlocked\n" +
                "Lore tutorial now active";
            reallyWhoKnows.gameObject.SetActive(true);
            reallyIDontEvenKnow.gameObject.SetActive(false);
        }
    }
}
