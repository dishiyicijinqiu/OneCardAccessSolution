using System;
namespace FengSharp.OneCardAccess.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class DataBaseKeyAttribute : Attribute
    {
        public DataBaseKeyAttribute(DataBaseKeyType KeyType)
        {
            this.KeyType = KeyType;
        }
        public DataBaseKeyAttribute()
        {
            this.KeyType = DataBaseKeyType.Int;
        }
        public DataBaseKeyType KeyType { get; set; }
    }
    public enum DataBaseKeyType
    {
        Guid,
        Int,
        UnKnown
    }
}
