using System;

namespace GXPEngine
{
	public class Shield : Sprite
	{
		private Sprite _graphic;

		public Shield () : base ("Images/Shieldhitbox.png") 
		{
			SetScaleXY (1.3, 1.3);
			SetXY (-80, -80);
			_graphic = new Sprite ("Images/Shield.png");
			_graphic.SetScaleXY (0.6, 0.6);
			_graphic.SetXY (-20, -20);
			AddChild (_graphic);

		}

//		void Update ()

	}
}

