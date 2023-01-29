using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFantasma : MonoBehaviour
{

    public GameObject fantasma;
    public float fantasmaInterval = 1.0f;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        float cameraHalfWidth = camera.orthographicSize * camera.aspect;
        float cameraHalfHeight = camera.orthographicSize;

        minX = camera.transform.position.x - cameraHalfWidth;
        maxX = camera.transform.position.x + cameraHalfWidth;
        minY = camera.transform.position.y - cameraHalfHeight;
        maxY = camera.transform.position.y + cameraHalfHeight;

        InvokeRepeating("CrearFantasma", 0, fantasmaInterval);

    }


    void CrearFantasma()
    {
        Debug.Log(minX);
        float x = maxX;
        float y = Random.Range(minY, maxY);
        Vector3 fantasmaPosition = new Vector3(x, y, 0);

        Instantiate(fantasma, fantasmaPosition, Quaternion.identity);
    }
}
