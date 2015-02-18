using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Scoreboard : Canvas
	{
		Font _scoreboardFont;
		Brush _scoreboardBrush;
		PointF _scoreboardPosition;
		PointF _levelWinnerPosition;
		Ship _ship;
		List<LevelWinner> _levelWinners;

		public Scoreboard (PointF pointy, SolidBrush brushy, List<LevelWinner> levelWinners = null, Ship ship = null) : base (2000,800)
		{
			_ship = ship;
			_levelWinners = levelWinners;
			_scoreboardFont = new Font ("Induction", 30 , FontStyle.Regular);
			_scoreboardBrush = brushy;
			_scoreboardPosition = pointy;
			_levelWinnerPosition = new PointF (pointy.X+50, pointy.Y-100);
			DrawLevelScore ();

		}

		public void DrawScore (int score)
		{
			string scoreMessage = "   " + score;

			graphics.Clear (Color.Transparent);

			graphics.DrawString(scoreMessage, _scoreboardFont, _scoreboardBrush, _scoreboardPosition);
		}

		private void DrawLevelScore()
		{
			if (_ship.PlayerNum == 1) {
				foreach (LevelWinner winner in _levelWinners) {
					AnimSprite newOrb = new AnimSprite ("Images/SpriteSheet.png", 16, 16);
					newOrb.SetXY (_levelWinnerPosition.X, _levelWinnerPosition.Y);
					newOrb.SetScaleXY (0.2, 0.2);
					switch (winner) {
					case LevelWinner.BLUE:
						newOrb.SetFrame (31);
						break;
					case LevelWinner.RED:
						newOrb.SetFrame (30);
						break;
					case LevelWinner.NULL:
						newOrb.SetFrame (30);
						break;
					default:
						break;
					}
					_levelWinnerPosition.X += newOrb.width;
					this.AddChild (newOrb);
				}
			} else if (_ship.PlayerNum == 2) {
				foreach (LevelWinner winner in _levelWinners) {
					AnimSprite newOrb = new AnimSprite ("Images/SpriteSheet.png", 16, 16);
					newOrb.SetXY (_levelWinnerPosition.X, _levelWinnerPosition.Y);
					newOrb.SetScaleXY (0.2, 0.2);
					switch (winner) {
					case LevelWinner.RED:
						newOrb.SetFrame (29);
						break;
					case LevelWinner.BLUE:
						newOrb.SetFrame (28);
						break;
					case LevelWinner.NULL:
						newOrb.SetFrame (28);
						break;
					default:
						break;
					}
					_levelWinnerPosition.X += newOrb.width;
					this.AddChild (newOrb);
				}
			}
		}

		public List<LevelWinner> LevelWinners
		{
			get { return this._levelWinners; }
			set { this._levelWinners = value; }
		}
	}
}




















