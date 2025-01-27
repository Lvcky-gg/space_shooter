using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle on text component
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
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

    }
    public void SetScore(int score)
    {
        _gameOverText.text = "";
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
       
            _gameOverText.text = "GAME OVER";
        
    }
}
