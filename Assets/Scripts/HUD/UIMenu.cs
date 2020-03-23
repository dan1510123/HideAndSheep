using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onClick();
        }
    }

    public void onClick()
    {
        if (Time.timeScale == 1)
        {
            Debug.Log("FREEZE");
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("EVERYBODY CLAP YOUR HANDS");
            Time.timeScale = 1;
        }

    }
}
