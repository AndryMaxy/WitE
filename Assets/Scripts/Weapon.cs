using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public abstract class Weapon : MonoBehaviour
{
    public Text text;

    public int count = 0;

    private AudioSource sound;

    protected virtual void Start()
    {
        sound = GetComponent<AudioSource>();
        UpdateView();
    }

    protected virtual void Update()
    {

    }

    public void Add(int count)
    {
        this.count += count;
        UpdateView();
    }

    public void Use()
    {
        if(isUseable())
        {
            sound.Play();
            Apply();
            --count;
            UpdateView();
        }
 
    }
    protected abstract void Apply();

    protected virtual bool isUseable()
    {
        return count > 0;
    }

    protected virtual void UpdateView()
    {
        text.text = count.ToString();
    }
}
