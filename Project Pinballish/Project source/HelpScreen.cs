using System;

namespace GXPEngine
{
	public class HelpScreen : GameObject
	{
		MyGame _game;
		AnimSprite _background;

		public HelpScreen (MyGame myGame)
		{
			_background = new AnimSprite ("Helpscreen.png",2,1);
			AddChild (_background);

			_game = myGame;
			game.Add (this);
		}

		void Update ()
		{
			if (Input.GetKey(Key.RIGHT)){
				_background.SetFrame(1);
			}
				if(Input.GetKey (Key.LEFT)){
					_background.SetFrame(0);
			}


			if (Input.GetKeyDown (Key.BACKSPACE)) {
				_game.SetState ("titleScreen");
			}
		}

	}
}
