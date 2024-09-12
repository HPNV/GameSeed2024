using UnityEngine;

namespace Plant.States
{
    public class PlantSelectState : PlantState
    {
        public PlantSelectState(global::Plant.Plant plant) : base(plant)
        {
        }

        public override void Update()
        {
            var origin = Plant.transform.position;
            var temp = SingletonGame.Instance.TileProvider.GetCurrTile().transform.position;
            temp.z = origin.z;
            Plant.transform.position = temp;
        }

        public override void OnEnter()
        {
            var sp = Plant.transform.GetComponent<SpriteRenderer>();
            var color = Color.red;
            color.a = 0.5f;
            sp.color = color;

            var collider = Plant.transform.GetComponent<Collider2D>();
        
            collider.enabled = false;
        
            var detector = Plant.transform.Find("Detector");
        
            var rangeScale = Plant.Data.range / 3;
        
            detector.localScale = new Vector3(rangeScale, rangeScale, 1);
        
            var renderer = detector.GetComponent<SpriteRenderer>();
        
            renderer.enabled = true;
        }

        public override void OnExit()
        {
            Plant.transform.GetComponent<SpriteRenderer>().color = Color.white;
        
            Plant.transform.GetComponent<Collider2D>().enabled = true;
        
            var detector = Plant.transform.Find("Detector");
            detector.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
