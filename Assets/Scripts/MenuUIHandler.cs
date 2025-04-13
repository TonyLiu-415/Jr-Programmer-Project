using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景加载需要的命名空间
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
        ColorPicker.Init();//初始化
       
        // 当颜色选择器上的颜色按钮被点击时，这将调用 NewColorSelected 函数，把color传入Mainmanager
        ColorPicker.onColorChanged += NewColorSelected;

        //事件和委托基础：在 C# 里，委托是一种特殊类型，用于引用方法。事件则是基于委托的一种机制，允许对象在特定情况发生时通知其他对象。ColorPicker.onColorChanged 就是 ColorPicker 类中定义的一个事件，它的类型是 System.Action<Color>，这是一种特殊的委托，能引用一个接受 Color 类型参数且无返回值的方法。
        //订阅事件：ColorPicker.onColorChanged += NewColorSelected; 这行代码将 NewColorSelected 方法添加到 ColorPicker.onColorChanged 事件的调用列表中，这一操作称为事件订阅。此时，NewColorSelected 方法就和 ColorPicker 类的颜色改变事件关联起来了。
        //事件触发：在 ColorPicker 类的内部实现中，当颜色选择器上的颜色按钮被点击，颜色发生改变时，ColorPicker 类会触发 onColorChanged 事件。

        ColorPicker.SelectColor(MainManager.Instance.TeamColor);

        //传入颜色参数：SelectColor 方法接收 MainManager.Instance.TeamColor 作为参数，该参数代表了需要选中的颜色。
        //颜色匹配：在 SelectColor 方法内部，会遍历 AvailableColors 数组，查找与传入颜色 color 相等的元素。
        //触发点击事件：一旦找到匹配的颜色，就会调用对应颜色按钮的 onClick 事件，模拟用户手动点击该按钮的操作。这会执行按钮点击事件的回调函数，更新 SelectedColor 属性，并触发 onColorChanged 事件。
        //也就是说，哪怕我不作点击，它也会调用保存数据来模拟一次点击操作。如果点击，那就更新数据
    }
    public void StartNew()//关联场景加载
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

    public void LoadColorClicked()//测试load功能正常
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
