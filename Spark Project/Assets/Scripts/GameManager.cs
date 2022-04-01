using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject starBody;
    public TextMeshProUGUI starText;
    public bool win;
    public bool lose;
    public int players;
    public int stars = 0;
    public int startStars;
    List<GameObject> uIs = new List<GameObject>();
    public GameObject[] UIs;
    public bool pause;
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    GridControl grid;

    private GameObject cam;
    private float initialCheckTimer;

    private void Awake()
    {
        grid = GameObject.Find("Main Camera").GetComponent<GridControl>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

            UIs = GameObject.FindGameObjectsWithTag("UI");

    }

    private void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<GridControl>().gamestart == true)
        {
            if (initialCheckTimer > 0)
                initialCheckTimer -= 1 * Time.deltaTime;

            if (players <= 0 && initialCheckTimer <= 0)
                lose = true;

            if (startStars > 0 && initialCheckTimer <= 0)
                starBody.SetActive(true);

            starText.SetText(stars + "/" + startStars);
        }
        else
        {
            startStars = 0;
            stars = 0;
            starBody.SetActive(false);

            initialCheckTimer = 0.1f;
            players = 0;
        }
            



        if (cam.GetComponent<GridControl>().gamestart == false)
        {
            win = false;
            lose = false;
        }

        if (win && cam.GetComponent<GridControl>().gamestart == true)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
            

        if (lose && cam.GetComponent<GridControl>().gamestart == true)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }
            

        if (cam.GetComponent<GridControl>().gamestart == false)
        {
            loseScreen.SetActive(false);
            winScreen.SetActive(false);
        }    

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause)
            {
                grid.editing = false;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                grid.editing = true;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            } 
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<DataHandler>().Load();
            Debug.Log("loaded");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<DataHandler>().Save();
            Debug.Log("saved");
        }
    }

    // Update is called once per frame
    //UI script sends their gameobject name 
    //then gets added to a list 
    // upon moveUI find all instances of list.getcomponent.UI.uiOFF or uiON
    public void UIlist(GameObject ui){ uIs.Add(ui); }

    public void moveUI()
    {
        int count = 0;
        GameObject local;
        while (count <= uIs.Count - 1)
        {
            local = uIs[count];
            local.GetComponent<UI>().UIOFF();
            count++;
        }
        //during Go/Start move everything but Edit 
        //then move health and star counter into canvas
    }
    public void backUI()
    {
        int count = 0;
        GameObject local;
        while (count <= uIs.Count - 1)
        {
            local = uIs[count];
            local.GetComponent<UI>().UION();
            count++;
        }
        //during Go/Start move everything but Edit 
        //then move health and star counter into canvas
    }

    
    public void userIntON()
    {
        foreach (GameObject ui in UIs)
        {
            ui.SetActive(false);
        }
    }
    public void userIntOFF()
    {
        foreach(GameObject ui in UIs)
        {
            ui.SetActive(true);
        }
    }

    public void MainMenu()
    {
         SceneManager.LoadScene("Main menu");
         Unpause();
    }

    public void Unpause()
    {
         pause = false;
         Time.timeScale = 1;
         pauseScreen.SetActive(false);
    }
}
