﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET
{
    public partial class StartSceneConfigCategory
    {
        public MultiMap<int, StartSceneConfig> Gates = new MultiMap<int, StartSceneConfig>();
        
        public MultiMap<int, StartSceneConfig> ProcessScenes = new MultiMap<int, StartSceneConfig>();
        
        public Dictionary<long, Dictionary<string, StartSceneConfig>> ZoneScenesByName = new Dictionary<long, Dictionary<string, StartSceneConfig>>();

        public StartSceneConfig LocationConfig;
        
        public List<StartSceneConfig> Robots = new List<StartSceneConfig>();

        public List<StartSceneConfig> Realms = new List<StartSceneConfig>();

        public StartSceneConfig[,] SceneTypeByScenes = new StartSceneConfig[IdGenerater.MaxZone, (int)SceneType.Max];
        
        public List<StartSceneConfig> GetByProcess(int process)
        {
            return this.ProcessScenes[process];
        }
        
        public StartSceneConfig GetBySceneName(int zone, string name)
        {
            return this.ZoneScenesByName[zone][name];
        }
        
        public override void AfterEndInit()
        {
            foreach (StartSceneConfig startSceneConfig in this.GetAll().Values)
            {
                this.ProcessScenes.Add(startSceneConfig.Process, startSceneConfig);
                
                if (!this.ZoneScenesByName.ContainsKey(startSceneConfig.Zone))
                {
                    this.ZoneScenesByName.Add(startSceneConfig.Zone, new Dictionary<string, StartSceneConfig>());
                }
                this.ZoneScenesByName[startSceneConfig.Zone].Add(startSceneConfig.Name, startSceneConfig);
                
                switch (startSceneConfig.Type)
                {
                    case SceneType.Gate:
                        this.Gates.Add(startSceneConfig.Zone, startSceneConfig);
                        break;
                    case SceneType.Location:
                        this.LocationConfig = startSceneConfig;
                        break;
                    case SceneType.Robot:
                        this.Robots.Add(startSceneConfig);
                        break;
                    case SceneType.Realm:
                        this.Realms.Add(startSceneConfig);
                        break;
                    default:
                        this.SceneTypeByScenes[startSceneConfig.Zone, (int)startSceneConfig.Type] = startSceneConfig;
                        break;
                }
            }
        }
        
        public StartSceneConfig GetBySceneType(int zone, SceneType sceneType)
        {
            var config = this.SceneTypeByScenes[zone, (int)sceneType];
            if (config == null)
            {
                Log.Error($"无法获取对应区服的Scene:Zone{zone}SceneType{sceneType}");
                
            }

            return config;
        }

        public long GetSceneInstanceId(int zone, SceneType sceneType)
        {
            try
            {
                StartSceneConfig startSceneConfig = Instance.GetBySceneType(zone, sceneType);
                return startSceneConfig.InstanceId;
            }
            catch (Exception e)
            {
               
                throw new Exception($"not found scene:{zone}{sceneType}",e);
            }
        }
    }
    
   

    public partial class StartSceneConfig: ISupportInitialize
    {
        public long InstanceId;
        
        public SceneType Type;

        public StartProcessConfig StartProcessConfig
        {
            get
            {
                return StartProcessConfigCategory.Instance.Get(this.Process);
            }
        }
        
        public StartZoneConfig StartZoneConfig
        {
            get
            {
                return StartZoneConfigCategory.Instance.Get(this.Zone);
            }
        }

        // 内网地址外网端口，通过防火墙映射端口过来
        private IPEndPoint innerIPOutPort;

        public IPEndPoint InnerIPOutPort
        {
            get
            {
                if (innerIPOutPort == null)
                {
                    this.innerIPOutPort = NetworkHelper.ToIPEndPoint($"{this.StartProcessConfig.InnerIP}:{this.OuterPort}");
                }

                return this.innerIPOutPort;
            }
        }

        private IPEndPoint outerIPPort;

        // 外网地址外网端口
        public IPEndPoint OuterIPPort
        {
            get
            {
                if (this.outerIPPort == null)
                {
                    this.outerIPPort = NetworkHelper.ToIPEndPoint($"{this.StartProcessConfig.OuterIP}:{this.OuterPort}");
                }

                return this.outerIPPort;
            }
        }

        public override void BeginInit()
        {
        }

        public override void EndInit()
        {
            this.Type = EnumHelper.FromString<SceneType>(this.SceneType);
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(this.Process, (uint) this.Id);
            this.InstanceId = instanceIdStruct.ToLong();
        }
    }
}