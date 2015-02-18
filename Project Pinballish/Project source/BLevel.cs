using System;

namespace GXPEngine
{
	public class BLevel : AnimSprite
	{
		MyGame _mygame;
		int _levelNum;
		private int _firstFrame;
		private int _lastFrame;
		private float _frame;

		public BLevel (MyGame myGame, int levelNum) : base ("SHABALABADINGDONG.png",1,1)
		{
			_levelNum = levelNum;
		
			_mygame =  myGame;

			_mygame.Add (this);

		}

		void Update ()
		{
		
			//something something darkside
			//something something complete
			_mygame.SetState ("level" + _levelNum);
		}

		void UpdateAnimation ()
		{
			_frame = _frame + 0.1f;

			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;

		}
	}
}

