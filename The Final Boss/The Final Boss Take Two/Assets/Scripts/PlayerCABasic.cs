using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCABasic : MonoBehaviour
{
    public PlayerMovement PM;
    public bool fireing = false, absorbing = false;
    public AudioSource suckySound, fireingSound, ChargingSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void suckysucky()
    {
        fireing = false;
        absorbing = true;
    }
    public void spittyspitty()
    {
        fireing = true;
        absorbing = false;
        ChargingSound.Play();
    }
    public void downAnItem()
    {
        PM.ItemsInStorage--;
        PM.sonOfSuckySucky.GetComponent<Animator>().SetInteger("ItemsInStorage", PM.ItemsInStorage);
        fireingSound.Play();
    }
    public void despawn()
    {
        PM.suckySucky.SetActive(false);
        suckysucky();
        PM.sonOfSuckySucky.GetComponent<Animator>().enabled = false;
        PM.suckySucky.GetComponent<Animator>().enabled = true;
        PM.sonOfSuckySucky.transform.localScale = new Vector3(7.704051f, 7.704051f, 7.704051f);
        PM.sonOfSuckySucky.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        PM.CounterDone = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Absorbtion collision Detected");
        if (collision.gameObject.CompareTag("BossProjectile") || collision.gameObject.CompareTag("SpecialBossProjectile") && absorbing)
        {
            if (collision.gameObject.GetComponent<VortexAttack>() != null)
            {
                if (!collision.gameObject.GetComponent<VortexAttack>().sucked)
                {
                    if (PM.ItemsInStorage < PM.storage)
                    {
                        collision.gameObject.GetComponent<VortexAttack>().sucked = true;
                        PM.ItemsInStorage++;
                        Debug.Log("You've absorbed a VA");
                        suckySound.Play();
                    }
                }
            }
            else if (collision.gameObject.GetComponent<SafeZoneAttack>() != null)
            {

                if (!collision.gameObject.GetComponent<SafeZoneAttack>().sucked)
                {
                    if (PM.ItemsInStorage < PM.storage)
                    {
                        collision.gameObject.GetComponent<SafeZoneAttack>().sucked = true;
                        PM.ItemsInStorage++;
                        Debug.Log("You've absorbed a SZA");
                        suckySound.Play();
                    }
                }

            }
            else if (collision.gameObject.GetComponent<LazerAttack>() != null)
            {

                if (!collision.gameObject.GetComponent<LazerAttack>().sucked)
                {
                    if (PM.ItemsInStorage < PM.storage)
                    {
                        collision.gameObject.GetComponent<LazerAttack>().sucked = true;
                        PM.ItemsInStorage++;
                        Debug.Log("You've absorbed a LA");
                        suckySound.Play();
                    }
                }

            }
            else if (collision.gameObject.GetComponent<BigBossAttack>() != null)
            {

                if (!collision.gameObject.GetComponent<BigBossAttack>().sucked)
                {
                    if (PM.ItemsInStorage < PM.storage)
                    {
                        collision.gameObject.GetComponent<BigBossAttack>().sucked = true;
                        PM.ItemsInStorage++;
                        Debug.Log("You've absorbed a BBA");
                        suckySound.Play();
                    }
                }

            }
            else if (collision.gameObject.GetComponent<BossAttack>() != null)
            {
                Debug.Log("You've absorbed a BA 1");
                if (!collision.gameObject.GetComponent<BossAttack>().sucked)
                {
                    Debug.Log("You've absorbed a BA 2");
                    if (PM.ItemsInStorage < PM.storage)
                    {
                        collision.gameObject.GetComponent<BossAttack>().sucked = true;
                        PM.ItemsInStorage++;
                        Debug.Log("You've absorbed a BA 3");
                        suckySound.Play();
                    }
                }

            }
        }
        if (collision.gameObject.CompareTag("Boss") && fireing)
        {
            PM.CAXPGain();
        }
    }
}
