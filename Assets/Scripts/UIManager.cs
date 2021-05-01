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
    private Image _livesImg;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void updateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
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
   
       



