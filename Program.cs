using System;

namespace DZCamp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Character[] characters = {
                new Character("Луизианна", 2, 3, "поражение, победа, победа, поражение"),
                new Character("Коркес", 4, 5, "победа, победа, победа, поражение, победа"),
                new Character("Нова", 5, 7, "победа, победа, поражение, победа, победа, победа, поражение"),
                new Character("Юн Джин", 1, 3, "победа, поражение, поражение"),
                new Character("Рэйко", 2, 5, "поражение, поражение, победа, победа, поражение"),
            };
            Console.WriteLine("Самый успешный герой: " + characters[0].GetMaxWins(characters));
            Console.WriteLine("Самый неуспешный герой: " + characters[0].GetLoseRate(characters));
            Console.WriteLine("Самый любимый герой: " + characters[0].GetFavouriteCharacter(characters));
            Console.WriteLine("Самый нелюбимый герой: " + characters[0].GetMinMatchesPlayer(characters));
            Console.WriteLine("Герой с самым большим винстриком: " + characters[0].GetHighWinStreakPlayer(characters));
        }
    }

    class Character
    {
        private string _name, _gameResults;
        private int _winCount, _gameCount;
        private int localGameCount;

        public Character(string name, int winCount, int gameCount, string gameResults)
        {
            _name = name;
            _winCount = winCount;
            _gameCount = gameCount;
            _gameResults = gameResults;
        }

        public string GetMaxWins(Character[] characters)
        {
            float countWinMax = 0f;
            string bestCharacterName = string.Empty;
            foreach (Character character in characters)
            {
                if ((character._winCount * 100f) / (character._gameCount * 100f) > countWinMax)
                {
                    countWinMax = (character._winCount * 100f) / (character._gameCount * 100f);
                    bestCharacterName = character._name;
                }
            }
            bestCharacterName += $" (винрейт {string.Format("{0:0.0}", countWinMax)})";
            return bestCharacterName;
        }

        public string GetLoseRate(Character[] characters)
        {

            float countLoseMax = 1f;
            string worstCharacterName = string.Empty;
            foreach (Character character in characters)
            {
                if ((character._winCount * 100f) / (character._gameCount * 100f) < countLoseMax)
                {
                    countLoseMax = (character._winCount * 100f) / (character._gameCount * 100f);
                    worstCharacterName = character._name;
                }
            }
            worstCharacterName += $" (винрейт {string.Format("{0:0.00}", countLoseMax)})";
            return worstCharacterName;
        }

        public string GetFavouriteCharacter(Character[] characters)
        {
            int matchCount = 0;
            string favouriteCharacterName = string.Empty;
            foreach (Character character in characters)
            {
                if (character._gameCount > matchCount)
                {
                    matchCount = character._gameCount;
                    favouriteCharacterName = character._name;
                }
            }
            localGameCount = matchCount;
            return favouriteCharacterName + $" ({matchCount} матчей)";
        }
        public string GetMinMatchesPlayer(Character[] characters)
        {
            int countOfGames = localGameCount;
            string unFavouriteCharacters = string.Empty;
            foreach (Character character in characters)
            {
                if (character._gameCount < countOfGames) countOfGames = character._gameCount;
            }

            foreach (Character character in characters)
            {
                if (countOfGames == character._gameCount) unFavouriteCharacters += character._name + $" ({countOfGames} матча), ";
            }
            unFavouriteCharacters = unFavouriteCharacters.Remove(unFavouriteCharacters.Length - 2);
            return unFavouriteCharacters;
        }
        public string GetHighWinStreakPlayer(Character[] characters)
        {
            Dictionary<string, int> highWinStreakPlayer = new Dictionary<string, int>();
            string matchResult = string.Empty;         
            string playersWithHighWinStreak = string.Empty;
            int streakSort = 0;
            foreach (Character character in characters)
            {
                int maxStreak = 0;
                int countWin = 0;
                string tempMatchesInfo = character._gameResults + ",";
                for (int i = 0; i < tempMatchesInfo.Length; i++)
                {
                    matchResult += tempMatchesInfo[i];
                    if (tempMatchesInfo[i] == ',')
                    {
                        matchResult = matchResult.Remove(matchResult.Length - 1);
                        i++;
                        if (matchResult == "победа")
                        {
                            countWin++;
                            if (maxStreak < countWin)
                            {
                                maxStreak = countWin;
                               // maxStreak += 1;
                            }
                        }
                        else countWin = 0;
                        
                        matchResult = string.Empty;
                    }
                }
                if (maxStreak != 0) countWin = 0;
                highWinStreakPlayer.Add(character._name, maxStreak);
            }

            foreach (var streakMax in highWinStreakPlayer)
            {
                if (streakSort < streakMax.Value) streakSort = streakMax.Value;
            }

            foreach (var character in highWinStreakPlayer)
            {
                if (character.Value < streakSort) highWinStreakPlayer.Remove(character.Key);
            }

            foreach (var character in highWinStreakPlayer)
            {
                playersWithHighWinStreak += character.Key + $" (винстрик {character.Value}), "; 
            }
            playersWithHighWinStreak = playersWithHighWinStreak.Remove(playersWithHighWinStreak.Length - 2);
            return playersWithHighWinStreak;
        }
    }

}