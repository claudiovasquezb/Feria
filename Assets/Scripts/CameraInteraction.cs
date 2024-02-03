using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraInteraction : MonoBehaviour
{
    private new Transform camera;

    private float rayDistance = 2;
    public Canvas PieceInformationCanvas;
    public Canvas MinimapCanvas;
    public Text titleText, descriptionText;
    private GameObject test;

    void Start()
    {
        camera = transform.Find("Camera");
        PieceInformationCanvas.enabled = false;
        test = PieceInformationCanvas.transform.GetChild(2).gameObject;
        test.SetActive(false);
    }


    void Update()
    {
        // Parametros:
        // 1) Desde donde iniciara el rayo
        // 2) Hacia donde se dirige el rayo (hacia adelante(eje x))
        // 3) Color del rayo
        Debug.DrawRay(camera.position, camera.forward * rayDistance, Color.red);
        if (Input.GetButtonDown("Interactable"))
        {
            // RaycastHit hit contiene la informacion del objeto que estamos mirando
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, LayerMask.GetMask("Interactable")))
            {
                hit.transform.GetComponent<Interactable>().Interact();
                //ShowPieceCanvas();
            }
        }

    }
    public void ShowPieceCanvas()
    {
        string title = "Titulo Pieza Artesanal";
        string description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        titleText.text = title;
        descriptionText.text = description;
        test.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        PieceInformationCanvas.enabled = true;
        MinimapCanvas.enabled = false;
    }
    public void HidePieceCanvas()
    {

        test.SetActive(false);
        PieceInformationCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        MinimapCanvas.enabled = true;
    }
}
    