using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor : MonoBehaviour
{
    private Button tempBtn;
    private bool onButton = false;
    public GameObject self;
    private float horz, vert;
    public float speedMod = 1;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        self.transform.Translate(new Vector2(horz, vert) * Time.deltaTime * speedMod);
        if (Input.GetAxis("Mouse X") != 0||Input.GetAxis("Mouse Y")!=0)
        {
            self.transform.position.Set(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Space)) && onButton)
        {
            tempBtn.onClick.Invoke();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MenuButtons"))
        {
            tempBtn = collision.gameObject.GetComponent<Button>();
            onButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tempBtn = null;
        onButton = false;
    }
}
