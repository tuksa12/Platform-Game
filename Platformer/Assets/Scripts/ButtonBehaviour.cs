using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void LoadLevelByIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ResetStats()
    {
        PlayerMovement.coins = 0;
        PlayerMovement.lives = 8;
    }

    [SerializeField]
    int scene;

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
        {
            ResetStats();
            SceneManager.LoadScene(scene);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    void QuitGame()
    {
        Application.Quit();
    }
}