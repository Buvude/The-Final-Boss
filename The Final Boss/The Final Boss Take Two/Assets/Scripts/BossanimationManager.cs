using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossanimationManager : MonoBehaviour
{
    public float trackingSpeed;
    public GameObject Player, self;
    private Vector2 target1;
    private bool moveTowardsPlayer=true;//true for testing purposes only
    public Animator bAA;
    public BossBehavior bB;
    // Start is called before the first frame update
    void Start()
    {
        target1.Set(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        target1.x = Player.gameObject.transform.position.x;
        if (moveTowardsPlayer)
        {
            StartCoroutine("movetowardsPlayer");
            moveTowardsPlayer = false;
        }
    }
    public void SetMoveTowardsPlayer()
    {
        moveTowardsPlayer = true;
        bAA.enabled.Equals(false);
    }
    public void SetMoveTowardsPlayerF()
    {
        moveTowardsPlayer = false;
        bAA.enabled.Equals(false);
    }
    IEnumerator movetowardsPlayer()
    {
        yield return new WaitForSeconds(3);
        self.transform.Translate(new Vector2(Player.transform.position.x,0f));
        bB.debugFire();
        yield return new WaitForSeconds(1);
        self.transform.Translate(new Vector2(-Player.transform.position.x, 0f));

    }
}
