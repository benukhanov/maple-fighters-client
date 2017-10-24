﻿using System.Collections.Generic;
using System.Linq;
using CommonTools.Log;
using Scripts.Gameplay;
using UnityEngine;

namespace InterestManagement.Scripts
{
    public class Region : MonoBehaviour, IRegion
    {
        public int Id { get; set; }
        public Rectangle PublisherArea { get; set; }

        private readonly Dictionary<int, ISceneObject> sceneObjects = new Dictionary<int, ISceneObject>();

        private void Update()
        {
            if (sceneObjects.Count > 0)
            {
                LogUtils.Log($"Name: {name} Entities: {sceneObjects.Count}");
            }
        }

        public void AddSubscription(ISceneObject sceneObject)
        {
            if (sceneObjects.ContainsKey(sceneObject.Id))
            {
                LogUtils.Log(MessageBuilder.Trace($"A scene object with id #{sceneObject.Id} already exists in a region."), LogMessageType.Error);
                return;
            }

            sceneObjects.Add(sceneObject.Id, sceneObject);

            AddSubscribersForSubscriber(sceneObject);
            AddSubscriberForSubscribers(sceneObject);

            LogUtils.Log(MessageBuilder.Trace($"Added subscription id #{sceneObject.Id} to Region id #{Id}"), LogMessageType.Warning);
        }

        public void RemoveSubscription(int sceneObjectId)
        {
            if (!sceneObjects.ContainsKey(sceneObjectId))
            {
                LogUtils.Log(MessageBuilder.Trace($"A scene object with id #{sceneObjectId} does not exists in a region."), LogMessageType.Error);
                return;
            }

            var sceneObject = sceneObjects[sceneObjectId];

            sceneObjects.Remove(sceneObjectId);

            RemoveSubscribersForSubscriber(sceneObject);
            RemoveSubscriberForSubscribers(sceneObjectId);

            LogUtils.Log(MessageBuilder.Trace($"Removed subscription id #{sceneObjectId} from Region id #{Id}"), LogMessageType.Warning);
        }

        public void RemoveSubscriptionForAllSubscribers(int sceneObject)
        {
            if (!sceneObjects.ContainsKey(sceneObject))
            {
                LogUtils.Log(MessageBuilder.Trace($"A scene object with id #{sceneObject} does not exists in a region."), LogMessageType.Error);
                return;
            }

            RemoveAllSubscribersForSubscriber(sceneObject);

            sceneObjects.Remove(sceneObject);

            RemoveSubscriberForAllSubscribers(sceneObject);
        }

        public bool HasSubscription(int sceneObject)
        {
            return sceneObjects.ContainsKey(sceneObject);
        }

        public IEnumerable<ISceneObject> GetAllSubscribers()
        {
            return sceneObjects.Values;
        }

        /// <summary>
        /// Add all subscribers for a new subscriber.
        /// </summary>
        /// <param name="sceneObject">A new subscriber</param>
        private void AddSubscribersForSubscriber(ISceneObject sceneObject)
        {
            var subscribers = sceneObjects.Values.Where(subscriber => subscriber.Id != sceneObject.Id).ToArray();
            if (subscribers.Length <= 0) return;
            {
                var subscriberArea = sceneObject.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                subscriberArea?.InvokeSubscribersAdded(subscribers);
            }
        }

        /// <summary>
        /// Remove subscribers for the subscriber that left this region.
        /// </summary>
        /// <param name="sceneObject">A removed subscriber</param>
        private void RemoveSubscribersForSubscriber(ISceneObject sceneObject)
        {
            var subscribersForRemoveList = new List<int>();

            foreach (var subscriber in GetAllSubscribers())
            {
                var subscriberArea = subscriber.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                var subscribedPublishers = subscriberArea.GetSubscribedPublishers().ToArray();

                foreach (var publisher in subscribedPublishers)
                {
                    if (!publisher.HasSubscription(sceneObject.Id))
                    {
                        subscribersForRemoveList.Add(subscriber.Id);
                    }
                }

                foreach (var publisher in subscribedPublishers)
                {
                    if (publisher.HasSubscription(sceneObject.Id))
                    {
                        subscribersForRemoveList.Remove(subscriber.Id);
                    }
                }
            }

            if (subscribersForRemoveList.Count <= 0) return;
            {
                var subscriberArea = sceneObject.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                subscriberArea.InvokeSubscribersRemoved(subscribersForRemoveList.ToArray());
            }
        }

        /// <summary>
        /// Add a new subscriber for all other subscribers.
        /// </summary>
        /// <param name="sceneObject">A new subscriber</param>
        private void AddSubscriberForSubscribers(ISceneObject sceneObject)
        {
            var subscribers = sceneObjects.Values.Where(subscriber => subscriber.Id != sceneObject.Id).ToArray();

            foreach (var subscriber in subscribers)
            {
                var subscriberArea = subscriber.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                subscriberArea.InvokeSubscriberAdded(sceneObject);
            }
        }

        /// <summary>
        /// Remove the subscriber that left from this region for other subscribers.
        /// </summary>
        /// <param name="sceneObjectId">A removed subscriber id</param>
        private void RemoveSubscriberForSubscribers(int sceneObjectId)
        {
            foreach (var subscriber in GetAllSubscribers())
            {
                var subscriberArea = subscriber.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                if (!subscriberArea.GetSubscribedPublishers().Any(publisher => publisher.HasSubscription(sceneObjectId)))
                {
                    subscriberArea.InvokeSubscriberRemoved(sceneObjectId);
                }
            }
        }

        /// <summary>
        /// Remove the subscriber that left from this region for all subscribers.
        /// </summary>
        /// <param name="sceneObjectId">A removed subscriber id</param>
        private void RemoveSubscriberForAllSubscribers(int sceneObjectId)
        {
            foreach (var subscriber in GetAllSubscribers())
            {
                var subscriberArea = subscriber.GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                subscriberArea.InvokeSubscriberRemoved(sceneObjectId);
            }
        }

        /// <summary>
        /// Remove subscribers for the subscriber that left from this region.
        /// </summary>
        /// <param name="sceneObjectId">A removed subscriber id</param>
        private void RemoveAllSubscribersForSubscriber(int sceneObjectId)
        {
            var subscribers = sceneObjects.Keys.ToArray();

            if (sceneObjects[sceneObjectId] == null) return;
            {
                var subscriberArea = sceneObjects[sceneObjectId].GetGameObject().GetComponent<IInterestArea>().AssertNotNull();
                subscriberArea.InvokeSubscribersRemoved(subscribers);
            }
        }
    }
}