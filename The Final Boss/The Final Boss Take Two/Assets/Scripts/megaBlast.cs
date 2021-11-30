using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megaBlast : MonoBehaviour
{
   
    public bool sucked;
    public GameObject self;
    public float speed;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        self.transform.Translate(Vector2.up / speed / 2);
        if (self.transform.position.y >= 20/*||self.transform.position.y>= 20*/)
        {
            player.GetComponent<PlayerMovement>().doneWithSpecial();
            Destroy(self);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag.ToString());
        if (collision.gameObject.CompareTag("Boss"))
        {
            player.GetComponent<PlayerMovement>().MegaBlastXPGain();
        }
    }

}
