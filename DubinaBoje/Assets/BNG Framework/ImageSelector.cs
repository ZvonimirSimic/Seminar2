using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImageSelector : MonoBehaviour
{
    public List<Texture2D> images;
    private List<double> sizes;
    public double size;
    public void showImage(int num)
    {
        Material mat = new Material(Shader.Find("Standard"));
        Material[] materials = GetComponent<MeshRenderer>().materials;
        mat.mainTexture = images[num];
        materials[0] = mat;
        GetComponent<MeshRenderer>().materials = materials;
        Debug.Log("Num = " + num + ", sizes size = " + sizes.Count);
        size = sizes[num];
    }
    // Start is called before the first frame update
    void Start()
    {
        sizes = new List<double>();
        sizes.Add(2);
        sizes.Add(4);
        sizes.Add(9);
        sizes.Add(18);
        sizes.Add(56);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
