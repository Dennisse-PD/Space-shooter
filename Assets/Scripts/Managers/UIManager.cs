using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;


    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImg;

    private GameManager _gameManager;
    private bool _isAlive = true;

    //Game won
    [SerializeField]
    private Text _playAgain;
    [SerializeField]
    private Text _gameWonText;
    private bool _isBossAlive = true;
   
    private BossFight _boss;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
       // _boss = GameObject.Find("BossFight").GetComponent<BossFight>();
       


        _scoreText.text = "Score: " + 0;
        _ammoText.text = 15 .ToString();

        if (_gameManager == null)
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
    public void updateAmmoCount(int playerAmmo)
    {
        _ammoText.text = playerAmmo +" /15".ToString();
    }

    public void updateLives(int currentLives)
    {
        
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives < 1)
        {
            onPlayerDeath();
            GameOverSequence(); 
        }
         
        
    }
    void onPlayerDeath()
    {
        _isAlive = false;
    }
    private void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }
   

    IEnumerator GameOverFlicker()
        {
        while (_isAlive == false)
                {
                
                _gameOverText.enabled = false;
                yield return new WaitForSeconds(0.5f);
                _gameOverText.enabled = true;
                yield return new WaitForSeconds(0.5f);
            }
        _gameOverText.enabled = false;
    }
    public void GameWonSequence()
    {
        BossIsDead();
        _gameWonText.gameObject.SetActive(true);
        _playAgain.gameObject.SetActive(true);
        StartCoroutine(GameWonFlicker());
        PlayAgain();

    }
    private void PlayAgain()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
    void BossIsDead()
    {
        _isBossAlive = false;
    }
    IEnumerator GameWonFlicker()
    {
        while (_isBossAlive == false)
        {

            _gameWonText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _gameWonText.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        _gameWonText.enabled = false;
    }

}



   
       



