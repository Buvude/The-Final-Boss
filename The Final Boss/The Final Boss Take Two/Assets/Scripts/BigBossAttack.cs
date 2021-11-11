using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossAttack : MonoBehaviour
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
        self.transform.Translate(Vector2.down/speed/2);
        if (self.transform.position.y <= - 12)
        {
            Destroy(self);
        }
    }
}
