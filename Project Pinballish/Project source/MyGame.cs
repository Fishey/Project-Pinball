using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public enum LevelWinner
	{
		NULL, RED, BLUE
	}

	public class MyGame : Game
	{ 
		private string _state = "";
		private Titlescreen _titleScreen;
		private Level _level;
		private ControlScreen _controlscreen;
		private HelpScreen _helpscreen;
		private HUD _hud;
		private Scoreboard _scoreboard;
		private Scoreboard _scoreboard2;
		private Winscreen _winscreen;

		private List<LevelWinner> _levelWinners = new List<LevelWinner> {LevelWinner.NULL, LevelWinner.NULL, LevelWinner.NULL};

		//pink fluffy unicorns with lasers

	
		public MyGame ():base (1920,1080, false,false) 
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
				_levelWinners = new List<LevelWinner> {LevelWinner.NULL, LevelWinner.NULL, LevelWinner.NULL};
				SoundManager.PlayMusic (SoundFile.MUSICMENU);
				_titleScreen = new Titlescreen (this);
				AddChild (_titleScreen);
				break;

			case "controls":
				_controlscreen = new ControlScreen (this);
				AddChild (_controlscreen);
				break;

			case "help":
				_helpscreen = new HelpScreen (this);
				AddChild (_helpscreen);
				break;

			case "level1":

				SoundManager.PlayMusic (SoundFile.MUSIC1);
				_level = new Level (this);
				_hud = new HUD (this, _level, _level.Ships);
				AddChild (_level);
				this.AddChild (_hud);

				_scoreboard = new Scoreboard (new PointF (-30,250), new SolidBrush (Color.Blue), _levelWinners, _level.Ships[0]);
				this.AddChild (_scoreboard);


				_scoreboard2 = new Scoreboard (new PointF (1700,250), new SolidBrush(Color.Red), _levelWinners, _level.Ships[1]);
				this.AddChild (_scoreboard2);
				break;

			case "level2":

				SoundManager.PlayMusic (SoundFile.MUSIC2);

				_level = new Level (this, 2);
				AddChild (_level);
				_hud = new HUD (this, _level, _level.Ships);
				this.AddChild (_hud);

				_scoreboard = new Scoreboard (new PointF (-30,250), new SolidBrush (Color.Blue), _levelWinners, _level.Ships[0]);
				this.AddChild (_scoreboard);


				_scoreboard2 = new Scoreboard (new PointF (1700,250), new SolidBrush(Color.Red), _levelWinners, _level.Ships[1]);
				this.AddChild (_scoreboard2);
				break;

			case "level3":

				SoundManager.PlayMusic (SoundFile.MUSIC3);
				_level = new Level (this, 3);
				AddChild (_level);
				_hud = new HUD (this, _level, _level.Ships);
				this.AddChild (_hud);

				_scoreboard = new Scoreboard (new PointF (-30,250), new SolidBrush (Color.Blue), _levelWinners, _level.Ships[0]);
				this.AddChild (_scoreboard);


				_scoreboard2 = new Scoreboard (new PointF (1700,250), new SolidBrush(Color.Red), _levelWinners, _level.Ships[1]);
				this.AddChild (_scoreboard2);
				break;

			case "Win":

				_winscreen = new Winscreen (this, _levelWinners);
				AddChild  (_winscreen);
				break;



			}
		
		}

		void stopState ()
		{
			switch (_state) {
			case "titleScreen":
				_titleScreen.Destroy ();
				this.RemoveChild (_titleScreen);
				break;

			case "controls":
				_controlscreen.Destroy ();
				this.RemoveChild (_controlscreen);
				break;

			case "help":
				_helpscreen.Destroy ();
				this.RemoveChild (_helpscreen);
				break;

			case "level1":
				_level.Destroy ();
				_hud.Destroy ();
				_scoreboard.Destroy ();
				_scoreboard2.Destroy ();
				this.RemoveChild (_hud);
				this.RemoveChild (_level);
				this.RemoveChild (_scoreboard);
				this.RemoveChild (_scoreboard2);
				break;
			
			case "level2":
				_level.Destroy ();
				_hud.Destroy ();
				_scoreboard.Destroy ();
				_scoreboard2.Destroy ();
				this.RemoveChild (_hud);
				this.RemoveChild (_level);
				this.RemoveChild (_scoreboard);
				this.RemoveChild (_scoreboard2);
				break;

			case "level3":
				_level.Destroy ();
				_hud.Destroy ();
				_scoreboard.Destroy ();
				_scoreboard2.Destroy ();
				this.RemoveChild (_hud);
				this.RemoveChild (_level);
				this.RemoveChild (_scoreboard);
				this.RemoveChild (_scoreboard2);
				break;

//			case "Win":
//
//				_winscreen.Destroy ();
//				this.RemoveChild (_winscreen);
//				break;
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

		public List<LevelWinner> LevelWinners
		{
			get { return this._levelWinners; }
			set { this._levelWinners = value; }
		}
	}
}

