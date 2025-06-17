using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    public float hoverHeight = 2.0f;// Текущая поддерживаемая высота
    public float hoverStep = 0.1f; // Шаг изменения высоты
    public float minHoverHeight = 0.5f; // Минимально допустимая высота
    public float maxHoverHeight = 5.0f; // Максимально допустимая высота

    public float speed = 3.0f;
    public float maxSpeed = 6.0f;
    public float rotationSpeed = 360.0f;
    public float hoverForce = 10.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
    }

    void Update()
    {
        // Управление изменением высоты 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hoverHeight = Mathf.Min(hoverHeight + hoverStep, maxHoverHeight);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            hoverHeight = Mathf.Max(hoverHeight - hoverStep, minHoverHeight);
        }
    }

    void FixedUpdate()
    {
        // Получаем ввод от клавиатуры и мыши
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        // Применяем силу для перемещения в горизонтальной и вертикальной плоскостях
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddRelativeForce(movement * speed);

        
        float rotationY = transform.localEulerAngles.y + mouseX * rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationY, 0);

        // Ограничение скорости
        Vector3 clampedVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        rb.linearVelocity = clampedVelocity;

        // Поддержка на заданной высоте
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 10f))
        {
            float currentHeight = hit.distance;
            float heightDifference = hoverHeight - currentHeight;

            float upwardForce = heightDifference * hoverForce;
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Acceleration);
        }
    }
}
