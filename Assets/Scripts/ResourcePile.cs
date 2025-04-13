using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;
    private float m_CurrentProduction = 0.0f;
    private float m_ProductionSpeed = 0.5f; //  private backing field
    public float ProductionSpeed// 条件set,无条件get
    {
        //这段代码是 C# 语言中定义的一个属性（Property）。属性是一种特殊的类成员，
        //它提供了一种灵活的方式来访问和修改类的私有字段，同时可以在访问和修改时添加额外的逻辑，增强了数据的封装性和安全性。
        get { return m_ProductionSpeed; } // getter returns backing field
        set {
            if (value < 0.0f)
            {
                Debug.LogError("You can't set a negative production speed!");
            }
            else
            {
                m_ProductionSpeed = value; // original setter now in if/else statement
            }
        } // setter uses backing field
    }
    //当有人获取 ProductionSpeed 属性时，它将返回支持字段。当有人设置 ProductionSpeed 属性时，支持字段将设置为他们输入的值。
    // 正常设置生产速度
    //ProductionSpeed = 1.0f;
    //    Debug.Log("ProductionSpeed: " + ProductionSpeed);

    //     尝试设置负数生产速度：报错
    //    ProductionSpeed = -0.5f;
    //}

private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {m_ProductionSpeed}/s";
        
    }
    
    
}
