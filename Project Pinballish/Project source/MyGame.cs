using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class MyGame : Game
	{
		static void Main() {
			new MyGame ().Start ();
		}

		private List<Ship> _ships;
		private Ship _ship = null;
		private Ship _ship2 = null;
		private Vec2 _center = null;


		public MyGame ():base (1280,720, false) {

			AddChild(new Sprite("background.jpg")); // add a beautiful background
			_ships = new List<Ship> (); // create the list for ships (Player 1 & 2 go here)

			//Create ships
			_ship = new Ship(25, 1);
			_ship.position.x = width/2 + width/4;
			_ship.position.y = height/2 - 50;
			_ships.Add (_ship);

			_ship2 = new Ship (25, 2);
			_ship2.position.x = width / 2 - width / 4;
			_ship2.position.y = height / 2 - 50;
			//_ships.Add (_ship2); //uncomment this to add a second ship

			foreach(Ship ship in _ships)
				AddChild (ship); // add the ships to the game

			_center = new Vec2 (width / 2, height / 2);
		}

		void Update() {
			foreach (Ship ship in _ships) {
				rotateAroundPoint (ship, _center, (float)(10 * Math.PI / 180.0f)); // make the ships turn around the center of the screen
				ship.Step ();
			}
		}

		void processInput(Ship ship)
		{
			if (Input.GetKey(Key.UP) && ship.PlayNum == 1)

		}

		void rotateAroundPoint(Ship sprite, Vec2 center, float angle) {
			double dx = sprite.x - center.x;
			double dy = sprite.y - center.y;

			double cosAngle = Math.Cos (angle/5);
			double sinAngle = Math.Sin (angle/5);
			if (Input.GetKey(Key.RIGHT) && sprite.PlayNum == 1)
			{
				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			else if (Input.GetKey(Key.LEFT) && sprite.PlayNum == 2)
			{
				cosAngle = Math.Cos (-angle / 5);
				sinAngle = Math.Sin (-angle / 5);

				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			sprite.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
		}


	}
}

