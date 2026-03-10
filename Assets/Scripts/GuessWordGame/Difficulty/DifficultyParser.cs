using System;

namespace Assets.Scripts.GuessWordGame
{
    public static class DifficultyParser
    {
        public static bool TryParse(string type, out Difficulty difficultyType)
        {
            return TryGetDifficultyByIndex(type[0], out difficultyType)
               || TryGetDifficultyEnumValue(type, out difficultyType);
        }
        private static bool TryGetDifficultyByIndex(char index, out Difficulty difficultyType)
        {
            difficultyType = Difficulty.None;

            if (!char.IsDigit(index))
                return false;

            difficultyType = index switch
            {
                '1' => Difficulty.Easy,
                '2' => Difficulty.Medium,
                '3' => Difficulty.Hard,
                _ => Difficulty.None,
            };

            if (difficultyType == Difficulty.None)
                return false;

            return true;
        }
        private static bool TryGetDifficultyEnumValue(string type, out Difficulty difficultyType)
        {
            return Enum.TryParse(type, true, out difficultyType)
                && Enum.IsDefined(typeof(Difficulty), difficultyType);
        }
    }
}
