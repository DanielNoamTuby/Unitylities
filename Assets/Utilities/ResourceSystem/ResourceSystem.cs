using System;
using System.Collections.Generic;
using UnityEngine;
using Unitylities.ResourceEnums;

namespace Unitylities
{
    public class ResourceSystem
    {
        // Dictionary to store resource types and quantities.
        private Dictionary<Resource, int> resources;

        // Events to notify when resource quantities change or when a resource is added or removed.
        public event EventHandler OnResourceQuantityChanged;
        public event EventHandler OnResourceAdded;
        public event EventHandler OnResourceRemoved;

        // Private event argument class to encapsulate resource quantity changes.
        private class OnResourceQuantityChangedEventArgs : EventArgs
        {
            public Resource resource;
            public int newQuantity;
        }
        // Private event argument class to encapsulate resource addition or removal.
        private class OnResourceAddedOrRemovedEventArgs : EventArgs
        {
            public Resource resource;
        }

        /// <summary>
        /// Constructor for the ResourceSystem class.
        /// Initializes the resource dictionary with initial resource types and quantities.
        /// </summary>
        /// <param name="resourceList">The list of initial resources.</param>
        public ResourceSystem(List<Resource> resourceList)
        {
            resources = new Dictionary<Resource, int>();

            foreach (Resource resource in resourceList)
            {
                resources.Add(resource, resource.Quantity);
            }
        }

        #region Methods
        /// <summary>
        /// Add a resource to the system.
        /// </summary>
        /// <param name="resource">The resource to add.</param>
        public void AddResource(Resource resource)
        {
            if (!resources.ContainsKey(resource))
            {
                resources.Add(resource, resource.Quantity);

                // Fire the ResourceChanged event.
                OnResourceAdded?.Invoke(this, new OnResourceAddedOrRemovedEventArgs
                {
                    resource = resource,
                });
            }
        }

        /// <summary>
        /// Remove a resource from the system.
        /// </summary>
        /// <param name="resource">The resource to remove.</param>
        public void RemoveResource(Resource resource)
        {
            if (resources.ContainsKey(resource))
            {
                resources.Remove(resource);

                // Fire the ResourceChanged event.
                OnResourceQuantityChanged?.Invoke(this, new OnResourceAddedOrRemovedEventArgs
                {
                    resource = resource,
                });
            }
        }

        /// <summary>
        /// Add a specified quantity to a resource.
        /// </summary>
        /// <param name="resource">The resource to add to.</param>
        /// <param name="quantity">The quantity to add.</param>
        public void AddToResource(Resource resource, int quantity)
        {
            if (resources.ContainsKey(resource))
            {
                resources[resource] += quantity;

                resources[resource] = Mathf.Clamp(resources[resource], 0, resource.MaxQuantity);

                // Fire the ResourceChanged event.
                OnResourceQuantityChanged?.Invoke(this, new OnResourceQuantityChangedEventArgs
                {
                    resource = resource,
                    newQuantity = resources[resource]
                });
            }
        }

        /// <summary>
        /// Subtract a specified quantity from a resource.
        /// </summary>
        /// <param name="resource">The resource to subtract from.</param>
        /// <param name="quantity">The quantity to subtract.</param>
        public void SubtractFromResource(Resource resource, int quantity)
        {
            if (resources.ContainsKey(resource))
            {
                resources[resource] -= quantity;

                resources[resource] = Mathf.Clamp(resources[resource], 0, resource.MaxQuantity);

                // Fire the ResourceChanged event.
                OnResourceQuantityChanged?.Invoke(this, new OnResourceQuantityChangedEventArgs
                {
                    resource = resource,
                    newQuantity = resources[resource]
                });
            }
        }
        #endregion
    }
}