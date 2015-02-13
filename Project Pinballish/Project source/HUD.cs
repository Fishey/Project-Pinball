using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class HUD : GameObject
	{
		private AnimSprite shell1;
		private AnimSprite shell2;
		private Level _level;
		private MyGame _game;
		private List<Ship> _shipList;
		public HUD (MyGame game, Level level, List<Ship> shipList) : base ()
		{
			_level = level;
			_game = game;
			_shipList = shipList;
			shell1 = new AnimSprite ("Images/Shell.png", 1, 1);
			shell1.SetOrigin (shell1.width, shell1.height);
			shell1.SetXY(2+shell1.width, this._game.height-shell1.height/2);
			this.AddChild (shell1);
			shell2 = new AnimSprite ("Images/Shell.png", 1, 1);
			shell2.SetOrigin (shell2.width, shell2.height);
			shell2.SetXY(this._game.width-5, this._game.height-shell2.height/2);
			this.AddChild (shell2);

			for (int i = 0; i < _shipList.Count; i++) {
				int position = 0;
				for (int y = 0; y < _shipList [i].Energy; y++) {
					if (_shipList [i].PlayerNum == 1) {
						Sprite energy;
						if (y == 0) {
							energy = new Sprite ("Images/Bottom Energy.png");

						} else if (y == 9) {
							energy = new Sprite ("Images/Top Energy.png");

						} else {
							energy = new Sprite ("Images/Mid Energy.png");
						}
						shell1.AddChild (energy);
						energy.SetXY (-shell1.width+18, -60-position);
						position += energy.height;
					}
					else if (_shipList [i].PlayerNum == 2) {
						Sprite energy;
						if (y == 0) {
							energy = new Sprite ("Images/Bottom Energy 2.png");

						} else if (y == 9) {
							energy = new Sprite ("Images/Top Energy 2.png");

						} else {
							energy = new Sprite ("Images/Mid Energy 2.png");
						}
						shell2.AddChild (energy);
						energy.SetXY (-shell2.width+18, -60-position);
						position += energy.height;
					}
				}
				position = 0;
			}
		}

		public void Update()
		{
		
		}
	}
}

