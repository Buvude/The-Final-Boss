using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSound : MonoBehaviour
{
    private GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shootSound()
    {
        self.GetComponent<AudioSource>().Play();
    }
}
