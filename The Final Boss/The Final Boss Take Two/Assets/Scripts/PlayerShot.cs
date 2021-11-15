using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameObject self;
    private PlayerMovement player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        self.transform.Translate(Vector2.up / speed);
        if (self.transform.position.y >= 20)
        {
            Destroy(self);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("made it this far");
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Just passing through");
            player.BasicAttackXPGain();
            Destroy(self);
        }
    }
}
