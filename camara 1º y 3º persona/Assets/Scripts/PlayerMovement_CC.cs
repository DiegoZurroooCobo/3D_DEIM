using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_CC : MonoBehaviour
{
    public float speed, rotationSpeed, gravityScale, jumpForce;

    private float yVelocity = 0;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravityScale = Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.isGrounded) 
        { 
            yVelocity = 0;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        Jump(jumpPressed);
        Movement(x, z);
        //Rotation(mouseX);
    }

    void Jump(bool jumpPressed) 
    {
        if (jumpPressed && characterController.isGrounded)
        {
            yVelocity += Mathf.Sqrt(jumpForce * 3 * gravityScale); // raiz cuadrada que suaviza el salto
        }
    }

    void Movement(float x, float z) 
    {
        Vector3 movementVector = transform.forward * speed * z + transform.right * speed * x;
        yVelocity -= gravityScale;
        movementVector.y = yVelocity;

        movementVector *= Time.deltaTime; // se mueve igual si importar el framerate

        if(x != 0 || z != 0) //codigo espaguetti feo, borrar mas tarde 
            characterController.Move(movementVector); // metodo de character controller para moverlo
    }

    void Rotation(float mouseX) 
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation); // el transform rote a la rotacion a la que estamos moviendo
    }
}
