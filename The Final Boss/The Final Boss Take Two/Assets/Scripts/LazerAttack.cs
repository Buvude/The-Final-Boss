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
        if (!lS.spawn && !lS.despawn)
        {
            lS.spawn = true;
            lS.despawnB();
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


}
