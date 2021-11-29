using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool CounterDone=true;
    public List<Text> Leveloutput = new List<Text>();
    public bool Immune=false, immuneSpriteActive=false;
    public AudioSource Fire, Hit;
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss, playerShot,Sheild,invincableSprite, suckySucky, sonOfSuckySucky, megaSheild, megaShot, MegaCounter;
    public Text XPCounter,NextPhaseXPTXT,totalXPTXT;
    public int HP=1, storage=1, mPMax=1, atk=1;//L-Joystick upgrades
    public int playerbS=1, playersS=1, playerbD=1, playersD=1, playerbC=1, playersC=1;//six buttons upgrades
    public int XP, cooldownTime, nextPhaseXP, totalXP = 0, MP = 0, ItemsInStorage=0;
    public int basicUpgradeCost = 10, specialUpgradeCost = 20, coreUpgradeCost = 50;
    private int basicUpgradeExponential, specialUpgradeExponential, coreUpgradeExponential;
    private bool shootAgain=true, dead=false, canMove=false, sCSActive=false, sCDActive=false,sCCActive=false;
    public int sCS, sCD, sCC;//special abilities counters
    private int sCTS, sCTD, sCTC;//special ability totals
    // Start is called before the first frame update
    void Start()
    {
        basicUpgradeExponential = basicUpgradeCost;
        specialUpgradeExponential = specialUpgradeCost;
        coreUpgradeExponential = coreUpgradeCost;
        XP = 0;
        nextPhaseXP = 20;
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
            if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetKeyDown(KeyCode.Keypad1))
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
        Leveloutput[0].text =  HP.ToString();
        Leveloutput[1].text =  MP.ToString()+"/"+mPMax.ToString();
        Leveloutput[2].text = ItemsInStorage.ToString()+"/" + storage.ToString();
        Leveloutput[3].text = "lvl: " + atk.ToString();
        Leveloutput[4].text = "lvl: " + playerbS.ToString();
        Leveloutput[5].text = "lvl: " + playerbD.ToString();
        Leveloutput[6].text = "lvl: " + playerbC.ToString();
        Leveloutput[7].text = "lvl: " + playersS.ToString();
        Leveloutput[8].text = "lvl: " + playersD.ToString();
        Leveloutput[9].text = "lvl: " + playersC.ToString();
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
            if(Boss.GetComponent<BossBehavior>().phaseType==4)
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
        }
        /*
    * upgrades
    * */
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HP++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            atk++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mPMax++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            storage++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            playerbS++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            playerbD++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            playerbC++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            playersS++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            playersD++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
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
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)&&CounterDone)// Basic Counter-Attack part 1/2
        {
            //CounterDone = false;
            suckySucky.SetActive(true);
            StartCoroutine("CA");
            
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3)&&CounterDone)// Basic Counter-Attack part 2/2
        {
            CounterDone = false;
            StopCoroutine("CA");
            StartCoroutine("FireOnMyMark");
            
        }

    }
    IEnumerator CA()
    {
        yield return new WaitForSeconds(5);
        suckySucky.SetActive(false);
    }
    IEnumerator FireOnMyMark()
    {
        while (suckySucky.GetComponent<SuckySuckyWaitScript>().wait)
        {
            yield return new WaitForEndOfFrame();
        }
        suckySucky.GetComponent<Animator>().enabled = false;
        if (ItemsInStorage > 0)
        {
            sonOfSuckySucky.GetComponent<Animator>().SetInteger("ItemsInStorage", ItemsInStorage);
            sonOfSuckySucky.GetComponent<Animator>().enabled = true;
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
    public void CAXPGain()
    {
        Boss.GetComponent<BossBehavior>().hit.Play();
        XP += 3*playerbC * atk;
        totalXP += 3 * playerbC * atk;
        sCC += playerbC * atk;
    }
    public void sheildSpecialGain()
    {
        sCD += playerbS*HP;
    }
    IEnumerator MegaSheildAction()
    {
        megaSheild.SetActive(true);
        yield return new WaitForSeconds(3 + playerbD);
        megaSheild.GetComponent<Animator>().SetTrigger("Destroy");
    }
    IEnumerator respawn()
    {
        Debug.Log("you are not immortal, be carefull");
        HP -= 1;
        if (HP >= 1)
        {
            yield return new WaitForSeconds(3);
            dead = false;
            self.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            self.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            //GameOver();
            yield return new WaitForSeconds(.1f);
        }
    }
    IEnumerator cooldown()
    {
        shootAgain = false;
        yield return new WaitForSeconds(cooldownTime);
        shootAgain = true;
    }
    public void NextPhase()
    {
        if (Boss.GetComponent<BossBehavior>().phaseType == 4)
        {
            Boss.GetComponent<BossBehavior>().phaseType = 0;
            Boss.GetComponent<BossBehavior>().phaseNumber++;
            Boss.GetComponent<BossBehavior>().totalPhase++;
            nextPhaseXP += 20 * Boss.GetComponent<BossBehavior>().phaseNumber;
            NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
        }
        else
        {
            Boss.GetComponent<BossBehavior>().phaseType += 1;
            Boss.GetComponent<BossBehavior>().totalPhase++;
            nextPhaseXP += 20 * Boss.GetComponent<BossBehavior>().phaseNumber;
            NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
        }
    }
    public void canMoveSet()
    {
        canMove = true;
    }
}
