using System.Linq;
using Experience;
using UnityEngine;

namespace Plant.States.Boomkin
{
    public class BoomkinDieState : PlantDieState
    {
        private static readonly int DieTrigger = Animator.StringToHash("Die");
        private bool _hasDamaged;
        public BoomkinDieState(Plant plant) : base(plant) {}

        public override void OnEnter()
        {
            //base.OnEnter();
            
            Plant.Animator.SetTrigger(DieTrigger);
            _hasDamaged = false;
        }
        
        public override void Update()
        {
            base.Update();
            
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            Plant.transform.localScale = Vector3.Lerp(Plant.transform.localScale, Vector3.one * 6, Time.deltaTime);
            
            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 0.8)
                Damage();
        }
        
        private void Damage()
        {
            if (_hasDamaged)
                return;
            
            _hasDamaged = true;

            var targets = Plant.TargetService.GetTargets()
                .Where(e => e is not null && 
                        Vector2.Distance(e.transform.position, Plant.transform.position) < Plant.Data.range)
                .ToList();
            
            foreach (var target in targets)
                target.Damage(Plant.Data.damage);
        }
    }
}