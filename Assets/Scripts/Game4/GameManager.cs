using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public float startDelay = 1;
    public static GameManager instance;

    /*private int timeToWin = 60; // tiempo en segundos que el jugador debe aguantar para ganar
    private int timeCounter = 0; // contador de tiempo*/
    [SerializeField] private TMP_Text TimeText;
    private float startingTime = 60f;
    private float timeRemaining;
    private bool playing = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TimeText = GameObject.Find("Canvas/Time").GetComponent<TMP_Text>();
        playing = true;
        // Iniciamos el contador de tiempo
        
        timeRemaining = startingTime;
        Update();

    }

    /*public IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeCounter++;

            // Si el contador de tiempo ha llegado al tiempo máximo, el jugador ha ganado
            /*if (timeCounter >= timeToWin)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WinScene");
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
            }
        }
    }*/
    void Update()
    {
    if (playing)
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
                if (timeRemaining == 0)
                {

                    GameOver();
                }
        }
        TimeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
        
    }
}


/*public IEnumerator GameOver()
    {
        Debug.Log(timeRemaining);
        yield return new WaitForSeconds(0.9f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOverGame4");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }*/

    
     public void GameOver()
    {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOverGame4");
    }
     

}
