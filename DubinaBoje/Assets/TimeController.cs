using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Text _heading;
    [SerializeField] private float _totalTime = 30f;

    public static Action OnTimerEnded;
    public static Action<bool> OnDisableButtons;

    private float _currentTime;
    private bool _isReady = false, nextQuestion = false, correct = false;

    private void OnEnable()
    {
        QuizController.OnStartTimer += SetTimer;
        QuizController.OnQuizEnded += ResetHeading;
        AnswerButton.OnAnswerClicked += PauseTimer;
    }

    private void OnDisable()
    {
        QuizController.OnStartTimer -= SetTimer;
        QuizController.OnQuizEnded -= ResetHeading;
        AnswerButton.OnAnswerClicked -= PauseTimer;
    }

    private void Start()
    {
        ResetTimer();
    }

    public void setCorrect()
    {
        _currentTime = 0;
    }

    private void Update()
    {
        if (_isReady)
        {
            if (_currentTime > 0f)
            {
                StartTimer();
            }
            else
            {
                _heading.text = "Vrijeme isteklo!";
                _isReady = false;
                OnTimerEnded?.Invoke();
                OnDisableButtons?.Invoke(false);
                ResetTimer();
                /*for (int i = 0; i < 4; i++)
                {
                    GameObject answer = GetComponentInParent<NextQuestion>().quiz.GetComponent<QuizController>()._landingPage.transform.GetChild(GetComponentInParent<NextQuestion>().quiz.GetComponent<QuizController>()._landingPage.transform.childCount-1).transform.GetChild(i).gameObject;
                    if (!answer.GetComponent<AnswerButton>().Correct)
                    {
                        answer.GetComponent<AnswerButton>().AnswerButtonClicked();
                        break;
                    }
                }*/
                GetComponentInParent<NextQuestion>().setTime();
                _currentTime = 5f;
                nextQuestion = true;
            }
        }
        else if(nextQuestion)
        {
            if (_currentTime > 0f)
            {
                StartTimer();
            }
            else
            {
                GetComponentInParent<NextQuestion>().Next();
                nextQuestion = false;
                correct = false;
            }
        }
    }

    private void StartTimer()
    {
        _currentTime -= Time.deltaTime;
        _heading.text = "Preostalo: " + _currentTime.ToString("F2") + " s";
        _heading.fontSize = 22;
    }

    public void EndTimer()
    {
        _currentTime = 5f;
        _heading.text = "Preostalo: " + _currentTime.ToString("F2") + " s";
        _heading.fontSize = 22;
        nextQuestion = true;
    }

    private void SetTimer()
    {
        ToggleTimer(true);
        ResetTimer();
    }

    private void ResetTimer()
    {
        _currentTime = _totalTime;
    }

    private void ToggleTimer(bool toggleValue)
    {
        _isReady = toggleValue;
    }

    private void PauseTimer(bool value)
    {
        Debug.Log("VRIJEDNOST JE " + value);
        if (value)
        {
            ToggleTimer(!value);
        }
        else
        {
            ToggleTimer(value);
        }
    }

    private void ResetHeading()
    {
        _heading.text = "Kviz";
        _heading.fontSize = 30;
    }
}
