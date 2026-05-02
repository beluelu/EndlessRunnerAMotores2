using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 150f;

    void Update()
    {
        transform.Rotate(0, 0, 150f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moneda +" + value);
            Destroy(gameObject);
        }
    }
}
