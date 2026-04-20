using UnityEngine;
using UnityEngine.Events;

namespace gameplay
{
    [RequireComponent(typeof(Collider))]
    public class GoalZone : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool disableOnTrigger = true;
        [SerializeField] private UnityEvent onGoalReached;

        private bool wasTriggered;

        private void Reset()
        {
            var triggerCollider = GetComponent<Collider>();
            triggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (wasTriggered)
                return;

            if (!IsTarget(other.transform))
                return;

            wasTriggered = true;
            onGoalReached?.Invoke();

            if (disableOnTrigger)
                gameObject.SetActive(false);
        }

        public void ResetZone()
        {
            wasTriggered = false;
            gameObject.SetActive(true);
        }

        private bool IsTarget(Transform other)
        {
            if (target != null)
                return other == target;

            return other.CompareTag("Player");
        }
    }
}
