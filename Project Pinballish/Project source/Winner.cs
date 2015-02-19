using System;

namespace GXPEngine
{
	public class Winner : GameObject
	{
		Sprite _graphic;

		public Winner (LevelWinner winner)
		{
			switch (winner) {
			case LevelWinner.BLUE:
				_graphic = new Sprite ("Blue Wins.png");
				break;
			case LevelWinner.RED:
				_graphic = new Sprite ("Red Wins.png");
				break;
			case LevelWinner.NULL:
				_graphic = new Sprite ("Blue Wins.png");
				break;
			}
		}


	}
}

