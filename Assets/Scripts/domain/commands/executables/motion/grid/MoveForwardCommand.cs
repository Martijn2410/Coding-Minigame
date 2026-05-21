using Cysharp.Threading.Tasks;
using domain.commands.executables.motion.move;
using UnityEngine;

namespace domain.commands.executables.motion.grid
{
    public class MoveForwardCommand : BaseMoveCommand
    {
        private readonly float distance;
        private readonly float duration;

        public MoveForwardCommand(ExecutableContext context, float distance, float duration) : base(context)
        {
            this.distance = distance;
            this.duration = duration;
        }

        public override async UniTask OnEnterAsync()
        {
            var rigidbody = Context.GameObject.GetComponent<Rigidbody>();
            if (rigidbody == null)
                throw new MissingComponentException($"MoveForwardCommand requires a Rigidbody on {Context.GameObject.name}.");

            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            var moveDirection = Context.GameObject.transform.forward;
            moveDirection.y = 0f;
            moveDirection = moveDirection.normalized;

            const float fallMultiplier = 10f;
            var totalDistance = distance;
            var speed = totalDistance / duration;
            var movedDistance = 0f;
            var cancellationToken = ExecutableContext.CancellationToken.Token;

            while (movedDistance < totalDistance)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);

                rigidbody.AddForce(Physics.gravity * (fallMultiplier - 1f), ForceMode.Acceleration);

                var remainingDistance = totalDistance - movedDistance;
                var stepDistance = Mathf.Min(speed * Time.fixedDeltaTime, remainingDistance);

                if (stepDistance <= 0f)
                    break;

                rigidbody.MovePosition(rigidbody.position + moveDirection * stepDistance);
                movedDistance += stepDistance;
            }

            await ExecuteNextCommand().AttachExternalCancellation(cancellationToken);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new MoveForwardCommand(context, distance, duration);
        }
    }
}
