using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject FortuneWheelDialog;
    void Awake() {
        FortuneWheelDialog.SetActive(false);
    }

    public void TurnOnFortuneWheelDialog() {
        // FortuneWheelManager wheelManager = FortuneWheelDialog.GetComponent<FortuneWheelManager>();
        // wheelManager.SetRewards();
        FortuneWheelDialog.SetActive(true);
    }
}
