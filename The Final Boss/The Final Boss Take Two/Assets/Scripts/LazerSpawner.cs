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
    public GameObject self, despawnObject;
    public LazerAttack lA;
    public BossBehavior bB;
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
        StartCoroutine("PostLazerCooldown");
        
    }
    public void SpawnB()
    {
        //self.transform.DOPath(WayPoints, 5f, PathType.CatmullRom, PathMode.Ignore, 10,  Color.red);
        self.GetComponent<Animator>().enabled = false;
        Tween t = target.DOPath(WayPoints, 5, pathtype).SetOptions(true);
        t.SetEase(Ease.Linear);
        t.SetLink(self).Play();
        StartCoroutine("lazerCooldown");

    }
    IEnumerator lazerCooldown()
    {
        Debug.Log("This shit is for our lazer fans!");
        yield return new WaitForSeconds(5);
        Debug.Log("The wait has ended");
        resetBool();
        lA.startCRShoot();
    }
    IEnumerator PostLazerCooldown()
    {
        yield return new WaitForSeconds(5);
        resetBool();
        bB.postLazer();

    }


}
