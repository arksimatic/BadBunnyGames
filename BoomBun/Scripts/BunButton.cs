using Godot;
using System;

namespace BadBunnyGames.BomBun
{
	public partial class BunButton : Button
	{
		public Int32 X { get; set; }
		public Int32 Y { get; set; }
		public Boolean IsBun { get; set; } = false;
		public Boolean IsUncovered { get; set; } = false;
		public Boolean IsLocked { get; set; } = false;
		public Int32 BunAroundCount { get; set; }
		public Int32 Index(Int32 columns) => Y * columns + X;
		private Texture2D _bombTexture;
		private Texture2D _emptyTexture;
		private Texture2D _coveredTexture;
		private Texture2D _lockedTexture;
		private GameController _board { get; set; }
		public override void _Ready()
		{
			_bombTexture = (Texture2D)ResourceLoader.Load($"{GlobalPaths.BoomBun}/Graphics/bomb.png");
			_emptyTexture = (Texture2D)ResourceLoader.Load($"{GlobalPaths.BoomBun}/Graphics/empty.png");
			_coveredTexture = (Texture2D)ResourceLoader.Load($"{GlobalPaths.BoomBun}/Graphics/unknown.png");
			_lockedTexture = (Texture2D)ResourceLoader.Load($"{GlobalPaths.BoomBun}/Graphics/lock.png");

			this.Icon = _coveredTexture;
			_board = GetParent<GameController>();
		}
		private void _on_pressed()
		{
			if (IsBun)
				GD.Print("BOMB");
			GD.Print($"BunBombsAround: {BunAroundCount}");
			_board.ButtonPressed(this);
		}

		public void Uncover(Boolean automatic)
		{

			if (IsLocked && !automatic)
				return;

			this.IsUncovered = true;

			if (IsBun)
			{
				this.Icon = _bombTexture;
			}
			else
			{
				this.Icon = _emptyTexture;
				if (BunAroundCount > 0)
					this.GetChild<Label>(0).Text = BunAroundCount.ToString();
			}

			Disabled = true;
		}

		private void _on_gui_input(InputEvent @event)
		{
			// lock if right click
			if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Right)
			{
				IsLocked = !IsLocked;
				if (IsLocked)
					this.Icon = _lockedTexture;
				else
					this.Icon = _coveredTexture;
			}
		}

	}
}
