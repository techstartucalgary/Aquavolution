using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public GameObject Skin1, Skin2, Skin3, Skin4, Skin5, Skin6, Skin7;
    GameObject ActiveSkin;
    int WhichSkinIsOn = 1;

    public GameObject Canvas;
    private UserInterface UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = Canvas.GetComponent<UserInterface>();
        ActiveSkin = Skin1;
        UpdateSkin(1);
    }

    public void SwitchSkin()
    {
        switch (WhichSkinIsOn) {
            case 1:
                ActiveSkin = Skin2;
                UpdateSkin(2);
                break;
            case 2:
                ActiveSkin = Skin3;
                UpdateSkin(3);
                break;
            case 3:
                ActiveSkin = Skin4;
                UpdateSkin(4);
                break;
            case 4:
                ActiveSkin = Skin5;
                UpdateSkin(5);
                break;
            case 5:
                ActiveSkin = Skin6;
                UpdateSkin(6);
                break;
            case 6:
                ActiveSkin = Skin7;
                UpdateSkin(7);
                break;
        }
        UI.DisplayLevelUp(false);
    }


    private void FixedUpdate()
    {
        if (transform.localEulerAngles.z < 180)
            ActiveSkin.GetComponent<SpriteRenderer>().flipX = true;
        else
            ActiveSkin.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void UpdateSkin(int SkinNum)
    {
        UpdateSkinNumber(SkinNum);
        DeactivateSkins();
        ActivateSkin();
    }

    private void UpdateSkinNumber(int SkinNum)
    {
        WhichSkinIsOn = SkinNum;
    }

    private void DeactivateSkins()
    {
        Skin1.gameObject.SetActive (false);
        Skin2.gameObject.SetActive (false);
        Skin3.gameObject.SetActive (false);
        Skin4.gameObject.SetActive (false);
        Skin5.gameObject.SetActive (false);
        Skin6.gameObject.SetActive (false);
        Skin7.gameObject.SetActive (false);
    }

    private void ActivateSkin()
    {
        ActiveSkin.gameObject.SetActive (true);
    }
}
