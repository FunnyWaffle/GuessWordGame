namespace GuessWordGame
{
    public class Text : UIElement
    {
        private List<object> _params = new();
        private readonly string _template;
        public Text(string template)
        {
            _template = template;
        }
        public string Value => string.Format(_template, _params.ToArray());
        public void Setparameters(params object[] parameters)
        {
            _params.Clear();
            _params.AddRange(parameters);
        }
    }
}
