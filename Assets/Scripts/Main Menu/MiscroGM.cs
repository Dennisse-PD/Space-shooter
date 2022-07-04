using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscroGM : MonoBehaviour
{
    private bool _isGameOver = false;
    private BossFight _boss;

    // Start is called before the first frame update
    void Start()
    {
        _boss = GameObject.Find("Boss").GetComponent<BossFight>();

    }

    // Update is called once per frame
    void Update()
    {
        UserInputOptions();
    }
    public void GameOver()
    {

        _isGameOver = true;
    }
    void UserInputOptions()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == false|| _boss._isBossAlive == false)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

