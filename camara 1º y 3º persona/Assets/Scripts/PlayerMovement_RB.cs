using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_RB : MonoBehaviour
{
    public float speed, rotationSpeed, jumpForce, sphereRadius; /*gravityScale*/ // para que rote a su propia velocidad y se puede elegir la velocidad de rotacion
    public string groundname;
    private Rigidbody rb;
    private float x, z, mouseX; //input
    private bool jumpPressed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        //gravityScale = -Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) 
        { 
            jumpPressed = true;
        }
        RotatePlayer();
    }
    void RotatePlayer() 
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation); // el transform rote a la rotacion a la que estamos moviendo
    }

    private void FixedUpdate()
    {
         ApplySpeed();
         ApplyJumpForce();
    }

    void ApplySpeed() 
    {
        rb.velocity = (transform.forward * speed * z) + (transform.right * speed * x) + new Vector3(0, rb.velocity.y, 0); // usar gravedad base de unity del objeto
        //+ (transform.up * gravityScale); // transform forward -> vector direccion. Por z para que avance cuando se presione la tecla
        //rb.AddForce(transform.up * gravityScale); // añade una fuerza de gravedad para que tenga aceleracion al caer. gravedad personalizada
    }

    void ApplyJumpForce() 
    { 
        if(jumpPressed) 
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce);
            jumpPressed = false; // vuelve el booleano de saltar a falso 
        }
    }

    private bool IsGrounded()
    {
        RaycastHit[] collider = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y - transform.localScale.y/2, transform.position.z), 
            sphereRadius, Vector3.up);

        for(int i = 0; i < collider.Length; i++) // recorremos elemento a elemento y comprobamos si ese elemento es suelo
        {
            if (collider[i].collider.gameObject.layer == LayerMask.NameToLayer(groundname)) // busca en el array de layers y si los layers con los que se choca son GroundMask
            { 
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }
}
