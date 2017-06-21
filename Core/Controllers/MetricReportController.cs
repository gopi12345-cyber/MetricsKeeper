using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdysTech.InfluxDB;
using AdysTech.InfluxDB.Client.Net;


namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class MetricReportController : Controller
    {
        InfluxDBClient _client;
        public MetricReportController(){
            //default configuration of InfluxDB ignores auth parameters unless specifically configured
            _client = new InfluxDBClient("http://localhost:8086", "root", "root");

        }

        [HttpPost("{id}/{value}")]
        public async Task<IActionResult> SubmitReport(int id, string value)
        {
            InfluxDatapoint<InfluxValueField> point = new InfluxDatapoint<InfluxValueField>();
            point.MeasurementName = "metric";
            point.UtcTimestamp = DateTime.UtcNow;
            point.Precision = TimePrecision.Seconds;
            point.Fields.Add("metric_id", new InfluxValueField(id));
            point.Fields.Add("value", new InfluxValueField(value));
            var result = await _client.PostPointAsync("mkk", point);
            return new ObjectResult(result);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitReport([FromBody]Dictionary<int,string> data){
            List<InfluxDatapoint<InfluxValueField>> points = new List<InfluxDatapoint<InfluxValueField>>();

            foreach (KeyValuePair<int,string> item in data){
                InfluxDatapoint<InfluxValueField> point = new InfluxDatapoint<InfluxValueField>();
                point.MeasurementName = "metric";
                point.UtcTimestamp = DateTime.UtcNow;
                point.Precision = TimePrecision.Seconds;
                point.Fields.Add("metric_id", new InfluxValueField(item.Key));
                point.Fields.Add("value", new InfluxValueField(item.Value));
                points.Append(point);
            }
            var result = await _client.PostPointsAsync("mkk", points);
            return new ObjectResult(result);
        }
    }
}
