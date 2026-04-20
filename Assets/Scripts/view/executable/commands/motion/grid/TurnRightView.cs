using domain.commands.executables;
using domain.commands.executables.motion.grid;
using UnityEngine;

namespace view.executable.commands.motion.grid
{
    public class TurnRightView : ExecutableView
    {
        [SerializeField, Min(0f)] private float degrees = 90f;
        [SerializeField, Min(0.01f)] private float duration = 0.2f;

        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);
            command = new TurnRightCommand(context, degrees, duration);
        }
    }
}
