using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;
using System.Threading.Tasks;

public class ProductModel
{
    public string nombre;
    public string descripcion;
}

public class PuestoCameraInteraction : MonoBehaviour
{
    private new Transform camera;

    private float rayDistance = 10;
    public Canvas shoppingCanvas, catalogoCanvas;
    public Canvas floatingJoystick;
    public TextMeshProUGUI descriptionText, titleText, priceText, shoppingDetail;
    public GameObject CraftPiece, PieceInformationPanel, catalogo;
    //public GameObject parent;
    private int idPuesto = 0, catalogueStatus = 0, idCataloguePiece = 0;
    private int idProducto = 0;
    private int indexDelimiter = 0;
    private string piezaArtesanalPath = "";
    private string titleProduct = "", descriptionProduct = "";
    private ShoppingCanvas sc;
    private List<ProductModel> productList = new List<ProductModel>();
    public static string singleTitleProduct = "";
    public static int singlePriceProduct = 0;
    void Start()
    {
        
        idPuesto = PlayerPrefs.GetInt("idPuesto");
        camera = transform.Find("Camera");
        shoppingCanvas.enabled = false;
        CraftPiece.SetActive(false);
        // catalogo.enabled = false;
        sc = GameObject.FindGameObjectWithTag("ShoppingCanvasTag").GetComponent<ShoppingCanvas>();
/*        catalogoTextName = CraftPiecePrefab.transform.Find("TitlePiece").GetComponent<TextMeshProUGUI>();
        catalogoTextPrice = CraftPiecePrefab.transform.Find("PricePiece").GetComponent<TextMeshProUGUI>();*/
    }


