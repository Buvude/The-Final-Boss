using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class BossBehavior : MonoBehaviour
{
    public int phaseNumber, phaseType, totalPhase;//Phase type: 1=basic, 2= bit, 3=Lazer, 4=vortex, 5=safe Zone
    //phaseNumber is the number of shots per attack fired
    public GameObject player, self, animationHolder;
    public Animator bA;
    // Start is called before the first frame update
    void Start()
    {
        //Boss comes in and starts monolouge (attack boss to skip)
        StartCoroutine("Monolouge");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Monolouge()
    {
        yield return new WaitForSeconds(5);
    }
}
