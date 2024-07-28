using Godot;
using System.Collections.Generic;

namespace BadBunnyGames.BunThree
{
    public static class Helper
    {
        public static Color GetRandomColor()
        {
            RandomNumberGenerator rng = new RandomNumberGenerator();
            rng.Randomize();
            return new Color(rng.RandfRange(0, 1), rng.RandfRange(0, 1), rng.RandfRange(0, 1));
        }
        public static T GetRandomItem<T>(this IList<T> list) where T : class
        {
            RandomNumberGenerator rng = new RandomNumberGenerator();
            rng.Randomize();
            var randomIndex = rng.RandiRange(0, list.Count - 1);
            return list[randomIndex];
        }
    }
}
