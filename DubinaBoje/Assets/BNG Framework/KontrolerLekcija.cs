using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KontrolerLekcija : MonoBehaviour
{
    private int indeksLekcije;
    public List<GameObject> lekcije;
    private GameObject sljedeci, prethodni, povratakGumb, slika, trenutnaLekcija;
    private List<string> tekst;
    private int indeks, indeksSlike;
    private bool slikaPrije = false;
    public void odaberiLekciju(int ind)
    {
        indeksLekcije = ind;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Naslov")
            {
                gameObject.transform.GetChild(i).GetComponent<TextMeshPro>().text = lekcije[ind].name;
            }
            else if (gameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Button>() != null)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (gameObject.transform.GetChild(i).name == "Prethodna")
            {
                prethodni = gameObject.transform.GetChild(i).gameObject;
            }
            if (gameObject.transform.GetChild(i).name == "Sljedeca")
            {
                sljedeci = gameObject.transform.GetChild(i).gameObject;
            }
            if (gameObject.transform.GetChild(i).name == "Povratak")
            {
                povratakGumb = gameObject.transform.GetChild(i).gameObject;
                povratakGumb.SetActive(true);
            }
            if (gameObject.transform.GetChild(i).name == "Slika")
            {
                slika = gameObject.transform.GetChild(i).gameObject;
            }
        }
        trenutnaLekcija = lekcije[ind];
        tekst = trenutnaLekcija.GetComponent<TextLekcija>().tekst;        
        indeks = -1;
        indeksSlike = -1;
        sljedeci.SetActive(true);
        ispisiDalje();
    }

    private string splitStr(string str)
    {
        string[] strings;
        if (str.Contains("{!}"))
        {
            Debug.Log("Splitanje se dogada!");
            strings = str.Split("{!}");
            string s = strings[0];
            Debug.Log(s);
            for (int i = 1; i < strings.Length; i++)
            {
                s += "\n";
                s += strings[i];
                Debug.Log(s);
            }
            str = s;
        }
        return str;
    }

    public void ispisiDalje()
    {
        indeks++;
        if (indeks > 0)
        {
            prethodni.gameObject.SetActive(true);
        }
        if (tekst[indeks].StartsWith("!"))
        {
            indeksSlike++;
            slika.gameObject.SetActive(true);
            slika.gameObject.GetComponent<RawImage>().texture = trenutnaLekcija.GetComponent<TextLekcija>().slikaInd(indeksSlike);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";
            slikaPrije = true;
        }
        else
        {
            slika.gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = splitStr(tekst[indeks]);
            slikaPrije = false;
        }
        if (indeks + 1 == tekst.Count)
        {
            sljedeci.gameObject.SetActive(false);
        }
        Debug.Log("Indeks: " + indeks);
    }

    public void ispisiPrije()
    {
        if (slikaPrije)
        {
            indeksSlike--;
            slikaPrije = false;
        }
        indeks--;
        sljedeci.gameObject.SetActive(true);
        if (tekst[indeks].StartsWith("!"))
        {
            slika.gameObject.SetActive(true);
            slika.gameObject.GetComponent<RawImage>().texture = trenutnaLekcija.GetComponent<TextLekcija>().slikaInd(indeksSlike);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";
            slikaPrije = true;
        }
        else
        {
            slika.gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = splitStr(tekst[indeks]);
            slikaPrije = false;
        }
        if (indeks == 0)
        {
            prethodni.gameObject.SetActive(false);
        }
        Debug.Log("Indeks: " + indeks);
    }
    // Start is called before the first frame update

    public void povratak()
    {
        tekst = new List<string>();
        indeks = -1;
        indeksSlike = -1;
        slika.gameObject.SetActive(false);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Naslov")
            {
                gameObject.transform.GetChild(i).GetComponent<TextMeshPro>().text = "Odaberi lekciju";
            }
            else if (gameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Button>() != null)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else if (gameObject.transform.GetChild(i).name == "Tekst na ploci")
            {
                gameObject.transform.GetChild(i).GetComponent<TextMeshPro>().text = "";
            }
        }
        povratakGumb.SetActive(false);
        prethodni.gameObject.SetActive(false);
        sljedeci.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
