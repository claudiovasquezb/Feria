using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class PortalGenerator : MonoBehaviour
{
    float x = 66f;
    float y = 0.1f;
    float z = 167f;

    [SerializeField] GameObject door;
    private GameObject newDoor;

    private GameObject cube;
    private GameObject canvasCube;
    private Text tecnicaText;

    // Start is called before the first frame update
    void Start()
    {
        // REALIZANDO PETICIÓN HTTP
        string server_url = Environment.SERVER_URL;
        StartCoroutine(GetWebData(server_url + "api/", "Tecnica"));

    }

    void ProcessServerResponse(string rawResponse)
    {
        // That text, is actually JSOn info, so we need to 
        // parse that into something we can navigate.
        JSONNode node = JSON.Parse(rawResponse);
        GameObject puertasPadre = new GameObject();
        puertasPadre.name = "Listado de puertas";
        //Creando las puertas segun las tecnicas disponibles
        for (int i = 0; i < node.Count; i++)
        {
            newDoor = Instantiate(door, new Vector3(x, y, z), Quaternion.identity);
            newDoor.transform.localScale = new Vector3(2, 2, 2);
            newDoor.name = node[i]["tec_nombre"];
            newDoor.transform.SetParent(puertasPadre.transform);
            cube = newDoor.transform.GetChild(6).gameObject;
            canvasCube = cube.transform.GetChild(0).gameObject;
            tecnicaText = canvasCube.transform.GetChild(0).GetComponent<Text>();
            tecnicaText.text = node[i]["tec_nombre"];
            x += 7;
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


}
