﻿using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Level : GameObject
	{
		private const int HEIGHT 	= 10;
		private const int WIDTH		= 10;
		private const int TILESIZE 	= 64;

		private List<Ship> _ships;
		private List<Projectile> _projectiles;
		private List<Asteroid> _asteroids;
		private Ship _ship = null;
		private Ship _ship2 = null;
		private Vec2 _center = null;
		private Titlescreen _titleScreen;
		private int[,] _data = new int[WIDTH,HEIGHT];

		MyGame _mg;

		public Level (MyGame MG, int level = 1)
		{
			_mg = MG;

			AddChild(new Sprite("background.png")); // add a beautiful background
			_ships = new List<Ship> (); // create the list for ships (Player 1 & 2 go here)
			_projectiles = new List<Projectile> (); // pew pews go here
			_asteroids = new List<Asteroid> (); // things to pew pew at go here
			//Create ships
			_ship = new Ship("BlueShark.png", 1, MG, this)	;
			_ship.position.x = _mg.width/2 + _mg.width/4;
			_ship.position.y = _mg.height/2 - 50;
			_ships.Add (_ship);

			_ship2 = new Ship ("RedShark.png", 2, MG, this, Vec2.zero, Vec2.zero);
			_ship2.position.x = _mg.width / 2 - _mg.width / 4;
			_ship2.position.y = _mg.height / 2 - 50;
			_ships.Add (_ship2); //uncomment this to add a second ship

			Asteroid asteroid1 = new Asteroid (0);
			asteroid1.SetXY (_mg.width / 2, _mg.height / 2);
			asteroid1.y = asteroid1.y + 50;
			_asteroids.Add (asteroid1);

			Asteroid asteroid2 = new Asteroid (0);
			asteroid2.SetXY (_mg.width / 2, _mg.height / 2);
			asteroid2.y = asteroid2.y - 50;
			asteroid2.x = asteroid2.x - 50;
			_asteroids.Add (asteroid2);

			foreach(Ship ship in _ships)
				AddChild (ship); // add the ships to the game

			foreach (Asteroid asteroid in _asteroids)
				AddChild (asteroid);

			_center = new Vec2 (_mg.width / 2, _mg.height / 2);
		}
		void Update() {
			Console.WriteLine (_ships [1].position);
			foreach (Ship ship in _ships) {
				rotateAroundPoint (ship, _center, (float)(5 * Math.PI / 180.0f)); // make the ships turn around the center of the screen
				processInput (ship);
				ship.Step ();
			}

			for (int i = _projectiles.Count - 1; i >= 0; i--)
			{
				if (_projectiles [i].Step ()) { // if object destroyed
					continue;
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
					foreach (Asteroid asteroid in _asteroids) {
						if (_projectiles [i].HitTest (asteroid)) {
							SoundManager.PlaySound (SoundFile.ASTEROIDBREAK);
							SoundManager.PlaySound (SoundFile.RICOCHET);
							asteroid.Destroy ();
							asteroid.SetXY (-1500, -1500);
							float dx = asteroid.x - _projectiles [i].position.x;
							float dy = asteroid.y - _projectiles [i].position.y;
							Vec2 normal = new Vec2 (dx, dy).Normalize ();
							_projectiles [i].velocity.Reflect (normal);
							_projectiles [i].rotation = _projectiles [i].velocity.GetAngleDegrees ();
						}
					}

					foreach (Ship ship in _ships) {
						if (_projectiles [i].HitTest (ship) && _projectiles[i].PlayerNum != ship.PlayerNum) {
							ship.LaserTimer = 100;
							ship.StunTimer = 100;
						}
					}
				}
			}
		}

		void rotateAroundPoint(Ship ship, Vec2 center, float angle) {
			if (ship.StunTimer == 0) {
				double dx = ship.x - center.x;
				double dy = ship.y - center.y;

				double cosAngle = Math.Cos (angle / 5);
				double sinAngle = Math.Sin (angle / 5);
				// player1 controls
				if (Input.GetKey (Key.RIGHT) && ship.PlayerNum == 1) {
					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
				} else if (Input.GetKey (Key.LEFT) && ship.PlayerNum == 1) {
					cosAngle = Math.Cos (-angle / 5);
					sinAngle = Math.Sin (-angle / 5);

					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
				}
				//player2 controls
				if (Input.GetKey (Key.D) && ship.PlayerNum == 2) {
					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
				} else if (Input.GetKey (Key.A) && ship.PlayerNum == 2) {
					cosAngle = Math.Cos (-angle / 5);
					sinAngle = Math.Sin (-angle / 5);

					ship.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
					ship.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
				}
				ship.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
			}
		}

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

		public List<Projectile> Projectiles
		{
			get { return this._projectiles; }
		}

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
	}
}

