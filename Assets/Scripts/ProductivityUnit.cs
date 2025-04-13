using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit // replace MonoBehaviour with Unit
{
    // new variables
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2;
    protected override void BuildingInRange()//这个在父类是被Update调用的，要作限制
    {
        // start of new code
        if (m_CurrentPile == null)//非常好用的“只执行一次”，只会在第一次满足条件时被赋值
        {
            ResourcePile pile = m_Target as ResourcePile;//m_Target是父类继承过来的

            if (pile != null)
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
            }
        }
        //仅当 m_Target 是 ResourcePile 类型时，表示法“as ResourcePile”才会将 pile 变量设置为 m_Target。如果 m_Target 是 Base，则这些类型将不匹配，并且 pile 将设置为 null。这是检查 m_Target 是否为资源堆的有效方法。
        //如果为 （pile ！= null），则 m_CurrentPile 被设置为该资源堆，并且其 ProductionSpeed 加倍。
        //在下一帧中，方法顶部的 if 语句将阻止此代码再次运行，因为 m_CurrentPile置为一个值（资源堆）。
    }
    void ResetProductivity()
    {
        if (m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
        }
    }
    public override void GoTo(Building target)
    {
        ResetProductivity(); //运动时要重新设置
        base.GoTo(target); // 指示脚本除了运行此 override 方法中的新代码外，还要运行原始方法。 
    }
    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}