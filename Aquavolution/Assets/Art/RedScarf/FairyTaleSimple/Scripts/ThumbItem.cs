using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace RedScarf.FairyTableSimple
{
    public class ThumbItem : MonoBehaviour,
        IPointerClickHandler
    {
        [SerializeField] RawImage m_Thumb;
        [SerializeField] Text m_Title;
        string m_Path;

        public Action<ThumbItem,string> OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
            {
                OnClick.Invoke(this,m_Path);
            }
        }

        public void Set(string path)
        {
            m_Path = path;
            var pathArray = path.Split('/');
            m_Title.text = pathArray[pathArray.Length-1];

#if UNITY_EDITOR

            var tex=UnityEditor.AssetDatabase.LoadAssetAtPath<Texture>(path+"/S1.psd");
            m_Thumb.texture = tex;
#endif
        }
    }
}
