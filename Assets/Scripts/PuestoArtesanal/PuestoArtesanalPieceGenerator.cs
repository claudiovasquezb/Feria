using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using UnityEngine.UI;
using TMPro;

public class PuestoArtesanalPieceGenerator : MonoBehaviour
{



    private int idPuesto = 0;
    private int indexDelimiter = 0;
    private string piezaArtesanalPath = "";
    [SerializeField] private GameObject puestoArtesanalPadre;
    [SerializeField] private Canvas tecnica, logoCanvas;
    void Start()
    {
        idPuesto = PlayerPrefs.GetInt("idPuesto");

        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        StartCoroutine(GetWebData(server_url + "api/PuestoArtesanal/getProductos/", idPuesto.ToString()));

    }

    void ProcessServerResponse(string rawResponse)
    {
        // That text, is actually JSOn info, so we need to 
        // parse that into something we can navigate.
        JSONNode node = JSON.Parse(rawResponse);
        GameObject listadoPiezasArtesanales = new GameObject();
        listadoPiezasArtesanales.name = "Lista de piezas artesanales";
        listadoPiezasArtesanales.transform.SetParent(puestoArtesanalPadre.transform, false);
        loadImagenLogo(node["pArt_logo"].ToString(), node["pArt_nombre"].ToString());
        setTecnicaImage();

        float x = -3.5f;
        float y = 0f;
        float z = -0.5f;
        for (int i = 0; i < node["productos"].Count; i++)
        {
            if(i < 9)
            {
                // Instanciando objetos dentro de la repisa de la izquierda
                if (z > 3.5f)
                {
                    z = -0.5f;
                }
                if (i < 3)
                {
                    y = 1f;
                }
                else if (i >= 3 && i < 6)
                {
                    y = -0.15f;
                }
                else if (i >= 6 && i < 9)
                {
                    y = -1.4f;
                }
                indexDelimiter = node["productos"][i]["prod_modelo3D"].ToString().IndexOf('.');
                piezaArtesanalPath = node["productos"][i]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                GameObject io = Instantiate(pa, new Vector3(-7f, y, z), Quaternion.identity);
                io.transform.SetParent(listadoPiezasArtesanales.transform, false);
                io.AddComponent<PieceDetailInformation>();
                MeshRenderer renderer = io.GetComponentInChildren<MeshRenderer>();
                BoxCollider col = io.AddComponent<BoxCollider>();
                col.size = renderer.bounds.size;
                io.transform.localScale = new Vector3(node["productos"][i]["prod_scale"], node["productos"][i]["prod_scale"], node["productos"][i]["prod_scale"]);
                io.layer = 6;
                io.name = node["productos"][i]["prod_id"];
                z += 2f;
            }
            if(i >= 9 && i < 18)
            {
                // Instanciando objetos dentro de la repisa del frente
                if (x > 0.5f)
                {
                    x = -3.5f;
                }
                if (i < 12)
                {
                    y = 1f;
                }
                else if (i >= 12 && i < 15)
                {
                    y = -0.2f;
                }
                else if (i >= 15 && i < 18)
                {
                    y = -1.4f;
                }
                indexDelimiter = node["productos"][i]["prod_modelo3D"].ToString().IndexOf('.');
                piezaArtesanalPath = node["productos"][i]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                GameObject io = Instantiate(pa, new Vector3(x, y, 6.5f), Quaternion.identity);
                io.transform.SetParent(listadoPiezasArtesanales.transform, false);
                io.AddComponent<PieceDetailInformation>();
                MeshRenderer renderer = io.GetComponentInChildren<MeshRenderer>();
                BoxCollider col = io.AddComponent<BoxCollider>();
                col.size = renderer.bounds.size;
                io.transform.localScale = new Vector3(node["productos"][i]["prod_scale"], node["productos"][i]["prod_scale"], node["productos"][i]["prod_scale"]);
                io.layer = 6;
                io.name = node["productos"][i]["prod_id"];
                x += 2f;
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
    private void loadImagenLogo(string url, string nombrePuesto)
    {
        Image logoPuesto = logoCanvas.transform.Find("Image").GetComponent<Image>();

        //logoCanvas.enabled = true;
        string urlWithoutQuotes = url.Replace("\"", "");
        string[] url2 = urlWithoutQuotes.Split('.');
        string rutaLogo = url2[0].Trim();
        if (rutaLogo.Length > 4)
        {

            logoPuesto.sprite = Resources.Load<Sprite>(rutaLogo);
        }
        else
        {
            //logoCanvas.enabled = false;
            LogoTemporal(nombrePuesto);
        }

    }
    private void LogoTemporal(string nombrePuesto)
    {
        TMP_Text logoTexto = logoCanvas.transform.Find("Nombre").GetComponent<TMP_Text>();
        //advertisingImageCanvas.enabled = true;
        string urlWithoutQuotes = nombrePuesto.Replace("\"", "");
        logoTexto.text = urlWithoutQuotes;
    }

    private void setTecnicaImage()
    {
        int idTecnica = PlayerPrefs.GetInt("idTecnica");
        Image tecnicaImagen = tecnica.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI tecnicaTexto = tecnica.transform.Find("Tecnica").GetComponent<TextMeshProUGUI>();
        switch (idTecnica)
        {
            case 1:
                tecnicaImagen.sprite = Resources.Load<Sprite>("madera");
                tecnicaTexto.text = "Tallado";
                break;
            case 2:
                tecnicaImagen.sprite = Resources.Load<Sprite>("cesteria");
                tecnicaTexto.text = "Cesteria";
                break;
            case 3:
                tecnicaImagen.sprite = Resources.Load<Sprite>("textileria");
                tecnicaTexto.text = "Textileria";
                break;
            case 4:
                tecnicaImagen.sprite = Resources.Load<Sprite>("modelado");
                tecnicaTexto.text = "Modelado";
                break;
            case 5:
                tecnicaImagen.sprite = Resources.Load<Sprite>("repujado");
                tecnicaTexto.text = "Repujado";
                break;
            default:
                break;
        }
        
    }
}
