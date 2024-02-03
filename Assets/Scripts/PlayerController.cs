using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float speed = 10f;
    public PlayerModel playerData;
    public GameObject Player;
    public FloatingJoystick joystick;
    public const string pathData = "Data/Player";
    public const string nameFileDataFeriaArtesanal = "PlayerDataFeriaArtesanal";
    public const string nameFileDataPuestoArtesanal = "PlayerDataPuestoArtesanal";
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (SceneManager.GetActiveScene().name == "FeriaArtesanal")
        {
            var dataFound = SaveLoadSystemData.LoadData<PlayerModel>(pathData, nameFileDataFeriaArtesanal);
            if (dataFound != null)
            {
                playerData = dataFound;
                Player.transform.position = new Vector3(playerData.x, playerData.y, playerData.z);
            }
            else
            {
                PlayerModel newPlayerData = new PlayerModel();
                newPlayerData.x = 83.2f;
                newPlayerData.y = 1;
                newPlayerData.z = 51;
                SaveLoadSystemData.SaveData(newPlayerData, pathData, nameFileDataFeriaArtesanal);
                Player.transform.position = new Vector3(newPlayerData.x, newPlayerData.y, newPlayerData.z);
            }
            
        }
        else if(SceneManager.GetActiveScene().name == "PuestoArtesanal") {
            PlayerModel newPlayerData = new PlayerModel();
            newPlayerData.x = 165f;
            newPlayerData.y = 1;
            newPlayerData.z = 337f;
            SaveLoadSystemData.SaveData(newPlayerData, pathData, nameFileDataPuestoArtesanal);
            /*            var dataFound = SaveLoadSystemData.LoadData<PlayerModel>(pathData, nameFileDataPuestoArtesanal);
                        if (dataFound != null)
                        {
                            playerData = dataFound;
                            Player.transform.position = new Vector3(playerData.x, playerData.y, playerData.z);
                        }
                        else
                        {
                            PlayerModel newPlayerData = new PlayerModel();
                            newPlayerData.x = 165f;
                            newPlayerData.y = 1;
                            newPlayerData.z = 337f;
                            SaveLoadSystemData.SaveData(newPlayerData, pathData, nameFileDataPuestoArtesanal);
                        }*/

        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
/*        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = new Vector3(hor, rigidbody.velocity.y, ver) * speed * Time.deltaTime;*/
        //KeyboardMovement();
        //FloatingMovement();
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SavePosition();
        }
    }



    public void KeyboardMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (hor != 0.0f || ver != 0.0f)
        {
            Vector3 dir = transform.forward * ver + transform.right * hor;
            dir.Normalize();
            rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);

        }
    }

    public void FloatingMovement()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if (x != 0.0f || y != 0.0f)
        {
            Vector3 dir = transform.forward * y + transform.right * x;
            dir.Normalize();
            rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);

        }
    }

    public void SavePosition()
    {
        if (SceneManager.GetActiveScene().name == "FeriaArtesanal")
        {
            playerData.x = Player.transform.position.x;
            playerData.y = Player.transform.position.y;
            playerData.z = Player.transform.position.z;
            SaveLoadSystemData.SaveData(playerData, pathData, nameFileDataFeriaArtesanal);
        }
    }



}
