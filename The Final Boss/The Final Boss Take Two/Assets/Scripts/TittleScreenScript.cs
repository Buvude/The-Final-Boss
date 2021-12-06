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
    }
}
