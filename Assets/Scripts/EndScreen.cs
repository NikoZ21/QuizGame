using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI gradetext;
    private Score _scoreKeeper;

    void Awake()
    {
        _scoreKeeper = FindObjectOfType<Score>();
    }

    void OnEnable()
    {
        finalScoreText.text = _scoreKeeper.FinalScore();
        _scoreKeeper.DisplayResult(gradetext);
    }
}
