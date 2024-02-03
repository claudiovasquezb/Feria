using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using System;

public class PuestoCanvasPieceInformation : MonoBehaviour
{
    //public Canvas PieceInformationCanvas;
    public GameObject CraftPiece, pieceInformationPanel, dialogPanel;
   //public GameObject parent;
    public static List<CartProductModel> shoppingCart = new List<CartProductModel>();
    public Canvas JoystickCanvas;
    public TextMeshProUGUI titleProduct, descriptionProduct;
    PuestoCameraInteraction pci;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CraftPiece.SetActive(false);
        pci = GameObject.FindGameObjectWithTag("Player").GetComponent<PuestoCameraInteraction>();
    }
    private void Update()
    {
        clearShoppingCart();
    }

    public void HidePieceCanvas()
    {
        CraftPiece.SetActive(false);
        pieceInformationPanel.SetActive(false);
        //PieceInformationCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        JoystickCanvas.enabled = true;
        // print("CraftPiece: " + CraftPiece.transform.childCount);
        if (CraftPiece.transform.childCount > 0)
        {
            Clear(CraftPiece);
        }
        // PlayerPrefs.SetInt("ShowPieceFromCatalogue", 0);
        // print("Catalogue status es 0");
        if (PlayerPrefs.GetInt("ShowPieceFromCatalogue") == 1)
        {
            pci.openCatalogoCanvas();
        }
        
    }
    public void HideDialogPanel()
    {
        dialogPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        JoystickCanvas.enabled = true;
        ShoppingCanvas.emptyCart();
        if (CraftPiece.transform.childCount > 0)
        {
            Clear(CraftPiece);
        }
    }


    public void AddToCart()
    {
        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        StartCoroutine(GetWebData( server_url + "api/PuestoArtesanal/", "getProductos"));

        HidePieceCanvas();
    }

    void ProcessServerResponse(string rawResponse)
    {
        int objectInCart = PlayerPrefs.GetInt("ObjectSelected");
        JSONNode node = JSON.Parse(rawResponse);
        for (int i = 0; i < node.Count; i++)
        {
            for (int j = 0; j < node[i]["productos"].Count; j++)
            {
                if (int.Parse(node[i]["productos"][j]["prod_id"]) == objectInCart)
                {
                    bool isExist = shoppingCart.Exists(x => x.id == objectInCart);
                    if (isExist)
                    {
                        Debug.Log("El producto ya se encuentra en el carrito");
                    }
                    else
                    {
                        shoppingCart.Add(new CartProductModel() { id = int.Parse(node[i]["productos"][j]["prod_id"]), cantidad = 1, nombre = node[i]["productos"][j]["prod_nombre"], precio = int.Parse(node[i]["productos"][j]["prod_price"]) });
                        Debug.Log(node[i]["productos"][j]["prod_nombre"] + " agregado al carrito");

                    }

                    PlayerPrefs.SetInt("ShoppingCount", shoppingCart.Count);
                }
            }
        }
    }

    public void clearShoppingCart()
    {
        if(PlayerPrefs.GetInt("ShoppingCount") == 0)
        {
            shoppingCart.Clear();

        }
    }

    IEnumerator GetWebData(string address, string entity)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + entity);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Algo salio mal: " + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            ProcessServerResponse(www.downloadHandler.text);
        }
    }

    // Borrando todos los hijos de un objeto en especifico
    public void Clear(GameObject go)
    {
        for (int i = go.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(go.transform.GetChild(i).gameObject);
        }
    }
}
