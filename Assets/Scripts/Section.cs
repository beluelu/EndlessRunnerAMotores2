using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<GameObject> obstacles;
    public float speed;
   

    private static int lastRandomIndex = -1;

    private void Start()
    {

        
        obstacles = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if(child.tag == "Obstacle")
            {
                obstacles.Add(child.gameObject);
            }
        }

        EnableRandomObstacle();
    }

    public void EnableRandomObstacle()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }

        int randomIndex = lastRandomIndex;
        while (randomIndex == lastRandomIndex)
        {
            randomIndex = Random.Range(0, obstacles.Count);
        }

        lastRandomIndex = randomIndex;

        GameObject selected = obstacles[randomIndex];
        selected.SetActive(true);

        // 🔥 llamar al script del piso
        Floor floorScript = selected.GetComponent<Floor>();
        if (floorScript != null)
        {
            floorScript.ActivateFloor();
        }
    }

    

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -20)
        {
            transform.Translate(Vector3.forward * 20 * 5);

            EnableRandomObstacle();
        }
    }

}
