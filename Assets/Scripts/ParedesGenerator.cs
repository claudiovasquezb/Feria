using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedesGenerator : MonoBehaviour
{
    float zIzquierda = 43.85f;
    float zDerecha = 43.86f;
    float xAbajo = 95.75f;
    float zPasilloIzquierda = 123.94f;
    float zPasilloDerecha = 123.94f;
    float xAbajoSalaPuertasIzquierda = 72.56f;
    float xAbajoSalaPuertasDerecha = 92.2f;
    float zIzquierdaSalaPuertas = 147.63f;
    float zDerechaSalaPuertas = 147.63f;
    float xArribaSalaPuertas = 64.27f;
    int idTecnica = 0;
    [SerializeField] private GameObject pared;
    private GameObject newPared;
    [SerializeField] private Material material_1;
    [SerializeField] private Material material_2;
    [SerializeField] private Material material_3;
    [SerializeField] private Material material_4;
    [SerializeField] private Material material_5;

    void Start()
    {
        idTecnica = PlayerPrefs.GetInt("idTecnica");
        GameObject paredesPadre = new GameObject();
        paredesPadre.name = "Paredes";
        Renderer rend = pared.GetComponent<Renderer>();
        switch (idTecnica)
        {
            case 1:
                rend.material = material_1;
                break;
            case 2:
                rend.material = material_2;
                break;
            case 3:
                rend.material = material_3;
                break;
            case 4:
                rend.material = material_4;
                break;
            case 5:
                rend.material = material_5;
                break;
            default:
                rend.material = material_1;
                break;
        }
        for (int i = 0; i < 10; i++)
        {
            // Instanciando paredes al lado izquierdo
            newPared = Instantiate(pared, new Vector3(65, 4, zIzquierda), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zIzquierda += 8;
        }

        for (int i = 0; i < 10; i++)
        {
            // Instanciando paredes al lado derecho
            newPared = Instantiate(pared, new Vector3(100, 4, zDerecha), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zDerecha += 8;
        }

        for (int i = 0; i < 4; i++)
        {
            // Instanciando paredes de abajo
            newPared = Instantiate(pared, new Vector3(xAbajo, 4, 40), Quaternion.identity);
            newPared.transform.localScale = new Vector3(9, 8, 0.3f);
            newPared.transform.SetParent(paredesPadre.transform);
            xAbajo -= 9;
        }

        // Instanciando paredes arriba a la izquierda
        newPared = Instantiate(pared, new Vector3(70.85f, 4, 119.8f), Quaternion.identity);
        newPared.transform.SetParent(paredesPadre.transform);
        newPared.transform.localScale = new Vector3(12, 8, 0.3f);

        // Instanciando paredes arriba a la derecha
        newPared = Instantiate(pared, new Vector3(94.14f, 4, 119.8f), Quaternion.identity);
        newPared.transform.SetParent(paredesPadre.transform);
        newPared.transform.localScale = new Vector3(12, 8, 0.3f);

        for (int i = 0; i < 3; i++)
        {
            // Instanciando paredes del pasillo a la izquierda
            newPared = Instantiate(pared, new Vector3(76.7f, 4, zPasilloIzquierda), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zPasilloIzquierda += 8;
        }

        for (int i = 0; i < 3; i++)
        {
            // Instanciando paredes del pasillo a la derecha
            newPared = Instantiate(pared, new Vector3(88.3f, 4, zPasilloDerecha), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zPasilloDerecha += 8;
        }

        for (int i = 0; i < 2; i++)
        {
            // Instanciando paredes de abajo a la izquierda de la sala de puertas
            newPared = Instantiate(pared, new Vector3(xAbajoSalaPuertasIzquierda, 4, 143.79f), Quaternion.identity);
            newPared.transform.SetParent(paredesPadre.transform);
            xAbajoSalaPuertasIzquierda -= 8;
        }

        for (int i = 0; i < 2; i++)
        {
            // Instanciando paredes de abajo a la izquierda de la sala de puertas
            newPared = Instantiate(pared, new Vector3(xAbajoSalaPuertasDerecha, 4, 143.79f), Quaternion.identity);
            newPared.transform.SetParent(paredesPadre.transform);
            xAbajoSalaPuertasDerecha += 8;
        }

        for (int i = 0; i < 3; i++)
        {
            // Instanciando paredes de la izquierda en la sala de puertas
            newPared = Instantiate(pared, new Vector3(60.42f, 4, zIzquierdaSalaPuertas), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zIzquierdaSalaPuertas += 8;
        }

        for (int i = 0; i < 3; i++)
        {
            // Instanciando paredes de la derecha en la sala de puertas
            newPared = Instantiate(pared, new Vector3(104.3f, 4, zDerechaSalaPuertas), Quaternion.identity);
            newPared.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newPared.transform.SetParent(paredesPadre.transform);
            zDerechaSalaPuertas += 8;
        }

        for (int i = 0; i < 6; i++)
        {
            if(i == 5)
            {
                newPared = Instantiate(pared, new Vector3(102.32f, 4, 167.77f), Quaternion.identity);
                newPared.transform.localScale = new Vector3(4.1f, 8, 0.3f);
                newPared.transform.SetParent(paredesPadre.transform);
                xArribaSalaPuertas += 8;
            } else
            {
                // Instanciando paredes de arriba en la sala de puertas
                newPared = Instantiate(pared, new Vector3(xArribaSalaPuertas, 4, 167.77f), Quaternion.identity);
                newPared.transform.SetParent(paredesPadre.transform);
            }
            xArribaSalaPuertas += 8;
        }
    }

}
