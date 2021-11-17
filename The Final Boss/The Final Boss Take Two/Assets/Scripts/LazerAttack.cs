using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : MonoBehaviour
{
    public List<GameObject> Lazers;
    public GameObject spawner, self;
    public LazerSpawner lS;
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
        lS.spawn = true;
        spawner.GetComponent<Animator>().SetTrigger("Spawn");
    }
    public void despawn()
    {
        lS.despawn = true;
        lS.despawnB();
    }


}
