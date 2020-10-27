using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
    public Button TurnButton;
    public GameObject Wheel; 			    // Rotatable Object with rewards
    public Text CoinsDeltaText; 		    // Pop-up text with wasted or rewarded coins amount
    public Text CurrentCoinsText; 		    // Pop-up text with wasted or rewarded coins amount
    public int TurnCost = 300;			    // How much coins user waste when turn whe wheel
    public int CurrentCoinsAmount = 1000;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    private int PreviousCoinsAmount;		    // For wasted coins animation

    public Sprite[] listIcons = new Sprite[12];
    private int[] listRewards = new int[] {300, 500, 1000, 200, 1000, 500, 100, 200, 300, 800, 500, 700};
    private FortuneWheelRewards fortuneWheel;

    private void Awake()
    {
        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();

        fortuneWheel = Wheel.GetComponent<FortuneWheelRewards>();
    }

    public void TurnWheel()
    {
        // Player has enough money to turn the wheel
        if (CurrentCoinsAmount >= TurnCost)
        {   
            // fortuneWheel.StartWheel();

            PreviousCoinsAmount = CurrentCoinsAmount;

            // Decrease money for the turn
            CurrentCoinsAmount -= TurnCost;

            // Show wasted coins
            CoinsDeltaText.text = "-" + TurnCost;
            CoinsDeltaText.gameObject.SetActive(true);

            // Animate coins
            StartCoroutine(HideCoinsDelta());
            StartCoroutine(UpdateCoinsAmount());
        }
    }

    void Update()
    {
        // Make turn button non interactable if user has not enough money for the turn
        if (fortuneWheel.IsStarted || CurrentCoinsAmount < TurnCost)
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (fortuneWheel.HaveResult) {
            StartCoroutine(HideCoinsDelta());
            RewardCoins(listRewards[fortuneWheel.GetResult()]);
        }

    }

    public void SetRewards() {
    }

    public void TurnOffFortuneWheelDialog() {
        gameObject.SetActive(false);
    }

    private void RewardCoins(int awardCoins)
    {
        CurrentCoinsAmount += awardCoins;
        CoinsDeltaText.text = "+" + awardCoins;
        CoinsDeltaText.gameObject.SetActive(true);
        StartCoroutine(UpdateCoinsAmount());
    }

    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds(1f);
        CoinsDeltaText.gameObject.SetActive(false);
    }

    private IEnumerator UpdateCoinsAmount()
    {
        // Animation for increasing and decreasing of coins amount
        const float seconds = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp(PreviousCoinsAmount, CurrentCoinsAmount, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();
    }
}
