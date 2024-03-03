using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using BNG;
using TMPro;
using System.Reflection;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rendRight;
    public GameObject panel, kvizPanel, igrac, lekcijaPanel, kvizPanel2, quizMngr;
    Vector3[] points;
    private string currentSetting = "";
    private ColorBlock defaultBlock = new ColorBlock();
    public UnityEngine.UI.Button btn, lastBtn, lastHoverBtn;
    private GameObject lastHitObject = null, selectedObject = null;
    public bool AlignRightLineRenderer(LineRenderer rendRight)
    {
        /*Ray ray;
        ray = new Ray(rendRight.transform.position, rendRight.transform.forward);*/
        RaycastHit hit;
        bool btnHit = false;

        if (Physics.Raycast(rendRight.transform.position, rendRight.transform.forward, out hit, 20))
        {
            if (hit.collider.gameObject.GetComponentInParent<UnityEngine.UI.Button>() != null)
            {
                Debug.Log("Button hit.");
                btn = hit.collider.gameObject.GetComponentInParent<UnityEngine.UI.Button>();
                Debug.Log("Ovo je: " + btn.name);
                btn.GetComponent<Image>().color = Color.cyan;
                if (lastHoverBtn != null && lastHoverBtn != btn)
                {
                    lastHoverBtn.GetComponent<Image>().color = Color.white;
                }
                lastHoverBtn = btn;
                Debug.Log("lasthoverbtn = " + lastHoverBtn.name);
                btnHit = true;
            }
            else
            {
                if (lastHoverBtn != null)
                {
                    Debug.Log("Nisam null!");
                    lastHoverBtn.GetComponent<Image>().color = Color.white;
                    lastHoverBtn = null;
                }
                btnHit = false;
            }
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<Outline>() != null)
            {
                if (BNG.ControllerBinding.RightTrigger.GetDown())
                {
                    //selectObject(hit.collider.gameObject);
                }
                hoverOut(true, hit.collider.gameObject);
            }
            else
            {
                hoverOut(false, null);
            }
            rendRight.useWorldSpace = true;
            rendRight.SetPosition(0, rendRight.transform.position);
            rendRight.SetPosition(1, hit.point);
            rendRight.startColor = Color.green;
            rendRight.endColor = Color.green;
            
            btnHit = true;
        }
        else
        {
            hoverOut(false, null);
            rendRight.useWorldSpace = false;
            rendRight.SetPosition(0, rendRight.transform.localPosition);
            rendRight.SetPosition(1, new Vector3(0, 0, 20));
            rendRight.startColor = Color.red;
            rendRight.endColor = Color.red;
            
        }
        rendRight.material.color = rendRight.startColor;
        return btnHit;
    }

    void hoverOut(bool state, GameObject newGameObject)
    {
        if (newGameObject != null)
        {
            if (lastHitObject == null)
            {
                lastHitObject = newGameObject;
                lastHitObject.GetComponent<Outline>().enabled = state;
            }
            else if (lastHitObject != newGameObject)
            {
                lastHitObject.GetComponent<Outline>().enabled = false;
                /*if (newGameObject != selectedObject)
                {*/
                lastHitObject = newGameObject;
                lastHitObject.GetComponent<Outline>().enabled = state;
                // }
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                lastHitObject.GetComponent<Outline>().enabled = state;
            }
            lastHitObject = null;
        }
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Outline>().enabled = true;
        }
    }

    void selectObject(GameObject obj)
    {
        Debug.Log("Entered selectObject.");
        if (selectedObject != null)
        {
            //selectedObject.GetComponent<Outline>().OutlineColor = Color.green;
            //selectedObject.GetComponent<Outline>().enabled = false;
        }
        /*if (selectedObject != obj)
        {*/
        selectedObject = obj;
        //selectedObject.GetComponent<Outline>().OutlineColor = Color.red;
        //selectedObject.GetComponent<Outline>().enabled = true;
        /*}
        else if(selectedObject == obj && obj != null && selectedObject != null)
        {
            selectedObject.GetComponent<Outline>().OutlineColor = Color.green;
            selectedObject.GetComponent<Outline>().enabled = false;
            selectedObject = null;
        }*/
    }

    public void menuSettings()
    {
        if (btn != null)
        {
            Debug.Log("Btn name: " + btn.name);
            if (btn.name == "Simulacija")  // gasi menu
            {
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = false;

                Debug.Log("Simulacija.");
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().transform.position = new Vector3((float)19.731, 0, (float)-3.897014);
                igrac.gameObject.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, -90, 0, 0);
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = true;

            }
            else if (btn.name == "Lekcija")  // gasi menu
            {
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = false;
                Debug.Log("Lekcija.");
                lekcijaPanel.SetActive(true);
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().transform.position = new Vector3((float)14.708, 0, 4);
                igrac.gameObject.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = true;

            }
            else if (btn.name == "Kviz")  // gasi menu
            {
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = false;
                Debug.Log("Kviz.");
                if (!kvizPanel.GetComponent<KvizController>().uTijeku)
                {
                    //kvizPanel2.GetComponent<MenuManager>().quizBtn();
                    quizMngr.GetComponent<QuizController>().enabled = true;
                    //kvizPanel.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().transform.position = new Vector3((float)8.5, (float)5.5, (float)8.98);
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = true;

            }
            else if (btn.name == "Kontrole")  // gasi menu
            {
                Debug.Log("Kontrole.");
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = false;
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().transform.position = new Vector3((float)6.14, 0, (float)8.98);
                igrac.gameObject.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 90, 0, 0);
                igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = true;
            }
            else if (btn.name == "Izlaz")  // gasi menu
            {
                Debug.Log("Application closed.");
                Application.Quit();
            }
        }
        btn = null;
    }

    public void kviz()
    {
        Debug.Log("Pokreni kviz!");
        quizMngr.GetComponent<QuizController>().RestartQuiz();
    }

    public void pokreniKviz()
    {
        if (btn != null)
        {

        }
        btn = null;
    }

    private void setDefaultBlock()
    {
        defaultBlock.normalColor = Color.white;
        defaultBlock.highlightedColor = Color.white;
        defaultBlock.fadeDuration = 0.1f;
        defaultBlock.colorMultiplier = 1;
        defaultBlock.pressedColor = Color.white;
        defaultBlock.disabledColor = Color.white;
        defaultBlock.selectedColor = Color.white;
    }

    private void chooseSetting()
    {
        if (currentSetting != "" && btn.name == lastBtn.name)
        {
            currentSetting = "";
            lastBtn.colors = defaultBlock;
        }
        else
        {
            if (currentSetting != "")
            {
                lastBtn.colors = defaultBlock;
            }
            currentSetting = btn.name;                  // Nullreference exception
            ColorBlock cb = defaultBlock;
            cb.normalColor = Color.cyan;
            btn.colors = cb;
            lastBtn = btn;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        setDefaultBlock();
        //rendRight = rendRight.gameObject.GetComponent<LineRenderer>();
        points = new Vector3[2];
        points[0] = Vector3.zero;
        points[1] = rendRight.transform.position + new Vector3(0, 0, 20);
        rendRight.SetPositions(points);
        rendRight.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AlignRightLineRenderer(rendRight); // stalno se provjerava je li laser pogodio nešto
        if (BNG.ControllerBinding.RightTriggerDown.GetDown())
        {
            Debug.Log("Stiso desni.");
        }
        if (AlignRightLineRenderer(rendRight) && BNG.ControllerBinding.RightTriggerDown.GetDown()) // ako je laser pogodio gumb i korisnik pritisnuo okidaè, gumb radi svoju funkciju
        {
            Debug.Log("Pressed");
            if (btn != null)
            {
                Debug.Log("Button not null.");
                btn.onClick.Invoke();
            }
        }
        if (BNG.ControllerBinding.StartButtonDown.GetDown() || BNG.ControllerBinding.AButtonDown.GetDown() || BNG.ControllerBinding.BButtonDown.GetDown())
        {
            igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = false;
            igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().transform.position = new Vector3((float)6.14, 0, (float)8.98);
            igrac.gameObject.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            igrac.gameObject.transform.GetChild(0).GetComponentInChildren<CharacterController>().enabled = true;

        }
        btn = null;
    }
}
