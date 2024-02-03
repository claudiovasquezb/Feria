using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ConfirmCartCanvas : MonoBehaviour
{
    public int numeroEscena;
    private Transform puestoArtesanal;
    private string nombrePuesto;
    private int idPuesto;
    public Canvas confirmCanvas;
    List<CartProductModel> cart = new List<CartProductModel>();
    void Start()
    {
        confirmCanvas.enabled = false;
    }
    private void Update()
    {
        cart = PuestoCanvasPieceInformation.shoppingCart;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(cart.Count > 0)
            {
                confirmCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(cart.Count <= 0)
            {
                SceneManager.LoadScene(numeroEscena);
            }
        }
    }
    public void confirmCart()
    {
        confirmCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void denyCart()
    {
        PlayerPrefs.SetInt("ShoppingCount", 0);
        SceneManager.LoadScene(numeroEscena);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
