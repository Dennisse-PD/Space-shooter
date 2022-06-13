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
    private Text _ammoText;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImg;

    private GameManager _gameManager;

    [SerializeField]
    private Text _waveText; //will go up everytime an enemy wave starts, and display with the text
    private int _currentWave = 0;

    private bool _isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        _waveText.text = "Wave " +  _currentWave; //sets the text value at the start. should I put this in update later?

        // StartCoroutine(TickFiveSeconds());
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        //get the text here from the player ammo thing, update the text here
        _scoreText.text = "Score: " + 0;
        _ammoText.text = "15/"+ 15.ToString();

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
        _ammoText.text =  "15/" + playerAmmo.ToString();
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
    //this [TEST] croutine plays for 5 seconds by controlling it with a counter.I can use it for the Wave Text Later
    IEnumerator TickFiveSeconds()
    {
        var wait = new WaitForSeconds(1f);
        int counter = 1;
        while (counter < 5)
        {
            Debug.Log("Tick");
            counter++;
            yield return wait;
        }
        Debug.Log("I am done ticking");
    }
}


   
       



