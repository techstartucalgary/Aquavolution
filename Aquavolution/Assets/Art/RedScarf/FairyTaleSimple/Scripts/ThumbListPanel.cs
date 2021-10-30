using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace RedScarf.FairyTableSimple
{
    public class ThumbListPanel : MonoBehaviour
    {
        [SerializeField] RawImage m_Image;
        [SerializeField] ScrollRect m_ThumbScroll;
        [SerializeField] ThumbItem m_ThumbItemSource;
        [SerializeField] Button m_PrevButton;
        [SerializeField] Button m_NextButton;
        [SerializeField] Text pathText;

        List<Texture> currentTexList;
        int currentIndex;

        private void Start()
        {
            m_NextButton.onClick.AddListener(()=> {
                currentIndex++;
                ShowImage(currentIndex);
            });
            m_PrevButton.onClick.AddListener(()=> {
                currentIndex--;
                ShowImage(currentIndex);
            });

            CreateThumbs();
        }

        void CreateThumbs()
        {
#if UNITY_EDITOR
            for (var i=m_ThumbScroll.content.childCount-1;i>=0;i--)
            {
                GameObject.Destroy(m_ThumbScroll.content.GetChild(i).gameObject);
            }

            var paths=UnityEditor.AssetDatabase.GetAllAssetPaths();
            var lastPath = "";
            var nameList = "";
            foreach (var path in paths)
            {
                if (UnityEditor.AssetDatabase.IsValidFolder(path) && path.Contains("FairyTaleSimple/PSD/"))
                {
                    var item = GameObject.Instantiate<ThumbItem>(m_ThumbItemSource);
                    item.transform.SetParent(m_ThumbScroll.content);
                    item.transform.localScale = Vector3.one;
                    item.Set(path);
                    item.OnClick = OnThumbClick;

                    lastPath = path;

                    var pathArray = path.Split('/');
                    var fileName = pathArray[pathArray.Length - 1];
                    nameList += "<BR>" + fileName + "\n";
                }
            }

            Debug.Log(nameList);

            OnThumbClick(null,lastPath);
#endif
        }

        void OnThumbClick(ThumbItem thumbItem, string path)
        {
#if UNITY_EDITOR

            currentTexList = new List<Texture>();

            var fullPath = Application.dataPath.Replace("Assets", "") + path;
            var files=Directory.GetFiles(path,"*.psd");
            foreach (var file in files)
            {
                var tex = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(file);
                if (tex!=null)
                {
                    currentTexList.Add(tex);
                }
            }

            ShowImage(0);
#endif
        }

        void ShowImage(int index)
        {
            if (currentTexList==null) return;

            currentIndex = Mathf.Clamp(index,0,currentTexList.Count-1);
            var tex = currentTexList[currentIndex];
            m_Image.texture = tex;
            var contentSize = new Vector2(1536,864);
            var ratio = Mathf.Min(contentSize.x / tex.width, contentSize.y/tex.height);
            m_Image.rectTransform.sizeDelta = new Vector2(tex.width*ratio,tex.height*ratio);

            pathText.text = UnityEditor.AssetDatabase.GetAssetPath(tex);
        }
    }
}