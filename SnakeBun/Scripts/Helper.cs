using Godot;
using System;

namespace BadBunnyGames.SnakeBun
{
	public class Helper
	{
		public static Single GetDegreesFromVector(Vector2 vector)
		{
			if (vector.X == -1) return 270;
			else if (vector.X == 1) return 90;
			else if (vector.Y == -1) return 0;
			else if (vector.Y == 1) return 180;
			else return 0;
		}

		public static String GetKeyCodeFromDegrees(Single degrees)
		{
			if (degrees == 270) return KeyCode.Left;
			else if (degrees == 90) return KeyCode.Right;
			else if (degrees == 0) return KeyCode.Up;
			else if (degrees == 180) return KeyCode.Down;
			else return "NULL";
		}
	}
}
