using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSesor : MonoBehaviour
{
    public BossBehavior bb;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            bb.GetComponent<SpriteRenderer>().sprite = bb.Left;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bb.GetComponent<SpriteRenderer>().sprite = bb.Middle;
        }
    }
}
