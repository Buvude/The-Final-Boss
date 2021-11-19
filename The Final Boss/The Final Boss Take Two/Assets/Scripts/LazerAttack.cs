using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : MonoBehaviour
{
    public List<GameObject> Lazers;
    public GameObject spawner, self;
    public LazerSpawner lS;
    public BossBehavior bB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn()
    {
        if (!lS.spawn && !lS.despawn)
        {
            lS.spawn = true;
            lS.SpawnB();
            Debug.Log("post spawn message");
        }
    }
    public void despawn()
    {
        if (!lS.spawn && !lS.despawn)
        {
            lS.despawn = true;
            lS.despawnB();
        }
    }
    IEnumerator Shoot()
    {
        Debug.Log("Corutine started");
        for (int i = 0; i < bB.phaseNumber+1; i++)
        {
            GameObject[] cloneGO=new GameObject[5];
            Lazers.CopyTo(cloneGO);
            Debug.Log(cloneGO.Length.ToString());
            yield return new WaitForSeconds(1);
            Debug.Log("start first for loop");
            int tempRandHold;
            tempRandHold=Random.Range(0, 5);
            Debug.Log(tempRandHold.ToString());
            Debug.Log(Lazers.Count.ToString());
            /*for (int ii = 0; ii < Lazers.Count; ii++)
            {

                if (ii != tempRandHold)
                {
                    Debug.Log("You have made it far young one");
                    Lazers[ii].GetComponent<Animator>().SetTrigger("Charge");
                }
                yield return new WaitForSeconds(5f);

            }*/
        }
        //despawn();
        
    }
    public void startCRShoot()
    {
        Debug.Log("If you've gotten this far, how has corutine not started?");
        _ = StartCoroutine("Shoot");
    }


}
