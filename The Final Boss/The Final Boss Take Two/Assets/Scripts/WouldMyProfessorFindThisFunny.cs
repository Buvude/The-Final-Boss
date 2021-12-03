using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WouldMyProfessorFindThisFunny : MonoBehaviour
{
    public bool basicShot, bigShot, Laser, Vortex, safeZone, pS, pBS, pL, pV, pSZ;
    public BossBehavior bb;
    public int tutorialLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!basicShot)
        {
            if (bb.phaseType == 0)
            {
                bb.player.GetComponent<PlayerMovement>().NextPhase();
            }
        }
    }
}
