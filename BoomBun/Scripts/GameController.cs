using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBunnyGames.BomBun
{
	public partial class GameController : GridContainer
	{
		public Int32 X { get; set; } = 3;
		public Int32 Y { get; set; } = 3;
		private PackedScene _bunMine { get; set; }
		private List<BunButton> _buttons;
		private Int32 _bombCount { get; set; } = 1;
		private CanvasLayer _addons;
		public override void _Ready()
		{
			_bunMine = GD.Load<PackedScene>($"{GlobalPaths.BoomBun}/Scenes/bun_bomb.tscn");
			_buttons = new List<BunButton>();
			_addons = GetNode<CanvasLayer>("%Addons");

			this.Columns = Y;

			for (int x = 0; x < X; x++)
			{
				for (int y = 0; y < Y; y++)
				{
					SpawnNextBunMine(x, y);
				}
			}

			AppendBombs();
			UpdateApproximateBombs();
		}

		private Int32[] GetRandomSubsetFromSet(Int32[] numbers)
		{
			return numbers.OrderBy(n => GD.RandRange(0, numbers.Length)).Take(_bombCount).ToArray();
		}
		public void AppendBombs()
		{
			Int32[] indexes = _buttons.Select(btn => btn.GetIndex()).ToArray();

			Int32[] bombIndexes = GetRandomSubsetFromSet(indexes);
			var bunButtonsWithBombs = _buttons.Where(btn => bombIndexes.Contains(btn.GetIndex()));

			foreach (var bunButton in bunButtonsWithBombs)
			{
				bunButton.IsBun = true;
			}
		}
		public void UpdateApproximateBombs()
		{
			foreach (var bunButton in _buttons)
			{
				bunButton.BunAroundCount = GetBombAroundCount(bunButton);
			}
		}
		private Int32 GetBombAroundCount(BunButton bunButton)
		{
			BunButton[] bunButtons = GetBunButtonsFromAround(bunButton);
			Int32 count = bunButtons.Where(bb => bb.IsBun).Count();
			return count;
		}
		private BunButton[] GetBunButtonsFromAround(BunButton bunButton)
		{
			List<BunButton> bunButtons = new();

			foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
			{
				var bunButtonAround = GetBunButtonFromDirection(bunButton, direction);
				if (bunButtonAround != null)
					bunButtons.Add(bunButtonAround);
			}

			return bunButtons.ToArray();
		}
		public BunButton GetBunButtonFromDirection(BunButton bunButton, Direction direction)
		{
			var x = bunButton.X;
			var y = bunButton.Y;

			switch (direction)
			{
				case Direction.Up:
					y--;
					break;
				case Direction.Down:
					y++;
					break;
				case Direction.Left:
					x--;
					break;
				case Direction.Right:
					x++;
					break;
				case Direction.UpLeft:
					x--;
					y--;
					break;
				case Direction.UpRight:
					x++;
					y--;
					break;
				case Direction.DownLeft:
					x--;
					y++;
					break;
				case Direction.DownRight:
					x++;
					y++;
					break;
			}

			if (x < 0 || x >= X || y < 0 || y >= Y)
			{
				return null;
			}

			return _buttons.FirstOrDefault(btn => btn.X == x && btn.Y == y);
		}
		public void ButtonPressed(BunButton bunButton)
		{
			bunButton.Uncover(false);
			if (bunButton.BunAroundCount == 0 && !bunButton.IsBun)
			{
				List<BunButton> bunButtonsToUncover = GetBunButtonsFromAround(bunButton).ToList();
				while (bunButtonsToUncover.Count > 0)
				{
					var inspectedBunButton = bunButtonsToUncover.First();
					if (!inspectedBunButton.Disabled)
					{
						inspectedBunButton.Uncover(true);
						if (inspectedBunButton.BunAroundCount == 0)
							bunButtonsToUncover.AddRange(GetBunButtonsFromAround(inspectedBunButton));
					}
					bunButtonsToUncover.RemoveAt(0);
				}
			}

			if (IsGameLost())
			{
				GetNode<Addons>("%Addons")._on_game_over();
			}
			else if (IsGameWon())
			{
				GD.Print("You won!");
			}
		}
		private Boolean IsGameWon()
		{
			GD.Print($"{_buttons.Where(btn => !btn.IsBun && btn.IsUncovered).Count()} remains");
			return _buttons.Where(btn => btn.IsBun || btn.IsUncovered).Count() == _buttons.Count();
		}
		private Boolean IsGameLost()
		{
			GD.Print($"{_buttons.Where(btn => btn.IsBun && btn.IsUncovered).Count()} bombs found");
			return _buttons.Where(btn => btn.IsBun && btn.IsUncovered).Count() > 0;
		}

		private void SpawnNextBunMine(Int32 x, Int32 y)
		{
			var button = _bunMine.Instantiate() as Button;
			button.Name = $"Button{_buttons.Count}";
			AddChild(button);

			var bunButton = button as BunButton;
			_buttons.Add(bunButton);
			bunButton.X = x;
			bunButton.Y = y;
		}
	}
}
