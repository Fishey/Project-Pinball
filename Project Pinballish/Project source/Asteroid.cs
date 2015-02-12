using System;

namespace GXPEngine
{
	public class Asteroid : AnimSprite
	{
		int _damagecounter;

		public Asteroid (int damage = 0) : base("placeholder.png", 8, 1)
		{
			_damagecounter = damage;
		}

		private void Update()
		{
			if (_damagecounter < 3) // I love you too, code
				this.SetFrame (_damagecounter);
			else
				this.Destroy ();
		}

		public bool TakeDamage()
		{
			_damagecounter++;
			if (_damagecounter >= 3)
				return true;
			else
				return false;
		}
	}
}

