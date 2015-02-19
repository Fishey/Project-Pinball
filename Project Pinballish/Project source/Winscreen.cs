//using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Winscreen : AnimSprite
	{
		MyGame _mygame;
		int bluewin;
		int redwin;


		public Winscreen (MyGame myGame, List<LevelWinner> levelwinner) : base ("Images/Winscreens.png", 2, 1)
		{

			_mygame = myGame;
			_mygame.Add (this);

			foreach (LevelWinner winner in levelwinner) {
				switch (winner) {

				case LevelWinner.BLUE:
					bluewin++;
					break;

				case LevelWinner.RED:
					redwin++;
					break;

				default:

					break;
				}

			}

			if (bluewin > redwin) {
				this.SetFrame (1);
			}
			else if (redwin > bluewin)
			{
				this.SetFrame (0);
			}

		
			}

		public void Update()
		{
			if (Input.GetKeyDown (Key.X)) {
				_mygame.SetState ("titleScreen");
			}
		}

		}

	}


