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
    private bool task1 = false, task2 = false, task3 = false, task4 = false, task5 = false, task6 = false, task7 = false, task8 = false, task9 = false, task10 = false;
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
             if (!bigShot)
            {
                if (bb.phaseType == 1)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
             if (!Laser)
            {
                if (bb.phaseType == 2)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
             if (!Vortex)
            {
                if (bb.phaseType == 3)
                {
                    bb.player.GetComponent<PlayerMovement>().NextPhase();
                }
            }
             if (!safeZone)
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
                case 1:
                    StartCoroutine("BasicAndSpecialAbilites");
                    break;
                case 2:
                    StartCoroutine("BossPhasesTutorial");
                    break;
                case 3:
                    sandbox = true;
                    basicShot = true;
                    bigShot = true;
                    Laser = true;
                    Vortex = true;
                    safeZone = true;
                    pm.XP = 99999999;
                    pm.HP = 99999999;
                    pm.MP = 99999999;
                    bb.bAM.toggleSidetoSideIdle();
                    pm.canMoveSet();
                    spawnGUI();
                    onScreenText.gameObject.SetActive(false);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad0)||Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("mega blask hit");
            task8 = true;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPeriod)||Input.GetKeyDown(KeyCode.C))
        {
            task9 = true;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.B))
        {
            task10 = true;
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
        spawnGUI();
        onScreenText.text = "Great job! You'll notice that onscreen instructions have apeared to show you the controls and other information";
        yield return new WaitForSeconds(5f);
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
        pm.Immune = true;
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
        onScreenText.text = "Once you do that, you will start to charge up a counter attack, try pressing 3 now. You will be able to absorb as many attacks as you have cargo space for, but you will always fire imedietly";
        task7 = false;
        while (!task7)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Great! Now let us practice your special attacks... fire at me until your Mega blast is ready, then fire that with the zero key! (charge completion rate is on the left)";
        task8 = false;
        while (!task8)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        onScreenText.text = "Great! This time things are a little tirckier, you need to block 5 attacks from me with your sheild to activate your mega sheild";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "So from now until the end of this tutorial, I will be fireing a projectile at you every 3 seconds, I'll also be moving around a bit. When the mega sheild is charged, use the decimal point key to activate it";
        StartCoroutine("FireEveryFewSeconds");
        task9 = false;
        while (!task9)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "The final ability of this tutorial is the Mega Counter laser, this will automatically make you invincible, allow you to collect projectiles like a normal counter and fire them at me... Activate it when ready with the + key on the keypad";
        task10 = false;
        while (!task10)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        smc.returnToBasicTutorialScreen();
    }
    IEnumerator BossPhasesTutorial()
    {
        pm.HP = 999999;
        //spawnGUI(); screen is to cluttered with it on
        pm.canMoveSet();
        onScreenText.text = "Welcome to the final tutorial, the other one is simply there to let you practice... What do you mean what about the sixth? I del- I mean... There never was a sixth...";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "We will be going through each of my phases one by one, when we get to the point where it says freeplay on screen, press the firebutton to move to the next phase.";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "Phase one: My basic shot, once I stop moving, I will wait for three seconds, then teleport above you, and fire\n" +
            "Make sure you keep moving while I am ready to fire";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "Freeplay";
        basicShot = true;
        task5 = false;
        bb.debugFire();
        while (!task5) 
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Nice job! Now let's move onto Phase Two: Big Shot, same strategies, but this one is bigger";
        yield return new WaitForSeconds(5f);
        onScreenText.text = "Freeplay";
        task5 = false;
        bigShot = true;
        pm.NextPhase();
        basicShot = false;
        while (!task5)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Great! Now for this one, just find a laser that isn't going to fire! " +
            "\n Hint: the ones that fire change color";
        task5 = false;
        bigShot = false;
        Laser = true;
        pm.NextPhase();
        yield return new WaitForSeconds(5f);
        onScreenText.text = "Freeplay";
        while (!task5)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Now just stay away from the center and you should be fine!";
        yield return new WaitForSeconds(5f);
        task5 = false;
        Laser = false;
        Vortex = true;
        pm.NextPhase();
        onScreenText.text = "Freeplay";
        while (!task5)
        {
            yield return new WaitForEndOfFrame();
        }
        onScreenText.text = "Now for the final attack, just get into the safe zone and you'll be fine, after this your on your own, good luck!";
        yield return new WaitForSeconds(5f);
        task5 = false;
        Vortex = false;
        safeZone = true;
        pm.NextPhase();
        onScreenText.text = "Freeplay";
        while (!task5)
        {
            yield return new WaitForEndOfFrame();
        }
        smc.returnToAdvancedTutorialScreen();
    }

    public void spawnGUI()
    {
        foreach (GameObject go in bb.gUI)
        {
            go.SetActive(true);
        }
    }
    
    IEnumerator FireEveryFewSeconds()
    {
        basicShot = true;
        while (true)
        {
            yield return new WaitForSeconds(3f);
            bb.debugFire();
        }
    }
}
