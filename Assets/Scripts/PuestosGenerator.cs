using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class PuestosGenerator : MonoBehaviour
{
    // SerializeField me permite ver la variable desde Unity sin tener que poner la variable publica
    [SerializeField] GameObject myPrefab;
    private GameObject nuevoPuesto;
    private GameObject cartel;
    private GameObject canvasCube;
    private TextMeshProUGUI nameText;
    private Canvas pieceImage1;
    private Image image1;
    [SerializeField] GameObject[] piecePrefabs;
    // Variables para las coordenadas de los puestos artesanales
    float xLateralIzquierda = 65.4f;
    float yLateralIzquierda = 0.05f;
    float zLateralIzquierda = 57.2f;
    float xCentralIzquierda = 82.5f;
    float yCentralIzquierda = 0.05f;
    float zCentralIzquierda = 57.8f;
    float xCentralDerecha = 82.8f;
    float yCentralDerecha = 0.05f;
    float zCentralDerecha = 57.2f;
    float xLateralDerecha = 99.5f;
    float yLateralDerecha = 0.05f;
    float zLateralDerecha = 57.8f;

    private int tecnicaId = 1;
    private int indexDelimiter = 0;
    private string piezaArtesanalPath = "";

    // Start is called before the first frame update
    void Start()
    {
        tecnicaId = PlayerPrefs.GetInt("idTecnica", 1);

        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        StartCoroutine(GetWebData(server_url + "api/PuestoArtesanal/getProductos/tecnica/", tecnicaId.ToString()));

    }

    void ProcessServerResponse(string rawResponse)
    {
        // That text, is actually JSOn info, so we need to 
        // parse that into something we can navigate.
        JSONNode node = JSON.Parse(rawResponse);
        GameObject listaPuestos = new GameObject();
        listaPuestos.name = "Lista de Puestos";
        for (int i = 0; i < node.Count; i++)
        {
            // Instanciando puestos artesanales de la fila izquierda
            if (node[i]["tec_id"] == tecnicaId && i < 10)
            {
                GameObject puestoPadre = new GameObject();
                puestoPadre.name = "Puesto_" + node[i]["pArt_nombre"];
                nuevoPuesto = Instantiate(myPrefab, new Vector3(xLateralIzquierda, yLateralIzquierda, zLateralIzquierda), Quaternion.Euler(0, 270, 0));
                nuevoPuesto.name = node[i]["pArt_id"];
                nuevoPuesto.transform.SetParent(puestoPadre.transform);
                puestoPadre.transform.SetParent(listaPuestos.transform);
                cartel = nuevoPuesto.transform.GetChild(3).gameObject;
                canvasCube = cartel.transform.GetChild(0).gameObject;
                nameText = canvasCube.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                nameText.text = node[i]["pArt_nombre"];
                // Instanciando objetos dentro de los puestos de la fila de la izquierda
                //zPiece = zLateralIzquierda;
                var zPiece = 55.4f + (6 * i);
                var zPiece2 = 58.3f + (6 * i);
                for (int j = 0; j < 6; j++)
                {
                    if(node[i]["productos"][j]["prod_modelo3D"].ToString().Length > 4)
                    {
                        indexDelimiter = node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.');
                        piezaArtesanalPath = node[i]["productos"][j]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                        GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                        if (j == 0)
                        {
                            GameObject io = Instantiate(pa, new Vector3(69f, 2.45f, zPiece), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if(j == 1)
                        {
                            GameObject io = Instantiate(pa, new Vector3(65.7f, 2.45f, zPiece2), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 2)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image1").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 3)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image2").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 4)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image3").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 5)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image4").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }

                    }

                }
                zLateralIzquierda += 6;
            }
            // Instanciando puestos artesanales de la fila central izquierda
            if (node[i]["tec_id"] == tecnicaId && i >= 10 && i < 19)
            {
                GameObject puestoPadre = new GameObject();
                puestoPadre.name = "Puesto_" + node[i]["pArt_nombre"];
                nuevoPuesto = Instantiate(myPrefab, new Vector3(xCentralIzquierda, yCentralIzquierda, zCentralIzquierda), Quaternion.Euler(0f, 90f, 0f));
                nuevoPuesto.name = node[i]["pArt_id"];
                nuevoPuesto.transform.SetParent(puestoPadre.transform);
                puestoPadre.transform.SetParent(listaPuestos.transform);
                cartel = nuevoPuesto.transform.GetChild(3).gameObject;
                canvasCube = cartel.transform.GetChild(0).gameObject;
                nameText = canvasCube.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                nameText.text = node[i]["pArt_nombre"];
                // Instanciando objetos dentro de los puestos de la fila central izquierda
                //zPiece = zCentralIzquierda - 2;
                var zPiece = 59.6f + (6 * (i - 10));
                var zPiece2 = 57f + (6 * (i - 10));
                for (int j = 0; j < 6; j++)
                {
                    if (node[i]["productos"][j]["prod_modelo3D"].ToString().Length > 4)
                    {
                        indexDelimiter = node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.');
                        piezaArtesanalPath = node[i]["productos"][j]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                        GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                        if (j == 0)
                        {
                            GameObject io = Instantiate(pa, new Vector3(79f, 2.45f, zPiece), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 1)
                        {
                            GameObject io = Instantiate(pa, new Vector3(82.1f, 2.45f, zPiece2), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 2)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image1").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 3)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image2").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 4)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image3").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 5)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image4").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }

                    }

                }
                zCentralIzquierda += 6;
            }

            // Instanciando puestos artesanales de la fila central derecha
            if (node[i]["tec_id"] == tecnicaId && i >= 19 && i < 28)
            {
                GameObject puestoPadre = new GameObject();
                puestoPadre.name = "Puesto_" + node[i]["pArt_nombre"];
                nuevoPuesto = Instantiate(myPrefab, new Vector3(xCentralDerecha, yCentralDerecha, zCentralDerecha), Quaternion.Euler(0, 270, 0));
                nuevoPuesto.name = node[i]["pArt_id"];
                nuevoPuesto.transform.SetParent(puestoPadre.transform);
                puestoPadre.transform.SetParent(listaPuestos.transform);
                cartel = nuevoPuesto.transform.GetChild(3).gameObject;
                canvasCube = cartel.transform.GetChild(0).gameObject;
                nameText = canvasCube.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                nameText.text = node[i]["pArt_nombre"];
                // Instanciando objetos dentro de los puestos de la fila central derecha
                //zPiece = zCentralDerecha;
                var zPiece = 55.4f + (6 * (i - 19));
                var zPiece2 = 58.3f + (6 * (i - 19));
                for (int j = 0; j < 6; j++)
                {
                    if (node[i]["productos"][j]["prod_modelo3D"].ToString().Length > 4)
                    {
                        indexDelimiter = node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.');
                        piezaArtesanalPath = node[i]["productos"][j]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                        GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                        if (j == 0)
                        {
                            GameObject io = Instantiate(pa, new Vector3(86f, 2.45f, zPiece), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 1)
                        {
                            GameObject io = Instantiate(pa, new Vector3(83.2f, 2.45f, zPiece2), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 2)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image1").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 3)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image2").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 4)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image3").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 5)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image4").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }

                    }

                }
                zCentralDerecha += 6;
            }
            // Instanciando puestos artesanales de la fila de la derecha
            if (node[i]["tec_id"] == tecnicaId && i >= 28 && i < 38)
            {
                GameObject puestoPadre = new GameObject();
                puestoPadre.name = "Puesto_" + node[i]["pArt_nombre"];
                nuevoPuesto = Instantiate(myPrefab, new Vector3(xLateralDerecha, yLateralDerecha, zLateralDerecha), Quaternion.Euler(0f, 90f, 0f));
                nuevoPuesto.name = node[i]["pArt_id"];
                nuevoPuesto.transform.SetParent(puestoPadre.transform);
                puestoPadre.transform.SetParent(listaPuestos.transform);
                cartel = nuevoPuesto.transform.GetChild(3).gameObject;
                canvasCube = cartel.transform.GetChild(0).gameObject;
                nameText = canvasCube.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                nameText.text = node[i]["pArt_nombre"];
                // Instanciando objetos dentro de los puestos de la fila de la derecha
                //zPiece = zLateralDerecha - 2;
                var zPiece = 59.5f + (6 * (i - 28));
                var zPiece2 = 56.7f + (6 * (i - 28));
                for (int j = 0; j < 6; j++)
                {
                    if (node[i]["productos"][j]["prod_modelo3D"].ToString().Length > 4)
                    {
                        indexDelimiter = node[i]["productos"][j]["prod_modelo3D"].ToString().IndexOf('.');
                        piezaArtesanalPath = node[i]["productos"][j]["prod_modelo3D"].ToString().Substring(1, indexDelimiter - 1);
                        GameObject pa = Resources.Load<GameObject>(piezaArtesanalPath);
                        if (j == 0)
                        {
                            GameObject io = Instantiate(pa, new Vector3(96f, 2.45f, zPiece), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 1)
                        {
                            GameObject io = Instantiate(pa, new Vector3(99f, 2.45f, zPiece2), Quaternion.identity);
                            io.transform.localScale = new Vector3(node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"], node[i]["productos"][j]["prod_scale"]);
                            io.layer = 6;
                            io.name = node[i]["productos"][j]["prod_nombre"];
                            io.transform.SetParent(puestoPadre.transform);
                        }
                        if (j == 2)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image1").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 3)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage1").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image2").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 4)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image3").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }
                        if (j == 5)
                        {
                            pieceImage1 = nuevoPuesto.transform.Find("PieceImage2").GetComponent<Canvas>();
                            image1 = pieceImage1.transform.Find("Image4").GetComponent<Image>();
                            string rutaPieceImage = loadImagenLogo(node[i]["productos"][j]["prod_imagen"]);
                            image1.sprite = Resources.Load<Sprite>(rutaPieceImage);
                        }

                    }

                }
                zLateralDerecha += 6;
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

    private string loadImagenLogo(string url)
    {
        string urlWithoutQuotes = url.Replace("\"", "");
        string[] url2 = urlWithoutQuotes.Split('.');
        string rutaLogo = url2[0].Trim();
        return rutaLogo;

    }



}
