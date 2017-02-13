using UnityEngine;
using System.Collections;

//负责英雄选择画面中的选择，以及右边hero0图像的显示，英雄名称的显示
public class heroselect : MonoBehaviour {

    // Use this for initialization
    private UISprite selectHeroIamge;
    private UILabel selectHeroName;
    private string[] heroNames =
{"泰兰德·语风（牧师）","阿努巴拉克（母鸡）","玛维·影歌（潜行者）","陈·风暴烈酒（武僧）"
,"伊利丹·怒风（恶魔猎手）","萨穆罗（战士）"
,"阿尔萨斯·米奈希尔（死亡骑士）","凯尔萨斯·逐日者（法师）","古尔丹·吴彦祖（术士）"
};

    void Awake()
    {
        selectHeroIamge = this.transform.parent.Find("hero0").GetComponent<UISprite>();
        selectHeroName = this.transform.parent.Find("hero_name").GetComponent<UILabel>();
    }
    void OnClick() {
        string heroname = this.gameObject.name;
        selectHeroIamge.spriteName = heroname;
        char heroIndexChar = heroname[heroname.Length - 1];
        int heroIndex = heroIndexChar - '0';
        selectHeroName.text=heroNames[heroIndex - 1];
            }//点击后，更改名称和图片
	

    

}
