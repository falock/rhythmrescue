using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class NoteReader : MonoBehaviour
	{
		[SerializeField] private TextAsset _notesFile;
		[SerializeField] private Transform[] _laneStarts;
		[SerializeField] private GameObject[] _notePrefabs;
		[SerializeField] private Transform _beatScroller;
		private string[] _notesLines;
		private int currentBeat = 0;
		private float _bpm;
		private float _beatDT;
		private float _timer = 0;
		private int _skipBeats;

		public float GetBPM()
		{
			return _bpm;
		}

		void Start()
		{
			// remove carage returns for windows files
			// then split up file so each line is a different array element
			_notesLines = _notesFile.text.Replace("\r", "").Split('\n');

			// get bpm from top of file
			if (!float.TryParse(_notesLines[0], out _bpm))
			{
				// display error if bpm not read correctly
				Debug.LogError("BPM not at top of file");
			}
			if (_bpm <= 0)
			{
				Debug.LogError("BPM cannot be <= 0");
			}
			// get time between each beat
			_beatDT = 60f / _bpm;

			/*
			//new additons
			previousFrameTime = getTimer();
			lastReportedPlayheadPosition = 0;
			*/
		}

		void Update()
		{

			// new additions
			/* songTime += getTimer() - previousFrameTime;
			previousFrameTime = getTimer();
			if (mySong.position != lastReportedPlayheadPosition)
			{
				songTime = (songTime + mySong.position) / 2;
				lastReportedPlayheadPosition = mySong.position;
			}
			*/
			//end of new additions

			// add time since previous frame to timer
			_timer += Time.deltaTime;
			if (_timer >= _beatDT)
			{
				_timer -= _beatDT;

				if (_skipBeats > 0)
				{
					_skipBeats--;
				}

				else
				{
					currentBeat++;
					// check for the end of song

					if (currentBeat >= _notesLines.Length)
					{
						// song ended 
					}

					else
					{
						// place notes
						string notesInfo = _notesLines[currentBeat];

						if (int.TryParse(notesInfo, out _skipBeats))
						{
							_skipBeats--;
						}

						else
						{
							// loop through all characters in line (each note in a lane)
							for (int i = 0; i < notesInfo.Length; i++)
							{
								// check if there is a note for that lane.
								switch (notesInfo[i])
								{
									case ' ':
										break;
									default:
										Instantiate(_notePrefabs[i], _laneStarts[i].position, Quaternion.identity, _beatScroller);
										break;
								}
							}
						}
					}
				}
			}
		}
	}
}