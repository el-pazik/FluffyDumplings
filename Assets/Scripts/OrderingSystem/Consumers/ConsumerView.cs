using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerView : MonoBehaviour
{
    [SerializeField] private GameObject consumerPref;
    [SerializeField] private Transform consumersSpawnPoint;
    [SerializeField] private TaskPoint taskPoint;
    private float queueDistance = 2.5f;

    public void ShowConsumer(Consumer consumer)
    {
        var consumersQueue = GetComponent<ConsumerController>().ConsumersQueue;
        ConsumerOnScene newConsumer = Instantiate(consumerPref, consumersSpawnPoint.position, consumersSpawnPoint.rotation, consumersSpawnPoint).GetComponent<ConsumerOnScene>();
        consumersQueue.Add(newConsumer);
        newConsumer.Consumer = consumer;
        newConsumer.DestinationPoint = new Vector2(taskPoint.transform.position.x + queueDistance * (consumersQueue.Count - 1), taskPoint.transform.position.y);
        if(consumersQueue.Count == 1)
        {
            newConsumer.OnConsumerInDestinationPoint.AddListener(AssignTask);
            newConsumer.OnConsumerGaneOrder.AddListener(SubmitTask);
        }
    }

    public void AssignTask()
    {
        taskPoint.ConsumerInPoint = GetComponent<ConsumerController>().ConsumersQueue[0];
    }

    public void SubmitTask()
    {
        var consumersQueue = GetComponent<ConsumerController>().ConsumersQueue;
        consumersQueue[0].OnConsumerGaneOrder.RemoveAllListeners();
        consumersQueue[0].OnConsumerInDestinationPoint.RemoveAllListeners();
        consumersQueue[0].OnConsumerInDestinationPoint.AddListener(consumersQueue[0].DestroyObject);
        consumersQueue[0].DestinationPoint = consumersSpawnPoint.position;
        consumersQueue.RemoveAt(0);
        if(consumersQueue.Count > 0)
        {
            consumersQueue[0].OnConsumerInDestinationPoint.AddListener(AssignTask);
            consumersQueue[0].OnConsumerGaneOrder.AddListener(SubmitTask);
            for (int i = 0; i < consumersQueue.Count; i++)
            {
                consumersQueue[i].DestinationPoint = new Vector2(taskPoint.transform.position.x + queueDistance * (consumersQueue.Count - 1), taskPoint.transform.position.y);
            }
        }
    }
}
