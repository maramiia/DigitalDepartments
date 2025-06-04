using UnityEngine;
using UnityEngine.SceneManagement;

public class BowlingScript : MonoBehaviour
{
    public Rigidbody dropBall;
    public Rigidbody launchBall; 
    public Transform dropStartPosition; 
    public Transform launchStartPosition;

    public GameObject rotatingPlatform; // Вращающаяся платформа

    public float boostForce = 10f;
    public float massStep = 10f; // На сколько увеличивать массу

    private Rigidbody platformRb;

    void Start()
    {
        ResetAll();

        if (rotatingPlatform != null)
        {
            platformRb = rotatingPlatform.GetComponent<Rigidbody>();

            if (platformRb != null)
            {
                platformRb.angularDamping = 0;
                platformRb.freezeRotation = false;
                platformRb.constraints = RigidbodyConstraints.FreezePosition |
                                         RigidbodyConstraints.FreezeRotationY;
            }
        }
    }

    void Update()
    {
        if (rotatingPlatform != null)
        {
            Rigidbody platformRb = rotatingPlatform.GetComponent<Rigidbody>();
            if (platformRb != null)
            {
                float fixedAngularSpeed = 1.5f;
                platformRb.angularVelocity = Vector3.up * fixedAngularSpeed;
            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            dropBall.isKinematic = false;
            dropBall.useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            launchBall.AddForce(Vector3.forward * boostForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            dropBall.mass += massStep;
            Debug.Log("Новая масса dropBall: " + dropBall.mass);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ResetAll()
    {
        dropBall.isKinematic = true;
        dropBall.useGravity = false;
        dropBall.transform.position = dropStartPosition.position;
        dropBall.mass = 100f;

        launchBall.isKinematic = false;
        launchBall.useGravity = true;
        launchBall.transform.position = launchStartPosition.position;
        
    }
}
