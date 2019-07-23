using UniRx;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    public class StepSuccesful : MonoBehaviour
    {
        public ReactiveCommand stepComplite = new ReactiveCommand();

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                gameObject.SetActive(false);
                stepComplite.Execute();
            }
        }

        public void SetPosition(Vector3 newPos)
        {
            gameObject.SetActive(true);
            transform.position = newPos;
        }
    }
}
