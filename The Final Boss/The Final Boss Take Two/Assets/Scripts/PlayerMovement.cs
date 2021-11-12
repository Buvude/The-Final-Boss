using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedMod = 1;
    private float horz, vert;
    public GameObject self, AnimationHolder, Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        AnimationHolder.transform.Translate(new Vector2(horz, vert) * Time.deltaTime * speedMod);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(Boss.GetComponent<BossBehavior>().phaseType==4)
            {
                Boss.GetComponent<BossBehavior>().phaseType = 0;
            }
            else
            {
                Boss.GetComponent<BossBehavior>().phaseType += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Boss.GetComponent<BossBehavior>().phaseNumber += 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Boss.GetComponent<BossBehavior>().debugFire();
        }
    }
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         Debug.Log("collision");
         if (collision.GetComponentInParent<GameObject>().CompareTag("Boss"))
         {
             PlayerDeath();
         }
         else
         {
             Debug.Log("Invalid Tag");
         }
     }*/ //Old version of what is below, I figured it out finially....
    private void OnTriggerEnter2D(Collider2D collision)
    {
           {
            Debug.Log("collision");
            if (collision.gameObject.CompareTag("Boss"))
            {
                PlayerDeath();
            }
            else
            {
                Debug.Log("Invalid Tag");
            }
        }  
    }
    
    public void PlayerDeath()
    {
        Debug.Log("Add death soon ");
    }
}
