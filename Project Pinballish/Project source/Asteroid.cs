using System;

namespace GXPEngine
{
	public class Asteroid : AnimSprite
	{
		int _damagecounter;

		public Asteroid (int damage) : base("placeholder.png", 8, 1)
		{
			_damagecounter = 0;
			SetFrame (0);
		}

		public void TakeDamage()
		{

		}
	}
}

