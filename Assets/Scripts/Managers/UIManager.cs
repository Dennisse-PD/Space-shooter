using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImg;

    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;

        if(_gameManager == null)
        {
            Debug.LogError("The Game_Manager is NULL!");
        }

  
      
    }
    void Update()
    {
        
    }

    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void updateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }

        void GameOverSequence()
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
            _gameManager.GameOver();
        }

        IEnumerator GameOverFlicker()
        {
            while (currentLives == 0)
                {
                
                _gameOverText.enabled = false;
                yield return new WaitForSeconds(0.5f);
                _gameOverText.enabled = true;
                yield return new WaitForSeconds(0.5f);
            }
            
        }
    }

}
   
       



