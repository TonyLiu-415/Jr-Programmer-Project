using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;//此关键字表示存储在该类成员中的值将由该类的所有实例共享。 
    public Color TeamColor; // new variable declared
    private void Awake()//特殊方法：该方法在创建对象后立即调用：
    {
       
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code


        //您刚刚添加了一个条件语句来检查 Instance 是否为 null。首次启动 Menu 场景时，不会有 MainManager 填充 Instance 变量。这意味着它将为 null，因此不会满足条件，并且脚本将按照您之前编写的方式继续。
        //但是，如果稍后再次加载 Menu 场景，则已经存在一个 MainManager，因此 Instance 不会为 null。在这种情况下，满足条件：额外的 MainManager 被销毁，脚本从那里退出。
        //此模式称为 singleton。使用它来确保 MainManager 的单个实例可以存在，因此它充当中心访问点。
        //如果不这样呢？由于有DontDestroyOnLoad，那保持到二场景的MainManager会跟着我们回到一场景，而一场景本身有一个MainManager，造成两个MainManager

        Instance = this;
        //第一行代码将“this”存储在类成员 Instance — MainManager 的当前实例中。
        //现在，您可以从任何其他脚本（例如 Unit 脚本）调用 MainManager.Instance 并获取指向该特定实例的链接。
        //不需要像在 Inspector 中将游戏对象分配给脚本属性时那样引用它。
        DontDestroyOnLoad(gameObject);//将附加到此脚本的 MainManager 游戏对象标记为在场景更改时不会销毁。
        LoadColor();//加载保持的颜色
    }

    [System.Serializable]//用Json格式保存并输出颜色
    class SaveData//什么要保存
    {
        public Color TeamColor;
    }
    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;
        //创建了保存数据的新实例，并使用保存在 MainManager 中的 TeamColor 变量填充其团队颜色类成员

        string json = JsonUtility.ToJson(data);
        //使用 JsonUtility.ToJson 将该实例转换为 JSON： 

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //使用特殊方法 File.WriteAllText 将字符串写入文件：
    }
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))//使用方法 File.Exists 来检查是否存在.json文件
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
    //什么形式保存，什么形式提取
}
