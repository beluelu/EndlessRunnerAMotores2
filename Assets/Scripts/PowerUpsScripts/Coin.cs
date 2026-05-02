using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void Start()
    {
        
        if (RemoteConfigManager.Instance != null)
        {
            value = RemoteConfigManager.Instance.coinsAmount;
        }
    }

    void Update()
    {
        transform.Rotate(0, 150f * Time.deltaTime, 0);
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
