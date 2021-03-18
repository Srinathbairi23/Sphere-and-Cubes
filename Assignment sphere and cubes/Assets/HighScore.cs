using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    Text highscore;
    GameObject game;
    private void Start()
    {
        highscore = GetComponent<Text>();
        string score = PlayerPrefs.GetInt("highscore").ToString();
        highscore.text = "High Score: " + score; 
    }

    //instantiating game obj which contains player and cubes.
    public void InstantiateGamePrefab(GameObject _object)
    {
        Instantiate(_object);
        game = _object;
    }
    
    //reloding the game
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
