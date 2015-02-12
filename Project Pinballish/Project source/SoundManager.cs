using System;
using System.Timers;
using System.Collections.Generic;

namespace GXPEngine
{
	public delegate void MusicEnd(SoundChannel music, EventArgs e);

	public enum SoundFile
	{
		NULL, PEW1, PEW2, MUSIC1, MUSIC2, MUSIC3, MUSICMENU
	}

	public class SoundManager
	{
		private static SoundChannel _music;
		private static SoundFile _song;
		private static Timer _timer;
		public static event MusicEnd FadeOut;
		public delegate void MusicEnd(SoundFile song);

		private static readonly Dictionary<SoundFile, Sound> SOUND_FILE_DICT
		= new Dictionary<SoundFile, Sound>
		{
			{SoundFile.PEW1 ,  		new Sound(@"Sounds/laser1.wav")},
			{SoundFile.PEW2,  		new Sound(@"Sounds/laser2.wav")},
			{SoundFile.MUSIC1,  	new Sound(@"Sounds/Space odyssey.wav", true, true)},
			{SoundFile.MUSIC2, 	 	new Sound(@"Sounds/Space odyssey 2.wav", true, true)},
			{SoundFile.MUSIC3,  	new Sound(@"Sounds/Space odyssey 3.wav", true, true)},
			{SoundFile.MUSICMENU,  	new Sound(@"Sounds/menu.wav", true, true)}

		};

		public SoundManager ()
		{

		}
			
		public static SoundFile MusicName
		{
			get { return _song; }
		}

		public static float MusicVolume
		{
			get { return _music.Volume; }
		}

		protected static void OnFadeOutComplete()
		{
			if (FadeOut != null) {
				FadeOut (_song);
			}
		}

		public static void StopMusic(bool fadeOut = false)
		{
			if (_music != null && fadeOut == false) {
				_music.Stop ();
				_music = null;
				_song = SoundFile.NULL;
			}
			else if (_music != null && fadeOut && _timer == null) {
				_timer = new Timer (10);
				_timer.Elapsed += OnTimerElapse;
				_timer.Start ();
			}
		}

		private static void OnTimerElapse(Object sender, EventArgs e)
		{
			_music.Volume = _music.Volume * 0.97f;
			if (_music.Volume <= 0.01) {
				OnFadeOutComplete ();
				_music.Stop ();
				_music = null;
				_song = SoundFile.NULL;
				_timer.Stop ();
				_timer = null;
			}
		}

		public static void PlaySound(SoundFile effect, float volumeLevel = 1.0f, float panLevel = 0.0f)
		{
			Sound soundEffect = SOUND_FILE_DICT[effect];
			SoundChannel soundPlayed = soundEffect.Play ();
			soundPlayed.Volume = volumeLevel;
			soundPlayed.Pan = panLevel;
		}

		public static void PlayMusic(SoundFile song)
		{
			if (_timer == null) {
				StopMusic ();
				Sound _currentBgMusic = SOUND_FILE_DICT [song];
				_song = song;
				_music = _currentBgMusic.Play ();
			}
		}

	}
}

