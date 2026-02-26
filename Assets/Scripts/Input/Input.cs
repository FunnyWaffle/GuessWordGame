namespace Assets.Scripts.Input
{
    public static class Input
    {
        public static readonly NewInput _inputActions = new();
        static Input()
        {
            _inputActions.Enable();
        }
    }
}
