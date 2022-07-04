using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private BossFight _boss;

    // Start is called before the first frame update
    void Start()
    {
      

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
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == false)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
