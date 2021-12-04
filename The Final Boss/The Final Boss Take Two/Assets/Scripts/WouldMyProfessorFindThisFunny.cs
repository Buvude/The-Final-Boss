using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WouldMyProfessorFindThisFunny : MonoBehaviour
{
    public bool basicShot, bigShot, Laser, Vortex, safeZone, pS, pBS, pL, pV, pSZ, readyToStart=false, started=false;
    public bool sandbox;
    public BossBehavior bb;
    public int tutorialLevel;
    public PlayerMovement pm;
    private Text onScreenText;
    private bool task1 = false, task2 = false, task3 = false, task4 = false, task5 = false, task6 = false, task7 = false, task8 = false;
    private SceneManagerClass smc;
    // Start is called before the first frame update
    void Start()
    {
        smc = GameObject.Find("SceneManager").GetComponent<SceneManagerClass>();
        onScreenText = bb.monolaougeTXT;
        smc.tutorialLoaded();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !task1)
        {
            task1 = true;
            Debug.Log("Task done");
        }
        if (Input.GetKeyDown(KeyCode.A) && !task2)
        {
            task2 = true;
            Debug.Log("Task done");
        }
        if (Input.GetKeyDown(KeyCode.S) && !task3)
        {
            task3 = true;
            Debug.Log("Task done");
        }
        if (Input.GetKeyDown(KeyCode.D) && !task4)
        {
            task4 = true;
            Debug.Log("Task done");
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            task5 = true;
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            task6 = true;
        }
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            task7 = true;
        }
        pm.sandbox = sandbox;
        /*
         * So it can manually set which attack to use 
         */
        if (started) 
        {
            if (!basicShot)
            {
                if (bb.phaseType == 0)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
            else if (!bigShot)
            {
                if (bb.phaseType == 1)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
            else if (!Laser)
            {
                if (bb.phaseType == 2)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
            else if (!Vortex)
            {
                if (bb.phaseType == 3)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
            else if (!safeZone)
            {
                if (bb.phaseType == 4)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
        }
        if (readyToStart)
        {
            readyToStart = false;
            switch (tutorialLevel)
            {
                case 0:
                    StartCoroutine("BasicMovementTutorial");
                    break;
            }
        }
    }
    IEnumerator BasicMovementTutorial()
    {
       
        onScreenText.text = "Welcome to the Basic Movement tutorial. Let's get started with something simple...";
        yield return new WaitForSeconds(5f);
        pm.canMoveSet();
        onScreenText.text = "Press the W, A, S, and D keys to move around, try it now!";
        task1 = false;
        task2 = false;
        task3 = false;
        task4 = false;
        while (!task1 || !task2 || !task3 || !task4)
        {
            //Debug.Log(task1.ToString()+task2.ToString()+task3.ToString()+task4.ToString());
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Nice job! Now Let's move onto something new!";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "Now try shooting at me! Press 1 on the keypad to shoot!";
        task5 = false;
        while (!task5)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Great job! You'll notice that onscreen instructions have apeared to show you the controls and other information";
        yield return new WaitForSeconds(5f);
        spawnGUI();
        onScreenText.text = "These will apear during normal gameplay... In 5 seconds you will return to the tutorial screen!";
        yield return new WaitForSeconds(5f);
        smc.returnToBasicTutorialScreen();
    }
    IEnumerator BasicAndSpecialAbilites()
    {
        pm.canMoveSet();
        onScreenText.text = "Hello, and welcome to the Basic and Special Abilities tutorial. Let's make sure you remember how to move and shoot!";
        task1 = false;
        task2 = false;
        task3 = false;
        task4 = false;
        task5 = false;
        while (!task1 || !task2 || !task3 || !task4||!task5)
        {
            //Debug.Log(task1.ToString()+task2.ToString()+task3.ToString()+task4.ToString());
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Great! Now let's try something new... again... You notice that sheild that is currently underneath you?";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "That means that you are immune to all damage! There are a couple of ways that this could trigger, but you'll have to find those on your own";
        yield return new WaitForSeconds(5f);
        spawnGUI();
        onScreenText.text = "See on the right, where it says MP? Every time you hit me, you will get 1MP, the sheild ability will always cost its level in MP, use it now by pressing 2";
        task6 = false;
        while (!task6)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Great job! Now let's try the Counter attack... this one is a little tricky. You need to hold down 3 and move close to one of my projectiles...";
        yield return new WaitForSeconds(5f);
        yield return new WaitForEndOfFrame();
    }
    public void spawnGUI()
    {
        foreach (GameObject go in bb.gUI)
        {
            go.SetActive(true);
        }
    }
}
