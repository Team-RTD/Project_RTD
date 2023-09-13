using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button reStart;
    public Button exit;
    public GameObject gameOver;

    // Update is called once per frame
    void Update()
    {
        if (Data_Manager.instance.curHp <= 0)
        {
            gameOver.SetActive(true);
            Data_Manager.instance.isPause = true;
            Time.timeScale = 0;
        }
    }

    public void ReStart()
    {
        Data_Manager.instance.isPause = false;
        Time.timeScale = 1;
        Data_Manager.instance.curHp = 5;
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
