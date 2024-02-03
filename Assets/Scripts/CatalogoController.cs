using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CatalogoController : MonoBehaviour
{
    // public Canvas catalogo;
    private int idPuesto = 0;
    public GameObject CraftPiecePrefab, pieceInformationPanel;
    public GameObject catalogoContent;
    private TextMeshProUGUI TitleCatalogIO, PriceCatalogIO;
    private Image ImageCatalogIO;
    PuestoCameraInteraction pci;
    void Start()
    {
        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        StartCoroutine(GetWebData(server_url + "api/PuestoArtesanal/", "getProductos"));

        idPuesto = PlayerPrefs.GetInt("idPuesto");
        //pieceInformationPanel.SetActive(false);
        PlayerPrefs.SetInt("CataloguePieceSelected", 0);
        PlayerPrefs.SetInt("ShowPieceFromCatalogue", 0);
        pci = GameObject.FindGameObjectWithTag("Player").GetComponent<PuestoCameraInteraction>();
    }

    
    void Update()
    {
        
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
                    GameObject CatalogIO = Instantiate(CraftPiecePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    CatalogIO.transform.SetParent(catalogoContent.transform);
                    CatalogIO.transform.localScale = new Vector3(1, 1, 1);
                    CatalogIO.layer = 9;
                    try
                    {
                        GameObject ButtonParent = CatalogIO.transform.GetChild(0).gameObject;
                        ButtonParent.layer = 9;
                        ImageCatalogIO = ButtonParent.transform.GetChild(0).gameObject.GetComponent<Image>();
                        TitleCatalogIO = ButtonParent.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
                        PriceCatalogIO = ButtonParent.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
                        Button button = CatalogIO.transform.GetChild(0).gameObject.GetComponent<Button>();
                        button.name = node[i]["productos"][j]["prod_id"];
                        button.onClick.AddListener(ShowPieceInformation);
                        TitleCatalogIO.text = node[i]["productos"][j]["prod_nombre"].ToString();
                        PriceCatalogIO.text = node[i]["productos"][j]["prod_price"].ToString();
                        loadImagenCatalogo(node[i]["productos"][j]["prod_imagen"].ToString());
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                    
                }
            }
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

    private void loadImagenCatalogo(string url)
    {
        string urlWithoutQuotes = url.Replace("\"", "");
        string[] url2 = urlWithoutQuotes.Split('.');
        string rutaLogo = url2[0].Trim();
        ImageCatalogIO.sprite = Resources.Load<Sprite>(rutaLogo);
    }
    public void ShowPieceInformation()
    {
        var btn = EventSystem.current.currentSelectedGameObject;

        PlayerPrefs.SetInt("CataloguePieceSelected", int.Parse(btn.name));
        PlayerPrefs.SetInt("ShowPieceFromCatalogue", 1);
        pci.ShowPieceCanvas();
        // pieceInformationCanvas.enabled = true;
        // catalogo.enabled = false;
        /*      var image = btn.transform.GetChild(0).GetComponent<Image>();
                var title = btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                var price = btn.transform.GetChild(2).GetComponent<TextMeshProUGUI>();*/



    }


}


/*class TextureTypeToSprite : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetPath.Contains("feria_"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
        }
    }
}*/
