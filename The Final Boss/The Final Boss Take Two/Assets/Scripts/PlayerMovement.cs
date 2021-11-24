using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public List<Text> Leveloutput = new List<Text>();
    public bool Immune=false;
    public AudioSource Fire, Hit;
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss, playerShot;
    public Text XPCounter,NextPhaseXPTXT,totalXPTXT;
    public int HP=1, storage=1, mPMax=1, atk=1;//L-Joystick upgrades
    public int playerbS=1, playersS=1, playerbD=1, playersD=1, playerbC=1, playersC=1;//six buttons upgrades
    public int XP, cooldownTime, nextPhaseXP, totalXP = 0, MP = 0, StorageCapacity=0;
    private bool shootAgain=true, dead=false, canMove=false;
    // Start is called before the first frame update
    void Start()
    {
        XP = 0;
        nextPhaseXP = 20;
        NextPhaseXPTXT.text = "XP needed for next phase: " + nextPhaseXP;
    }

    // Update is called once per frame
    void Update()
    {
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
        Leveloutput[2].text = StorageCapacity.ToString()+"/" + storage.ToString();
        Leveloutput[3].text = "lvl: " + atk.ToString();
        Leveloutput[4].text = "lvl: " + playerbS.ToString();
        Leveloutput[5].text = "lvl: " + playerbD.ToString();
        Leveloutput[6].text = "lvl: " + playerbC.ToString();
        Leveloutput[7].text = "lvl: " + playersS.ToString();
        Leveloutput[8].text = "lvl: " + playersD.ToString();
        Leveloutput[9].text = "lvl: " + playersC.ToString();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
           {
            Debug.Log("collision");
            if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossProjectile"))
            {
                LossOfLife();
            }
            else if (collision.gameObject.CompareTag("PlayerProjectile"))
            {

            }
            else if(!collision.gameObject.CompareTag("SafeZone"))
            {
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
        if (MP < mPMax)
        {
            MP++;
        }
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
