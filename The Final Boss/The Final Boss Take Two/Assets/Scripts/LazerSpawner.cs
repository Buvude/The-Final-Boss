using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerSpawner : MonoBehaviour
{
    public Transform target;
    public PathType pathtype = PathType.CatmullRom;
    public bool spawn = false, despawn = false;
    public Vector3[] WayPoints = new Vector3[3];
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
        else if (collision.gameObject.CompareTag("BossProjectile") && despawn)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void resetBool()
    {
        spawn = false;
        despawn = false;
    }
    public void despawnB()
    {
        //self.transform.DOPath(WayPoints, 5f, PathType.CatmullRom, PathMode.Ignore, 10,  Color.red);
        self.GetComponent<Animator>().enabled = false;
        Tween t = target.DOPath(WayPoints, 5, pathtype).SetOptions(true); 
        t.SetEase(Ease.Linear);
        t.SetLink(self).Play();

    }       
}
