﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OPCDA2MSA
{
    class Config
    {
        private static CfgJson cfg = null;

        public static CfgJson GetConfig() {
            if (cfg != null) {
                return cfg;
            }
            string jsonfile = Path.Combine(Application.StartupPath, "config.json");
            Console.WriteLine(jsonfile);
            if (!File.Exists(jsonfile))
            {
                throw new Exception("找不到配置文件：" + jsonfile);
            }
            string jsonStr = File.ReadAllText(jsonfile);
            //Console.WriteLine(jsonStr);
            cfg = JsonConvert.DeserializeObject<CfgJson>(jsonStr);
            if (cfg == null) {
                throw new Exception("配置文件：" + jsonfile+ "，不是有效的JSON文件");
            }
            Console.WriteLine(JsonConvert.SerializeObject(cfg));
            return cfg;
        }
    }

    class CfgJson
    {
        public OpcDaJson Opcda { get; set; }
        public ModbusJson Modbus { get; set; }
        public MsaJson Msa { get; set; }
        // 指标注册表 位号->编码
        public Dictionary<string, string> Registers { get; set; }
    }

     class OpcDaJson
    {
        public string Host { get; set; }
        public string Node { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    class ModbusJson
    {
        public ModbusSlaveJson Slave { get; set; }
    }

    class ModbusSlaveJson
    {
        // 监听地址
        public string Ip { get; set; }
        // 监听端口
        public int Port { get; set; }
        // 站号
        public byte Station { get; set; }
    }

    class MsaJson
    {
        // 服务器IP
        public string Ip { get; set; }
        // 服务器端口
        public int Port { get; set; }
        // 设备唯一编码
        public uint Mn { get; set; }
        // 维持TCP的心跳间隔，单位毫秒
        public int Heartbeat { get; set; }
        // OPCDA数据上报间隔，单位毫秒
        public int Interval { get; set; }
    }
}
