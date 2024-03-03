using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowValue : MonoBehaviour
{
    public GameObject izvor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (izvor.GetComponent<ImageSelector>() != null)
        {
            if (izvor.GetComponent<ImageSelector>().size != 0)
            {
                    GetComponentInChildren<TextMeshPro>().text = "Zauzeæe slike na disku: " + izvor.GetComponent<ImageSelector>().size.ToString() + "MB";            
            }
        }
        else if(izvor.GetComponent<ImageSelector3D>() != null)
        {
            if (izvor.GetComponent<ImageSelector3D>().size != 0)
            {
                GetComponentInChildren<TextMeshPro>().text = "Zauzeæe slike na disku: " + izvor.GetComponent<ImageSelector3D>().size.ToString() + "MB";
            }
        }
    }
}
