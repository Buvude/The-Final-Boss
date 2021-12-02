using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool paused = false;
    public List<GameObject> pauseListPlayer = new List<GameObject>();
    public Animator megaCounterAnimations;
    public bool CounterDone = true;
    public List<Text> Leveloutput = new List<Text>();
    public bool Immune = false, immuneSpriteActive = false, counterSpecial = false;
    public AudioSource Fire, Hit, Music;
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss, playerShot, Sheild, invincableSprite, suckySucky, sonOfSuckySucky, megaSheild, megaShot, MegaCounter, tempHolder, bossAnimationHolder;
    public Text XPCounter, NextPhaseXPTXT, totalXPTXT;
    public int HP = 1, storageSpace = 1, mPMax = 1, atk = 1, HPLevel = 1;//L-Joystick upgrades
    public int playerbS = 1, playersS = 1, playerbD = 1, playersD = 1, playerbC = 1, playersC = 1;//six buttons upgrades
    public int XP, cooldownTime, nextPhaseXP, totalXP = 0, MP = 0, ItemsInStorage = 0;
    public int basicUpgradeCost = 10, specialUpgradeCost = 20, coreUpgradeCost = 50;
    private int basicUpgradeExponential, specialUpgradeExponential, coreUpgradeExponential;
    private bool shootAgain = true, dead = false, canMove = false, sCSActive = false, sCDActive = false, sCCActive = false, specialOngoing = false;
    public int sCS, sCD, sCC;//special abilities counters
    private int sCTS, sCTD, sCTC;//special ability totals
    private float speedHolderB, speedHolderP;//to hold speed of projectiles when paused
    // Start is called before the first frame update
    void Start()
    {
        basicUpgradeExponential = basicUpgradeCost;
        specialUpgradeExponential = specialUpgradeCost;
        coreUpgradeExponential = coreUpgradeCost;
        XP = 0;
        nextPhaseXP = 40;
        NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
    }

    // Update is called once per frame
    void Update()
    {
        sCTS = playersS * 5;
        sCTD = playersD * 5;
        sCTC = playersC * 5;
        if (Immune && !immuneSpriteActive)
        {
            immuneSpriteActive = true;
            invincableSprite.gameObject.SetActive(true);
        }
        if (!Immune && immuneSpriteActive)
        {
            immuneSpriteActive = false;
            invincableSprite.gameObject.SetActive(false);
        }
        /*
         * Bounds setting
         */
        if (AnimationHolder.transform.position.x >= 15.76)
        {
            AnimationHolder.transform.Translate(-1f, 0, 0);
        }
        if (AnimationHolder.transform.position.x <= -15.77)
        {
            AnimationHolder.transform.Translate(1f, 0, 0);
        }
        if (AnimationHolder.transform.position.y >= 8.06)
        {
            AnimationHolder.transform.Translate(0, -1f, 0);
        }
        if (AnimationHolder.transform.position.y <= -8.03)
        {
            AnimationHolder.transform.Translate(0, 1f, 0);
        }
        /*
         * Bounds setting end
         */
        if (totalXP >= nextPhaseXP)
        {
            NextPhase();
        }
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        /*
         * making sure your not dead and it's not the monolouge
         * also all player controls
         */
        if (canMove && !dead)
        {
            AnimationHolder.transform.Translate(new Vector2(horz, vert) * Time.deltaTime * speedMod);
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (shootAgain)//impliments a cooldown
                {
                    Fire.Play();
                    StartCoroutine("cooldown");
                    Instantiate(playerShot, AnimationHolder.transform);
                }
            }
        }
        /*
         * Player Control end
         * Upgrade text begin
         */
        Leveloutput[0].text = HP.ToString();
        Leveloutput[1].text = MP.ToString() + "/" + mPMax.ToString();
        Leveloutput[24].text = ItemsInStorage.ToString() + "/" + storageSpace.ToString();
        Leveloutput[3].text = "lvl: " + atk.ToString();
        Leveloutput[4].text = "lvl: " + playerbS.ToString();
        Leveloutput[5].text = "lvl: " + playerbD.ToString();
        Leveloutput[6].text = "lvl: " + playerbC.ToString();
        Leveloutput[7].text = "lvl: " + playersS.ToString();
        Leveloutput[8].text = "lvl: " + playersD.ToString();
        Leveloutput[9].text = "lvl: " + playersC.ToString();
        Leveloutput[10].text = Mathf.Pow(specialUpgradeCost, playersS).ToString() + "XP";
        Leveloutput[11].text = Mathf.Pow(specialUpgradeCost, playersD).ToString() + "XP";
        Leveloutput[12].text = Mathf.Pow(specialUpgradeCost, playersC).ToString() + "XP";
        Leveloutput[13].text = Mathf.Pow(basicUpgradeCost, playerbS).ToString() + "XP";
        Leveloutput[14].text = Mathf.Pow(basicUpgradeCost, playerbD).ToString() + "XP";
        Leveloutput[15].text = Mathf.Pow(basicUpgradeCost, playerbC).ToString() + "XP";
        Leveloutput[16].text = Mathf.Pow(coreUpgradeCost, atk).ToString() + "XP";
        Leveloutput[17].text = Mathf.Pow(coreUpgradeCost, HPLevel).ToString() + "XP";
        Leveloutput[18].text = Mathf.Pow(coreUpgradeCost, mPMax).ToString() + "XP";
        Leveloutput[19].text = Mathf.Pow(coreUpgradeCost, storageSpace).ToString() + "XP";
        //10-19
        /*
         * xp text updates
         */
        XPCounter.text = "XP: " + XP.ToString();
        totalXPTXT.text = "Total XP: " + totalXP.ToString();


        if (sCS >= sCTS && !sCSActive)
        {
            sCSActive = true;
            Leveloutput[20].text = "Special Shot is Active!";
        }
        else if (sCS < sCTS)
        {
            Leveloutput[20].text = sCS.ToString() + "/" + sCTS.ToString() + " until Special Shot is avaliable";
        }
        if (sCD >= sCTD && !sCDActive)
        {
            sCDActive = true;
            Leveloutput[21].text = "Special Sheild is Active!";
        }
        else if (sCD < sCTD)
        {
            Leveloutput[21].text = sCD.ToString() + "/" + sCTD.ToString() + " until Special Sheild is avaliable";
        }
        if (sCC >= sCTC && !sCCActive)
        {
            sCCActive = true;
            Leveloutput[22].text = "Special Sheild is Active!";
        }
        else if (sCC < sCTC)
        {
            Leveloutput[22].text = sCC.ToString() + "/" + sCTC.ToString() + " until Special Counter is avaliable";
        }

        /*
         * upgrade text end
         */
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Boss.GetComponent<BossBehavior>().phaseType == 4)
            {
                Boss.GetComponent<BossBehavior>().phaseType = 0;
            }
            else
            {
                Boss.GetComponent<BossBehavior>().phaseType += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Boss.GetComponent<BossBehavior>().phaseNumber += 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Boss.GetComponent<BossBehavior>().debugFire();
        }//Debug Purposes only*/
        /*
    * upgrades
    * */

        if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Pow(coreUpgradeCost, HPLevel) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(coreUpgradeCost, HPLevel).ToString());
            HP++;
            HPLevel++;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Mathf.Pow(coreUpgradeCost, atk) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(coreUpgradeCost, atk).ToString());
            atk++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Mathf.Pow(coreUpgradeCost, mPMax) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(coreUpgradeCost, mPMax).ToString());
            mPMax++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Mathf.Pow(coreUpgradeCost, storageSpace) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(coreUpgradeCost, storageSpace).ToString());
            storageSpace++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7) && Mathf.Pow(basicUpgradeCost, playerbS) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(basicUpgradeCost, playerbS).ToString());
            playerbS++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8) && Mathf.Pow(basicUpgradeCost, playerbD) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(basicUpgradeCost, playerbD).ToString());
            playerbD++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9) && Mathf.Pow(basicUpgradeCost, playerbC) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(basicUpgradeCost, playerbC).ToString());
            playerbC++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4) && Mathf.Pow(basicUpgradeCost, playersS) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(specialUpgradeCost, playersS).ToString());
            playersS++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5) && Mathf.Pow(basicUpgradeCost, playersD) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(specialUpgradeCost, playersD).ToString());
            playersD++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6) && Mathf.Pow(basicUpgradeCost, playersC) <= XP)
        {
            XP -= int.Parse(Mathf.Pow(specialUpgradeCost, playersC).ToString());
            playersC++;
        }
        /*
         * Abilities
         * */
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))//Basic Sheild
        {
            if (MP >= playerbD)
            {
                MP -= playerbD;
                Sheild.GetComponent<Animator>().SetInteger("Shield Level", playerbD);
                Sheild.SetActive(true);
            }
        }
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3) && CounterDone)// Basic Counter-Attack part 1/2
        {
            //CounterDone = false;
            suckySucky.SetActive(true);
            megaCounterAnimations.SetTrigger("Normal");
            StartCoroutine("CA");

        }
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3) && CounterDone)// Basic Counter-Attack part 2/2
        {
            CounterDone = false;
            StopCoroutine("CA");
            StartCoroutine("FireOnMyMark");

        }
        /*
         * Special abilities
         */
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0) && sCSActive && !specialOngoing)//Mega blast
        {
            tempHolder = Instantiate(megaShot, AnimationHolder.transform);
            sCSActive = false;
            specialOngoing = false;
            sCS = 0;
        }
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.KeypadPeriod) && sCDActive && !specialOngoing)
        {
            specialOngoing = true;
            StartCoroutine("MegaSheild");
        }
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.KeypadPlus) && sCCActive && !specialOngoing)
        {
            specialOngoing = true;
            Immune = true;
            MegaCounter.SetActive(true);
            megaCounterAnimations.SetTrigger("Mega");
            StartCoroutine("MegaCounterEvent");
        }

        /*
         * Pause buttons
         */
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!paused)
            {
                paused = true;
                TutorialPause();
                Music.Pause();
            }
            else if (paused)
            {
                paused = false;
                TutorialUnpause();
                Music.UnPause();
            }
        }

    }

    IEnumerator MegaCounterEvent()
    {
        counterSpecial = true;
        while (ItemsInStorage < storageSpace)
        {
            yield return new WaitForEndOfFrame();

        }
        Debug.Log("made it thorugh the first loop");
        while (suckySucky.GetComponent<SuckySuckyWaitScript>().wait)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("made it through the second loop");
        suckySucky.GetComponent<Animator>().enabled = false;
        sonOfSuckySucky.GetComponent<Animator>().SetInteger("ItemsInStorage", ItemsInStorage);
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        sonOfSuckySucky.GetComponent<Animator>().enabled = true;

    }
    public void TutorialPause()//REMEMBER TO PAUSE ALL CORUTINES might be uneeded
    {
        Boss.GetComponent<BossBehavior>().monolaougeTXT.text = "Paused";
        Boss.GetComponent<BossBehavior>().monolaougeTXT.gameObject.SetActive(true);
        foreach (GameObject go in Boss.GetComponent<BossBehavior>().gUI)
        {
            go.SetActive(false);
        }
        foreach(Text txt in Leveloutput)
        {
            txt.gameObject.SetActive(false);
        }
        Boss.GetComponent<Animator>().enabled = false;
        bossAnimationHolder.GetComponent<Animator>().enabled = false;

        switch (Boss.GetComponent<BossBehavior>().phaseType)//stops movement from boss projectiles, but stores the value for unpause
        {
            case 0:
                if (bossAnimationHolder.GetComponentInChildren<BossAttack>() != null)
                {
                    speedHolderB = bossAnimationHolder.GetComponentInChildren<BossAttack>().speed;
                    bossAnimationHolder.GetComponentInChildren<BossAttack>().speed = 0;
                }
                break;
            case 1:
                if (bossAnimationHolder.GetComponentInChildren<BigBossAttack>() != null)
                {
                    speedHolderB = bossAnimationHolder.GetComponentInChildren<BigBossAttack>().speed;
                    bossAnimationHolder.GetComponentInChildren<BigBossAttack>().speed = 0;
                }
                break;
            case 2:

                //Lasers take care of themselves... mostly, it isn't perfect, but it'll work for my needs
                break;
            case 3:
                if (GameObject.Find("VortexAttack(Clone)") != null)
                {
                    //once again, takes care of itself
                    /*GameObject lasers = GameObject.Find("VortexAttack(Clone)").GetComponentInChildren<GameObject>();
                    GameObject.Find("VortexAttack(Clone)").GetComponent<Animator>().enabled = false;
                    foreach(Animator anime in GameObject.Find("VortexAttack(Clone)").GetComponentInChildren<GameObject>().GetComponentsInChildren<Animator>())
                    {
                        anime.enabled = false;//why did I name this variable anime? I have no clue... I just did...
                    }*/
                }
                break;
            case 4:
                if (GameObject.Find("SafeZoneAttack(Clone)") != null)
                {
                    GameObject.Find("SafeZoneAttack(Clone)").GetComponentInChildren<Animator>().enabled = false;
                }
                break;
        }
        foreach (GameObject go in pauseListPlayer)
        {
            if (go.activeInHierarchy)
            {
                if (go.GetComponent<Animator>() != null)
                {
                    go.GetComponent<Animator>().enabled = false;
                }
            }
        }
        if (AnimationHolder.GetComponentInChildren<PlayerShot>() != null)
        {
            speedHolderP = AnimationHolder.GetComponentInChildren<PlayerShot>().speed;
            AnimationHolder.GetComponentInChildren<PlayerShot>().speed = 0;
        }
    }
    public void TutorialUnpause()//REMEMBER TO UNPAUSE ALL CORUTINES might be unneeded
    {
        Boss.GetComponent<BossBehavior>().monolaougeTXT.text = "";
        foreach (GameObject go in Boss.GetComponent<BossBehavior>().gUI)
        {
            go.SetActive(true);
        }
        foreach (Text txt in Leveloutput)
        {
            txt.gameObject.SetActive(true);
        }


        switch (Boss.GetComponent<BossBehavior>().phaseType)//stops movement from boss projectiles, but stores the value for unpause
        {
            case 0:
                if (bossAnimationHolder.GetComponentInChildren<BossAttack>() != null)
                {
                    bossAnimationHolder.GetComponentInChildren<BossAttack>().speed = speedHolderB;
                    Boss.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    Boss.GetComponent<Animator>().enabled = true;
                    bossAnimationHolder.GetComponent<Animator>().enabled = true;
                }
                break;
            case 1:
                if (bossAnimationHolder.GetComponentInChildren<BigBossAttack>() != null)
                {

                    bossAnimationHolder.GetComponentInChildren<BigBossAttack>().speed = speedHolderB;
                    Boss.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    Boss.GetComponent<Animator>().enabled = true;
                    bossAnimationHolder.GetComponent<Animator>().enabled = true;
                }
                break;
            case 2:

                //Lasers take care of themselves... mostly, it isn't perfect, but it'll work for my needs
                break;
            case 3:
                if (GameObject.Find("VortexAttack(Clone)") != null)
                {
                    //once again, takes care of itself
                    /*GameObject lasers = GameObject.Find("VortexAttack(Clone)").GetComponentInChildren<GameObject>();
                    GameObject.Find("VortexAttack(Clone)").GetComponent<Animator>().enabled = false;
                    foreach(Animator anime in GameObject.Find("VortexAttack(Clone)").GetComponentInChildren<GameObject>().GetComponentsInChildren<Animator>())
                    {
                        anime.enabled = false;//why did I name this variable anime? I have no clue... I just did...
                    }*/
                    Boss.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    Boss.GetComponent<Animator>().enabled = true;
                    bossAnimationHolder.GetComponent<Animator>().enabled = true;
                }
                break;
            case 4:
                if (GameObject.Find("SafeZoneAttack(Clone)") != null)
                {
                    GameObject.Find("SafeZoneAttack(Clone)").GetComponentInChildren<Animator>().enabled = true;
                    Boss.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    Boss.GetComponent<Animator>().enabled = true;
                    bossAnimationHolder.GetComponent<Animator>().enabled = true;
                }
                break;
        }
        foreach (GameObject go in pauseListPlayer)
        {
            if (go.activeInHierarchy)
            {
                if (go.GetComponent<Animator>() != null)
                {
                    go.GetComponent<Animator>().enabled = true;
                }
            }
        }
        if (AnimationHolder.GetComponentInChildren<PlayerShot>() != null)
        {
            AnimationHolder.GetComponentInChildren<PlayerShot>().speed = speedHolderP;
        }
    }

    public void doneWithSpecial()
    {
        specialOngoing = false;
    }
    IEnumerator MegaSheild()
    {
        megaSheild.SetActive(true);
        Immune = true;
        int countdown;
        countdown = playersD * HP * 5;
        Leveloutput[23].gameObject.SetActive(true);
        Leveloutput[23].text = countdown + " seconds left on sheild";        
        for (int i = 0; i < playersD * HP * 5; i++)
        {
            Leveloutput[23].text = countdown + " seconds left on sheild";
            yield return new WaitForSeconds(1);
            countdown--;
        }
        Leveloutput[23].gameObject.SetActive(false);
        megaSheild.SetActive(false);
        Immune = false;
        sCSActive = false;
    }
    IEnumerator CA()
    {
        yield return new WaitForSeconds(5);
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        suckySucky.SetActive(false);
    }
    IEnumerator FireOnMyMark()
    {
        while (suckySucky.GetComponent<SuckySuckyWaitScript>().wait)
        {
            yield return new WaitForEndOfFrame();
        }
        suckySucky.GetComponent<Animator>().enabled = false;
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        if (ItemsInStorage > 0)
        {
            sonOfSuckySucky.GetComponent<Animator>().SetInteger("ItemsInStorage", ItemsInStorage);
            sonOfSuckySucky.GetComponent<Animator>().enabled = true;
            megaCounterAnimations.SetTrigger("Normal");
            //sonOfSuckySucky.GetComponent<Animator>().SetTrigger("fireTheStorage);

        }
        else
        {
            CounterDone = true;
            suckySucky.SetActive(false);
            suckySucky.GetComponent<Animator>().enabled = true;
        }
        
    }
   
