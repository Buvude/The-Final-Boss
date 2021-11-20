using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource Fire, Hit;
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss, playerShot;
    public Text XPCounter;
    public int HP=1, storage=1, mPMax=1, atk=1;//L-Joystick upgrades
    public int playerbS=1, playersS=1, playerbD=1, playersD=1, playerbC=1, playersC=1;//six buttons upgrades
    public int XP, cooldownTime;
    private bool shootAgain=true;
    // Start is called before the first frame update
    void Start()
    {
        XP = 0; 
    }

    // Update is called once per frame
    void Update()
    {
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
                PlayerDeath();
            }
            else if (collision.gameObject.CompareTag("PlayerProjectile"))
            {

            }
            else
            {
                Debug.Log("Invalid Tag");
            }
        }  
    }
    
    public void PlayerDeath()
    {
        Hit.Play();
        Debug.Log("Add death soon ");
        self.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public void BasicAttackXPGain()
    {
        XP += atk * playerbS;
        XPCounter.text = "XP: " + XP.ToString();
    }
    IEnumerator cooldown()
    {
        shootAgain = false;
        yield return new WaitForSeconds(cooldownTime);
        shootAgain = true;
    }
}
