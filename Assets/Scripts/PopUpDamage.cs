using UnityEngine;
using TMPro;

public class PopUpDamage : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float lifetime = 1f;

    TextMeshPro textMesh;
    Color textColor;

    private void Awake()
    {
        textMesh = GetComponent <TextMeshPro>();
        textColor = textMesh.color;
    }

    private void SetDamage(int damage)
    {
        textMesh.text = damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        textColor.a -= Time.deltaTime / lifetime;
        textMesh.color = textColor;

        if(textColor.a <= 0f)
        {
            Destroy(gameObject);
        }    
    }
}
