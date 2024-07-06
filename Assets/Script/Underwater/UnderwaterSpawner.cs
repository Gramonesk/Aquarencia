using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnderwaterSpawner : MonoBehaviour
{
    //[Serializable]
    //public struct TurtleVariant
    //{
    //    public float chance;
    //    public List<Sprite> sprites;
    //    public List<AnimationClip> animations;
    //    public List<TurtPriceData> TurtleScores;
    //}

    //[Header("Turtle Variants\nSet Last Variant as 100%")]
    //public List<TurtleVariant> Variants;
    [Serializable]
    public struct Prefabs
    {
        public GameObject TurtlePrefab;
        public float chance;
    }
    [Header("Spawner Settings")]
    public RectTransform spawnArea;
    public List<Prefabs> TurtlePrefabs;
    public float MinTime, MaxTime;
    //public ObjectPool<TurtleBehaviour> turtles;

    private Vector2 center, size;
    private float time = 0;
    private void Awake()
    {
        center = (Vector2)Camera.main.ScreenToWorldPoint(spawnArea.position);
        size = ((Vector2)Camera.main.ScreenToWorldPoint(((Vector2)spawnArea.position) + spawnArea.rect.size / 2) - center);
        size = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
        //turtles = new ObjectPool<TurtleBehaviour>(createTurtle, getTurtle, returnTurtle, destroyTurtle, true, 150, 10_000);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            time = UnityEngine.Random.Range(MinTime, MaxTime);
            int rand = UnityEngine.Random.Range(0, 100);
            int amount;
            if (rand > 80) amount = 3;
            else if (rand > 50) amount = 2;
            else amount = 1;
            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int variant = UnityEngine.Random.Range(0, TurtlePrefabs.Count);
                    int chances = UnityEngine.Random.Range(0, 101);
                    if (TurtlePrefabs[variant].chance >= chances)
                    {
                        var a = Instantiate(TurtlePrefabs[variant].TurtlePrefab);
                        a.transform.position = center + new Vector2(UnityEngine.Random.Range(-size.x, size.x), UnityEngine.Random.Range(-size.y, size.y));
                        break;
                    }
                }
            }
        }
    }
    //private TurtleBehaviour createTurtle()
    //{
    //    GameObject instance = Instantiate(TurtlePrefab, Vector2.zero, Quaternion.identity);
    //    TurtleBehaviour turtle = instance.GetComponent<TurtleBehaviour>();
    //    turtle.OnRelease = turtles.Release;
    //    return turtle;
    //}
    //private void getTurtle(TurtleBehaviour instance)
    //{
    //    instance.SM.setstate(UnderwaterTurtle.swimming);
    //    foreach(var variant in Variants)
    //    {
    //        int chances = UnityEngine.Random.Range(0, 101);
    //        Debug.Log(chances);
    //        if(variant.chance >= chances)
    //        {
    //            int random_ind = UnityEngine.Random.Range(0, variant.sprites.Count);
    //            instance.gameObject.GetComponent<SpriteRenderer>().sprite = variant.sprites[random_ind];
    //            var price = instance.GetComponent<TurtlePrice>();
    //            price.scores = variant.TurtleScores[random_ind].Turtlescores;
    //            price.Set();
    //            break;
    //        }
    //    }
    //    //var state = controller.layers[0].stateMachine.defaultState;
    //    //controller.SetStateEffectiveMotion(state, animations[random_ind]);

    //    instance.gameObject.SetActive(true);
    //    instance.transform.position = center + new Vector2(UnityEngine.Random.Range(-size.x, size.x), UnityEngine.Random.Range(-size.y, size.y));
    //}
    //public void returnTurtle(TurtleBehaviour instance)
    //{
    //    instance.gameObject.SetActive(false);
    //}
    //private void destroyTurtle(TurtleBehaviour instance)
    //{
    //    Destroy(instance);
    //}
}
