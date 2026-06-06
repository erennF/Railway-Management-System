using System;
using System.Collections.Generic;
namespace Railway.Api.Models;
public class Sensor
{
    public int SensorId { get; set; } 
    public string SensorName { get; set; } = null!; 
    public string SensorType { get; set; } = null!; 
    public int TrainId { get; set; } 
    public virtual Train Train { get; set; } = null!; 
}