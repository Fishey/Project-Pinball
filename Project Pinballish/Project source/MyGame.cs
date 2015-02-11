using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class MyGame : Game
	{ 
		private string _state = "";
		private Titlescreen _titleScreen;
		private Level _level;

		//pink fluffy unicorns with lasers

	
		public MyGame ():base (1920,1080, false) 
		{
			scaleX = 0.5f;
			scaleY = 0.5f;

			SetState ("titleScreen");
		}

		public void SetState (string state) // Setting which state the game should start on 
				{
					if (state == _state)
						return;
					stopState ();
					_state = state;
					startState ();
				}


		void startState ()
		{
			switch (_state) {
			case "titleScreen":
				_titleScreen = new Titlescreen (this);
				AddChild (_titleScreen);
				break;
			case "level":
				_level = new Level (this);
				AddChild (_level);
				break;
			}	
		}

		void stopState ()
		{
			switch (_state) {
			case "titleScreen":
				_titleScreen.Destroy ();

					break;
			case "level":
				_level.Destroy ();
				break;
			}
		}
	


		static void Main() 
		{

			new MyGame ().Start ();

		}
	}
}

