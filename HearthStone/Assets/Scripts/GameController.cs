using UnityEngine;
using System.Collections;
//核心脚本文件，负责整个游戏进程的控制，协调其他脚本，完成协程运行
//负责计时，如烧绳子，控制发牌时间与动画播放时间的一致
//负责控制游戏状态，发牌，打牌，与结束
//调用发牌器，声明发牌器Randomgenerator，生成一个发牌器，播放动画，在播放结束后加入手牌。
//将卡牌放入mycard中，在此处进行播放是因为要控制协程播放，实际方法在mycard脚本中
public enum GameState
{
    CardGenerating,
    PlayCard,
    End
}//定义泛型，存储游戏状态

public class GameController : MonoBehaviour {
    //

    public static GameController _instance;//单例模式，方便作为程序接口
    public GameState gameState = GameState.CardGenerating;//设置为发牌状态
    public float cycleTime = 60f;//回合时间
    public myCard MyCard;//引用公共类mycard，并使用其脚本用来存储新产生的卡牌，在此处定义，便于在卡牌生成后进行存储，调用mycard里的add方法
    public EnemyCard EnemyCard;//引用公共类enemycard，并使用其脚本用来存储新产生的卡牌，在此处定义，便于在卡牌生成后进行存储，调用mycard里的add方法

    public int roundIndex = 0;//表示当前的回合数。
    public delegate void OnNewRoundEvent(string heroName);//当回合（控制方 发牌方）转变的时候,事件与委托
    public event OnNewRoundEvent OnNewRound;//定义事件



    private UISprite wickpopeSprite;//绳子的图像
    private float timer = 0;//计时器
    private float wickpopeLength;//绳子长度
    private string currentHeroName="hero1";
    private CardGenerator cardGenerator;//给发牌器的脚本一个引用，可以在gamecontroller里使用它

    //mycard和cardgenerator都使用了引用，以便在此处调用这两个类中的方法。
    void Awake()
    {
        _instance = this;
        wickpopeSprite = this.transform.Find("wickpope").GetComponent<UISprite>();
        wickpopeLength = wickpopeSprite.width;//存储绳子长度
        wickpopeSprite.width = 0;//起初不显示绳子
        this.cardGenerator = this.GetComponent<CardGenerator>();
        //由于mycard是公共类可以直接使用，而此处定义的cardgenerator是该函数定义的
        //其父类即为gamecontroller，使该子类赋值为该游戏物体中的CardGenerator脚本。要比公共类的定义更复杂
        //给当前回合的英雄发牌

        StartCoroutine(GenerateCardForHero1());//开始协程
        
    }

    void Update()
    {
        
        if(gameState==GameState.PlayCard)//当前状态为打牌时，才开始计时
        {
            timer += Time.deltaTime;
            if(timer>cycleTime)
            {
                TransformPlayer();//强制结束回合
            }
            else if((cycleTime-timer)<=15)//开始烧绳子
            {
                 wickpopeSprite.width=(int)(((cycleTime-timer)/15f)*wickpopeLength);//绳子长度变化
            }
        }
        if(gameState == GameState.CardGenerating)
        {

        }
    }
    public void TransformPlayer()//转变发牌方
    {
        if (currentHeroName == "hero1")
        {
            currentHeroName = "hero2";
            roundIndex++;
            OnNewRound(currentHeroName);

            if (roundIndex >= 2)
            {
                StartCoroutine(DealCard());
            }
            GameObject.Find("EnemyArea").GetComponent<enemyai>().AddCard();
            GameObject.Find("EnemyArea").GetComponent<enemyai>().UpdateShow();
            TransformPlayer();
        }
        else
        {
            currentHeroName = "hero1";
            roundIndex++;
            OnNewRound(currentHeroName);

            if (roundIndex >= 2)
            {
                StartCoroutine(DealCard());
            }

            
        }
       
           
        










        //每回合给两方发两张牌。

    }
    IEnumerator DealCard()
    {
        gameState = GameState.CardGenerating;
        if (currentHeroName =="hero1")
        {
            GameObject cardGo = cardGenerator.RandomGenerateCard();
            yield return new WaitForSeconds(2f);
            MyCard.AddCard(cardGo);
        }
        else
        {
            GameObject cardGo = cardGenerator.RandomGenerateCard();
            yield return new WaitForSeconds(2f);
            EnemyCard.AddCard(cardGo);
        }
        gameState = GameState.PlayCard;
        GameObject.Find("myCard").GetComponent<myCard>().UpdateShow();
        timer = 0;
    }
    private IEnumerator GenerateCardForHero1()
    {
        yield return new WaitForSeconds(2f);//等待对战动画播放
        GameObject cardGo=cardGenerator.RandomGenerateCard();//使用cardgenerator脚本中的随机生成卡牌的方法自动返回一张卡牌
        yield return new WaitForSeconds(2f);
        MyCard.AddCard(cardGo);//调用mycard的公共方法addcard进行卡牌添加，添加到mycard游戏组件中
        //把这个卡片放入mycard卡牌管理中
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        MyCard.AddCard(cardGo);
        //再来一张
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        MyCard.AddCard(cardGo);
        //
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        MyCard.AddCard(cardGo);


        //敌人发牌阶段
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        EnemyCard.AddCard(cardGo);//调用mycard的公共方法addcard进行卡牌添加，添加到mycard游戏组件中
        //把这个卡片放入mycard卡牌管理中
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        EnemyCard.AddCard(cardGo);
        //再来一张
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        EnemyCard.AddCard(cardGo);
        //
        cardGo = cardGenerator.RandomGenerateCard();
        yield return new WaitForSeconds(2f);
        EnemyCard.AddCard(cardGo);


        gameState = GameState.PlayCard;//游戏开始
        timer = 0;//计时器开始计时，准备绳子

        yield return new WaitForSeconds(2f);
        
       
    }
}


