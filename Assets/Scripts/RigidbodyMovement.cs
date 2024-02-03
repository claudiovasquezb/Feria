using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private Vector3 JoystickMovementInput;
    private float xRot;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    [SerializeField] private  FloatingJoystick joystick;
    //[SerializeField] private float Jumpforce;


    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //JoystickMovementInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        MovePlayer();
        //MovePlayerCamera();
        //MovePlayerWithJoystick();
    }

    private void MovePlayer() {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
    }

    private void MovePlayerCamera() {
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void MovePlayerWithJoystick() {
        if (joystick.Horizontal != 0.0f || joystick.Vertical != 0.0f)
            PlayerBody.velocity = new Vector3(joystick.Horizontal * Speed, PlayerBody.velocity.y, joystick.Vertical * Speed);
    }
}
