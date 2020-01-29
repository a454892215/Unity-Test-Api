using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CommUtil.Scripts.comm
{
    public class Hp : MonoBehaviour
    {
        private int _maxHp = 100; //最大血量
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
            _maxHp = Random.Range(100, 300);
            _currentHp = _maxHp;
            _textMeshProUgui.text = _currentHp + "/" + _maxHp;
        }

        public void OnHpChange(int value)
        {
            _currentHp += value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
            _textMeshProUgui.text = _currentHp + "/" + _maxHp;
            var hpTransformLocalScale = _transformHp.localScale;
            _originScaleX = hpTransformLocalScale.x;
            hpTransformLocalScale.x = _currentHp / (float) _maxHp * _originScaleX;
            _transformHp.localScale = hpTransformLocalScale;
        }
    }
}