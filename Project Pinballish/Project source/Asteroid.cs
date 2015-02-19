using System;
using System.Drawing;

namespace GXPEngine
{
	public class Asteroid : Ball
	{
		int _damagecounter;
		int _radius;
		AnimSprite _graphic;
		public Asteroid (int damage = 0, int pradius = 80) : base(pradius, null, null, Color.Transparent)
		{
			_radius = pradius;
			_graphic = new AnimSprite ("Images/SpriteSheet.png", 16, 16);
			this.SetScaleXY (0.5, 0.5);
			if (radius == 80) {
				_graphic.SetXY (-65, -70);
				_graphic.SetScaleXY (0.8, 0.8);
			} else if (radius == 60) {
				_graphic.SetXY (-65, -60);
				_graphic.SetScaleXY (0.8, 0.8);
			} else if (radius == 40) {
				_graphic.SetScaleXY (1.2, 1.2);
				_graphic.SetXY (-125, -120);
			}

			if (_damagecounter <= 3 && _radius == 80) {
				_graphic.SetFrame (_damagecounter + 16);
			} else if (_damagecounter < 3 && _radius == 60) {
				_graphic.SetFrame (_damagecounter + 20);
			} else if (_radius == 40)
				_graphic.SetFrame (23);
			this.SetOrigin (this.width / 2, this.height / 2);
			this.AddChild (_graphic);
			_damagecounter = damage;
		}

		public bool TakeDamage()
		{
			_damagecounter++;
			if (_damagecounter <= 3 && _radius == 80) {
				_graphic.SetFrame (_damagecounter + 16);

				return false;
			}


			else if (_damagecounter <= 2 && _radius == 60) {
				_graphic.SetFrame (_damagecounter + 20);
				return false;
			} else if (_radius == 40 && _damagecounter < 1)

				return false;
			else 

				return true;
		}
	}
}

