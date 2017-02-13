using UnityEngine;
using System.Collections;

//负责开场动画，进入游戏，英雄选择界面显示
public class StartMenu : MonoBehaviour
{

    public MovieTexture movTexture;

    public bool isDrawMov = true;

    public bool isShowMessage = false;

    public TweenScale logoTweenScale;//logo动画
    public TweenPosition selectRoleTween;//角色选择画面出现动画

    private bool isCanShowSelectRole = false;

    private UISprite hero1;//选择英雄时的图片显示

    // Use this for initialization
    void Start()
    {
        movTexture.loop = false;
        movTexture.Play();
        logoTweenScale.AddOnFinished(this.onLogoTweenFinish);
        //logo动画要在点击鼠标后再播放
        //使用结束时才调用的方法


    }
    void Awake()
    {
        hero1 = GameObject.Find("hero0").GetComponent<UISprite>();
    }
    // Update is called once per frame

    void Update()
    {
        //播放动画时点击鼠标左键，出现提示文字，问是否要跳过动画
        //再按一次鼠标左键，即可跳过动画
        if (isDrawMov)
        {
            if (Input.GetMouseButtonDown(0) && isShowMessage == false)
            {
                isShowMessage = true;
            }
            else if (Input.GetMouseButtonDown(0) && isShowMessage == true)
            {
                StopMov();
            }
        }
        //判断播放
       if (isDrawMov != movTexture.isPlaying) {
        StopMov();
         }

        //logo播放完再点鼠标左键时，出现选择界面
        if (isCanShowSelectRole && Input.GetMouseButtonDown(0))
        {
            selectRoleTween.PlayForward();
            isCanShowSelectRole = false;
        }


    }

    //播放动画，全屏播放
    void OnGUI()
    {
       if (isDrawMov)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture);
            if (isShowMessage)
           {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 40), "再点击一次屏幕退出动画的播放");
            }//在屏幕中显示提示文字
       }
    }

    private void StopMov()
    {
       movTexture.Stop();
        isDrawMov = false;

        logoTweenScale.PlayForward();//开始播放logo动画
    }
    private void onLogoTweenFinish()
    {
        isCanShowSelectRole = true;

    }//logo播放结束，角色选择界面可使用

    public void OnPlayButtonClick()//监听开始游戏键，迷雾出现，并
    {
        BlackMask._instance.Show();
        VSShow._instance.Show(hero1.spriteName, "hero" + Random.Range(1, 10));
        //用已选英雄与随机一位英雄进行对战
        StartCoroutine(LoadPlayScene());//两秒后协程载入游戏打牌画面
    }
    IEnumerator LoadPlayScene()//协程载入
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(1);

    }
}
