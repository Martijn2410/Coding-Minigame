using System.Collections;
using UnityEngine;
using view;

namespace gameplay
{
    public class ProgrammingGameFlowController : MonoBehaviour
    {
        [Header("Execution")]
        [SerializeField] private CommandsManager commandsManager;
        [SerializeField, Min(0f)] private float maxRunTimeSeconds = 15f;

        [Header("Feedback")]
        [SerializeField] private GameObject winStateUi;
        [SerializeField] private GameObject failStateUi;

        private bool isRunning;
        private Coroutine timeoutCoroutine;

        private void Awake()
        {
            ResetFeedback();
        }

        public void RunProgram()
        {
            if (commandsManager == null)
                return;

            StopProgramInternal();
            ResetFeedback();

            isRunning = true;
            commandsManager.Execute();

            if (maxRunTimeSeconds > 0f)
                timeoutCoroutine = StartCoroutine(RunTimeout());
        }

        public void StopProgram()
        {
            StopProgramInternal();
            ResetFeedback();
        }

        public void HandleGoalReached()
        {
            if (!isRunning)
                return;

            StopProgramInternal();
            if (winStateUi != null)
                winStateUi.SetActive(true);
        }

        public void HandleFailReached()
        {
            if (!isRunning)
                return;

            StopProgramInternal();
            if (failStateUi != null)
                failStateUi.SetActive(true);
        }

        private IEnumerator RunTimeout()
        {
            yield return new WaitForSeconds(maxRunTimeSeconds);

            if (isRunning)
                HandleFailReached();
        }

        private void StopProgramInternal()
        {
            if (timeoutCoroutine != null)
            {
                StopCoroutine(timeoutCoroutine);
                timeoutCoroutine = null;
            }

            if (commandsManager != null)
                commandsManager.Stop();

            isRunning = false;
        }

        private void ResetFeedback()
        {
            if (winStateUi != null)
                winStateUi.SetActive(false);

            if (failStateUi != null)
                failStateUi.SetActive(false);
        }
    }
}
