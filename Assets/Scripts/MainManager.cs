using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;//�˹ؼ��ֱ�ʾ�洢�ڸ����Ա�е�ֵ���ɸ��������ʵ������ 
    public Color TeamColor; // new variable declared
    private void Awake()//���ⷽ�����÷����ڴ���������������ã�
    {
       
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code


        //���ո������һ�������������� Instance �Ƿ�Ϊ null���״����� Menu ����ʱ�������� MainManager ��� Instance ����������ζ������Ϊ null����˲����������������ҽű���������֮ǰ��д�ķ�ʽ������
        //���ǣ�����Ժ��ٴμ��� Menu ���������Ѿ�����һ�� MainManager����� Instance ����Ϊ null������������£���������������� MainManager �����٣��ű��������˳���
        //��ģʽ��Ϊ singleton��ʹ������ȷ�� MainManager �ĵ���ʵ�����Դ��ڣ�������䵱���ķ��ʵ㡣
        //����������أ�������DontDestroyOnLoad���Ǳ��ֵ���������MainManager��������ǻص�һ��������һ����������һ��MainManager���������MainManager

        Instance = this;
        //��һ�д��뽫��this���洢�����Ա Instance �� MainManager �ĵ�ǰʵ���С�
        //���ڣ������Դ��κ������ű������� Unit �ű������� MainManager.Instance ����ȡָ����ض�ʵ�������ӡ�
        //����Ҫ���� Inspector �н���Ϸ���������ű�����ʱ������������
        DontDestroyOnLoad(gameObject);//�����ӵ��˽ű��� MainManager ��Ϸ������Ϊ�ڳ�������ʱ�������١�
        LoadColor();//���ر��ֵ���ɫ
    }

    [System.Serializable]//��Json��ʽ���沢�����ɫ
    class SaveData//ʲôҪ����
    {
        public Color TeamColor;
    }
    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;
        //�����˱������ݵ���ʵ������ʹ�ñ����� MainManager �е� TeamColor ����������Ŷ���ɫ���Ա

        string json = JsonUtility.ToJson(data);
        //ʹ�� JsonUtility.ToJson ����ʵ��ת��Ϊ JSON�� 

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //ʹ�����ⷽ�� File.WriteAllText ���ַ���д���ļ���
    }
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))//ʹ�÷��� File.Exists ������Ƿ����.json�ļ�
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
    //ʲô��ʽ���棬ʲô��ʽ��ȡ
}
