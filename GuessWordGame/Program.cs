namespace GuessWordGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            UI.HideCursor();
            while (true)
            {
                Input.ReadInput();
                UI.UpdateUI();
            }
        }
    }
}
