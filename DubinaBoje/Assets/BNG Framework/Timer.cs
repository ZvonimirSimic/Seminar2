using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, TimerObserver
{
    private float pocVel;
    private double vrijeme;
    private double trenutnoVrijeme;

    public void setVrijeme(double vrij)
    {
        vrijeme = vrij;
        trenutnoVrijeme = vrij;
    }

    public void timePassed(double promj)
    {
        //Debug.Log("Time passed: " + (vrijeme - promj));
        trenutnoVrijeme = vrijeme - promj;
        if (trenutnoVrijeme <= 0)
        {
            trenutnoVrijeme = 0;
            float postotakVremena = (float)(trenutnoVrijeme / vrijeme);
            transform.localScale = new Vector3(postotakVremena, transform.localScale.y, transform.localScale.z);
            //transform.localPosition = new Vector3(-((1 - postotakVremena) / 2), transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            float postotakVremena = (float)(trenutnoVrijeme / vrijeme);
            //Debug.Log("Lokacija: " + (-((1 - postotakVremena) / 2)));
            transform.localScale = new Vector3(postotakVremena*pocVel, transform.localScale.y, transform.localScale.z);
            //transform.localPosition = new Vector3(-postotakVremena*pocLok/2+3*pocLok/2, transform.localPosition.y, transform.localPosition.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pocVel = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
