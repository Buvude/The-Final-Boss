using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public Sprite Left, Middle, Right;
    public bool paused = false;
    public AudioSource synth, BigShot, BasicShot, Lazer, hit;
    public int phaseNumber, phaseType, totalPhase;//Phase type: 0=basic, 1= big, 2=Lazer, 3=vortex, 4=safe Zone
    //phaseNumber is the number of shots per attack fired, totalPhase is just to keep track of score at the end and maybe other stuff later
    public GameObject player, self, animationHolder;
    private GameObject temp;
    public List<GameObject> weaponType = new List<GameObject>(), gUI=new List<GameObject>();
    public Animator bA, bAA;
    public Text monolaougeTXT, currentPhase, nextPhase, totalPhaseTXT;
    private Vector3 startingPoint = new Vector3(0f, 8.97f, 0);
    public BossanimationManager bAM;
    private LazerAttack lA;
    //public LazerSpawner lS;
    // Start is called before the first frame update
    void Start()
    {
        //Boss comes in and starts monolouge (attack boss to skip)
        StartCoroutine("Monolouge");
    }

    // Update is called once per frame
    void Update()
    {
        paused=player.GetComponentInChildren<PlayerMovement>().paused;
        totalPhaseTXT.text = "Total phases survived: " + totalPhase.ToString();
        switch (phaseType)
        {
            case 0:
                currentPhase.text = "Current phase: Basic shot";
                nextPhase.text = "Next phase: Big shot";
                break;
            case 1:
                currentPhase.text = "Current phase: Big shot";
                nextPhase.text = "Next phase: Lazer cannons";
                break;
            case 2:
                currentPhase.text = "Current phase: Lazer cannons";
                nextPhase.text = "Next phase: Vortex";
                break;
            case 3:
                currentPhase.text = "Current phase: Vortex";
                nextPhase.text = "Next phase: Safe Zone";
                break;
            case 4:
                currentPhase.text = "Current phase: Safe Zone";
                nextPhase.text = "Next phase: Basic shot";
                break;
        }
    }
    IEnumerator Monolouge()
    {
        yield return new WaitForSeconds(5);
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
        monolaougeTXT.gameObject.SetActive(false);
        bAA.SetTrigger("Side to side");
        player.GetComponentInChildren<PlayerMovement>().canMoveSet();
        foreach(GameObject guiII in gUI)
        {
            guiII.SetActive(true);
        }

    }
    public void debugFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (phaseType == 0)//basic shot
        {
            float additallup = 0;
            for (int i = 0; i < phaseNumber; i++)
            {
                additallup += player.transform.position.x - animationHolder.transform.position.x;//this should move the boss to the right position.
                animationHolder.transform.Translate(player.transform.position.x - animationHolder.transform.position.x, 0f, 0f);
                Instantiate(weaponType[phaseType], animationHolder.transform);
                BasicShot.Play();
                yield return new WaitForSeconds(1);
                while (paused)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            animationHolder.transform.Translate(-additallup, 0f, 0f);
            bAM.toggleSidetoSideIdle();
        }
        else if (phaseType == 1)//big shot
        {
            float additallup = 0;
            for (int i = 0; i < phaseNumber; i++)
            {
                additallup += player.transform.position.x - animationHolder.transform.position.x;//this should move the boss to the right position.
                animationHolder.transform.Translate(player.transform.position.x - animationHolder.transform.position.x, 0f, 0f);
                Instantiate(weaponType[1], animationHolder.transform);
                BigShot.Play();
                yield return new WaitForSeconds(2);
                while (paused)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            animationHolder.transform.Translate(-additallup, 0f, 0f);
            bAM.toggleSidetoSideIdle();
        }
        else if (phaseType == 2)//Lazer shot
        {
            
            temp = Instantiate(weaponType[2]);
            lA = temp.gameObject.GetComponentInChildren<LazerAttack>();
            //Debug.Log(lA.ToString());
            lA.spawn();
        }
        else if (phaseType == 3)//Vortex Shot
        {
            temp = Instantiate(weaponType[3]);
            temp.GetComponent<Animator>().SetInteger("AttackAmount", phaseNumber);

        }
        else if(phaseType==4)//Safe Zone Attack
        {
            for (int i = 0; i < phaseNumber; i++)
            {
                temp = Instantiate(weaponType[4]);
                //temp.GetComponentInChildren<SZAttackCounter>().setNumber(phaseNumber);
                yield return new WaitForSeconds(15f);
            }
            bAM.toggleSidetoSideIdle();

            
        }
    }
    public void postLazer()
    {
        Destroy(temp);
        bAM.toggleSidetoSideIdle();
    }
    
}
