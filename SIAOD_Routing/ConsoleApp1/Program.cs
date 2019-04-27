using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Device
    {
        [JsonRequired]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonRequired]
        [JsonProperty("ipAddress")]
        public string IPAddress { get; set; }

        [JsonRequired]
        [JsonProperty("type")]
        public TypeCode Type { get; set; }

        public Device(int id, string ipAddress)
        {
            this.Id = id;
            this.IPAddress = ipAddress;
        }

        public void BadFunction()
        {
            throw new DeviceException(this, "pizda");
        }
    }

    [Serializable]
    public class DeviceException : Exception
    {

        public DeviceException() { }
        public DeviceException(Device device, string message)
            : base(message + $"\n{device.ToString()} {nameof(device.Id)}:{device.Id} {nameof(device.IPAddress)}:{device.IPAddress}\n") { }
        public DeviceException(string message, Exception inner) : base(message, inner) { }
        protected DeviceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Device device = new Device(44, "192.168.0.50");
            //Dictionary<string, Func<double[], double>> doubleDictionary = default;
            JSchema jSchema = new JSchema();
            JSchemaGenerator jSchemaGenerator = new JSchemaGenerator();
            var schema = jSchemaGenerator.Generate(typeof(Device));
            Console.WriteLine(schema);
            try
            {
                device.BadFunction();
            }
            catch (DeviceException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            Console.ReadKey();
        }
    }
}
