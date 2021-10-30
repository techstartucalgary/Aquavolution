using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class FFGrid : MonoBehaviour
{
    public void Awake()
    {
        m_myTrnsform = gameObject.transform;
    }
    public void Update()
    {
#if UNITY_EDITOR
        if (m_refresh == false)
            return;

        m_refresh = false;

        int childCount = m_myTrnsform.childCount;
        int numHeight = (int)((float)childCount / (float)m_numRow);
        float totalWidth = 0;
        float totalHeight = 0;
        if(m_numRow != 0 && childCount > m_numRow)
        {
            totalWidth = m_numRow * m_width;
            totalHeight = numHeight * m_height;
        }
        else
        {
            totalWidth = childCount * m_width;
            totalHeight = 0;
        }

        for (int i = 0; i < m_myTrnsform.childCount; ++i)
        {
            Transform child = m_myTrnsform.GetChild(i);
            int widthIndex = i;
            int heightIndex = 0;
            if (m_numRow > 0)
            {
                widthIndex = i % m_numRow;
                heightIndex = (int)((float)i / (float)m_numRow);
            }
            
            child.localPosition = new Vector3(-(totalWidth / 2) + m_width * widthIndex + (float)m_width / 2, (totalHeight / 2) - m_height * heightIndex, 0);
        }
#endif
    }

    public void AddChild(Transform child)
    {
        child.parent = m_myTrnsform;
    }

    public bool m_refresh = false;
    public float m_width;
    public float m_height;
    public int m_numRow;

    Transform m_myTrnsform;
}
