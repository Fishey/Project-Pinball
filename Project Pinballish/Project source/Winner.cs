using System;

namespace GXPEngine
{
	public class Winner : GameObject
	{
		Sprite _graphic;
		int _timer;
		Level _level;

		public Winner (MyGame MG, Level level, LevelWinner winner)
		{
			SetXY (MG.width / 2, MG.height / 2);
			_level = level;
			_timer = 120;
			switch (winner) {
			case LevelWinner.BLUE:
				_graphic = new Sprite ("Images/Blue Wins.png");
				break;
			case LevelWinner.RED:
				_graphic = new Sprite ("Images/Red Wins.png");
				break;
			case LevelWinner.NULL:
				_graphic = new Sprite ("Images/Blue Wins.png");
				break;
			}
			_graphic.SetOrigin (_graphic.width / 2, _graphic.height / 2);
			_graphic.SetScaleXY (0.2, 0.2);
			this.AddChild (_graphic);

		}
		public void Update()
		{
			if (_timer > 0)
				_timer--;
			else {
				this.Destroy ();
				_level.RemoveChild (this);
			}
		}
	}
}