     void Update()
     {
        idCataloguePiece = PlayerPrefs.GetInt("CataloguePieceSelected");
        catalogueStatus = PlayerPrefs.GetInt("ShowPieceFromCatalogue", 0);


        if (!EventSystem.current.IsPointerOverGameObject())
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
                    PlayerPrefs.SetInt("ObjectSelected", int.Parse(hit.transform.name));
                    PlayerPrefs.SetInt("ShowPieceFromCatalogue", 0);

                    idProducto = PlayerPrefs.GetInt("ObjectSelected");
                    hit.transform.GetComponent<Interactable>().Interact();

                    // Para heroku 
                    // StartCoroutine(GetWebData("https://feria-3d.herokuapp.com/api/PuestoArtesanal/", "getProductos"));

                    // Para localhost
                    // StartCoroutine(GetWebData("http://localhost:8080/api/PuestoArtesanal/", "getProductos"));

                    // Para servidores de la UV
                    // StartCoroutine(GetWebData("http://api.feria-3d.informatica.uv.cl/api/PuestoArtesanal/", "getProductos"));

                    floatingJoystick.enabled = false;
                    ShowPieceCanvas();
                }


            }
            if (Input.GetButtonDown("Interactable"))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, LayerMask.GetMask("ShoppingCart")))
                {

                    floatingJoystick.enabled = false;
                    sc.ShowShoppingCanvas();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            if (Input.GetButtonDown("Interactable"))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, LayerMask.GetMask("Catalogo")))
                {

                    floatingJoystick.enabled = false;
                    openCatalogoCanvas();
                }
            }


        }


     }

    // Mostrando objeto 3D en el canvas
    void ProcessServerResponse2(string rawResponse)
    {
        int objectInCart = PlayerPrefs.GetInt("ObjectSelected");
        JSONNode node = JSON.Parse(rawResponse);
        for (int i = 0; i < node.Count; i++)
        {
            for (int j = 0; j < node[i]["productos"].Count; j++)
            {
                if (catalogueStatus == 0)
                {
                    if (int.Parse(node[i]["productos"][j]["prod_id"]) == objectInCart)
                    {
                        titleText.text = node[i]["productos"][j]["prod_nombre"];
                        descriptionText.text = node[i]["productos"][j]["prod_descrip"];
                        priceText.text = "Precio: $" + node[i]["productos"][j]["prod_price"];
                        singleTitleProduct = node[i]["productos"][j]["prod_nombre"];
                        singlePriceProduct = int.Parse(node[i]["productos"][j]["prod_price"]);
                        break;
                    }

                }
                else if(catalogueStatus == 1)
                {

                    if (int.Parse(node[i]["productos"][j]["prod_id"]) == idCataloguePiece)
                    {
                        titleText.text = node[i]["productos"][j]["prod_nombre"];
                        descriptionText.text = node[i]["productos"][j]["prod_descrip"];
                        priceText.text = "Precio: $" + node[i]["productos"][j]["prod_price"];
                        singleTitleProduct = node[i]["productos"][j]["prod_nombre"];
                        singlePriceProduct = int.Parse(node[i]["productos"][j]["prod_price"]);
                        break;
                    }

                }


            }
        }
    }
    IEnumerator GetWebData2(string address, string entity)
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
            ProcessServerResponse2(www.downloadHandler.text);
        }
    }

    public async void ShowPieceCanvas()
    {
        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        await AsyncGetWebData( server_url + "api/PuestoArtesanal/", "getProductos" );

        catalogo.SetActive(false);
        CraftPiece.SetActive(true);
        PieceInformationPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        



    }

    
    void ProcessServerResponse(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        for (int i = 0; i < node.Count; i++)
        {
            if (node[i]["pArt_id"] == idPuesto)
            {
                for (int j = 0; j < node[i]["productos"].Count; j++)
                { 
                    if(catalogueStatus == 0)
                    {

                        if (int.Parse(node[i]["productos"][j]["prod_id"]) == idProducto)
                        {
                            productList.Add(new ProductModel() { nombre = node[i]["productos"][j]["prod_nombre"], descripcion = node[i]["productos"][j]["prod_descrip"] });
                            SetPieceData(
                                node[i]["productos"][j]["prod_nombre"],
                                node[i]["productos"][j]["prod_descrip"],
                                "Precio: $" + node[i]["productos"][j]["prod_price"]
                            );
                            Render3DModel(
                                node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.'),
                                node[i]["productos"][j]["prod_modelo3D"].ToString(),
                                float.Parse(node[i]["productos"][j]["prod_scale"], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"))
                            );

                            // Creando orden de compra con paypal
                            NewStoreAction.INSTANCE.OpenPurchaseItemScreen(
                                node[i]["productos"][j]["prod_nombre"],
                                node[i]["productos"][j]["prod_descrip"],
                                node[i]["productos"][j]["prod_price"]
                                );
                            break;
                        }
                    } else if(catalogueStatus == 1)
                    {
                        if (int.Parse(node[i]["productos"][j]["prod_id"]) == idCataloguePiece)
                        {
                            PlayerPrefs.SetInt("ObjectSelected", int.Parse(node[i]["productos"][j]["prod_id"]));
                            productList.Add(new ProductModel() { nombre = node[i]["productos"][j]["prod_nombre"], descripcion = node[i]["productos"][j]["prod_descrip"] });
                            print("Nombre desde catalogo: " + node[i]["productos"][j]["prod_nombre"]);
                            SetPieceData(
                                node[i]["productos"][j]["prod_nombre"],
                                node[i]["productos"][j]["prod_descrip"],
                                "Precio: $" + node[i]["productos"][j]["prod_price"]
                            );
                            Render3DModel(
                                node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.'),
                                node[i]["productos"][j]["prod_modelo3D"].ToString(),
                                float.Parse(node[i]["productos"][j]["prod_scale"], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"))
                            );

                            // Creando orden de compra con paypal
                            NewStoreAction.INSTANCE.OpenPurchaseItemScreen(
                                node[i]["productos"][j]["prod_nombre"],
                                node[i]["productos"][j]["prod_descrip"],
                                node[i]["productos"][j]["prod_price"]
                                );
                            break;
                        }
                    }

                }
            }
        }
    }

    public async Task<string> AsyncGetWebData(string address, string entity)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + entity);
        www.SendWebRequest();
        while (!www.isDone)
        {
            await Task.Yield();
        }
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Algo salio mal: " + www.error);
            return www.error;
        }
        else
        {
            ProcessServerResponse(www.downloadHandler.text);
            return null;
        }
    }

    public static void SetLayer(GameObject parent, int layer, bool includeChildren = true)
    {
        parent.layer = layer;
        if (includeChildren)
        {
            foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = layer;
            }
        }
    }
    public void openCatalogoCanvas()
    {
        catalogo.SetActive(true);
        catalogoCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void closeCatalogoCanvas()
    {
        catalogo.SetActive(false);
        catalogoCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetPieceData(string title, string description, string price) {
        titleText.text = title;
        descriptionText.text = description;
        priceText.text = price;
    }

    private void Render3DModel(int Delimiter, string Path, float scale)
    {
        indexDelimiter = Delimiter;
        piezaArtesanalPath = Path.Substring(1, indexDelimiter - 1);
        GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
        GameObject io = Instantiate(pa, new Vector3(0, -0.7f, 0), Quaternion.identity);
        io.transform.parent = CraftPiece.transform;
        io.transform.localPosition = new Vector3(0, -0.7f, 0);
        io.transform.localScale = new Vector3(scale, scale, scale);
        io.layer = 5;
        SetLayer(io, 5);
        io.AddComponent<PieceDetailInformation>();
        CraftPiece.AddComponent<GrabRotation>();
        MeshRenderer renderer = CraftPiece.GetComponentInChildren<MeshRenderer>();
        BoxCollider col = CraftPiece.GetComponent<BoxCollider>();
        col.center = new Vector3(0, -0.7f, 0);
        col.size = renderer.bounds.size;
    }

}
