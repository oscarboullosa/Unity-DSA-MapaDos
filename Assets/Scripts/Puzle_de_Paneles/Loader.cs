using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager_Puzle.instance == null)
            Instantiate(gameManager);
    }
}