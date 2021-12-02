using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : MonoBehaviour
{
    public bool sucked, paused;
    public List<GameObject> Lazers;
    public GameObject spawner, self;
    public LazerSpawner lS;
    private BossBehavior bB;
    // Start is called before the first frame update
    void Start()
    {
        bB = GameObject.Find("BossAnimate").GetComponentInChildren<BossBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        paused = bB.paused;
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
        for (int i = 0; i < bB.phaseNumber; i++)
        {
            List<GameObject> cloneGO = new List<GameObject>(Lazers);
            //Debug.Log(cloneGO.Length.ToString());
            yield return new WaitForSeconds(1);
            while (paused)
            {
                yield return new WaitForEndOfFrame();
            }
            //Debug.Log("start first for loop");
            int tempRandHold;
            tempRandHold=Random.Range(0, 5);
            Debug.Log(tempRandHold.ToString());
            Debug.Log(Lazers.Count.ToString());
            cloneGO.RemoveAt(tempRandHold);
            foreach(GameObject item in cloneGO)//holy shit this loop actually worked? I should not be suprised but still...
            {
                item.GetComponent<Animator>().SetTrigger("Charge");
                //item.GetComponentInParent<AudioSource>().Play();
                self.GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(5);
            while (paused)
            {
                yield return new WaitForEndOfFrame();
            }

            /*if (tempRandHold != 4)
            {
                for (int ii = tempRandHold - 1; ii < cloneGO.Length; ii++)
                {

                }
                
            }
            else
            {

            }*/
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
        despawn();
        
    }
    public void startCRShoot()
    {
        Debug.Log("If you've gotten this far, how has corutine not started?");
        _ = StartCoroutine("Shoot");
    }
    


}
