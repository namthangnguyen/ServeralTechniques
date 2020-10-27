using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public RectTransform mainPanel, carShop, powerUpShop;
    public Image playBtn;

    private void Start()
    {
        FadeEffect(playBtn, 0.5f, 1.5f, 1.5f);
        MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
    }

    public void ButtonMethod(string value)
    {
        switch (value)
        {
            case "CarShopBtn":
                MoveUI(mainPanel, new Vector2(-500, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(carShop, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                break;
            case "PowerShopBtn":
                MoveUI(mainPanel, new Vector2(-500, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(powerUpShop, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;
            case "CarShopBackBtn":
                MoveUI(carShop, new Vector2(500, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f,0f, Ease.OutFlash);
                break;
            case "PowerShopBackBtn":
                MoveUI(powerUpShop, new Vector2(0, 820), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;
        }
    }

    void FadeEffect(Image _image, float fadeTo, float fadeDuration, float delay)
    {
        _image.DOFade(fadeTo, fadeDuration);
        _image.DOFade(1, fadeDuration).SetDelay(delay).OnComplete(() => FadeEffect(_image, fadeTo, fadeDuration, delay));
    }

    void MoveUI(RectTransform _traansform, Vector2 position, float moveTime, float delayTime, Ease ease)
    {
        _traansform.DOAnchorPos(position, moveTime).SetDelay(delayTime).SetEase(ease);
    }
}
