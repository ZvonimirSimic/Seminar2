using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KvizController : MonoBehaviour
{
    public float VrijemePitanja = 30;
    private float lastTime, newTime;
    private List<TimerObserver> timerObservers;
    System.Random rnd;
    private int trenutno = -1, zadnje, tocOdg, brTocnih;
    public GameObject odgovoriPanel, lijevi, desni;
    private GameObject postotakPloca;
    private List<string> pitanja = new List<string>();
    private List<List<string>> odgovori = new List<List<string>>();
    private double postotak = 0;
    private bool mjeriVrijeme = false, zavrsiPitanje = false, zapocni = false, ponovnoZapocni = false;
    public bool uTijeku = false;
    private Vector3 pocetnaLokacija;
    public void zapocniKviz()
    {
        uTijeku = true;
        for(int p = 0; p < gameObject.transform.childCount; p++)
        {
            if (gameObject.transform.GetChild(p).name == "Postotak")
            {
                postotakPloca = gameObject.transform.GetChild(p).gameObject;
            }
        }
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        const Int32 BufferSize = 128;
        int i = 0;
        using (var fileStream = File.OpenRead("PitIOdg.txt"))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            String line;
            List<string> odg = null;
            Debug.Log("Tu sam");

            while ((line = streamReader.ReadLine()) != null)
            {
                if (i % 5 == 0)
                {
                    pitanja.Add(line);
                    if (odg != null)
                    {
                        odgovori.Add(odg);
                    }
                    odg = new List<string>();
                }
                else
                {
                    odg.Add(line);
                }
                i++;
            }
            odgovori.Add(odg);
        }
        rnd = new System.Random(DateTime.Now.Millisecond);
        random(rnd);
        lastTime = Time.time;
        newTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(VrijemePitanja);
        notifyTimerObservers(newTime-lastTime);
        mjeriVrijeme = true;
        odgovoriPanel.SetActive(true);
        trenutno = 0;
        brTocnih = 0;
        postotak = 0;
        zadnje = pitanja.Count;
        gameObject.transform.GetChild(trenutno).gameObject.GetComponent<TextMeshPro>().text = trenutno + 1 + ". " + pitanja[trenutno];
        for(int j = 0; j < 4; j++)
        {
            string odg = odgovori[trenutno][j];
            if (odg.StartsWith("!"))
            {
                odg = odg.Substring(1);
                tocOdg = j;
            }
            odgovoriPanel.gameObject.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = odg;
        }
    }
    public void zapoc()
    {
        lastTime = Time.time;
        newTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(5);
        notifyTimerObservers(newTime - lastTime);
        zapocni = true;
    }
    public void pZapoc()
    {
        lastTime = Time.time;
        newTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(5);
        notifyTimerObservers(newTime - lastTime);
        ponovnoZapocni = true;
    }
    public void ponovnoZapocniKviz()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        rnd = new System.Random(DateTime.Now.Millisecond);
        random(rnd);
        lastTime = Time.time;
        newTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(VrijemePitanja);
        notifyTimerObservers(newTime - lastTime);
        mjeriVrijeme = true;
        odgovoriPanel.SetActive(true);
        trenutno = 0;
        brTocnih = 0;
        postotak = 0;
        zadnje = pitanja.Count;
        gameObject.transform.GetChild(trenutno).gameObject.GetComponent<TextMeshPro>().text = trenutno + 1 + ". " + pitanja[trenutno];
        for (int j = 0; j < 4; j++)
        {
            string odg = odgovori[trenutno][j];
            if (odg.StartsWith("!"))
            {
                odg = odg.Substring(1);
                tocOdg = j;
            }
            odgovoriPanel.gameObject.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = odg;
        }
    }

    private void postaviPitanje()
    {
        lijevi.GetComponent<LineRenderer>().enabled = true;
        desni.GetComponent<LineRenderer>().enabled = true;
        lijevi.GetComponent<LineRendererSettingsLeft>().enabled = true;
        desni.GetComponent<LineRendererSettings>().enabled = true;
        if ((trenutno+1) == zadnje)
        {
            zavrsiKviz();
        }
        else
        {
            lastTime = Time.time;
            newTime = Time.time;
            GetComponentInChildren<Timer>().setVrijeme(VrijemePitanja);
            notifyTimerObservers(newTime - lastTime);
            mjeriVrijeme = true;
            trenutno++;
            Debug.Log("Broj djece: " + gameObject.transform.childCount);
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = trenutno + 1 + ". " + pitanja[trenutno];
            for (int j = 0; j < 4; j++)
            {
                string odg = odgovori[trenutno][j];
                if (odg.StartsWith("!"))
                {
                    odg = odg.Substring(1);
                    tocOdg = j;
                }
                odgovoriPanel.gameObject.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = odg;
            }
        }
    }

    private void zavrsiKviz()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "Kviz završen!";
        trenutno = -1;
        odgovoriPanel.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void odgovorNaPitanje(int i)
    {
        lijevi.GetComponent<LineRenderer>().enabled = false;
        desni.GetComponent<LineRenderer>().enabled = false;
        lijevi.GetComponent<LineRendererSettingsLeft>().enabled = false;
        desni.GetComponent<LineRendererSettings>().enabled = false;
        mjeriVrijeme = false;
        odgovoriPanel.gameObject.transform.GetChild(tocOdg).GetComponent<Image>().color = Color.green;
        if(i == tocOdg)
        {
            brTocnih++;
        }
        else
        {
            odgovoriPanel.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.red;
        }
        postotak = (double)brTocnih / (trenutno + 1) * 100;
        if ((int)postotak == postotak)
        {
            postotakPloca.gameObject.GetComponent<TextMeshPro>().text = "Postotak: " + postotak + "% (" + brTocnih + "/" + (trenutno + 1) + ")";
        }
        else
        {
            postotakPloca.gameObject.GetComponent<TextMeshPro>().text = "Postotak: " + postotak.ToString("0.00") + "% (" + brTocnih + "/" + (trenutno + 1) + ")";
        }
        lastTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(5);
        notifyTimerObservers(newTime - lastTime);
        zavrsiPitanje = true;
    }

    private void random(System.Random rnd)
    {
        int n = pitanja.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            string s = pitanja[k];
            pitanja[k] = pitanja[n];
            pitanja[n] = s;
            List<String> pit = odgovori[k];
            odgovori[k] = odgovori[n];
            odgovori[n] = pit;
        }
        for(int i = 0; i < odgovori.Count; i++)
        {
            n = 4;
            while(n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string s = odgovori[i][k];
                odgovori[i][k] = odgovori[i][n];
                odgovori[i][n] = s;
            }
        }        
    }

    private void addTimerObserver(TimerObserver to)
    {
        timerObservers.Add(to);
    }

    private void removeTimerObserver(TimerObserver to)
    {
        timerObservers.Remove(to);
    }

    private void notifyTimerObservers(double time)
    {
        foreach (TimerObserver to in timerObservers)
        {
            to.timePassed(time);
        }
    }

    private void vrijemeIsteklo()
    {
        lijevi.GetComponent<LineRenderer>().enabled = false;
        desni.GetComponent<LineRenderer>().enabled = false;
        lijevi.GetComponent<LineRendererSettingsLeft>().enabled = false;
        desni.GetComponent<LineRendererSettings>().enabled = false;
        mjeriVrijeme = false;
        odgovoriPanel.gameObject.transform.GetChild(tocOdg).GetComponent<Image>().color = Color.green;
        postotak = (double)brTocnih / (trenutno + 1) * 100;
        if ((int)postotak == postotak)
        {
            postotakPloca.gameObject.GetComponent<TextMeshPro>().text = "Postotak: " + postotak + "% (" + brTocnih + "/" + (trenutno + 1) + ")";
        }
        else
        {
            postotakPloca.gameObject.GetComponent<TextMeshPro>().text = "Postotak: " + postotak.ToString("0.00") + "% (" + brTocnih + "/" + (trenutno + 1) + ")";
        }
        lastTime = Time.time;
        newTime = Time.time;
        GetComponentInChildren<Timer>().setVrijeme(5);
        notifyTimerObservers(newTime - lastTime);
        zavrsiPitanje = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*timerObservers = new List<TimerObserver>();
        addTimerObserver(GetComponentInChildren<Timer>());
        Debug.Log("Timer: " + GetComponentInChildren<Timer>().name);*/
    }

    // Update is called once per frame
    void Update()
    {
        newTime = Time.time;
        if (mjeriVrijeme)
        {
            notifyTimerObservers(newTime - lastTime);
            if(newTime-lastTime > VrijemePitanja)
            {
                vrijemeIsteklo();
            }
        }else if (zavrsiPitanje)
        {
            notifyTimerObservers(newTime - lastTime);
            if (newTime - lastTime > 5.0f)
            {
                zavrsiPitanje = false;
                for (int i = 0; i < odgovoriPanel.gameObject.transform.childCount; i++)
                {
                    odgovoriPanel.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
                postaviPitanje();
            }
        }else if (zapocni)
        {
            notifyTimerObservers(newTime - lastTime);
            if (newTime - lastTime > 5.0f)
            {
                zapocni = false;
                for (int i = 0; i < odgovoriPanel.gameObject.transform.childCount; i++)
                {
                    odgovoriPanel.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
                zapocniKviz();
            }
        }
        else if (ponovnoZapocni)
        {
            notifyTimerObservers(newTime - lastTime);
            if (newTime - lastTime > 5.0f)
            {
                ponovnoZapocni = false;
                for (int i = 0; i < odgovoriPanel.gameObject.transform.childCount; i++)
                {
                    odgovoriPanel.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
                ponovnoZapocniKviz();
            }
        }
    }
}
