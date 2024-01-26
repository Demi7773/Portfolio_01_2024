using System.Collections.Generic;
using UnityEngine;

public class ObjectPool :MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] List<GameObject> BCannonPool = new List<GameObject>();
    [SerializeField] List<GameObject> LRCannonPool = new List<GameObject>();
    [SerializeField] List<GameObject> SCannonPool = new List<GameObject>();
    [SerializeField] List<GameObject> GCannonPool = new List<GameObject>();
    [SerializeField] List<GameObject> UCanonPool = new List<GameObject>();
    [SerializeField] List<GameObject> LCanonPool = new List<GameObject>();

    [SerializeField] List<GameObject> smoke = new List<GameObject>();

    [SerializeField] int poolCount;

    [SerializeField] GameObject BalancedCannonballPrefab;
    [SerializeField] GameObject LongRangeCannonballPrefab;
    [SerializeField] GameObject SpeedCannonballPrefab;
    [SerializeField] GameObject GlassCannonballPrefab;
    [SerializeField] GameObject UnstableCannonballPrefab;
    [SerializeField] GameObject VolleyCannonballPrefab;

    [SerializeField] GameObject poolSmoke;

    [SerializeField] Transform cannonBallsParent;
    [SerializeField] Transform smokeParent;

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitalizePool(BCannonPool, BalancedCannonballPrefab);
        InitalizePool(LRCannonPool, LongRangeCannonballPrefab);
        InitalizePool(SCannonPool, SpeedCannonballPrefab);
        InitalizePool(GCannonPool, GlassCannonballPrefab);
        InitalizePool(UCanonPool, UnstableCannonballPrefab);
        InitalizePool(LCanonPool, VolleyCannonballPrefab);

        InitializeSmokePool();
    }

    private void InitializeSmokePool()
    {
        smoke = new List<GameObject>();
        GameObject temporarySmoke;
        for (int i = 0; i < poolCount; i++)
        {
            temporarySmoke = Instantiate(poolSmoke, smokeParent);
            temporarySmoke.SetActive(false);
            smoke.Add(temporarySmoke);
        }
    }

    private void InitalizePool(List<GameObject> cannonballPool,GameObject cannonball)
    {

        GameObject temporaryCannonBall;
        for (int i = 0; i < poolCount; i++)
        {
            temporaryCannonBall = Instantiate(cannonball, cannonBallsParent);
            temporaryCannonBall.SetActive(false);
            cannonballPool.Add(temporaryCannonBall);
        }
    }



    public GameObject FetchBalancedPool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!BCannonPool[i].activeInHierarchy)
            {
                BCannonPool[i].transform.position = position;
                BCannonPool[i].SetActive(true);
                return BCannonPool[i];
            }
        }
        return null;
    }
    public GameObject FetchLongRAngePool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!LRCannonPool[i].activeInHierarchy)
            {
                LRCannonPool[i].transform.position = position;
                LRCannonPool[i].SetActive(true);
                return LRCannonPool[i];
            }
        }
        return null;
    }
    public GameObject FetchSpeedPool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!SCannonPool[i].activeInHierarchy)
            {
                SCannonPool[i].transform.position = position;
                SCannonPool[i].SetActive(true);
                return SCannonPool[i];
            }
        }
        return null;
    }
    public GameObject FetchGlassPool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!GCannonPool[i].activeInHierarchy)
            {
                GCannonPool[i].transform.position = position;
                GCannonPool[i].SetActive(true);
                return GCannonPool[i];
            }
        }
        return null;
    }
    public GameObject FetchUnstablePool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!UCanonPool[i].activeInHierarchy)
            {
                UCanonPool[i].transform.position = position;
                UCanonPool[i].SetActive(true);
                return UCanonPool[i];
            }
        }
        return null;
    }    
    
    public GameObject FetchVolleyPool(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!LCanonPool[i].activeInHierarchy)
            {
                LCanonPool[i].transform.position = position;
                LCanonPool[i].SetActive(true);
                return LCanonPool[i];
            }
        }
        return null;
    }


    public GameObject FetchPooledSmoke(Vector3 position)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!smoke[i].activeInHierarchy)
            {
                smoke[i].transform.position = position;
                smoke[i].SetActive(true);
                return smoke[i];
            }
        }
        return null;
    }
}
