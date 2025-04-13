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
    public float ProductionSpeed// ����set,������get
    {
        //��δ����� C# �����ж����һ�����ԣ�Property����������һ����������Ա��
        //���ṩ��һ�����ķ�ʽ�����ʺ��޸����˽���ֶΣ�ͬʱ�����ڷ��ʺ��޸�ʱ��Ӷ�����߼�����ǿ�����ݵķ�װ�ԺͰ�ȫ�ԡ�
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
    //�����˻�ȡ ProductionSpeed ����ʱ����������֧���ֶΡ����������� ProductionSpeed ����ʱ��֧���ֶν�����Ϊ���������ֵ��
    // �������������ٶ�
    //ProductionSpeed = 1.0f;
    //    Debug.Log("ProductionSpeed: " + ProductionSpeed);

    //     �������ø��������ٶȣ�����
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
