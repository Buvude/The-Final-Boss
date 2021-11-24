using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SZAttackCounter : MonoBehaviour
{
    public GameObject boss;
    public int number;
    public Animator selfAnimator;
    public GameObject self;
    public AudioSource charge, attack;
    public SafeZoneAttack sZA;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void incriment()
    {
        number--;
        selfAnimator.SetInteger("AN",number);
    }
    public void playChargeSound()
    {
        charge.Play();
    }
    public void playAttackSound()
    {
        attack.Play();
    }
    public void selfDestruct()
    {
        Destroy(self);
    }
    public void setNumber(int num)
    {
        number = num;
        selfAnimator.SetInteger("AN", number);
    }
    public void Reset()
    {
        sZA.moveSZ();
    }

}
