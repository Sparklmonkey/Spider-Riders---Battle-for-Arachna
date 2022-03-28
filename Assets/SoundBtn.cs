using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundBtn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite hoverSoundIcon, soundIcon, hoverMuteIcon, muteIcon;
    [SerializeField]
    private Image soundButton;
    private bool isMuted = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMuted)
        {
            soundButton.sprite = hoverSoundIcon;
        }
        else
        {
            soundButton.sprite = hoverMuteIcon;
        }
        isMuted = !isMuted;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isMuted)
        {
            soundButton.sprite = hoverMuteIcon;
        }
        else
        {
            soundButton.sprite = hoverSoundIcon;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isMuted)
        {
            soundButton.sprite = muteIcon;
        }
        else
        {
            soundButton.sprite = soundIcon;
        }
    }
}
