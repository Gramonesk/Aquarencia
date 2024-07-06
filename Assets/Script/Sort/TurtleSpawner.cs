using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TurtleSpawner : MonoBehaviour
{
    [Header("Main Config")]
    public RectTransform spawnArea;
    public GameObject TurtlePrefab;
    public List<Sprite> turtlesprites;

    [SerializeField] private float maxInterpolationtime;
    [SerializeField] private float SlowestInterval, FastestInterval;

    private float elapsed, interval;
    private Vector2 center, size;
    private ObjectPool<TurtleEntity> PooledObject;
    private void Awake()
    {
        elapsed = 0;
        center = (Vector2)Camera.main.ScreenToWorldPoint(spawnArea.position);
        Debug.Log(center);
        size = ((Vector2)Camera.main.ScreenToWorldPoint(((Vector2)spawnArea.position) + spawnArea.rect.size / 2) - center);
        size = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));

        Debug.Log(size);
        PooledObject = new ObjectPool<TurtleEntity>(createTurtle, getTurtle, returnTurtle, destroyTurtle, true, 150, 10_000);
    }
    private TurtleEntity createTurtle()
    {
        GameObject instance = Instantiate(TurtlePrefab, Vector2.zero, Quaternion.identity);
        TurtleEntity turtle = instance.GetComponent<TurtleEntity>();
        turtle.OnRelease = PooledObject.Release;
        return turtle;
    }
    private void getTurtle(TurtleEntity instance)
    {
        instance.SM.MoveToState(TurtleState.egg);
        instance.gameObject.SetActive(true);
        var x = Random.Range(0, turtlesprites.Count);
        instance.defaultSprite = turtlesprites[x];
        instance.transform.position = center + new Vector2(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y));
    }
    public void returnTurtle(TurtleEntity instance)
    {
        instance.gameObject.SetActive(false);
    }
    private void destroyTurtle(TurtleEntity instance)
    {
        Destroy(instance);
    }
    public void Update()
    {
        elapsed += Time.deltaTime;
        interval = Mathf.Lerp(SlowestInterval, FastestInterval, 1 - Mathf.Clamp01((ScoreSort.instance.time - Time.time) / maxInterpolationtime));
        if (interval < elapsed)
        {
            elapsed = 0;
            PooledObject.Get();
        }
    }
}
