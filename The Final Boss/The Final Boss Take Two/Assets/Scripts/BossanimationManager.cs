using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossanimationManager : MonoBehaviour
{
    public float trackingSpeed;
    public GameObject Player, self;
    //private Vector2 target1;
    //private bool moveTowardsPlayer=false;//true for testing purposes only
    public Animator bAA;
    public BossBehavior bB;
    // Start is called before the first frame update
    void Start()
    {
       // target1.Set(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //target1.x = Player.gameObject.transform.position.x;
       /* if (moveTowardsPlayer)
        {
            moveTowardsPlayer = false;
            
        }*/
    }
    public void StartFireRoutine()
    {
        bAA.GetComponent<Animator>().enabled = false;
        StartCoroutine("movetowardsPlayer");
        //moveTowardsPlayer = true;
        
    }
    public void toggleSidetoSideIdle()
    {
        bAA.GetComponent<Animator>().enabled = true;
        bAA.SetTrigger("Side to side");
    }
    IEnumerator movetowardsPlayer()
    {
        yield return new WaitForSeconds(3);
        bB.debugFire();

    }
}
