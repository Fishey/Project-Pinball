using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Scoreboard : Canvas
	{
		Font ScoreboardFont;
		Brush ScoreboardBrush;
		PointF ScoreboardPosition;
		List<LevelWinner> _levelWinners = new List<LevelWinner>();

		public Scoreboard (PointF pointy, SolidBrush brushy) : base (2000,800)
		{
			ScoreboardFont = new Font ("Induction", 30 , FontStyle.Regular);
			ScoreboardBrush = brushy;
			ScoreboardPosition = pointy;

		}

		public void DrawScore (int score)
		{
			string scoreMessage = "   " + score;

			graphics.Clear (Color.Transparent);

			graphics.DrawString(scoreMessage, ScoreboardFont, ScoreboardBrush, ScoreboardPosition);
		}

		public List<LevelWinner> LevelWinners
		{
			get { return this._levelWinners; }
			set { this._levelWinners = value; }
		}
	}
}




















