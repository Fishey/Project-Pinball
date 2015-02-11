﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class MyGame : Game
	{ 
		//pink fluffy unicorns
		static void Main() {
			new MyGame ().Start ();
		}
		private string _state = "";
		private List<Ship> _ships;
		private List<Projectile> _projectiles;
		private List<Asteroid> _asteroids;
		private Ship _ship = null;
		private Ship _ship2 = null;
		private SoundChannel _pewpew;
		private SoundChannel _pewpew2;
		private Vec2 _center = null;
		private Titlescreen _titleScreen;

		public MyGame ():base (1280,720, false) {

			AddChild(new Sprite("background.png")); // add a beautiful background
			_ships = new List<Ship> (); // create the list for ships (Player 1 & 2 go here)
			_projectiles = new List<Projectile> (); // pew pews go here
			_asteroids = new List<Asteroid> (); // things to pew pew at go here
			//Create ships
			_ship = new Ship(18, 1)	;
			_ship.position.x = width/2 + width/4;
			_ship.position.y = height/2 - 50;
			_ships.Add (_ship);

			_ship2 = new Ship (18, 2, Vec2.zero, Vec2.zero, Color.Red);
			_ship2.position.x = width / 2 - width / 4;
			_ship2.position.y = height / 2 - 50;
			_ships.Add (_ship2); //uncomment this to add a second ship

			Asteroid asteroid1 = new Asteroid ();
			asteroid1.SetXY (width / 2, height / 2);
			asteroid1.y = asteroid1.y + 50;
			_asteroids.Add (asteroid1);

			Asteroid asteroid2 = new Asteroid ();
			asteroid2.SetXY (width / 2, height / 2);
			asteroid2.y = asteroid2.y - 50;
			asteroid2.x = asteroid2.x - 50;
			_asteroids.Add (asteroid2);

			foreach(Ship ship in _ships)
				AddChild (ship); // add the ships to the game

			foreach (Asteroid asteroid in _asteroids)
				AddChild (asteroid);

			_center = new Vec2 (width / 2, height / 2);
		}

		void Update() {
			foreach (Ship ship in _ships) {
				rotateAroundPoint (ship, _center, (float)(5 * Math.PI / 180.0f)); // make the ships turn around the center of the screen
				processInput (ship);
				ship.Step ();
			}
			foreach (Projectile bullet in _projectiles) {
				bullet.Step ();
				foreach(Asteroid asteroid in _asteroids){
					if (bullet.HitTest (asteroid)) {
						asteroid.Destroy ();
						asteroid.SetXY (-1500, -1500);
						float dx = asteroid.x - bullet.position.x;
						float dy = asteroid.y - bullet.position.y;
						Vec2 normal = new Vec2 (dx, dy).Normalize ();
						bullet.velocity.Reflect (normal);
						bullet.rotation = bullet.velocity.GetAngleDegrees ();
					}
						}
			}
		}

		void processInput(Ship ship)
		{
			switch (ship.PlayerNum) {
			case 1:
				if (Input.GetKeyDown (Key.UP)) {
					Console.WriteLine ("pew pew!");
					Projectile bullet = new Projectile (10);
					bullet.position = ship.position.Clone();
					bullet.velocity = new Vec2 (10, 0);
					bullet.velocity.SetAngleDegrees (ship.rotation);
					bullet.rotation = ship.rotation;
					_projectiles.Add (bullet);
					AddChild (bullet);
					if (_pewpew != null) {
						_pewpew.Stop ();
					}
					Sound pewpew = new Sound ("laser1.wav");
					_pewpew = pewpew.Play ();
					
				}
				break;
			case 2:
				if (Input.GetKeyDown (Key.W)) {
					Console.WriteLine ("pew pew!");
					Projectile bullet = new Projectile (10);
					bullet.position = ship.position.Clone();
					bullet.velocity = new Vec2 (10, 0);
					bullet.velocity.SetAngleDegrees (ship.rotation);
					bullet.rotation = ship.rotation;
					_projectiles.Add (bullet);
					AddChild (bullet);
					if (_pewpew2 != null) {
						_pewpew2.Stop ();
					}
					Sound pewpew = new Sound ("laser2.wav");
					_pewpew2 = pewpew.Play ();
				}
				break;
			default:
				break;
			}


		}

		void rotateAroundPoint(Ship sprite, Vec2 center, float angle) {
			double dx = sprite.x - center.x;
			double dy = sprite.y - center.y;

			double cosAngle = Math.Cos (angle/5);
			double sinAngle = Math.Sin (angle/5);
			// player1 controls
			if (Input.GetKey(Key.RIGHT) && sprite.PlayerNum == 1)
			{
				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			else if (Input.GetKey(Key.LEFT) && sprite.PlayerNum == 1)
			{
				cosAngle = Math.Cos (-angle / 5);
				sinAngle = Math.Sin (-angle / 5);

				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			//player2 controls
			if (Input.GetKey(Key.D) && sprite.PlayerNum == 2)
			{
				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}
			else if (Input.GetKey(Key.A) && sprite.PlayerNum == 2)
			{
				cosAngle = Math.Cos (-angle / 5);
				sinAngle = Math.Sin (-angle / 5);

				sprite.position.x = (float)(center.x + dx * cosAngle - dy * sinAngle);
				sprite.position.y = (float)(center.y + dx * sinAngle + dy * cosAngle);
			}

			sprite.rotation = (float)Math.Atan2 (dy, dx) * 180 / (float)Math.PI - 180;
		}

			public void SetState (string state) // Setting which state the game should start on 
				{
					if (state == _state)
						return;
					stopState ();
					_state = state;
					startState ();
				}


		void startState ()
		{
			switch (_state) {
			case "titleScreen":
				_titleScreen = new Titlescreen (this);
				AddChild (_titleScreen);
				break;
			}	
		}

		void stopState ()
		{
			switch (_state) {
			case "titleScreen":
					_titleScreen.Destroy();
					break;
			}
		}
	}
}

