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
            if (!(dTime > 1)) return;
            print("==============:" + baseAnimal + "   dTime:" + dTime + "  other.tag:" + other.tag);
            baseAnimal.hp.OnHpChange(-10);
            if (baseAnimal.hp.CurrentHp < 1)
            {
                Destroy(other.gameObject);
            }
            _lastValidAttackTime = Time.time;
        }
    }
}