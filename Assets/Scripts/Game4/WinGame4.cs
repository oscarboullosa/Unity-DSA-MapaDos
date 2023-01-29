using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame4 : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game4Win");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
