﻿using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace GXPEngine
{
	public enum ShipSprites
	{
		NULL, REDSHARK, BLUESHARK, REDSHIP, BLUESHIP
	}

	public class Level : GameObject
	{
		private const int HEIGHT 	= 10;
		private const int WIDTH		= 10;
		private const int TILESIZE 	= 80;

		private List<Ship> _ships;
		private List<Projectile> _projectiles;
		private List<Asteroid> _asteroids;
		private Ship _ship = null;
		private Ship _ship2 = null;
		private Vec2 _center = null;
		private int[,] _data = new int[WIDTH,HEIGHT];
		private HUD _hud;
		Scoreboard _scoreboard;
		Scoreboard _scoreboard2;

		MyGame _mg;

		public Level (MyGame MG, int level = 1)
		{
			_mg = MG;

			AddChild(new Sprite("background.png")); // add a beautiful background



			_ships = new List<Ship> (); // create the list for ships (Player 1 & 2 go here)
			_projectiles = new List<Projectile> (); // pew pews go here
			_asteroids = new List<Asteroid> (); // things to pew pew at go here
			//Create ships
			_ship = new Ship(ShipSprites.BLUESHARK, 1, MG, this)	;
			_ship.position.x = _mg.width/2 + _mg.width/4;
			_ship.position.y = _mg.height/2 - 50;
			_ships.Add (_ship);

			_ship2 = new Ship (ShipSprites.REDSHARK, 2, MG, this, Vec2.zero, Vec2.zero);
			_ship2.position.x = _mg.width / 2 - _mg.width / 4;
			_ship2.position.y = _mg.height / 2 - 50;
			_ships.Add (_ship2);

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
				
			_hud = new HUD (_mg, this, _ships);
			this.AddChild (_hud);
			_center = new Vec2 (_mg.width / 2, _mg.height / 2);


			_scoreboard = new Scoreboard (new PointF (-45,100));
			this.AddChild (_scoreboard);


			_scoreboard2 = new Scoreboard (new PointF (1690,100));
			this.AddChild (_scoreboard2);
		


		}

		public void Scoreboard()
		{

			_scoreboard.DrawScore (_ship._score);
			_scoreboard2.DrawScore (_ship2._score);
		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																//ROTATION AND HIT TEST
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void Update() {
			foreach (Ship ship in _ships) {
				rotateAroundPoint (ship, _center, (float)(5 * Math.PI / 180.0f)); // make the ships turn around the center of the screen
				processInput (ship);
				ship.Step ();
			}


			for (int i = _projectiles.Count - 1; i >= 0; i--)
			{
				bool projectilebroken;
				if (_projectiles [i].Step ()) { // if object destroyed
					continue;
				}

				foreach (Ship ship in _ships) {
					if (_projectiles [i].HitTest (ship) && _projectiles [i].PlayerNum != ship.PlayerNum) {
						ship.LaserTimer = 100;
						ship.StunTimer = 100;
						_ships [_projectiles [i].PlayerNum].addscore (-10);
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
/*
				else {
					for (int y = _projectiles.Count - 1; y >= 0; y--)
					{
						if (_projectiles [i].HitTest (_projectiles[y])) {
							if (_projectiles[y] != _projectiles[i])
							{
								_projectiles[y].Destroy ();
								_projectiles.Remove (_projectiles [y]);
								_projectiles [i].Destroy ();
								_projectiles.Remove (_projectiles [i]);
								break;
							}
						}
					}
				}
*/ // Derpy projectile collision
					if (_projectiles.Count > 0) {
					for (int y = _asteroids.Count - 1; y >= 0; y--)
					{
						if (_projectiles [i].HitTest (_asteroids[y]) && _projectiles[i].HitTimer == 0) {
							SoundManager.PlaySound (SoundFile.RICOCHET);
							float dx = _asteroids[y].x - _projectiles [i].position.x;
							float dy = _asteroids[y].y - _projectiles [i].position.y;
							Vec2 normal = new Vec2 (dx, dy).Normalize ();
							_projectiles [i].velocity.Reflect (normal);
							_projectiles [i].rotation = _projectiles [i].velocity.GetAngleDegrees ();
							_projectiles[i].HitTimer = 5;
							_ships [_projectiles [i].PlayerNum - 1].addscore (10);


							if (_asteroids [y].TakeDamage ()) {
								SoundManager.PlaySound (SoundFile.ASTEROIDBREAK);
								_asteroids.Remove (_asteroids [y]);
								_ships [_projectiles [i].PlayerNum - 1].addscore (10);
							}
						}
					}


				}

				if (_asteroids.Count == 0)
					_mg.SetState ("level2");
			}
			Scoreboard ();

		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																	//ROTATING AROUND A POINT
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void rotateAroundPoint(Ship ship, Vec2 center, float angle) {
			double dx = ship.x - center.x;
			double dy = ship.y - center.y;

			double cosAngle = Math.Cos (angle/5);
			double sinAngle = Math.Sin (angle/5);
			// player1 controls
			if (Input.GetKey(Key.RIGHT) && ship.PlayerNum == 1)
			{
				ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			else if (Input.GetKey(Key.LEFT) && ship.PlayerNum == 1)
			{
				cosAngle = Math.Cos (-angle / 5);
				sinAngle = Math.Sin (-angle / 5);

				ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			//player2 controls
			if (Input.GetKey(Key.D) && ship.PlayerNum == 2)
			{
				ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			else if (Input.GetKey(Key.A) && ship.PlayerNum == 2)
			{
				cosAngle = Math.Cos (-angle / 5);
				sinAngle = Math.Sin (-angle / 5);

				ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			ship.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
		}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
																					//PEWPEW
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void processInput(Ship ship)
		{
			if (ship.StunTimer == 0) {
				switch (ship.PlayerNum) {
				case 1:
					if (Input.GetKeyDown (Key.UP) && ship.StunTimer == 0) {
						ship.Fire ();
					}
					break;
				case 2:
					if (Input.GetKeyDown (Key.W)) {
						ship.Fire ();
					}
					break;
				default:
					break;
				}
			}

		}


		public HUD Hud
		{
			get { return this._hud; }
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
																	    	//CA
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void addAsteroid (int x, int y, int tile)
		{

			switch (tile) {

			case 1: 
				Asteroid asteroidfull = new Asteroid (2);
				AddChild (asteroidfull);
				asteroidfull.SetXY (x+580, y+140);
				_asteroids.Add (asteroidfull);
				break;

			case 2:
				Asteroid asteroidhalf = new Asteroid (1);
				AddChild (asteroidhalf);
				asteroidhalf.SetXY (x+580, y+140);
				_asteroids.Add (asteroidhalf);
				break;

			case 3:
				Asteroid asteroid = new Asteroid (0);
				AddChild (asteroid);
				asteroid.SetXY (x+580, y+140);
				_asteroids.Add (asteroid);
				break;
				

			}

		}


	}

}




