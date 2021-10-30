using UnityEngine;
using Spine.Unity;

public class ViewUnit : MonoBehaviour
{
    public void Awake()
    {
        m_spine = gameObject.GetComponent<SkeletonAnimation>();
    }
    public void Update()
    {
        if (m_animation == false)
            return;

        m_currTime += Time.deltaTime;
        if (m_currTime < m_entry.AnimationEnd)
            return;

        m_animation = false;
        SetAnimation("idle", true);
    }

    public void SetAnimation(string aniName, bool loop)
    {
        if (m_spine.skeleton.Data.FindAnimation(aniName) == null)
        {
            Debug.Log(name);
            return;            
        }

        m_currTime = 0;
        m_animation = true;        
        m_entry = m_spine.state.SetAnimation(0, aniName, loop);
    }

    public void ChangeSkin(string skinName)
    {
        m_spine.skeleton.SetSkin(skinName);
        m_spine.skeleton.SetToSetupPose();
    }

    float m_currTime;
    bool m_animation;
    Spine.TrackEntry m_entry;
    SkeletonAnimation m_spine;
}
