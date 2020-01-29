using CommUtil.Scripts.Base;
using UnityEngine;

namespace CommUtil.Scripts.damager
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Box2DDamager : MonoBehaviour
    {
        private float _lastValidAttackTime;

        private void OnTriggerStay2D(Collider2D other)
        {
            var baseAnimal = other.gameObject.GetComponent<BaseAnimal>();
            if (baseAnimal == null || gameObject.tag.Equals(other.tag)) return;
            var dTime = Time.time - _lastValidAttackTime;
            if (!(dTime > 0.2f)) return;
            print("==============:" + baseAnimal + "   dTime:" + dTime + "  other.tag:" + other.tag);
            baseAnimal.Hp.OnHpChange(-(int) Random.Range(10f, 20f));
            if (baseAnimal.Hp.CurrentHp < 1)
            {
                Destroy(other.gameObject);
            }

            _lastValidAttackTime = Time.time;
        }
    }
}