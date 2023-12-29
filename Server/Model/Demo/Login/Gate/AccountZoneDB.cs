using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ChildType(typeof(RoleInfoDB))]
    [ComponentOf(typeof(GateUser))]
    public class AccountZoneDB:Entity,IAwake,IDestroy
    {
        public string Account;
        
        public int LoginZoneId;

        public long LastRoleId;

        [BsonIgnore]
        public RoleInfoDB CurRole
        {
            get
            {
                return this.GetChild<RoleInfoDB>(this.LastRoleId);
            }
        }

    }
}