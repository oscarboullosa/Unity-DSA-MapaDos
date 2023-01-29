using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{

    public float speed = 5.0f;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - (Vector3)direction * speed * Time.deltaTime;

    }


}

