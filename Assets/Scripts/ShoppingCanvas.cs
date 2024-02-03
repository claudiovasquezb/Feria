using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class test
{
    public int id;
    public string name;
    public int cantidad;
}
public class ShoppingCanvas : MonoBehaviour
{
    public Canvas shoppingCanvas, JoystickCanvas;
    //private ArrayList cart = new ArrayList();
    /*public TextMeshProUGUI nameText;*/
    /*private Canvas panel, container;*/
    public GameObject nombrePrefab, inputFieldAmountPrefab, trashIconPrefab, precioPrefab, totalPrefab, paperBag;
    public static TextMeshProUGUI nombreReference, precioReference, totalTextReference, nombreText, cantidadText, accionesText, precioText, totalText;
    private Button trashIconReference;
    private TMP_InputField cantidadReference;
    public static int total = 0;

    List<CartProductModel> cart = new List<CartProductModel>();

    private Button tb;
    TextMeshProUGUI precioInstance, totalInstance;

    void Start()
    {
        shoppingCanvas.enabled = false;
        paperBag.SetActive(false);
        // obteniendo la referencia de los gameobject del canvas
        nombreText = GameObject.Find("NombreText").GetComponent<TextMeshProUGUI>();
        cantidadText = GameObject.Find("CantidadText").GetComponent<TextMeshProUGUI>();
        accionesText = GameObject.Find("AccionesText").GetComponent<TextMeshProUGUI>();
        precioText = GameObject.Find("PrecioText").GetComponent<TextMeshProUGUI>();
        totalText = GameObject.Find("TotalText").GetComponent<TextMeshProUGUI>();

        // obteniendo la referencia de los prefabs usados en el canvas
        nombreReference = nombrePrefab.GetComponent<TextMeshProUGUI>();
        cantidadReference = inputFieldAmountPrefab.GetComponent<TMP_InputField>();
        trashIconReference = trashIconPrefab.GetComponent<Button>();
        precioReference = precioPrefab.GetComponent<TextMeshProUGUI>();
        totalTextReference = totalPrefab.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //cart = PuestoCanvasPieceInformation.shoppingCart;
        cart = PuestoCanvasPieceInformation.shoppingCart;
        if (cart.Count > 0)
        {
            paperBag.SetActive(true);
        }
        else if(cart.Count <= 0)
        {
            paperBag.SetActive(false);
        }
    }

    public void HideShoppingCanvas()
    {
        shoppingCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        JoystickCanvas.enabled = true;
        Clear(nombreText);
        Clear(cantidadText);
        Clear(accionesText);
        Clear(precioText);
        Clear(totalText);
        total = 0;
    }

    public void ShowShoppingCanvas()
    {
        shoppingCanvas.enabled = true;
        calcularPrecioTotal();
        for (int i = 0; i < cart.Count; i++)
        {
            // agregando los inputs field debajo de "cantidad"
            TMP_InputField ifa = Instantiate(cantidadReference, new Vector3(0, 0, 0), Quaternion.identity);
            ifa.name = cart[i].id.ToString();
            ifa.text = cart[i].cantidad.ToString();
            ifa.transform.SetParent(cantidadText.transform);
            ifa.transform.localPosition = new Vector3(0, -60 * (i + 1), 0);
            ifa.transform.localScale = new Vector3(1, 1, 1);
            ifa.characterValidation = TMP_InputField.CharacterValidation.Integer;
            ifa.onValueChanged.AddListener(delegate { ValueChangeCheck();});

            // agregando los nombres de los productos debajo de "Nombre"
            TextMeshProUGUI ti = Instantiate(nombreReference, new Vector3(0, 0, 0), Quaternion.identity);
            ti.name = cart[i].id.ToString();
            ti.text = cart[i].nombre.ToString();
            ti.transform.SetParent(nombreText.transform);
            ti.transform.localPosition = new Vector3(0, -60 * (i + 1), 0);
            ti.transform.localScale = new Vector3(1, 1, 1);

            // agregando botones debajo de "Acciones"
            tb = Instantiate(trashIconReference, new Vector3(0, 0, 0), Quaternion.identity);
            tb.name = cart[i].id.ToString();
            tb.transform.SetParent(accionesText.transform);
            tb.transform.localPosition = new Vector3(0, -60 * (i + 1), 0);
            tb.transform.localScale = new Vector3(1, 1, 1);
            tb.onClick.AddListener(delegate { deleteProduct(); });
            
            // agregando el precio de los productos debajo de "precio"
            precioInstance = Instantiate(precioReference, new Vector3(0, 0, 0), Quaternion.identity);
            precioInstance.name = cart[i].id.ToString();
            precioInstance.text =  "$" + cart[i].precio.ToString();
            precioInstance.transform.SetParent(precioText.transform);
            precioInstance.transform.localPosition = new Vector3(0, -60 * (i + 1), 0);
            precioInstance.transform.localScale = new Vector3(1, 1, 1);
        }
        // agregando el total de los productos debajo de "total"
        totalInstance = Instantiate(totalTextReference, new Vector3(0, 0, 0), Quaternion.identity);
        totalInstance.name = "totalPrice";
        totalInstance.text = "$" + total.ToString();
        totalInstance.transform.SetParent(totalText.transform);
        totalInstance.transform.localPosition = new Vector3(0, -60, 0);
        totalInstance.transform.localScale = new Vector3(1, 1, 1);

        // Creando orden de compra con paypal
        NewStoreAction.INSTANCE.OpenPurchaseItemScreen(
            "Piezas artesanales",
            "Piezas artesanales agregadas al carrito de compras",
            total.ToString()
            );
        print("Total: " + total.ToString());
    }
    public static void emptyCart()
    {
        PlayerPrefs.SetInt("ShoppingCount", 0);
        Clear(nombreText);
        Clear(cantidadText);
        Clear(accionesText);
        Clear(precioText);
        Clear(totalText);
        total = 0;
    }

