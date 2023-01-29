using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScreen : MonoBehaviour
{
    public static FinalScreen instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
