using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Language : MonoBehaviour
{
    private string json;
    public int NumLang;
    public Sprite[] SpriteLanguage; 
    public static lang lan = new lang();
    void Awake()
    {
       if(!PlayerPrefs.HasKey("Lang"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Lang", "Ru");
                NumLang = 0;
            }
            else
            {
                PlayerPrefs.SetString("Lang", "En");
                NumLang = 1;
            }

        }
       else
        {
            if(PlayerPrefs.GetString("Lang") == "Ru")
            {
                NumLang = 0;
            }
            else
            {
                NumLang = 1;
            }
        }
        GetComponent<Image>().sprite = SpriteLanguage[NumLang];
        LangLoad();
    }
    void LangLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            string path = Path.Combine(Application.streamingAssetsPath + "/" + PlayerPrefs.GetString("Lang") + ".json");
            WWW reader = new WWW(path);
            while(!reader.isDone) { }
            json = reader.text;
            lan = JsonUtility.FromJson<lang>(json);
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/" + PlayerPrefs.GetString("Lang") + ".json");
            lan = JsonUtility.FromJson<lang>(json);
    #endif
        //json = File.ReadAllText(Application.streamingAssetsPath +"/"+ PlayerPrefs.GetString("Lang") + ".json");
        //lan = JsonUtility.FromJson<lang>(json);
    }
    public class lang
    {
        public string startText;
        public string TextScore;
        public string TextBestScore;
    }
    public void SettingLang()
    {
        if(NumLang == 0)
        {
            NumLang = 1;
            PlayerPrefs.SetString("Lang", "En");
            
        }
        else
        {
            NumLang = 0;
            PlayerPrefs.SetString("Lang", "Ru");
            
        }
        GetComponent<Image>().sprite = SpriteLanguage[NumLang];
        LangLoad();
    }
}