    // Borrando todos los hijos de un objeto en especifico
    public static void Clear(TextMeshProUGUI go)
    {
        for (int i = go.transform.childCount - 1; i >= 0; i--)
        {
            TextMeshProUGUI.Destroy(go.transform.GetChild(i).gameObject);
        }
    }

    public void deleteProduct()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        Transform transformActions = accionesText.gameObject.transform.Find(go.name);
        Transform transformNombre = nombreText.gameObject.transform.Find(go.name);
        Transform transformCantidad = cantidadText.gameObject.transform.Find(go.name);
        Transform transformPrecio = precioText.gameObject.transform.Find(go.name);
        TextMeshProUGUI transformTotalText = totalText.gameObject.transform.Find("totalPrice").GetComponent<TextMeshProUGUI>();
        Destroy(transformActions.gameObject);
        Destroy(transformNombre.gameObject);
        Destroy(transformCantidad.gameObject);
        Destroy(transformPrecio.gameObject);
        try
        {
            var itemToRemove = cart.SingleOrDefault(r => r.id == int.Parse(go.name));
            if (itemToRemove != null)
                cart.Remove(itemToRemove);
        }
        catch (System.Exception)
        {

            throw;
        }
        calcularPrecioTotal();
        transformTotalText.text = "$" + total.ToString();
    }

    public void calcularPrecioTotal()
    {
        total = 0;
        foreach (var item in cart)
        {
            total += item.precio;
        }
    }

    //Cambiando el valor de cantidad del segun el input y aplicando el precio segun la cantidad
    public void ValueChangeCheck()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        Transform transformCantidad = cantidadText.gameObject.transform.Find(go.name);
        TMP_InputField ifield = transformCantidad.GetComponent<TMP_InputField>();
        TextMeshProUGUI totalChange = totalText.transform.Find("totalPrice").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI precioChange = precioText.transform.Find(ifield.name).GetComponent<TextMeshProUGUI>();
        foreach (var item in cart)
        {
            if(ifield.text.Length > 0)
            {
                if (item.id == int.Parse(ifield.name))
                {
                    if (int.Parse(ifield.text) > 0)
                    {
                        int cantidadAuxiliar = item.cantidad;
                        item.cantidad = int.Parse(ifield.text);
                        item.precio = (item.precio / cantidadAuxiliar) * int.Parse(ifield.text);
                        Debug.Log("id: " + item.id + "cantidad: " + item.cantidad + "Precio: " + item.precio);
                        calcularPrecioTotal();
                        totalChange.text = "$" + total.ToString();
                        precioChange.text = "$" + item.precio.ToString();
                    }
                    else if (int.Parse(ifield.text) == 0)
                    {
                        ifield.text = "1";
                    }
                }
            }

            
        }

        // Creando orden de compra con paypal
        NewStoreAction.INSTANCE.OpenPurchaseItemScreen(
            "Piezas artesanales",
            "Piezas artesanales agregadas al carrito de compras",
            total.ToString()
            );
        print("Total: " + total.ToString());
    }

}
