using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace GXPEngine
{
	public enum ShipType
	{
		NULL, REDSHARK, BLUESHARK, REDSHIP, BLUESHIP
	}

	public enum PowerUpType
	{
		NULL, ENERGYUP, MULTIPLIER, SPEEDUP, SPEEDDOWN
	}

	public class Level : GameObject
	{
		private const int HEIGHT 	= 40;
		private const int WIDTH		= 40;
		private const int TILESIZE 	= 20;

		private int _level;
		private List<Ship> _ships;
		private List<Projectile> _projectiles;
		private List<Asteroid> _asteroids;
		private List<PowerUp> _powerUps;
		private Ship _ship = null;
		private Ship _ship2 = null;
		private Vec2 _center = null;
		private MyGame _mg;

		private int[,] _data = new int[WIDTH,HEIGHT];

		public Level (MyGame MG, int level = 1, List<Ship> ships = null)
		{
			_level = level;
			_mg = MG;
			if (ships != null) {
				_ships = ships;

				_ship = _ships [0];
				_ship.position.x = _mg.width / 2 - _mg.width / 4;
				_ship.position.y = _mg.height / 2 - 50;
				_ship2 = ships [1];
				_ship2.position.x = _mg.width/2 + _mg.width/4;
				_ship2.position.y = _mg.height/2 - 50;
			} else {
				_ships = new List<Ship> (); // create the list for ships (Player 1 & 2 go here)

				//Create ships
				_ship = new Ship(ShipType.BLUESHIP, 1, MG, this);
				_ship.position.x = _mg.width / 2 - _mg.width / 4;
				_ship.position.y = _mg.height / 2 - 50;
				_ships.Add (_ship);

				_ship2 = new Ship (ShipType.REDSHARK, 2, MG, this);
				_ship2.position.x = _mg.width/2 + _mg.width/4;
				_ship2.position.y = _mg.height/2 - 50;
				_ships.Add (_ship2);
			}

			_projectiles = new List<Projectile> (); // pew pews go here
			_asteroids = new List<Asteroid> (); // things to pew pew at go here
			_powerUps = new List<PowerUp> (); // things that make you pew pew even more go here

			AddChild(new Sprite("background.png")); // add a beautiful background



			ReadLevel (level);
			for (int j = 0; j < HEIGHT; j++) {
				for (int i = 0; i < WIDTH; i++) {
					int tile = _data [j, i];
					if (tile != 0)
						addAsteroid (i * TILESIZE, j * TILESIZE, tile);
				}
			}
				
			foreach(Ship ship in _ships)
				AddChild (ship); // add the ships to the game
			foreach (Asteroid asteroid in _asteroids)
				AddChild (asteroid);
				

			_center = new Vec2 (_mg.width / 2, _mg.height / 2);

		}

		public void Scoreboard()
		{
			_mg.Scoreboard.DrawScore (_ships[0]._score);
			_mg.Scoreboard2.DrawScore (_ships[1]._score);
		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																//ROTATION AND HIT TEST
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void Update() {

			foreach (Ship ship in _ships) {
				rotateAroundPoint (ship, _center, (float)(5 * Math.PI / 180.0f)); // make the ships turn around the center of the screen
				rotateAsteroids (_center);
				processInput (ship);
				ship.Step ();
			}

			foreach (PowerUp powerup in _powerUps) {
				powerup.Step ();
				if (powerup.x > _mg.width)
					powerup.velocity.Reflect (new Vec2 (_mg.width, 0).Normal ());
				else if (powerup.x < 0)
					powerup.velocity.Reflect (new Vec2 (0, 0).Normal ());
				else if (powerup.y < 0)
					powerup.velocity.Reflect (new Vec2 (0, 0).Normal ());
				else if (powerup.y > _mg.height)
					powerup.velocity.Reflect (new Vec2 (0, _mg.height).Normal ());
			}

			resolveCollisions ();
			Scoreboard ();

			if (_asteroids.Count == 0 || _ships[0].Energy + _ships[1].Energy == 0 & _projectiles.Count == 0 && _level < 3)
				_mg.SetState ("level" + (_level+1));
		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																	//ROTATING AROUND A POINT
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void rotateAroundPoint(Ship ship, Vec2 center, float angle) {
			double dx = ship.x - center.x;
			double dy = ship.y - center.y;

			double cosAngle = Math.Cos (angle/(5-ship.Speed));
			double sinAngle = Math.Sin (angle/(5-ship.Speed));
			// player1 controls
			if (ship.StunTimer == 0) {
				if (Input.GetKey (Key.RIGHT) && ship.PlayerNum == 1) {
					ship.Flip (false, false);
					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
					ship.UpdateAnimation ();

				} else if (Input.GetKey (Key.LEFT) && ship.PlayerNum == 1) {
					ship.Flip (true, true);

					cosAngle = Math.Cos (-angle / 5);
					sinAngle = Math.Sin (-angle / 5);

					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
					ship.UpdateAnimation ();
				}
				//player2 controls
				if (Input.GetKey (Key.D) && ship.PlayerNum == 2) {
					ship.Flip (false, false);

					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
					ship.UpdateAnimation ();

				} else if (Input.GetKey (Key.A) && ship.PlayerNum == 2) {
					ship.Flip (true, true);

					cosAngle = Math.Cos (-angle / 5);
					sinAngle = Math.Sin (-angle / 5);

					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
					ship.UpdateAnimation ();
				}
			}
			ship.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
		}

		void rotateAsteroids(Vec2 center, float angle = 5 * (float)Math.PI / 2880.0f)
		{

			double cosAngle = Math.Cos (angle/5);
			double sinAngle = Math.Sin (angle/5);
			foreach (Asteroid asteroid in _asteroids) {
				double dx = asteroid.x - center.x;
				double dy = asteroid.y - center.y;

				asteroid.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				asteroid.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
				//asteroid.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
			}
		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																					//PEWPEW
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void processInput(Ship ship)
		{
			if (ship.StunTimer == 0) {
				switch (ship.PlayerNum) {
				case 1:
					if (Input.GetKeyDown (Key.UP)) {
						ship.Fire ();
					}

					 if (Input.GetKeyDown (Key.DOWN) && ship.ShieldTimer == 0) {
						ship.Shield ();
					}
					break;

				case 2:
					if (Input.GetKeyDown (Key.W)) {
						ship.Fire ();
					}

					if (Input.GetKeyDown (Key.S) && ship.ShieldTimer == 0) {
						ship.Shield ();
					}
					break;
				default:
					break;
				}
			}

		}

		public List<Ship> Ships
		{
			get { return this._ships; }
		}

		public HUD Hud
		{
			get { return this._mg.Hud; }
		}

		public List<Projectile> Projectiles
		{
			get { return this._projectiles; }
		}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																			//LEVELS
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void ReadLevel(int level)
		{
			string path = "level" + level + ".txt";
			StreamReader reader = new StreamReader (path);
			string fileData = reader.ReadToEnd ();
			reader.Close ();

			string[] lines = fileData.Split ('\n');

			for (int i = 0; i < HEIGHT; i++) {
				string[] cols = lines [i].Split (',');

				for (int j = 0; j < WIDTH; j++) {
					string col = cols [j];
					_data [j, i] = int.Parse (col);
				}
			}


		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																			//Collisions
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void resolveCollisions()
		{
			for (int i = _powerUps.Count - 1; i >= 0; i--)
			{


			}

			for (int i = _projectiles.Count - 1; i >= 0; i--)
			{
				if (_projectiles [i].Step ()) { // if object destroyed
					continue;
				}

				foreach (Ship ship in _ships) {
					if (_projectiles [i].HitTest (ship) && _projectiles [i].PlayerNum != ship.PlayerNum && _projectiles[i].CatchTimer ==0) {
						ship.LaserTimer = 100;
						ship.StunTimer = 100;
						if (_projectiles [i].PlayerNum == 1)
							_ships [1].addscore (-50);
						else
							_ships [0].addscore (-50);

						_projectiles [i].Destroy ();
						_projectiles.Remove (_projectiles [i]);
						if (i > 0)
							i--;
						break;
					} else if (_projectiles [i].HitTest (ship) && _projectiles [i].PlayerNum == ship.PlayerNum && Projectiles[i].CatchTimer == 0)
					{
						ship.Energy++;
						this.Hud.addEnergy (ship);
						_projectiles [i].CatchTimer = 50;
						_projectiles [i].Destroy ();
						_projectiles.Remove (_projectiles [i]);
						if (i > 0)
							i--;
						break;
					}
				}

				if (_projectiles.Count > 0) {
					for (int y = _asteroids.Count - 1; y >= 0; y--)
					{
						if (_projectiles [i].HitTest (_asteroids[y]) && _projectiles[i].HitTimer == 0) {
							SoundManager.PlaySound (SoundFile.RICOCHET);
							float dx = _asteroids[y].x - _projectiles [i].position.x;
							float dy = _asteroids[y].y - _projectiles [i].position.y;
							Vec2 normal = new Vec2 (dx, dy).Normalize();
							_projectiles [i].velocity.Reflect (normal);
							_projectiles [i].rotation = _projectiles [i].velocity.GetAngleDegrees ();
							_projectiles[i].HitTimer = 5;
							_ships [_projectiles [i].PlayerNum - 1].addscore (10);


							if (_asteroids [y].TakeDamage ()) {
								SoundManager.PlaySound (SoundFile.ASTEROIDBREAK);
								int randomNum = Utils.Random (0, 100);
								PowerUp newPowerUp;
								if (randomNum >= 90) {
									newPowerUp = new PowerUp (PowerUpType.ENERGYUP, this, new Vec2(_asteroids[y].x, _asteroids[y].y));
								} else if (randomNum >= 80) {
									newPowerUp = new PowerUp (PowerUpType.MULTIPLIER, this, new Vec2(_asteroids[y].x, _asteroids[y].y));
								} else if (randomNum >= 70) {
									newPowerUp = new PowerUp (PowerUpType.SPEEDDOWN, this, new Vec2(_asteroids[y].x, _asteroids[y].y));
								} else if (randomNum >= 60) {
									newPowerUp = new PowerUp (PowerUpType.SPEEDUP, this, new Vec2(_asteroids[y].x, _asteroids[y].y));
								} else {
									newPowerUp = new PowerUp (PowerUpType.NULL, this);
								}

								if (newPowerUp.PowerUpType != PowerUpType.NULL) {
									this.AddChild (newPowerUp);
									_powerUps.Add (newPowerUp);
								}
								_asteroids.Remove (_asteroids [y]);
								_ships [_projectiles [i].PlayerNum - 1].addscore (10);

							}
						}
					}


				}

			}
		}
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																	    	//CA
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void addAsteroid (int x, int y, int tile)
		{
			int yplus = 160;
			switch (tile) {

			case 1: 
				Asteroid asteroidfull = new Asteroid (0);
				AddChild (asteroidfull);
				asteroidfull.SetXY (x+580, y+yplus);
				_asteroids.Add (asteroidfull);
				break;

			case 2:
				Asteroid asteroidhalf = new Asteroid (0, 60);
				AddChild (asteroidhalf);
				asteroidhalf.SetXY (x+580, y+yplus);
				_asteroids.Add (asteroidhalf);
				break;

			case 3:
				Asteroid asteroid = new Asteroid (0, 40);
				AddChild (asteroid);
				asteroid.SetXY (x+580, y+yplus);
				_asteroids.Add (asteroid);
				break;
				

			}

		}


	}

}




