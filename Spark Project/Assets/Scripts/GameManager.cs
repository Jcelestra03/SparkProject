using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text starText;
    public bool win;

    int stars = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        starText.text = stars.ToString() + " Stars:";
    }

    // Update is called once per frame

    public void AddPoint()
    {
        stars += 1;
        starText.text = stars.ToString() + " Stars:";
    }
}
