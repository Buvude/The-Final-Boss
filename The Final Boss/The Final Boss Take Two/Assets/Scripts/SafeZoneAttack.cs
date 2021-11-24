using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneAttack : MonoBehaviour
{
    private int storage;
    public List<Transform> SafeZoneSpawn = new List<Transform>();
    public GameObject Self,SafeZone;
    // Start is called before the first frame update
    void Start()
    {
        storage = Random.Range(0, 4);
        SafeZone.transform.position = SafeZoneSpawn[storage].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Immune = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Immune = false;
        }
    }
    public void moveSZ()
    {
        Debug.Log("Should be random now");
        storage = Random.Range(0, 4);
        SafeZone.transform.position = SafeZoneSpawn[storage].position;
    }
}
