using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class spawner : MonoBehaviour
{
    public ConnectionHandler counts;
    
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject yellow;

    public List<GameObject> bande;
    public List<Transform> points;
    private Vector3 placetobe;

    private int owow;

    public List<GameObject> goodbande;
    public List<int> rand;
    private int currcount;


    void checkCurcount()
    {
        currcount = counts.sentint;
    }

    // Start is called before the first frame update
    List<int> randonselecter()
    {
        checkCurcount();
        while (rand.Count != currcount)
        {
            owow = Random.Range(0, 36);
            if (rand.Contains(owow))
            {
                Debug.Log("rand already has " + owow);
            }
            else
            {
                rand.Add(owow);
            }
        }
        return rand;
    }

    public void SpawnLine(bool isconnected)
    {
        if (isconnected)
        {
            goodbande.Add(green);
            goodbande.Add(yellow);
            goodbande.Add(blue);
            checkCurcount();
            Debug.Log(currcount);
            for (int i = 0; i < currcount; i++)
            {
                int whichbanda = Random.Range(0, 3);
                Debug.Log("im here");
                placetobe = gameObject.transform.GetChild(randonselecter()[i]).position;
                Debug.Log(gameObject.transform.GetChild(randonselecter()[i]).position);
                bande.Add(Instantiate(goodbande[whichbanda], placetobe, Quaternion.Euler(0f, 90f, 4.188f)));    
                Debug.Log(bande.Count); 
            } 
            rand.Clear();
        }
        else
        {
            int bigrand;
            bigrand = Random.Range(0, 36);
            for (int i = 0; i < 1; i++)
            {
                Debug.Log("one red on the plate");
                placetobe = gameObject.transform.GetChild(bigrand).position;
                Debug.Log(gameObject.transform.GetChild(bigrand).position);
                bande.Add(Instantiate(red, placetobe, Quaternion.Euler(0f, 90f, 4.188f)));    
                Debug.Log(bande.Count); 
            } 
            rand.Clear();
        }
    }

    public void killbande()
    {
        foreach (var banda in bande)
        {
            StartCoroutine(Smallkardobandelo(banda));
        }
        bande.Clear();
        
        IEnumerator Smallkardobandelo(GameObject banda)
        {
            banda.GetComponent<Rigidbody>().isKinematic = true;
            banda.GetComponent<BoxCollider>().enabled = false;
            for (int i = 0; i < 10; i++)
            {
                Vector3 currscale = banda.transform.localScale;
                banda.transform.localScale = banda.transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(banda);
        }
    }
}
