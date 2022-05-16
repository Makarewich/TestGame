using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public bool NewSpawn, SpawnNext, Play, TheEnd;
    bool DestroyPlay;
    public bool Null;
    public GameObject[] Boxes;
    public GameObject NextBox, MainBox, ButtonPlay, LangButton;
    public Vector3 SpawnBox;
    public float TouchStartX, TouchX;
    private int Shots, MaxSpawnBox = 1, RandAd;
    public int Score, BestScore;
    public Text ScoreTxt, BestTxt, PlayText;
    public LineRenderer Aim; 
    void Start()
    {
        RandAd = Random.Range(10,20);
        if(!PlayerPrefs.HasKey("Best"))
        {
            PlayerPrefs.SetInt("Best", BestScore);
        }
        else
        {
            BestScore = PlayerPrefs.GetInt("Best");
        }
        MainBox = Instantiate(Boxes[0], new Vector3(0,0,0), Quaternion.identity);
        MainBox.GetComponent<CubeThrow>().PushObj = true;
        NewSpawn = false;
        
    }
    void Update()
    {
        ScoreTxt.text = Language.lan.TextScore + Score;
        BestTxt.text = Language.lan.TextBestScore + BestScore;
       if (Play && !Null)
       {
            if (Input.GetMouseButtonDown(0))
            {
                TouchStartX = Input.mousePosition.x - (TouchX * 150);
                Aim.gameObject.SetActive(true);
                MainBox.GetComponent<CubeThrow>().Touch = true;
            }
            if (Input.GetMouseButton(0) && Aim.gameObject.activeSelf)
            {
                    if (TouchX > -1.57f && TouchX < 1.57f)
                    {
                        TouchX = (Input.mousePosition.x - TouchStartX) / 150;

                    }
                    else if (TouchX > 1.57f) TouchX = 1.56f;
                    else if (TouchX < -1.57f) TouchX = -1.56f;
            }
            if (Input.GetMouseButtonUp(0) && Aim.gameObject.activeSelf)
            {
                    MainBox.GetComponent<CubeThrow>().Touch = false;
                    MainBox.GetComponent<CubeThrow>().TouchUp = true;
                    Aim.gameObject.SetActive(false);
                    Null = true;
                    Shots++;
            }
            if (NewSpawn)
            {
                if (TouchX > 1.57f) TouchX = 1.56f;
                if (TouchX < -1.57f) TouchX = -1.56f;
                MainBox = Instantiate(Boxes[Random.Range(0, MaxSpawnBox)], new Vector3(TouchX, 0, 0), Quaternion.identity);
                MainBox.GetComponent<CubeThrow>().PushObj = true;
                NewSpawn = false;
            }
            if (SpawnNext && NextBox != null)
            {
                GameObject BoxJump = Instantiate(NextBox, SpawnBox, Quaternion.Euler(0, Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
                BoxJump.GetComponent<CubeThrow>().Jump = true;
                BoxJump.GetComponent<CubeThrow>().PushObj = false;
                SpawnNext = false;
            }
            if (Shots >= RandAd) 
            {
                GetComponent<InitAd>().ShowAdMob();
                Shots = 0;
                RandAd = Random.Range(10, 20);
                if (MaxSpawnBox < 4) MaxSpawnBox++;
            }
        }
        else PlayText.text = Language.lan.startText;

        if (TheEnd)
        {
            PlayerPrefs.SetInt("Best", BestScore);
            SceneManager.LoadScene(0);
        }

        if (Score > BestScore) BestScore = Score;

        if (DestroyPlay)
        {
            ButtonPlay.transform.localScale = new Vector2(ButtonPlay.transform.localScale.x - 0.1f, ButtonPlay.transform.localScale.y - 0.1f);
            if (ButtonPlay.transform.localScale.x < 0.05f)
            {
                MainBox.GetComponent<CubeThrow>().Touch = false;
                Play = true;
                Destroy(ButtonPlay);
                Destroy(LangButton);
                DestroyPlay = false;
            }
        }
        
    }
    public void PlayGame()
    { 
        DestroyPlay = true;
    }
}
