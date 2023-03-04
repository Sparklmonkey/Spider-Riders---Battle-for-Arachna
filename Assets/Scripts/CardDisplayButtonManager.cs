using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayButtonManager : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> selectCardBtnSprites, deleteCardBtnSprites;
    [SerializeField]
    private Button selectCardBtn, deleteCardBtn;


    private enum SpriteCase
    {
        Selected,
        NotSelected
    }   
}
