using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
    public List<Sprite> frames;
    public float framesPerSecond = 10.0f;
    [SerializeField]
    private Image animRenderer;
    void Update()
    {
            int index = (int)(Time.time * framesPerSecond);
            index %= frames.Count;
            animRenderer.sprite = frames[index];
    }

}
