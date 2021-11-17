using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerSpawner : MonoBehaviour
{
    public bool spawn = false, despawn = false;
    public Vector3[] WayPoints;
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossProjectile")&&spawn)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void resetBool()
    {
        spawn = false;
        despawn = false;
    }
    public void despawnB()
    {
       // self.transform.DOMove(WayPoints.GetValue(1), 5f, false);
    }       
}
