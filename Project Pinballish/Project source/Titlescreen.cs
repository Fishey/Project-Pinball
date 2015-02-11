using System;

namespace GXPEngine
{
	public class Titlescreen : GameObject
	{
		StartButton _startbutton;
		QuitButton _quitbutton;
		Sprite _background;
		MyGame _game;
		LevelSelector _levelselector;



		public Titlescreen (MyGame myGame)
		{
			_background = new Sprite ("titlescreenph.jpg");
			AddChild (_background);

			_game = myGame;
			game.Add (this);

			_startbutton = new StartButton ();
			AddChild (_startbutton);
			_startbutton.SetXY (500, 200);

			_quitbutton = new QuitButton ();
			AddChild (_quitbutton);
			_quitbutton.SetXY (500, 300);

			_levelselector = new LevelSelector ();
			AddChild (_levelselector);
			_levelselector.SetXY (450, 200);

		}
		void Update ()
		{
			if (Input.GetKeyDown (Key.DOWN))
			{
				if (_levelselector.y == _startbutton.y)
				{
					_levelselector.SetXY (450, 300);
				}
		    }
		

			if (Input.GetKeyDown (Key.UP))
			{
				if (_levelselector.y == _quitbutton.y) 
				{
					_levelselector.SetXY (450, 200);
				}
			}
		
			if (_levelselector.y == _startbutton.y) {
				if (Input.GetKey (Key.Z)) {
					_game.SetState ("level");
				}
			}

			if (_levelselector.y == _quitbutton.y) {
				if (Input.GetKey (Key.Z)) {
					Environment.Exit (0);
				}
		 }
		 
		}


	}
}

