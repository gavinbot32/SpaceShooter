using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ButtonController : MonoBehaviour
{

    public bool leaderboard;
    public TextMeshProUGUI[] scores;
    public TextMeshProUGUI highscoreTxt;
    public int highscore;
    // Start is called before the first frame update
    void Start()
    {
        if (leaderboard)
        {
           
            for(int i = 0; i < scores.Length; i++)
            {
                if (PlayerPrefs.HasKey("score" + i)){
                    int x = PlayerPrefs.GetInt("score" + i);
                    print(x);
                    scores[i].text = x.ToString();
                }
                else
                {
                    scores[i].text = "00000000";
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_Scene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void clear_all()
    {
        for(int i = 0; i < scores.Length; i++)
        {
            print(i);
            if (PlayerPrefs.HasKey("score" + i.ToString()))
            {
                PlayerPrefs.DeleteKey("score" + i.ToString());
            }
          
            scores[i].text = "00000000";
        }
    }
    public void quitOn()
    {
        Application.Quit();
    }
}
