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
        Skin1.gameObject.SetActive (true);
        Skin2.gameObject.SetActive (false);
        Skin3.gameObject.SetActive (false);
        Skin4.gameObject.SetActive (false);
        Skin5.gameObject.SetActive (false);
        Skin6.gameObject.SetActive (false);
        Skin7.gameObject.SetActive (false);
    }

    public void SwitchSkin()
    {
        switch (WhichSkinIsOn) {
            case 1:
                WhichSkinIsOn = 2;
                ActiveSkin = Skin2;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (true);
                Skin3.gameObject.SetActive (false);
                Skin4.gameObject.SetActive (false);
                Skin5.gameObject.SetActive (false);
                Skin6.gameObject.SetActive (false);
                Skin7.gameObject.SetActive (false);
                break;
            case 2:
                WhichSkinIsOn = 3;
                ActiveSkin = Skin3;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (false);
                Skin3.gameObject.SetActive (true);
                Skin4.gameObject.SetActive (false);
                Skin5.gameObject.SetActive (false);
                Skin6.gameObject.SetActive (false);
                Skin7.gameObject.SetActive (false);
                break;
            case 3:
                WhichSkinIsOn = 4;
                ActiveSkin = Skin4;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (false);
                Skin3.gameObject.SetActive (false);
                Skin4.gameObject.SetActive (true);
                Skin5.gameObject.SetActive (false);
                Skin6.gameObject.SetActive (false);
                Skin7.gameObject.SetActive (false);
                break;
            case 4:
                WhichSkinIsOn = 5;
                ActiveSkin = Skin5;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (false);
                Skin3.gameObject.SetActive (false);
                Skin4.gameObject.SetActive (false);
                Skin5.gameObject.SetActive (true);
                Skin6.gameObject.SetActive (false);
                Skin7.gameObject.SetActive (false);
                break;
            case 5:
                WhichSkinIsOn = 6;
                ActiveSkin = Skin6;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (false);
                Skin3.gameObject.SetActive (false);
                Skin4.gameObject.SetActive (false);
                Skin5.gameObject.SetActive (false);
                Skin6.gameObject.SetActive (true);
                Skin7.gameObject.SetActive (false);
                break;
            case 6:
                WhichSkinIsOn = 7;
                ActiveSkin = Skin7;
                Skin1.gameObject.SetActive (false);
                Skin2.gameObject.SetActive (false);
                Skin3.gameObject.SetActive (false);
                Skin4.gameObject.SetActive (false);
                Skin5.gameObject.SetActive (false);
                Skin6.gameObject.SetActive (false);
                Skin7.gameObject.SetActive (true);
                break;
        }
        UI.DisplayLevelUp(false);
    }
    private void FixedUpdate(){
        if (transform.localEulerAngles.z < 180)
            ActiveSkin.GetComponent<SpriteRenderer>().flipX = true;
        else
            ActiveSkin.GetComponent<SpriteRenderer>().flipX = false;
    }

}
