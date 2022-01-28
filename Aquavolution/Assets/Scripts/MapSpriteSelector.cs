using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{
    // References to all our map sprites
    public Sprite U, D, R, L, UD, RL, UR, UL, DR, DL, ULD, RUL, DRU, LDR, UDRL;
    public bool Up, Down, Left, Right;
    public int Type;
    public Color NormalColor, EnterColor;
    Color MainColor;
    SpriteRenderer SR;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        MainColor = NormalColor;
        PickSprite();
        PickColor();
    }

    void PickSprite()
    {
        if (Up){
			if (Down){
				if (Right){
					if (Left){
						SR.sprite = UDRL;
					}else{
						SR.sprite = DRU;
					}
				}else if (Left){
					SR.sprite = ULD;
				}else{
					SR.sprite = UD;
				}
			}else{
				if (Right){
					if (Left){
						SR.sprite = RUL;
					}else{
						SR.sprite = UR;
					}
				}else if (Left){
					SR.sprite = UL;
				}else{
					SR.sprite = U;
				}
			}
			return;
		}
		if (Down){
			if (Right){
				if(Left){
					SR.sprite = LDR;
				}else{
					SR.sprite = DR;
				}
			}else if (Left){
				SR.sprite = DL;
			}else{
				SR.sprite = D;
			}
			return;
		}
		if (Right){
			if (Left){
				SR.sprite = RL;
			}else{
				SR.sprite = R;
			}
		}else{
			SR.sprite = L;
		}
    }

    void PickColor()
    {
        if (Type == 0)
            MainColor = NormalColor;
        else
            MainColor = EnterColor;

        SR.color = MainColor;
    }
}
