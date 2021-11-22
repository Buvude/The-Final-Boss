using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexAttack : MonoBehaviour
{
    public GameObject self;
    private GameObject Boss;
    public Animator VA, VL1, VL2, VL3, VL4;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("BossAnimate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loop()
    {
        VA.SetInteger("AttackAmount",VA.GetInteger("AttackAmount") - 1);
    }
    public void destroySelf()
    {
        Boss.GetComponent<Animator>().enabled = true;
        Boss.GetComponent<Animator>().SetTrigger("Side to side");
        Destroy(self);
    }
    public void startTheAttack()
    {
        VL1.SetTrigger("Go");
        VL2.SetTrigger("Go");
        VL3.SetTrigger("Go");
        VL4.SetTrigger("Go");
    }
}
