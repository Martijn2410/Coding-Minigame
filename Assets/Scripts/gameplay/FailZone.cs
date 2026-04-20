using UnityEngine;
using UnityEngine.Events;

namespace gameplay
{
    [RequireComponent(typeof(Collider))]
    public class FailZone : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private UnityEvent onFailed;

        private void Reset()
        {
            var triggerCollider = GetComponent<Collider>();
            triggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsTarget(other.transform))
                return;

            onFailed?.Invoke();
        }

        private bool IsTarget(Transform other)
        {
            if (target != null)
                return other == target;

            return other.CompareTag("Player");
        }
    }
}
