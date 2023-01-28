using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderJuego1 : MonoBehaviour
{
    public GameObject gameManagerJuego1;

    void Awake()
    {
        if (GameManagerJuego1.instance == null)

            Instantiate(gameManagerJuego1);

    }
}
