using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void Update()
    {
        transform.Rotate(0, 150f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moneda +" + value);

            GameManager.instance.AddCoins(value);

            Destroy(gameObject);
        }
    }
}
