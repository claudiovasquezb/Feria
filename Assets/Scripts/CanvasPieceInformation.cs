using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPieceInformation : MonoBehaviour
{
    public Canvas PieceInformationCanvas;
    public Canvas MinimapCanvas;
    private GameObject test;
    void Start()
    {
        test = PieceInformationCanvas.transform.GetChild(2).gameObject;
        test.SetActive(false);
    }

    public void HidePieceCanvas()
    {

           test.SetActive(false);
           PieceInformationCanvas.enabled = false;
           Cursor.lockState = CursorLockMode.Locked;
           MinimapCanvas.enabled = true;
    }
}
