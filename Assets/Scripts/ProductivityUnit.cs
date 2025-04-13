using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit // replace MonoBehaviour with Unit
{
    // new variables
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2;
    protected override void BuildingInRange()//����ڸ����Ǳ�Update���õģ�Ҫ������
    {
        // start of new code
        if (m_CurrentPile == null)//�ǳ����õġ�ִֻ��һ�Ρ���ֻ���ڵ�һ����������ʱ����ֵ
        {
            ResourcePile pile = m_Target as ResourcePile;//m_Target�Ǹ���̳й�����

            if (pile != null)
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
            }
        }
        //���� m_Target �� ResourcePile ����ʱ����ʾ����as ResourcePile���ŻὫ pile ��������Ϊ m_Target����� m_Target �� Base������Щ���ͽ���ƥ�䣬���� pile ������Ϊ null�����Ǽ�� m_Target �Ƿ�Ϊ��Դ�ѵ���Ч������
        //���Ϊ ��pile ��= null������ m_CurrentPile ������Ϊ����Դ�ѣ������� ProductionSpeed �ӱ���
        //����һ֡�У����������� if ��佫��ֹ�˴����ٴ����У���Ϊ m_CurrentPile��Ϊһ��ֵ����Դ�ѣ���
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
        ResetProductivity(); //�˶�ʱҪ��������
        base.GoTo(target); // ָʾ�ű��������д� override �����е��´����⣬��Ҫ����ԭʼ������ 
    }
    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}