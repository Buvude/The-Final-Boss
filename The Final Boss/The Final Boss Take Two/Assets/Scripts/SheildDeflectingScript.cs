using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildDeflectingScript : MonoBehaviour
{
    public GameObject self;
    public bool MadeInvincable;
    public PlayerMovement pm;
    public AudioSource dink;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void selfDestruct()
    {
        self.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered shield trigger");
        if (collision.gameObject.CompareTag("BossProjectile"))
        {
            Destroy(collision.gameObject);
            pm.sheildSpecialGain();
            dink.Play();
        }
        else if (collision.gameObject.CompareTag("SpecialBossProjectile"))
        {
            MadeInvincable = true;
            pm.Immune = true;
            pm.sheildSpecialGain();
            dink.Play();
            dink.Play();
        }
    }
}
