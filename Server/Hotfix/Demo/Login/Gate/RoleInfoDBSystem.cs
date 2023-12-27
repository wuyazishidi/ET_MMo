namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public static class RoleInfoDBSystem
    {
        public static GeteRoleInfo ToMessage(this RoleInfoDB self)
        {
            return new GeteRoleInfo() { UnitId = self.Id, Level = self.Level, Name = self.Name };
        }
    }
}