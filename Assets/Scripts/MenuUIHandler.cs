using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//����������Ҫ�������ռ�
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();//��ʼ��
       
        // ����ɫѡ�����ϵ���ɫ��ť�����ʱ���⽫���� NewColorSelected ��������color����Mainmanager
        ColorPicker.onColorChanged += NewColorSelected;

        //�¼���ί�л������� C# �ί����һ���������ͣ��������÷������¼����ǻ���ί�е�һ�ֻ��ƣ�����������ض��������ʱ֪ͨ��������ColorPicker.onColorChanged ���� ColorPicker ���ж����һ���¼������������� System.Action<Color>������һ�������ί�У�������һ������ Color ���Ͳ������޷���ֵ�ķ�����
        //�����¼���ColorPicker.onColorChanged += NewColorSelected; ���д��뽫 NewColorSelected ������ӵ� ColorPicker.onColorChanged �¼��ĵ����б��У���һ������Ϊ�¼����ġ���ʱ��NewColorSelected �����ͺ� ColorPicker �����ɫ�ı��¼����������ˡ�
        //�¼��������� ColorPicker ����ڲ�ʵ���У�����ɫѡ�����ϵ���ɫ��ť���������ɫ�����ı�ʱ��ColorPicker ��ᴥ�� onColorChanged �¼���

        ColorPicker.SelectColor(MainManager.Instance.TeamColor);

        //������ɫ������SelectColor �������� MainManager.Instance.TeamColor ��Ϊ�������ò�����������Ҫѡ�е���ɫ��
        //��ɫƥ�䣺�� SelectColor �����ڲ�������� AvailableColors ���飬�����봫����ɫ color ��ȵ�Ԫ�ء�
        //��������¼���һ���ҵ�ƥ�����ɫ���ͻ���ö�Ӧ��ɫ��ť�� onClick �¼���ģ���û��ֶ�����ð�ť�Ĳ��������ִ�а�ť����¼��Ļص����������� SelectedColor ���ԣ������� onColorChanged �¼���
        //Ҳ����˵�������Ҳ����������Ҳ����ñ���������ģ��һ�ε�����������������Ǿ͸�������
    }
    public void StartNew()//������������
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()//����load��������
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
