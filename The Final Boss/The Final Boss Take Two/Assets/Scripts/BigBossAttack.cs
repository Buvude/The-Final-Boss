using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossAttack : MonoBehaviour
{
    public bool sucked;
    public GameObject self;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {

            self.transform.Translate(Vector2.down / speed / 2);
            if (self.transform.position.y <= -12/*||self.transform.position.y>= 20*/)
            {
                Destroy(self);
            }
        }
    }
}
