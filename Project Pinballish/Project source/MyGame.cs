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
		private ControlScreen _controlscreen;
		private HUD _hud;
		private Scoreboard _scoreboard;
		private Scoreboard _scoreboard2;

		//pink fluffy unicorns with lasers

	
		public MyGame ():base (1920,1080, false) 
		{
			//scaleX = 0.5f;
			//scaleY = 0.5f;

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
				SoundManager.PlayMusic (SoundFile.MUSICMENU);
				_titleScreen = new Titlescreen (this);
				AddChild (_titleScreen);
				break;

			case "controls":
				_controlscreen = new ControlScreen (this);
				AddChild (_controlscreen);
				break;


			case "level":
				SoundManager.PlayMusic (SoundFile.MUSIC1);
				_level = new Level (this);
				_hud = new HUD (this, _level, _level.Ships);
				AddChild (_level);
				this.AddChild (_hud);

				_scoreboard = new Scoreboard (new PointF (-45,100), new SolidBrush (Color.Blue));
				this.AddChild (_scoreboard);


				_scoreboard2 = new Scoreboard (new PointF (1690,100), new SolidBrush(Color.Red));
				this.AddChild (_scoreboard2);
				break;
			case "level2":
				SoundManager.PlayMusic (SoundFile.MUSIC2);
				_level = new Level (this, 2);
				AddChild (_level);
				break;
			case "level3":
				SoundManager.PlayMusic (SoundFile.MUSIC3);
				_level = new Level (this, 3);
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

			case "controls":
				_controlscreen.Destroy ();
				break;

			case "level":
				_level.Destroy ();
				_hud.Destroy ();
				this.RemoveChild (_hud);
				break;
			
			case "level2":
				_level.Destroy ();
				_hud.Destroy ();
				this.RemoveChild (_hud);
				break;

			case "level3":
				_level.Destroy ();
				_hud.Destroy ();
				this.RemoveChild (_hud);
				break;
			}
		}
	


		static void Main() 
		{

			new MyGame ().Start ();

		}

		public Scoreboard Scoreboard
		{
			get { return this._scoreboard; }
		}

		public Scoreboard Scoreboard2
		{
			get { return this._scoreboard2; }
		}

		public HUD Hud
		{
			get { return this._hud; }
		}
	}
}

