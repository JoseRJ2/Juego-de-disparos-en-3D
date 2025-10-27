using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Referencias")]
    public Camera playerCamera;

    [Header("Generales")]
    public float gravedad = -9.81f;

    [Header("Salto")]
    public float salto = 1.9f;

    [Header("Rotación")]
    public float sensibilidadMouse = 60f;

    Vector3 moveInput = Vector3.zero;
    CharacterController characterController;
    Vector3 rotacionInput = Vector3.zero;
    private float camaraVertical;

    [Header("Movimiento")]
    public float WalkSpeed = 5f;
    public float Correr = 10f;
    private void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    private void Update(){
        Move();
        Mirar();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * Correr;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * WalkSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(salto * -2f * gravedad);
            }
        }
        moveInput.y += gravedad * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }
    
    private void Mirar()
    {
        rotacionInput.x = Input.GetAxis("Mouse X") * sensibilidadMouse; //* Time.deltaTime;
        rotacionInput.y = Input.GetAxis("Mouse Y") * sensibilidadMouse; //* Time.deltaTime;

        camaraVertical += rotacionInput.y;
        camaraVertical = Mathf.Clamp(camaraVertical, -70, 70);

        transform.Rotate(Vector3.up * rotacionInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-camaraVertical, 0, 0);
    }

}
