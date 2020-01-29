using TMPro;
using UnityEngine;

namespace CommUtil.Scripts.comm
{
    public class Hp : MonoBehaviour
    {
        public int maxHp = 100; //最大血量
        private int _currentHp;
        private float _originScaleX;
        private Transform _transformHp;
        private TextMeshProUGUI _textMeshProUgui;

        public int CurrentHp => _currentHp;

        private void Awake()
        {
            _transformHp = transform.Find("HP");
            _textMeshProUgui = transform.Find("Canvas/HpText").GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _currentHp = maxHp;
            _textMeshProUgui.text = _currentHp + "/" + maxHp;
            _originScaleX = _transformHp.localScale.x;
        }

        public void OnHpChange(int value)
        {
            _currentHp += value;
            _currentHp = Mathf.Clamp(_currentHp, 0, maxHp);
            _textMeshProUgui.text = _currentHp + "/" + maxHp;
            var hpTransformLocalScale = _transformHp.localScale;
            hpTransformLocalScale.x = _currentHp / (float) maxHp * _originScaleX;
            _transformHp.localScale = hpTransformLocalScale;
        }
    }
}