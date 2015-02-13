using System;
using System.Drawing;

namespace GXPEngine
{
	public class Scoreboard : Canvas
	{
		Font ScoreboardFont;
		Brush ScoreboardBrush;
		PointF ScoreboardPosition;


		public Scoreboard (PointF pointy) : base (2000,800)
		{
			ScoreboardFont = new Font ("Induction", 30 , FontStyle.Regular);
			ScoreboardBrush = new SolidBrush (Color.Red);
			ScoreboardPosition = pointy;

		}

		public void DrawScore (int score)
		{
		
			string scoreMessage = "   " + score;

			graphics.Clear (Color.Transparent);

			graphics.DrawString(scoreMessage, ScoreboardFont, new SolidBrush(Color.Red), ScoreboardPosition);


		}
	}
}




















