using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelRewards : MonoBehaviour {

    private bool _isStarted = false;
    public bool IsStarted {get { return _isStarted;}}
    private bool _haveResult = false;
    public bool HaveResult {get {return _haveResult;}}


    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    private float[] _sectorAngle = new float[] {30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360};
    private float _startAngle = 0f;
    private float _finalAngle;
    private float _currentLerpRotationTime;

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

    public void SetupRewards(int[] rewards) {
        for (int i = 0; i < numOfChild; i++) {
            listRewards[i].GetChild(1).GetComponent<Text>().text = rewards[i].ToString();
        }
    }

    public void TurnWheel() {
        if (!_isStarted) {
            _currentLerpRotationTime = 0f;

            int randomFullCircle = UnityEngine.Random.Range(4, 7);
            float randomFinalAngle = _sectorAngle[UnityEngine.Random.Range(0, _sectorAngle.Length)];
            
            _finalAngle = -(randomFullCircle * 360 + randomFinalAngle);
            _isStarted = true;
            _haveResult = false;
        }
    }

    void Update() {
        if (_isStarted) {
            float maxLerpRotationTime = 4f;

            // increment timer once per frame
            _currentLerpRotationTime += Time.deltaTime;

            if (_currentLerpRotationTime > maxLerpRotationTime || this.transform.eulerAngles.z == _finalAngle) {
                _currentLerpRotationTime = maxLerpRotationTime;
                _isStarted = false;
                _startAngle = _finalAngle % 360;

                _haveResult = true;
            }

            // Calculate current position using linear interpolation
            float t = _currentLerpRotationTime / maxLerpRotationTime;

            // This formulae allows to speed up at start and speed down at the end of rotation.
            // Try to change this values to customize the speed
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
            this.transform.eulerAngles = new Vector3(0, 0, angle);

        } else return;
    }

    public int GetResult() {
        // vì trục x nằm ngang, nên muốn lấy reward ở mũi kim => phải -90 độ
        // %360 để index không bị quá 11
        _haveResult = false;
        return (-((int)_startAngle - 90) % 360) / 30;
    }
}
