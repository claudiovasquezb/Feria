using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Vector2 sensibility;
    private new Transform camera;
    
    void Start()
    {
        Cursor.visible = true;
        // Capturar cursor dentro de mi escena, para que no se salga de la pantalla
        //Cursor.lockState = CursorLockMode.Locked;
        // Buscar un objeto dentro de la jerarquia de mi Player (objetos que estan dentro de Player)
        camera = transform.Find("Camera");
    }

    void Update()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
       float hor = Input.GetAxis("Mouse X");
       float ver = Input.GetAxis("Mouse Y");
       // Si es distinto de 0 quiere decir que estamos moviendo la camara horizontalmente
       if(hor != 0) {
           // Cambiar rotacion en el objeto Player
           transform.Rotate(Vector3.up * hor * sensibility.x);
       }
       // Si es distinto de 0 quiere decir que estamos moviendo la camara verticalmente
        if(ver != 0) {
           // Cambiar rotacion en el objeto Camera (No en el Player)
           //camera.Rotate(Vector3.left * ver * sensibility.y);
           // Traer las rotaciones x locales de la camara
           float angle = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;
           if(angle > 180) {
               angle -= 360;
           }
           // Para mover la camara verticalmente entre un rango fijo, utilizamos Clamp
           // Angulo, valor minimo y valor maximo de la camara.
           angle = Mathf.Clamp(angle, -80, 80);
           // Asignar nuestras nuevas rotaciones a la camara
           camera.localEulerAngles = Vector3.right * angle;
       }
    }
}
