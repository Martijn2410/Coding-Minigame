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
            var moveVector = Context.GameObject.transform.forward;
            moveVector.y = 0f;
            moveVector = moveVector.normalized * distance;

            await MoveBy(moveVector, duration);
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new MoveForwardCommand(context, distance, duration);
        }
    }
}
