using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealMapCanvas : MonoBehaviour
{
    public Canvas realMapCanvas;
    public GameObject player;

    public PlayerModel playerData;
    public const string pathData = "Data/Player";
    public const string nameFileData = "PlayerDataFeriaArtesanal";
    // Start is called before the first frame update
    void Start()
    {
        realMapCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RealMap")) {
            if (realMapCanvas.enabled == false)
            {
                realMapCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (realMapCanvas.enabled == true) {
                realMapCanvas.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

    }
    public void CheckPointStartButton()
    {
        player.transform.localPosition = new Vector3(83.1f, 1, 52.93f);
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CheckPointMiddleLeftButton()
    {
        player.transform.localPosition = new Vector3(74.5f, 1, 85);
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CheckPointMiddleRightButton()
    {
        player.transform.localPosition = new Vector3(91.49f, 1, 86.48f);
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CheckPointTopButton()
    {
        player.transform.localPosition = new Vector3(81.57f, 1, 147.62f);
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TalladoButton()
    {
        PlayerPrefs.SetInt("idTecnica", 1);
        SceneManager.LoadScene(0);
        resetPlayerPosition();
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CesteriaButton()
    {
        PlayerPrefs.SetInt("idTecnica", 2);
        SceneManager.LoadScene(0);
        resetPlayerPosition();
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void TextileriaButton()
    {
        PlayerPrefs.SetInt("idTecnica", 3);
        SceneManager.LoadScene(0);
        resetPlayerPosition();
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ModeladoButton()
    {
        PlayerPrefs.SetInt("idTecnica", 4);
        SceneManager.LoadScene(0);
        resetPlayerPosition();
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RepuajdoButton()
    {
        PlayerPrefs.SetInt("idTecnica", 5);
        SceneManager.LoadScene(0);
        resetPlayerPosition();
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void resetPlayerPosition()
    {
        playerData.x = 83.2f;
        playerData.y = 1;
        playerData.z = 51;
        SaveLoadSystemData.SaveData(playerData, pathData, nameFileData);
    }

    public void closeCanvas()
    {
        realMapCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openCanvas()
    {
        realMapCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
