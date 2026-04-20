using Cysharp.Threading.Tasks;
using domain.commands.executables.motion.rotate;
using UnityEngine;

namespace domain.commands.executables.motion.grid
{
    public class TurnRightCommand : BaseRotateCommand
    {
        private readonly float degrees;
        private readonly float duration;

        public TurnRightCommand(ExecutableContext context, float degrees, float duration) : base(context)
        {
            this.degrees = degrees;
            this.duration = duration;
        }

        public override async UniTask OnEnterAsync()
        {
            await RotateBy(new Vector3(0f, degrees, 0f), duration);
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new TurnRightCommand(context, degrees, duration);
        }
    }
}
