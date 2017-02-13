using UnityEngine;
using System.Collections;
using System.Threading;
public class hero1 : hero {
    //继承hero方法即可
    public GameObject game_win;
    public float timer=0;
    void Start()
    {
        game_win.SetActive(false);
        string heroName = PlayerPrefs.GetString("hero1");
        sprite.spriteName = heroName;
    }
    void Update()
    {
        

        if (hpCount <= 0)
        {
            game_win.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 6f)
            {
                Application.LoadLevel(0);
                hpCount = 30;//强制结束回合
            }
            
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            Debug.Log(string.Format("Timer2 is up !!! time=${0}", Time.time));
        }
    }
}
