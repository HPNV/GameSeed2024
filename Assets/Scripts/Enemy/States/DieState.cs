using Manager;
using UnityEngine;

namespace Enemy.States
{
    public class DieState : BaseState
    {
        public DieState(EnemyBehaviour enemy) : base(enemy){} 

        public override void OnEnter()
        {
            var position = Enemy.transform.position;
            SoundFXManager.instance.PlayGameSoundOnce(Resources.Load<AudioClip>("Audio/Enemy/Slime Death"));
            SingletonGame.Instance.ExperienceManager.Spawn(3, new Vector3(position.x, position.y, position.z));
            SingletonGame.Instance.ResourceManager.Spawn(1, new Vector3(position.x, position.y, position.z));
            Object.Destroy(Enemy.gameObject);
        }
    }
}