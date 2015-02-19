using System;

namespace GXPEngine
{
	public class BLevel : AnimSprite
	{
		MyGame _mygame;
		Level _level;
		int _levelNum;
		private int _firstFrame = 0;
		private int _lastFrame = 3;
		private float _frame;
		private int Timer;
		private bool playedSound1;
		private bool playedSound2;
		private bool playedSound3;
		private bool playedSoundGo;


		public BLevel (MyGame myGame, int levelNum, Level level) : base ("CountDown.png",4,1)
		{
			_level = level;
			Timer = 180;

			_levelNum = levelNum;
		
			_mygame =  myGame;

			_mygame.Add (this);

			SetXY (myGame.width / 2, myGame.height / 2);
			SetOrigin (this.width / 2, this.height / 2);

		}

		void Update ()
		{
			Timer--;
			if (currentFrame == 0 && !playedSound3) {
				SoundManager.PlaySound (SoundFile.COUNTDOWN3);
				playedSound3 = true;
			} else if (currentFrame == 1 && !playedSound2) {
				SoundManager.PlaySound (SoundFile.COUNTDOWN2);
				playedSound2 = true;
			} else if (currentFrame == 2 && !playedSound1) {
				SoundManager.PlaySound (SoundFile.COUNTDOWN1);
				playedSound1 = true;
			} else if (currentFrame == 3 && !playedSoundGo) {
				SoundManager.PlaySound (SoundFile.COUNTDOWNGO);
				playedSoundGo = true;
			}
		
			if (Timer == 0) {
				//something something darkside
				//something something complete
				_level.RemoveChild (this);
				this.Destroy ();
			}
			UpdateAnimation ();

		}

		void UpdateAnimation ()
		{
			_frame = _frame + 0.022f;
			this.alpha = 1 - _frame % 1;
			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			this.SetFrame ((int)_frame);

		}
	}
}

