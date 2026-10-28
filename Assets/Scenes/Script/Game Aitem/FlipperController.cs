using UnityEngine;

public class FlipperController : MonoBehaviour
{
    [Header("ëÄçÏÉLÅ[")]
    public KeyCode pressKey = KeyCode.A;

    [Header("âÒì]ê›íË")]
    public float rotateAngle = 40f;
    public float rotateSpeed = 720f;

    [Header("âÒì]é≤")]
    public Vector3 rotationAxis = Vector3.up;

    private Rigidbody rb;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startRotation = rb.rotation;
        targetRotation = startRotation;
    }

    void Update()
    {
        if (Input.GetKey(pressKey))
        {
            targetRotation = startRotation *
                Quaternion.AngleAxis(rotateAngle, rotationAxis);
        }
        else
        {
            targetRotation = startRotation;
        }
    }

    void FixedUpdate()
    {
        Quaternion nextRotation = Quaternion.RotateTowards(
            rb.rotation,
            targetRotation,
            rotateSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(nextRotation);
    }
}
