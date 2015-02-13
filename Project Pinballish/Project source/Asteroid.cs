using System;

namespace GXPEngine
{
	public class Asteroid : Ball
	{
		int _damagecounter;
		AnimSprite _graphic;
		public Asteroid (int damage = 0, int pradius = 70) : base(pradius)
		{
			_graphic = new AnimSprite ("Images/Asteroid.png", 1, 1);
			this.SetOrigin (this.width / 2, this.height / 2);
			this.AddChild (_graphic);
			_graphic.SetXY (-110, -100);
			_damagecounter = damage;
			this.SetScaleXY (0.5, 0.5);
		}

		private void Update()
		{
			if (_damagecounter < 3) // I love you too, code
				_damagecounter = _damagecounter;
				//this.SetFrame (_damagecounter);
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

