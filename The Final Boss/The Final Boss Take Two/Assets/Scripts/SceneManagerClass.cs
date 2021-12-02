using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerClass : MonoBehaviour
{
    
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void skipTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
