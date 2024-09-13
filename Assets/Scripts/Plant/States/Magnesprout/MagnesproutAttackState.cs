using System.Linq;
using Manager;
using Projectile;
using UnityEngine;

namespace Plant.States.Magnesprout
{
    public class MagnesproutAttackState : PlantAttackState
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        public MagnesproutAttackState(Plant plant) : base(plant){}

        public override void OnEnter()
        {
            Plant.Animator.SetTrigger(AttackTrigger);
        }

        public override void Update()
        {
            var stateInfo = Plant.Animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9f)
            {
                Plant.ChangeState(EPlantState.Idle);
                return;
            }
            
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime > 0.2f)
            {
                Magnetize();
            }
        }


        private void Magnetize()
        {
            var resources = SingletonGame.Instance.ResourceManager
                .ActiveResources
                .Where(r => Vector2.Distance(Plant.transform.position, r.transform.position) <= Plant.Data.range)
                .ToList();
            
            if (resources.Count == 0)
                return;
            

            foreach (var resource in resources)
            {
                resource.transform.position = Vector2.MoveTowards(resource.transform.position, Plant.transform.position, 5 * Time.deltaTime);
            }

            foreach (var resource in resources)
            {
                if (Vector2.Distance(resource.transform.position, Plant.transform.position) <= 0.5f)
                {
                    SingletonGame.Instance.ResourceManager.Pickup(resource);
                }
            }
        }
    }
}