using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleScreenScript : MonoBehaviour
{
    public GameObject tittle, scoreboard, tutorialStart, tuturialBasic, tutorialAdvanced;//different pages
    private SceneManagerClass smc;
    public cursor curse;
    public Text Explanation;
    public bool unlock1, unlock2, finalUnlock;
    public Text unlock1txt, unlock2txt, unlock3txt;
    public Button unlock1btn, unlock2btn, unlock3btn, WhoKnows, WhoActuallyKnowstho;
    // Start is called before the first frame update
    void Start()
    {
        smc = GameObject.Find("SceneManager").GetComponent<SceneManagerClass>();
        smc.TittleLoaded();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
