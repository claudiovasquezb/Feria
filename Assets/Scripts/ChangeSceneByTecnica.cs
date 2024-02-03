using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneByTecnica : MonoBehaviour
{
    // Start is called before the first frame update
    public int numeroEscena;
    private int tecnicaSelected = 0;

    public PlayerModel playerData;
    public const string pathData = "Data/Player";
    public const string nameFileData = "PlayerDataFeriaArtesanal";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (gameObject.name)
            {
                case "Tallado":
                    PlayerPrefs.SetInt("idTecnica", 1);
                    tecnicaSelected = PlayerPrefs.GetInt("idTecnica");
                    resetPlayerPosition();
                    SceneManager.LoadScene(numeroEscena);
                    break;
                case "Cestería":
                    PlayerPrefs.SetInt("idTecnica", 2);
                    tecnicaSelected = PlayerPrefs.GetInt("idTecnica");
                    resetPlayerPosition();
                    SceneManager.LoadScene(numeroEscena);
                    break;
                case "Textileria":
                    PlayerPrefs.SetInt("idTecnica", 3);
                    tecnicaSelected = PlayerPrefs.GetInt("idTecnica");
                    resetPlayerPosition();
                    SceneManager.LoadScene(numeroEscena);
                    break;
                case "Modelado":
                    PlayerPrefs.SetInt("idTecnica", 4);
                    tecnicaSelected = PlayerPrefs.GetInt("idTecnica");
                    resetPlayerPosition();
                    SceneManager.LoadScene(numeroEscena);
                    break;
                case "Repujado":
                    PlayerPrefs.SetInt("idTecnica", 5);
                    tecnicaSelected = PlayerPrefs.GetInt("idTecnica");
                    resetPlayerPosition();
                    SceneManager.LoadScene(numeroEscena);
                    break;
                default:
                    // code block
                    break;
            }
        }
    }

    private void resetPlayerPosition()
    {
        playerData.x = 83.2f;
        playerData.y = 1;
        playerData.z = 51;
        SaveLoadSystemData.SaveData(playerData, pathData, nameFileData);
    }
    public void iniciar()
    {
        SceneManager.LoadScene(numeroEscena);
    }
}
