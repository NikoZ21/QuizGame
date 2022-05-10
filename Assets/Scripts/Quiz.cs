using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("General")]
    public bool isComplete = false;

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Butttons")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Question Counter")]
    [SerializeField] TextMeshProUGUI questionCounter;
    private int _questionIndex = 1;
    private int _numberOfQuestions;

    [Header("Scoring")]
    private Score _scoreKeeper;


    private void Awake()
    {
        _numberOfQuestions = questions.Count;
        _scoreKeeper = FindObjectOfType<Score>();
    }

    void Start()
    {
        _scoreKeeper.IncrementQuestionSeen();
        GetRandomQuestion();
        DisplayQuestion();
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            _scoreKeeper.IncrementQuestionSeen();
            UpdateQuesitionCounter();
            GetRandomQuestion();
            DisplayQuestion();
            SetButtonState(true);
            SetDefaultButtonSprites();
        }
        else
        {
            isComplete = true;
        }
    }

    private void UpdateQuesitionCounter()
    {
        _questionIndex++;
        questionCounter.text = _questionIndex.ToString() + "/" + _numberOfQuestions.ToString();
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        questions.Remove(currentQuestion);
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        SetButtonState(false);
        DisplayAnswer(index);
    }

    private void DisplayAnswer(int index)
    {
        StartCoroutine(DisplayAnswerCoroutine(index));
    }

    private IEnumerator DisplayAnswerCoroutine(int index)
    {
        Image correctButtonImage;
        Image wrongButtonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            _scoreKeeper.IncrementCorrectAnswers();
            correctButtonImage = answerButtons[index].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
        else
        {
            var correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();

            correctButtonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            correctButtonImage.sprite = correctAnswerSprite;

            wrongButtonImage = answerButtons[index].GetComponentInChildren<Image>();
            wrongButtonImage.sprite = wrongAnswerSprite;
        }

        yield return new WaitForSeconds(2f);

        GetNextQuestion();
    }
}
