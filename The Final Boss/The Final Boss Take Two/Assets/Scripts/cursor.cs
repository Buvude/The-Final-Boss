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
    public Vector3 spawn = new Vector3(6.98999977f, -0.379999995f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Bounds setting
         */
        if (self.transform.position.x >= 15.76)
        {
            self.transform.Translate(-1f, 0, 0);
        }
        if (self.transform.position.x <= -15.77)
        {
            self.transform.Translate(1f, 0, 0);
        }
        if (self.transform.position.y >= 8.06)
        {
            self.transform.Translate(0, -1f, 0);
        }
        if (self.transform.position.y <= -8.03)
        {
            self.transform.Translate(0, 1f, 0);
        }
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        self.transform.Translate(new Vector2(horz, vert) * /*Time.deltaTime */ speedMod/110);
        if (Input.GetAxis("Mouse X") != 0||Input.GetAxis("Mouse Y")!=0)
        {
            self.transform.position.Set(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Space)) && onButton)
        {
            tempBtn.onClick.Invoke();
        }

    }
    public void newMenu()
    {
        self.transform.SetPositionAndRotation(spawn, new Quaternion());
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
