using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartInformationCanvas : MonoBehaviour
{
    public Canvas startInformationCanvas;
    private Toggle m_toggle;
    public Canvas joystickCanvas;
    void Start()
    {
        // PlayerPrefs.SetInt("startInformationStatus", 0);
        if (PlayerPrefs.GetInt("startInformationStatus") == 0)
        {
            m_toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
            startInformationCanvas.enabled = true;
            joystickCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            m_toggle.onValueChanged.AddListener(delegate {
                ToggleValueChanged(m_toggle);
            });
        }
        else
        {
            HideCanvas();
        }



    }

    public void HideCanvas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startInformationCanvas.enabled = false;
        joystickCanvas.enabled = true;
        
    }
    void ToggleValueChanged(Toggle change)
    {
        HideCanvas();
        PlayerPrefs.SetInt("startInformationStatus", 1);
        
    }
}
