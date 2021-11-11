using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public int phaseNumber, phaseType, totalPhase;//Phase type: 0=basic, 1= big, 2=Lazer, 3=vortex, 4=safe Zone
    //phaseNumber is the number of shots per attack fired, totalPhase is just to keep track of score at the end and maybe other stuff later
    public GameObject player, self, animationHolder;
    public List<GameObject> weaponType = new List<GameObject>();
    public Animator bA;
    public Text monolaougeTXT;
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
        monolaougeTXT.gameObject.SetActive(false);
        
    }
    public void debugFire()
    {
        StartCoroutine("Fire");
    }
    
    IEnumerator Fire()
    {
        for (int i = 0; i < phaseNumber; i++)
        {
            Instantiate(weaponType[phaseType],animationHolder.transform);
            yield return new WaitForSeconds(1);
        }
        
    }
}