/* private void OnTriggerEnter2D(Collider2D collision)
 {
     Debug.Log("collision");
     if (collision.GetComponentInParent<GameObject>().CompareTag("Boss"))
     {
         PlayerDeath();
     }
     else
     {
         Debug.Log("Invalid Tag");
     }
 }*/ //Old version of what is below, I figured it out finially....
private void OnTriggerStay2D(Collider2D collision)
    {
           {
            
            if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossProjectile")||collision.gameObject.CompareTag("SpecialBossProjectile"))
            {
                Debug.Log("collision");
                LossOfLife();
            }
            else if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                Debug.Log("collision");
            }
            else if(!collision.gameObject.CompareTag("SafeZone")&&!collision.gameObject.CompareTag("CAA"))
            {
                Debug.Log("collision");
                Debug.Log("Invalid Tag");
            }
        }  
    }
    
    public void LossOfLife()
    {
        if (!Immune&&!dead)
        {
            Hit.Play();
            Debug.Log("Add death soon ");
            self.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            dead = true;
            StartCoroutine("respawn");
        }
    }
    public void BasicAttackXPGain()
    {
        XP += atk * playerbS;
        totalXP += atk * playerbS;
        XPCounter.text = "XP: " + XP.ToString();
        totalXPTXT.text = "Total XP: " + totalXP.ToString();
        sCS+=playerbS*atk;
        if (MP < mPMax)
        {
            MP++;
        }
    }
    public void MegaBlastXPGain()
    {
        MP = mPMax;
        XP += atk * playerbS * playersS * 5;
        totalXP += atk * playerbS * playersS * 5;
        Boss.GetComponent<BossBehavior>().hit.Play();
        doneWithSpecial();
        Destroy(tempHolder);
        sCSActive = false;  
    }

    public void CAXPGain()
    {
        Debug.Log("Basic counter XP Gain");
        Boss.GetComponent<BossBehavior>().hit.Play();
        XP += 3*playerbC * atk;
        totalXP += 3 * playerbC * atk;
        sCC += playerbC * atk;
    }
    public void MegaCounterXPGain()
    {
        Debug.Log("Mega counter XP Gain");
        Boss.GetComponent<BossBehavior>().hit.Play();
        XP += atk * playerbC * playersC * 5;
        totalXP += atk * playerbC * playersC * 5;
        sCC = 0;
        Immune = false;
        counterSpecial = false;
        specialOngoing = false;
        sCCActive = false;
    }
    public void sheildSpecialGain()
    {
        sCD += playerbS*HP;
    }
    IEnumerator MegaSheildAction()
    {
        megaSheild.SetActive(true);
        yield return new WaitForSeconds(3 + playerbD);
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        megaSheild.GetComponent<Animator>().SetTrigger("Destroy");
    }
    IEnumerator respawn()
    {
        Debug.Log("you are not immortal, be carefull");
        HP -= 1;
        if (HP >= 1)
        {
            yield return new WaitForSeconds(3);
            while (paused)
            {
                yield return new WaitForEndOfFrame();
            }
            dead = false;
            self.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            self.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            //GameOver();
            yield return new WaitForSeconds(.1f);
            while (paused)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator cooldown()
    {
        shootAgain = false;
        yield return new WaitForSeconds(cooldownTime);
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        shootAgain = true;
    }
    public void NextPhase()
    {
        if (Boss.GetComponent<BossBehavior>().phaseType == 4)
        {
            Boss.GetComponent<BossBehavior>().phaseType = 0;
            Boss.GetComponent<BossBehavior>().phaseNumber++;
            Boss.GetComponent<BossBehavior>().totalPhase++;
            nextPhaseXP += 40 * Boss.GetComponent<BossBehavior>().phaseNumber;
            NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
        }
        else
        {
            Boss.GetComponent<BossBehavior>().phaseType += 1;
            Boss.GetComponent<BossBehavior>().totalPhase++;
            nextPhaseXP += 40 * Boss.GetComponent<BossBehavior>().phaseNumber;
            NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
        }
    }
    public void canMoveSet()
    {
        canMove = true;
    }
}
