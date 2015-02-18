using System;

namespace GXPEngine
{
	public class Titlescreen : GameObject
	{
		StartButton _startbutton;
		QuitButton _quitbutton;
		ControlButton _controlbutton;
		HelpButton _helpbutton;
		Sprite _background;
		MyGame _game;
		LevelSelector _levelselector;



		public Titlescreen (MyGame myGame)
		{
			_background = new Sprite ("Titlescreen.png");
			AddChild (_background);

			_game = myGame;
			game.Add (this);

			_startbutton = new StartButton ();
			AddChild (_startbutton);
			_startbutton.SetXY (800, 300);

			_controlbutton = new ControlButton ();
			AddChild (_controlbutton);
			_controlbutton.SetXY (650, 450);

			_helpbutton = new HelpButton ();
			AddChild (_helpbutton);
			_helpbutton.SetXY (800,600);

			_quitbutton = new QuitButton ();
			AddChild (_quitbutton);
			_quitbutton.SetXY (840, 750);

			_levelselector = new LevelSelector ();
			AddChild (_levelselector);
			_levelselector.SetXY (450, 300);

		}

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
		void Update ()
		{
			if (Input.GetKeyDown (Key.DOWN))
			{
				if (_levelselector.y == _startbutton.y) 
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450, 450);

				} else if (_levelselector.y == _controlbutton.y) 
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450, 600);

				} 
				else if (_levelselector.y == _helpbutton.y)
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450, 750);
				}
		    }
		

			if (Input.GetKeyDown (Key.UP))
			{
				if (_levelselector.y == _quitbutton.y) 
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450, 600);
				}

				else if (_levelselector.y == _helpbutton.y)
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450,450);
				}
				else if (_levelselector.y == _controlbutton.y) 
				{
					SoundManager.PlaySound (SoundFile.SELECTION);
					_levelselector.SetXY (450, 300);
				}
			
			}
		
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			if (_levelselector.y == _startbutton.y) {
				_startbutton.SetFrame (1);


				if (Input.GetKey (Key.Z)) {
					_game.SetState ("level");
				}
			} 

			else
				_startbutton.SetFrame (0);

			if (_levelselector.y == _controlbutton.y) {
				_controlbutton.SetFrame (1);

			
				if (Input.GetKey (Key.Z)) {
					_game.SetState ("controls");
				}
			} else
				_controlbutton.SetFrame (0);
				

			if (_levelselector.y == _helpbutton.y) {
				_helpbutton.SetFrame (1);

				if (Input.GetKey (Key.Z)) {
					_game.SetState ("help");
				}
			}
				else
					_helpbutton.SetFrame (0);
			









			if (_levelselector.y == _quitbutton.y) {
				_quitbutton.SetFrame (1);

				if (Input.GetKey (Key.Z)) {
					Environment.Exit (0);
				} 

		 }
			else
				_quitbutton.SetFrame (0);
		}


	}
}

