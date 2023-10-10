using System.Collections.Generic;
using Unitylities.ResourceEnums;

namespace Unitylities
{
    public class Resource
    {
        #region Fields
        private ResourceType type;
        private string description;
        private int quantity;
        private int maxQuantity;
        #endregion

        #region Properties
        public ResourceType Type => type;
        public string Description => description;
        public int Quantity => quantity;
        public int MaxQuantity => maxQuantity;
        #endregion

        #region Constructor
        public Resource(ResourceType type, string description, int quantity, int maxQuantity)
        {
            this.type = type;
            this.description = description;
            this.quantity = quantity;
            this.maxQuantity = maxQuantity;
        }
        #endregion
    }
}