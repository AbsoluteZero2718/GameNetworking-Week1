using UnityEngine;
using Unity.Netcode;

public class FloatingText : NetworkBehaviour
{
    [SerializeField] public float DestroyTime = 3f;
    [SerializeField]public Vector3 Offset = new Vector3(0, 2, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
