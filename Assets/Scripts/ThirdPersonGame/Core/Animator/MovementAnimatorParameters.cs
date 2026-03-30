namespace Assets.Scripts.ThirdPersonGame.Core
{
    public static class MovementAnimatorParameters
    {
        public static readonly int ForwardSpeed = UnityEngine.Animator.StringToHash(nameof(ForwardSpeed));
        public static readonly int SideSpeed = UnityEngine.Animator.StringToHash(nameof(SideSpeed));
        public static readonly int IsJumpReleased = UnityEngine.Animator.StringToHash(nameof(IsJumpReleased));
        public static readonly int Grounded = UnityEngine.Animator.StringToHash(nameof(Grounded));

        public static readonly int RotationAngle = UnityEngine.Animator.StringToHash(nameof(RotationAngle));
    }
}
