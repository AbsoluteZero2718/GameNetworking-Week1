using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    public Vector3 initialVelocity;
    public Rigidbody rb;
    public float lifetime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = initialVelocity;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
