using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("Low Score")]
    [SerializeField] string terrible = "Terrible";
    [SerializeField] Color red;

    [Header("Average Score")]
    [SerializeField] string goodJob = "Good Job";
    [SerializeField] Color yellow;

    [Header("High Score")]
    [SerializeField] string greatJob = "Great Job";
    [SerializeField] Color green;

    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionSeen()
    {
        questionsSeen++;
    }

    public string FinalScore()
    {
        return correctAnswers + "/" + questionsSeen;
    }

    public void DisplayResult(TextMeshProUGUI gradetext)
    {

        switch (correctAnswers)
        {
            case 0:
                gradetext.text = terrible;
                gradetext.color = red;
                break;
            case 1:
                gradetext.text = terrible;
                gradetext.color = red;
                break;
            case 5:
                gradetext.text = greatJob;
                gradetext.color = green;
                break;
            default:
                gradetext.text = goodJob;
                gradetext.color = yellow;
                break;
        }

    }
}
