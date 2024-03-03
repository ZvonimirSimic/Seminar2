using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using Microsoft.Win32;
using UnityEngine.UI;

public class ImageManipulator : MonoBehaviour
{
    public long zauzece = 0;
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

    public static double CompareColors(Color a, Color b)
    {
        return 100 * (
            1.0 - ((double)(
                Math.Abs(a.r - b.r) +
                Math.Abs(a.g - b.g) +
                Math.Abs(a.b - b.b)
            ) / (256.0 * 3))
        );
    }

    public void convert(int bitovi)
    {
        Debug.Log("Pritisnut gumb za " + bitovi + " bita.");
        Texture2D itemBGTex = LoadImg("Assets/BNG Framework/Prefabs/Image.jpg");
        Texture2D newTexture = new Texture2D(itemBGTex.width, itemBGTex.height);
        Color[] pixels = itemBGTex.GetPixels();
        List<Color> boje = pixels.ToList();
        List<Color> odabraneBoje = new List<Color>();
        HashSet<Color> razliciteBoje = new HashSet<Color>(pixels);
        if (razliciteBoje.Count > Math.Pow(2, bitovi))
        {
            /*for (int i = 0; i < Math.Pow(2, bitovi); i++)
            {
                Color most = boje.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                boje.RemoveAll(item => item == most);
                odabraneBoje.Add(most);
                Debug.Log("Najcesca boja: " + most);
            }*/
            int k = 0;
            Color kost = boje.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            boje.RemoveAll(item => item == kost);
            odabraneBoje.Add(kost);
            while(k < Math.Pow(2,bitovi)-1)
            {
                Color most = boje.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                boje.RemoveAll(item => item == most);
                if(CompareColors(odabraneBoje[k], most) < 99.98){
                    odabraneBoje.Add(most);
                    k++;
                }
                Debug.Log(k);
            }
            Debug.Log("Razlika: " + CompareColors(odabraneBoje[0], odabraneBoje[1]));
            /*odabraneBoje[0] = Color.black;
            odabraneBoje[1] = Color.white;*/
            for (int i = 0; i < pixels.Length; i++)
            {
                double most = CompareColors(pixels[i], odabraneBoje[0]);
                int index = 0;
                for (int j = 1; j < odabraneBoje.Count; j++)
                {
                    if (most < CompareColors(pixels[i], odabraneBoje[j]))
                    {
                        most = CompareColors(pixels[i], odabraneBoje[j]);
                        index = j;
                    }
                }
                pixels[i] = odabraneBoje[index];
                //Debug.Log("Pixel[" + i + "]: " + pixels[i]);
            }

            newTexture.SetPixels(pixels);
            newTexture.Apply();
            byte[] newTex = newTexture.EncodeToJPG();
            File.WriteAllBytes("Assets/BNG Framework/Prefabs/Image2.jpg", newTex);
            /*var image1 = new System.IO.FileInfo("Assets/BNG Framework/Prefabs/Image2.jpg");
            long vel = image1.Length;
            /*var image2 = new System.IO.FileInfo("Assets/BNG Framework/Prefabs/Image2.jpg");
            Debug.Log("Original image size: " + image1.Length + ". New image size: " + image2.Length);
            Texture2D imageForMaterial = LoadImg("Assets/BNG Framework/Prefabs/Image2.jpg");
            Material mat = new Material(Shader.Find("Standard"));
            Material[] materials = GetComponent<MeshRenderer>().materials;
            mat.mainTexture = imageForMaterial;
            materials[0] = mat;
            GetComponent<MeshRenderer>().materials = materials;*/

        }
        else
        {
            /*byte[] newTex = itemBGTex.EncodeToJPG();
            File.WriteAllBytes("Assets/BNG Framework/Prefabs/Image24.jpg", newTex);
            var image1 = new System.IO.FileInfo("Assets/BNG Framework/Prefabs/Image24.jpg");
            long vel = image1.Length;
            Texture2D imageForMaterial = LoadImg("Assets/BNG Framework/Prefabs/Image24.jpg");
            Material mat = new Material(Shader.Find("Standard"));
            Material[] materials = GetComponent<MeshRenderer>().materials;
            mat.mainTexture = imageForMaterial;
            materials[0] = mat;
            GetComponent<MeshRenderer>().materials = materials;*/

        }

    }

    public void showImage(string i)
    {
        string s = "Assets/Image" + i + ".jpg";
        var image1 = new System.IO.FileInfo(s);
        //long vel = image1.Length;
        //zauzece = vel / 1024;
        Texture2D imageForMaterial = LoadImg(s);
        zauzece = imageForMaterial.GetPixels().Length * int.Parse(i) / 1024; 
        Material mat = new Material(Shader.Find("Standard"));
        Material[] materials = GetComponent<MeshRenderer>().materials;
        mat.mainTexture = imageForMaterial;
        materials[0] = mat;
        GetComponent<MeshRenderer>().materials = materials;
    }
    // Start is called before the first frame update
    void Start()
    {
        //convert(2);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
