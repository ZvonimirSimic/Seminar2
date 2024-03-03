using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuestion : MonoBehaviour
{
    public GameObject lijevi, desni, quiz;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setTime()
    {
        lijevi.GetComponent<LineRenderer>().enabled = false;
        desni.GetComponent<LineRenderer>().enabled = false;
        lijevi.GetComponent<LineRendererSettingsLeft>().enabled = false;
        desni.GetComponent<LineRendererSettings>().enabled = false;
        quiz.GetComponentInChildren<TimeController>().EndTimer();
    }

    public void Next()
    {
        lijevi.GetComponent<LineRenderer>().enabled = true;
        desni.GetComponent<LineRenderer>().enabled = true;
        lijevi.GetComponent<LineRendererSettingsLeft>().enabled = true;
        desni.GetComponent<LineRendererSettings>().enabled = true;
        quiz.GetComponent<QuizController>().NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
