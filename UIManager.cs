using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //handle on text component
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    // Start is called before the first frame update
    void Start()
    {
       // _liveSprites
        _scoreText.text = "Score:  " + 0;
    }

    // Update is called once per frame
    void Update()
    {

        GameOverSequence();
    }
    void GameOverSequence()
    {
      
            if (_restartText.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        
    }
    public void SetScore(int score)
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score:  " + score;
    }
    public void UpdateLives(int currentLives)
    {
        switch(currentLives)
        {
            case 0:
                _LivesImg.sprite = _liveSprites[0];
                GameOver();
                break;
            case 1:
                _LivesImg.sprite = _liveSprites[1];
                break;
            case 2:
                _LivesImg.sprite = _liveSprites[2];
                break;
            case 3:
                _LivesImg.sprite = _liveSprites[3];
                break;

        }
    }
    void GameOver()
    {
       
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverRoutine());
            

    }
    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }
}
