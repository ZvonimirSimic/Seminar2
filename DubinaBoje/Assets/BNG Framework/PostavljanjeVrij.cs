using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostavljanjeVrij : MonoBehaviour
{
    public GameObject manipulator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manipulator.GetComponent<ImageManipulator>() != null)
        {
            if(manipulator.GetComponent<ImageManipulator>().zauzece != 0)
            {
                if (manipulator.GetComponent<ImageManipulator>().zauzece < 1024)
                {
                    GetComponentInChildren<TextMeshPro>().text = "Zauzece slike na disku: " + manipulator.GetComponent<ImageManipulator>().zauzece.ToString() + "KB";
                }
                else
                {
                    GetComponentInChildren<TextMeshPro>().text = "Zauzece slike na disku: " + (manipulator.GetComponent<ImageManipulator>().zauzece/1024).ToString() + "MB";
                }
            }
        }
        else if (manipulator.GetComponent<Object3DManipulator>() != null)
        {
            if (manipulator.GetComponent<Object3DManipulator>().zauzece != 0)
            {
                if (manipulator.GetComponent<Object3DManipulator>().zauzece < 1024)
                {
                    GetComponentInChildren<TextMeshPro>().text = "Zauzece slike na disku: " + manipulator.GetComponent<Object3DManipulator>().zauzece.ToString() + "KB";
                }
                else
                {
                    GetComponentInChildren<TextMeshPro>().text = "Zauzece slike na disku: " + (manipulator.GetComponent<Object3DManipulator>().zauzece / 1024).ToString() + "MB";
                }
            }
        }
    }
}
