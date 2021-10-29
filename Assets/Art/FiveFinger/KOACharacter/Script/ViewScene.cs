using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class ViewScene : MonoBehaviour
{
    private static ViewScene m_instance;

    public static ViewScene Instance
    {
        get { return m_instance; }
    }

    public void Awake()
    {
        m_instance = this;
    }

    public void Start()
    {
        for(int i = 0 ; i < m_list.Length ; ++i )
        {
            if (m_list[i] == null)
                continue;

            GameObject unitObject = GameObject.Instantiate(m_list[i]);
            m_unitList.Add(unitObject.AddComponent<ViewUnit>());
            m_grid.AddChild(unitObject.transform);
        }

        m_grid.m_refresh = true;
    }

    public void Update()
    {
    }    

    public void AttackAnimation()
    {
        SetAnimation("attack");
    }

    public void SkillAnimation()
    {
        SetAnimation("skill");
    }

    public void WalkAnimation()
    {
        SetAnimation("walking");
    }

    public void SetAnimation(string aniName)
    {
        for (int i = 0; i < m_unitList.Count; ++i)
        {
            m_unitList[i].SetAnimation(aniName, false);
        }
    }

    public void ChangePSkin1()
    {
        ChangeSkin("p1");
    }
    public void ChangePSkin2()
    {
        ChangeSkin("p2");
    }
    public void ChangePSkin3()
    {
        ChangeSkin("p3");
    }
    public void ChangePSkin4()
    {
        ChangeSkin("p4");
    }
    public void ChangePSkin5()
    {
        ChangeSkin("p5");
    }
    public void ChangePSkin6()
    {
        ChangeSkin("p6");
    }

    public void ChangeESkin1()
    {
        ChangeSkin("e1");
    }
    public void ChangeESkin2()
    {
        ChangeSkin("e2");
    }
    public void ChangeESkin3()
    {
        ChangeSkin("e3");
    }
    public void ChangeESkin4()
    {
        ChangeSkin("e4");
    }
    public void ChangeESkin5()
    {
        ChangeSkin("e5");
    }
    public void ChangeESkin6()
    {
        ChangeSkin("e6");
    }

    public void ChangeSkin(string skinName)
    {
        for (int i = 0; i < m_unitList.Count; ++i)
        {
            m_unitList[i].ChangeSkin(skinName);
        }

        SetAnimation("idle");
    }

    public GameObject[] m_list = new GameObject[20];
    List<ViewUnit> m_unitList = new List<ViewUnit>();
    public FFGrid m_grid;
}
