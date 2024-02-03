using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MercadoPago : MonoBehaviour
{

    List<CartProductModel> cart = new List<CartProductModel>();
    List<Checkout> checkoutList = new List<Checkout>();
    private int total = 0;
    private string singleTitleProduct = "";
    private int singlePriceProduct = 0;

    private string resultado = "";
    void Start()
    {
        // GameObject.Find("BuyButton").GetComponent<Button>().onClick.AddListener(Upload);
        // GameObject.Find("SingleBuyButton").GetComponent<Button>().onClick.AddListener(SingleUpload);

    }
    void Upload() => StartCoroutine(PostPaymentByTotal());
    void SingleUpload() => StartCoroutine(PostSinglePayment());

    IEnumerator PostPaymentByTotal()
    {
        total = ShoppingCanvas.total;

        WWWForm form = new WWWForm();
        form.AddField("title", "Productos");
        form.AddField("price", total);
        form.AddField("quantity", 1);

        // Para localhost...http://localhost:8080/checkout\
        // Para heroku...https://feria-3d.herokuapp.com/checkout
        // Para servidores de la UV...https://api.feria-3d.informatica.uv.cl/checkout 

        using (UnityWebRequest www = UnityWebRequest.Post("https://feria-3d.herokuapp.com/checkout", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("Result: " + www.downloadHandler.text);
                Application.OpenURL(www.downloadHandler.text);
            }
        }
    }

    IEnumerator PostSinglePayment()
    {
        singleTitleProduct = PuestoCameraInteraction.singleTitleProduct;
        singlePriceProduct = PuestoCameraInteraction.singlePriceProduct;

        WWWForm form = new WWWForm();
        form.AddField("title", singleTitleProduct);
        form.AddField("price", singlePriceProduct);
        form.AddField("quantity", 1);

        // Para localhost...http://localhost:8080/checkout\
        // Para heroku 
        // using 
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/checkout", form);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Result: " + www.downloadHandler.text);
            resultado = www.downloadHandler.text;
            StartCoroutine(PostPaymentStatus());
        }
        
    }

    IEnumerator PostPaymentStatus()
    {
        Application.OpenURL(resultado);

        WWWForm form = new WWWForm();
        form.AddField("", "");

        UnityWebRequest www = UnityWebRequest.Get(resultado);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Resultado estado: " + www.downloadHandler.text);
        }

    }



}

public class Checkout
{
    public string title;
    public int price;
    public int quantity;
}