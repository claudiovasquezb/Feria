using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCanvas : MonoBehaviour
{
    public Canvas helpCanvas, startInformationCanvas;
    void Start()
    {
        helpCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Help"))
        {
            if (helpCanvas.enabled == false)
            {
                helpCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (helpCanvas.enabled == true)
            {
                helpCanvas.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }
    public void closeCanvas()
    {
        helpCanvas.enabled = false;
        if(startInformationCanvas.enabled == true)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    public void openCanvas()
    {
        helpCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
