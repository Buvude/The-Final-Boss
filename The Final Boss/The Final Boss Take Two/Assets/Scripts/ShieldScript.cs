using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public SheildDeflectingScript otherOne;
    public Animator selfAnimation;
    public GameObject selfDestruct2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void decreaseShieldLevel()
    {
        selfAnimation.SetInteger("Shield Level", selfAnimation.GetInteger("Shield Level") - 1);
    }
    public void selfDestruct()
    {
        selfDestruct2.SetActive(false);
        if (otherOne.MadeInvincable)
        {
            otherOne.MadeInvincable = false;
            otherOne.pm.Immune = false;
        }
    }
    
}
