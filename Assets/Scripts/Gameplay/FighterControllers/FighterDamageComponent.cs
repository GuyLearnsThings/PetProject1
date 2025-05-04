using UnityEngine;

public class FighterDamageComponent : MonoBehaviour
{
    [SerializeField] private float _damageValue;

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Нанесение урона с перезарядкой
    }
}
