using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : SingletonMonoBehaviour<TouchEffect>
{
    private GameObject[] _touchEffects;

    // Start is called before the first frame update
    void Start()
    {
        _touchEffects = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            _touchEffects[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 5; i++)
        {
            CheckInput(GameUtil.GetKeyCodeByLineNum(i), i); 
        }
    }

    void CheckInput(KeyCode key,int num)
    {
        if (Input.GetKeyDown(key))
        {
            PlayEffect(num);
        }
    }

    public void PlayEffect(int num)
    {
        StartCoroutine(ClickEffect(num));
    }

    IEnumerator ClickEffect(int num)
    {
        if (_touchEffects[num].activeInHierarchy)
            yield break;

        _touchEffects[num].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _touchEffects[num].SetActive(false);
    }
}
