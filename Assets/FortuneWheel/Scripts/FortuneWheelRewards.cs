using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FortuneWheelRewards : MonoBehaviour {


    private bool _isStarted = false;
    public bool IsStarted {get { return _isStarted;}}
    private bool _haveResult = false;
    public bool HaveResult {get {return _haveResult;}}


    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    private float[] _sectorAngle = new float[] {30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360};
    private float _startAngle = 0f;
    private float _finalAngle;
    private int numOfChild;
    private Transform[] listRewards;

    void Awake() {
        Transform rewardGroup = transform.GetChild(0);
        numOfChild = rewardGroup.childCount;
        listRewards = new Transform[numOfChild];

        for (int i = 0; i < numOfChild; i++) {
            listRewards[i] = rewardGroup.GetChild(i);
            listRewards[i].GetChild(1).GetComponent<Text>().text = "?";
        }
    }

    public void SetupRewards(int[] rewards, Sprite[] icons) {
        for (int i = 0; i < numOfChild; i++) {
            listRewards[i].GetChild(0).GetComponent<Image>().sprite = icons[i];
            listRewards[i].GetChild(1).GetComponent<Text>().text = rewards[i].ToString();
        }
    }

    public void StartWheel(int numOfRewards, int[] rewards, Sprite[] sprites, System.Action callback) {
        if (!_isStarted) {

            int randomFullCircle = UnityEngine.Random.Range(4, 7);
            float randomFinalAngle = _sectorAngle[UnityEngine.Random.Range(0, _sectorAngle.Length)];
            
            _finalAngle = -(randomFullCircle * 360 + randomFinalAngle);
            _isStarted = true;
            _haveResult = false;
            StartCoroutine(DOTweenCompletion());
        }        
    }

    IEnumerator DOTweenCompletion(){
        Tween myTween = gameObject.transform.DORotate(new Vector3(0, 0, _finalAngle), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutElastic);
        yield return myTween.WaitForKill();
        // This log will happen after the tween has completed
        _isStarted = false;
        _startAngle = _finalAngle % 360;
        _haveResult = true;
    }

    public int GetResult() {
        // vì trục x nằm ngang, nên muốn lấy reward ở mũi kim => phải -90 độ
        // %360 để index không bị quá 11
        _haveResult = false;
        return (-((int)_startAngle - 90) % 360) / 30;
    }
}
