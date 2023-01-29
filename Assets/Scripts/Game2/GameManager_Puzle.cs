using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_Puzle : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public static GameManager_Puzle instance = null;
    
    private TMP_Text movementsAllowedText;
    private TMP_Text movementsRemainingText;
    private TMP_Text levelText;
    private GameObject levelImage;
    private GameObject board;
    private BoardManager boardScript;
    private List<Sprite> sprite1;
    private List<Sprite> sprite2;
    private int level;
    private int movementsAllowed;
    private int movementsRemaining;
    private bool end;
    private bool doingSetup;
    private bool failed;

    public Sprite[] sprites;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        end = false;
        failed = false;
        level = 1;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        movementsAllowedText = GameObject.Find("AllowedText").GetComponent<TextMeshProUGUI>();
        movementsRemainingText = GameObject.Find("RemainingText").GetComponent<TextMeshProUGUI>();
        NextLevel();
    }

    void HideLevelImage()
    {
        boardScript.BoardUpdate(sprite1, sprite2);
        board = GameObject.Find("Board");
        movementsRemainingText.text = "Movements remaining: " + movementsRemaining;
        movementsAllowedText.text = "Movements allowed: " + movementsAllowed;
        levelImage.SetActive(false);
        doingSetup = false;
        board.SetActive(true);
    }

    void HideBoard()
    {
        board = GameObject.Find("Board");
        board.SetActive(false);
        levelImage.SetActive(true);
    }


    public void SelectCrate(Vector3 mousePos)
    {
        if (!doingSetup && !failed)
        {
            movementsRemaining--;
            movementsRemainingText.text = "Movements remaining: " + movementsRemaining;
            if (!end)
            {
                int x = Convert.ToInt32(mousePos[0]);
                int y = Convert.ToInt32(mousePos[1]);
                int index = (x - 6) * 4 + y;
                UpdateCrate(index);
                boardScript.BoardUpdate(sprite1, sprite2);
                Check();
            }
        }
    }

    void Check()
    {
        board = GameObject.Find("Board");
        int j = 0;
        for (int i = 0; i < sprite1.Count; i++)
        {
            if (sprite1[i] == sprite2[i])
            {
                j++;
            }
        }
        if (sprite1.Count == j && movementsRemaining >=0)
        {
            level++;
            NextLevel();
        }
        else if (movementsRemaining == 0)
        {
            failed = true;
            //levelText.text = "You have lost the game";
            //Invoke("HideBoard", 1);
            StartCoroutine(GameOver());
        }
    }

    void UpdateCrate(int index)
    {
        int[] num = new int[0];
        if (index == 0)
            num = new int[] { 0, 1, 4, 5 };
        else if (index == 1 || index == 2)
            num = new int[] { -1, 0, 1, 3, 4, 5 };
        else if (index == 3)
            num = new int[] { -1, 0, 3, 4 };
        else if (index == 4 || index == 8)
            num = new int[] { -4, -3, 0, 1, 4, 5 };
        else if (index == 5 || index == 6 || index == 9 || index == 10)
            num = new int[] { -5, -4, -3, -1, 0, 1, 3, 4, 5 };
        else if (index == 7 || index == 11)
            num = new int[] { -5, -4, -1, 0, 3, 4 };
        else if (index == 12)
            num = new int[] { -4, -3, 0, 1 };
        else if (index == 13 || index == 14)
            num = new int[] { -5, -4, -3, -1, 0, 1 };
        else if (index == 15)
            num = new int[] { -5, -4, 0, -1 };
        foreach (int n in num)
        {
            if (sprite2[index + n] == sprites[0])
            {
                sprite2[index + n] = sprites[1];
            }
            else if (sprite2[index + n] == sprites[1])
            {
                sprite2[index + n] = sprites[0];
            }
        }
    }

    void NextLevel()
    {
        doingSetup = true;
        sprite1 = new List<Sprite>();
        sprite2 = new List<Sprite>();
        int rand = Random.Range(0, 15);
        if (level == 1)
        {
            levelImage.SetActive(true);
            movementsAllowed = 1;
            for (int i = 0; i < 16; i++)
            {
                Sprite sprite = sprites[Random.Range(0, sprites.Length)];
                sprite1.Add(sprite);
                sprite2.Add(sprite);
            }
            UpdateCrate(rand);
        }
        else if (level == 2)
        {
            board = GameObject.Find("Board");
            Invoke("HideBoard", 1);
            movementsAllowed = 5;
            for (int i = 0; i < 16; i++)
            {
                Sprite sprite = sprites[Random.Range(0, sprites.Length)];
                sprite1.Add(sprite);
                sprite2.Add(sprite);
            }
            UpdateCrate(rand);
            int rand2 = Random.Range(0, 15);
            while (rand2 == rand)
            {
                rand2 = Random.Range(0, 15);
            }
            UpdateCrate(rand2);
        }
        /**else if (level == 3)
        {
            board = GameObject.Find("Board");
            Invoke("HideBoard", 1);
            movementsAllowed = 10;
            for (int i = 0; i < 16; i++)
            {
                Sprite sprite = sprites[Random.Range(0, sprites.Length)];
                sprite1.Add(sprite);
                sprite2.Add(sprite);
            }
            UpdateCrate(rand);
            int rand2 = Random.Range(0, 15);
            while (rand2 == rand)
            {
                rand2 = Random.Range(0, 15);
            }
            UpdateCrate(rand2);
            int rand3 = Random.Range(0, 15);
            while (rand3 == rand2 && rand3 == rand)
            {
                rand3 = Random.Range(0, 15);
            }
            UpdateCrate(rand3);
        }**/
        else
        {
            end = true;
            levelText.text = "You have finished the game";
            Invoke("HideBoard", 1);
            StartCoroutine(GameWon());
        }
        if (!end)
        {
            levelText.text = "Level " + level;
            Invoke("HideLevelImage", levelStartDelay);
            movementsRemaining = movementsAllowed;
        }
    }

    IEnumerator GameWon()
    {

        yield return new WaitForSeconds(1.9f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WinSceneJuego2");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.9f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOverSceneJuego2");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}