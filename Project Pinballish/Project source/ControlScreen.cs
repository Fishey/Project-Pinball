using System;

namespace GXPEngine
{
	public class ControlScreen : GameObject
	{
		MyGame _game;
		Sprite _background;

		public ControlScreen (MyGame myGame)
		{
			_background = new Sprite ("Controlscreen.png");
			AddChild (_background);

			_game = myGame;
			game.Add (this);
		}

		void Update ()
		{
			if (Input.GetKeyDown (Key.X)) {
				_game.SetState ("titleScreen");
			}
		}

	}
}

