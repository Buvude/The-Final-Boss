using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildDeflectingScript : MonoBehaviour
{
    
    public bool MadeInvincable;
    public PlayerMovement pm;
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
        Debug.Log("Entered shield trigger");
        if (collision.gameObject.CompareTag("BossProjectile"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SpecialBossProjectile"))
        {
            MadeInvincable = true;
            pm.Immune = true;
        }
    }
}
