using System;
using System.Drawing;

namespace GXPEngine
{
	public class Asteroid : Ball
	{
		int _damagecounter;
		int _radius;
		AnimSprite _graphic;
		public Asteroid (int damage = 0, int pradius = 70) : base(pradius, null, null, Color.Transparent)
		{
			_radius = pradius;
			_graphic = new AnimSprite ("Images/Asteroids.png", 4, 2);
			this.SetScaleXY (0.5, 0.5);
			if (radius == 70) {
				_graphic.SetXY (-100, -100);
			} else if (radius == 60) {
				_graphic.SetXY (-65, -60);
				_graphic.SetScaleXY (0.8, 0.8);
			}

			if (_damagecounter >= 3 && _radius == 70) {
				_graphic.SetFrame (_damagecounter);
			} else if (_damagecounter < 3 && _radius == 60)
			{
				_graphic.SetFrame (_damagecounter + 4);
			}
			this.SetOrigin (this.width / 2, this.height / 2);
			this.AddChild (_graphic);
			_damagecounter = damage;
		}

		public bool TakeDamage()
		{
			_damagecounter++;
			if (_damagecounter <= 3 && _radius == 70) {
				_graphic.SetFrame (_damagecounter);
				return false;
			} else if (_damagecounter <= 2 && _radius == 60) {
				_graphic.SetFrame (_damagecounter + 4);
				return false;
			} else
				this.Destroy ();
				return true;
		}
	}
}

