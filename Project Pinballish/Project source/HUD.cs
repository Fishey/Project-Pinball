using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class HUD : GameObject
	{
		private Sprite shell1;
		private Sprite shell2;
		private Level _level;
		private MyGame _game;
		private List<Ship> _shipList;
		private List<Sprite> _hpBar1;
		private List<Sprite> _hpBar2;
		public HUD (MyGame game, Level level, List<Ship> shipList) : base ()
		{
			_level = level;
			_game = game;
			_shipList = shipList;
			_hpBar1 = new List<Sprite> ();
			_hpBar2 = new List<Sprite> ();
			shell1 = new Sprite ("Images/Shell.png");
			shell1.SetOrigin (shell1.width, shell1.height);
			shell1.SetXY(2+shell1.width, this._game.height-shell1.height/2);
			this.AddChild (shell1);
			shell2 = new Sprite ("Images/Shell.png");
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
						_hpBar1.Add (energy);
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
						_hpBar2.Add (energy);
						energy.SetXY (-shell2.width+18, -60-position);
						position += energy.height;
					}
				}
				position = 0;
			}
		}
		public void removeEnergy(Ship ship)
		{
			if (ship.PlayerNum == 1) {
				_hpBar1 [ship.Energy - 1].Destroy ();
				this.RemoveChild (_hpBar1 [ship.Energy-1]);
				_hpBar1.Remove (_hpBar1 [ship.Energy - 1]);
			} else if (ship.PlayerNum == 2) {
				_hpBar2 [ship.Energy - 1].Destroy ();
				this.RemoveChild (_hpBar2 [ship.Energy-1]);
				_hpBar2.Remove (_hpBar2 [ship.Energy - 1]);
			}
		}

		public void addEnergy(Ship ship)
		{
			if (ship.PlayerNum == 1) {
				Sprite energy;
				if (_hpBar1.Count == 9) {
					energy = new Sprite ("Images/Top Energy.png");
				} else if (_hpBar1.Count < 9 && _hpBar1.Count > 0) {
					energy = new Sprite ("Images/Mid Energy.png");
				} else {
					energy = new Sprite ("Images/Bottom Energy.png");
				}
				if (_hpBar1.Count > 0)
					energy.SetXY (_hpBar1 [ship.Energy - 2].x, _hpBar1 [ship.Energy - 2].y - energy.height);
				else {
					energy.SetXY (-shell1.width+18, -60);

				}
				_hpBar1.Add (energy);
				shell1.AddChild (energy);
			} else if (ship.PlayerNum == 2) {
				Sprite energy;
				if (_hpBar2.Count == 9) {
					energy = new Sprite ("Images/Top Energy 2.png");
				} else if (_hpBar2.Count < 9 && _hpBar2.Count > 0) {
					energy = new Sprite ("Images/Mid Energy 2.png");
				} else {
					energy = new Sprite ("Images/Bottom Energy 2.png");
				}
				if (_hpBar2.Count > 0)
					energy.SetXY (_hpBar2 [ship.Energy - 2].x, _hpBar2 [ship.Energy - 2].y - energy.height);
				else {
					energy.SetXY (-shell2.width+18, -60);

				}
				_hpBar2.Add (energy);
				shell2.AddChild (energy);
			}

		}
	}
}

