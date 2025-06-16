using UnityEngine;

public class Moveaircraft : MonoBehaviour
{
    private Rigidbody rb;
    public float Speed = 5.0f;
    public float RotationSpeed = 1.0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                RigidbodyConstraints.FreezePositionY |
                                  RigidbodyConstraints.FreezeRotationZ;                     
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 moveForce = new Vector3(moveInput * Speed, 0f, 0f);
        rb.AddRelativeForce(moveForce, ForceMode.Force);
        
        float turnInput = Input.GetAxis("Horizontal");
        Vector3 turnTorque = new Vector3(0f, turnInput * RotationSpeed, 0f);
        rb.AddTorque(turnTorque, ForceMode.Force);
    }
}
