using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject[] notes;
    private float[] _timing;
    private int[] _lineNum;

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _audioSource;
    private float _startTime = 0;

    public float timeOffset = -1;

    private bool isPlaying = false;
    public GameObject startButton;

    public Text scoreText;
    public static int _score = 0;

    void Start()
    {
        _audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        _timing = new float[1024];
        _lineNum = new int[1024];
        LoadCSV();
    }

    void LoadCSV()
    {

        TextAsset csv = Resources.Load(filePass) as TextAsset;
        Debug.Log(csv.text);
        StringReader reader = new StringReader(csv.text);

        int i = 0;
        while (reader.Peek() > -1)
        {

            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for (int j = 0; j < values.Length; j++)
            {
                _timing[i] = float.Parse(values[0]);
                _lineNum[i] = int.Parse(values[1]);
            }
            i++;
        }
    }

    public void StartGame()
    {
        _score = 0;
        startButton.SetActive(false);
        _startTime = Time.time;
        _audioSource.Play();
        isPlaying = true;
        StartCoroutine(CheckMusic(() =>
        {
            Debug.Log("END");
            SceneManager.LoadScene("ResultScene");
        }
        ));
    }

    void CheckNextNotes()
    {
        while(_timing[_notesCount] + timeOffset < GetMusicTime() && _timing[_notesCount] != 0)
        {
            SpawnNotes(_lineNum[_notesCount]);
            _notesCount++;
        }
    }

    void SpawnNotes(int num)
    {
        Instantiate(notes[num],
            new Vector3(-4.0f + (2.0f * num), 8.0f, 0),
            Quaternion.identity);
    }

    float GetMusicTime()
    {
        return Time.time - _startTime;
    }

    private void Update()
    {
        if (isPlaying)
        {
            CheckNextNotes();
            scoreText.text = _score.ToString();
        }
    }

    public void GoodTimingFuc(int num)
    {
        EffectManager.Instance.PlayEffect(num);
        _score++;
    }
    public delegate void FunctionType();
    IEnumerator CheckMusic(FunctionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!_audioSource.isPlaying)
            {
                callback();
                break;
            }
        }

    }
    public static int GetScore()
    {
        return _score;
    }
}

