using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject self;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        self.transform.Translate(Vector2.down/speed);
        if (self.transform.position.y <= -12)
        {
            Destroy(self);
        }
    }
}
