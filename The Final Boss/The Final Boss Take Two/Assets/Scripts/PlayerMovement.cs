using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool Immune=false;
    public AudioSource Fire, Hit;
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss, playerShot;
    public Text XPCounter,NextPhaseXPTXT,totalXPTXT;
    public int HP=1, storage=1, mPMax=1, atk=1;//L-Joystick upgrades
    public int playerbS=1, playersS=1, playerbD=1, playersD=1, playerbC=1, playersC=1;//six buttons upgrades
    public int XP, cooldownTime, nextPhaseXP, totalXP=0;
    private bool shootAgain=true, dead=false;
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
        if (totalXP >= nextPhaseXP)
        {
            NextPhase();
        }
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        AnimationHolder.transform.Translate(new Vector2(horz, vert) * Time.deltaTime * speedMod);
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (shootAgain)
            {
                Fire.Play();
                StartCoroutine("cooldown");
                Instantiate(playerShot, AnimationHolder.transform);
            }
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
        if (!Immune)
        {
            Hit.Play();
            Debug.Log("Add death soon ");
            self.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            dead = true;
        }
    }
    public void BasicAttackXPGain()
    {
        XP += atk * playerbS;
        totalXP += atk * playerbS;
        XPCounter.text = "XP: " + XP.ToString();
        totalXPTXT.text = "Total XP: " + totalXP.ToString();
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
}
