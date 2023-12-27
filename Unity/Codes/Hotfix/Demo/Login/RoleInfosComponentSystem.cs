namespace ET
{
    [FriendClass(typeof(RoleInfosComponent))]
    [FriendClass(typeof(RoleInfo))]
    public static class RoleInfosComponentSystem
    {
        public static void ClearRoleInfo(this RoleInfosComponent self)
        {
            foreach (RoleInfo roleInfo in self.RoleInfos)
            {
                roleInfo?.Dispose();
            }

            self.RoleInfos.Clear();
        }

        public static void AddRoleInfo(this RoleInfosComponent self, GeteRoleInfo geteRoleInfo)
        {
            RoleInfo roleInfo = self.AddChildWithId<RoleInfo>(geteRoleInfo.UnitId);
            roleInfo.Name = geteRoleInfo.Name;
            roleInfo.Level = geteRoleInfo.Level;
            self.RoleInfos.Add(roleInfo);
        }
        
        public static RoleInfo GetRoleInfoByIndex(this RoleInfosComponent self,int index)
        {
            if (index < 0 || index >= self.RoleInfos.Count)
            {
                return null;
            }

            return self.RoleInfos[index];
        }
    }
}