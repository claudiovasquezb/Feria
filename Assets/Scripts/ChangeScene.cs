using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class ChangeScene : MonoBehaviour
{
    public int numeroEscena;
    private Transform puestoArtesanal;
    private string nombrePuesto;
    private int idPuesto;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nombrePuesto = gameObject.transform.parent.name.ToString().Trim();
            if (int.TryParse(nombrePuesto, out idPuesto))
            {
                PlayerPrefs.SetInt("idPuesto", idPuesto);
            } else
            {
                Debug.Log("Hubo un error al parsear");
            }
            SceneManager.LoadScene(numeroEscena);
        }
    }
    public void iniciar()
    {
        SceneManager.LoadScene(numeroEscena);
    }

}
