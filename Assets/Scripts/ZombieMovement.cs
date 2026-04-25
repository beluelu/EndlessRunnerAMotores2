using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform pointB;
    public float speed = 2f;

    private bool reached = false;

    void Update()
    {
        if (reached) return;

        transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            reached = true;
            Debug.Log("Zombie llegó");

            // OPCIONAL:
            // Destroy(gameObject);  // si querés que desaparezca
        }
    }
}
