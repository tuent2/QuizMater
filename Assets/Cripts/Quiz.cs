using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questionSO = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [Header("Answer")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    [Header("Button Color")]
    [SerializeField] Sprite DefaultAnswerSprite;
    [SerializeField] Sprite CorrectAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI score;
    CountScore countScore;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    // Start is called before the first frame update
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        // getNextQuestion();
        //displayQuestion();
        countScore = FindObjectOfType<CountScore>();
        progressBar.maxValue = questionSO.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    public void onAnswerSelected(int index)
    {
        // if(index == questionSO.getCorrentAnwerIndex()){
        //     questionText.text = "Correct";
        //     Image buttonImage = answerButton[index].GetComponent<Image>();
        //     buttonImage.sprite = CorrectAnswerSprite;
        // } else {
        //     questionText.text = "Câu trả lời đúng là: " + questionSO.getAnswer(questionSO.getCorrentAnwerIndex());
        //     Image buttonCorrectAnswerImage = answerButton[questionSO.getCorrentAnwerIndex()].GetComponent<Image>();
        //     buttonCorrectAnswerImage.sprite = CorrectAnswerSprite;
        // }

        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.CancerTimer();
        score.text = "Score: " + countScore.CalculateScore() + "%";

    }

    void displayAnswer(int index)
    {
        if (index == currentQuestion.getCorrentAnwerIndex())
        {
            questionText.text = "Correct";
            Image buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = CorrectAnswerSprite;
            countScore.IncrementCorrectAnswers();

        }
        else
        {
            questionText.text = "Câu trả lời đúng là: " + currentQuestion.getAnswer(currentQuestion.getCorrentAnwerIndex());
            Image buttonCorrectAnswerImage = answerButton[currentQuestion.getCorrentAnwerIndex()].GetComponent<Image>();
            buttonCorrectAnswerImage.sprite = CorrectAnswerSprite;
        }
        // setButtonState(false);
        // timer.CancerTimer();
    }
    void getNextQuestion()
    {
        if (questionSO.Count > 0)
        {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuestion();
            progressBar.value++;
            countScore.IncrementquestionSeen();
        }
    }

    void getRandomQuestion()
    {
        int index = Random.Range(0, questionSO.Count);
        currentQuestion = questionSO[index];
        if (questionSO.Contains(currentQuestion))
        {
            questionSO.Remove(currentQuestion);
        }


    }

    void setDefaultButtonSprites()
    {
        // Image correctAnswerImage = answerButton[currentQuestion.getCorrentAnwerIndex()].GetComponent<Image>();
        // correctAnswerImage.sprite = DefaultAnswerSprite;

        for (int i = 0; i < answerButton.Length; i++)
        {
            Image correctAnswerImage = answerButton[i].GetComponent<Image>();
            correctAnswerImage.sprite = DefaultAnswerSprite;
        }
    }

    void displayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();
        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
    }

    void setButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }


}
