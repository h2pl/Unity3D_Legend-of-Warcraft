using UnityEngine;
using System.Collections;

public class hero2 : hero {
    //hero2继承hero方法， 可使用父类所有方法，但pref中值不同，所以需要在此处修改
    public GameObject game_info;
    public float timer = 0;
    void Start()
    {
        game_info.SetActive(false);
        string heroName = PlayerPrefs.GetString("hero2");
        sprite.spriteName = heroName;

    }
    void Update()
    {
        

        if (hpCount <= 0)
        {
            game_info.SetActive(true);
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
