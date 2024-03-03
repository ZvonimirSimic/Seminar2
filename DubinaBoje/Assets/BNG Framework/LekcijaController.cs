using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LekcijaController : MonoBehaviour
{
    private string indeksLekcije;
    List<List<string>> lekcija = new List<List<string>>();
    private int indeks;
    private GameObject sljedeci, prethodni, povratakGumb, slika;
    public void odaberiLekciju(string name)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Naslov")
            {
                gameObject.transform.GetChild(i).GetComponent<TextMeshPro>().text = name;
            }
            else if (gameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Button>() != null)
            {
                if (gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).name == name)
                {
                    indeksLekcije = gameObject.transform.GetChild(i).name.Substring(gameObject.transform.GetChild(i).name.Length-1);
                }
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
        string imeLekcije = "Assets/Lekcija" + indeksLekcije.ToString() + ".txt";
        const Int32 BufferSize = 128;
        using (var fileStream = File.OpenRead(imeLekcije))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            String line;
            List<string> odg = new List<string>();
            while ((line = streamReader.ReadLine()) != null)
            {
                if(line != "")
                {
                    odg.Add(line);
                }
                else
                {
                    lekcija.Add(odg);
                    odg = new List<string>();
                }
            }
            lekcija.Add(odg);
        }
        indeks = -1;
        sljedeci.SetActive(true);
        ispisiDalje();
    }

    

    public static Texture2D LoadImg(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    private void showImage(string ime)
    {
        string result = ime.Substring(1);
        Debug.Log("Ime slike: " + result);
        string s = "Assets/" + result;
        Texture2D imageForMaterial = LoadImg(s);
        slika.gameObject.GetComponent<RawImage>().texture = imageForMaterial;
    }

    public void ispisiDalje()
    {
        indeks++;
        if (indeks > 0)
        {
            prethodni.gameObject.SetActive(true);
        }
        string s = "";
        for(int i = 0; i < lekcija[indeks].Count; i++)
        {
            s = s + lekcija[indeks][i];
            if((i+1) != lekcija[indeks].Count) {
                s = s + "\n";
            }
        }
        if (s.StartsWith("!"))
        {
            slika.gameObject.SetActive(true);
            showImage(s);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";
        }
        else
        {
            slika.gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = s;
        }
        if(indeks + 1 == lekcija.Count)
        {
            sljedeci.gameObject.SetActive(false);
        }
        Debug.Log("Indeks: " + indeks);
    }
    public void ispisiPrije()
    {
        indeks--;
        sljedeci.gameObject.SetActive(true);
        string s = "";
        for (int i = 0; i < lekcija[indeks].Count; i++)
        {
            s = s + lekcija[indeks][i];
            if ((i + 1) != lekcija[indeks].Count)
            {
                s = s + "\n";
            }
        }
        if (s.StartsWith("!"))
        {
            slika.gameObject.SetActive(true);
            showImage(s);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";
        }
        else
        {
            slika.gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = s;
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
        lekcija = new List<List<string>>();
        indeks = -1;
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
            else if(gameObject.transform.GetChild(i).name == "Tekst na ploci")
            {
                gameObject.transform.GetChild(i).GetComponent<TextMeshPro>().text = "";
            }
        }
        povratakGumb.SetActive(false);
        prethodni.gameObject.SetActive(false);
        sljedeci.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
