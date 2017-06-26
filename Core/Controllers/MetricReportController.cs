using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdysTech.InfluxDB;
using AdysTech.InfluxDB.Client.Net;
using Core.Repository;
using Core.Tools;
using Core.Data;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Core.Controllers
{

    public class MetricSubmissionReport
    {
        public int Id { get; set; }
        public bool IsAccepted { get; set; }
        public string RejectionReason { get; set; }
        public bool IsSavedSuccessfully { get; set; }
        public string DBName { get; set; }
        [JsonIgnore]
        public InfluxDatapoint<InfluxValueField> DataPoint { get; set; } = null;    
    }



    [Route("api/[controller]")]
    public class MetricReportController : Controller
    {
        private InfluxDBClient _client;
        private IMetricRepository _metricsRepo;
        public MetricReportController(IMetricRepository mRepo){
            ConfigurationReader conf = new ConfigurationReader();
            //default configuration of InfluxDB ignores login and password parameters unless specifically configured
            _client = new InfluxDBClient(conf.Configuration["Data:InfluxDBHost"],
                                         conf.Configuration["Data:InfluxDBLogin"],
                                         conf.Configuration["Data:InfluxDBPassword"]);
            _metricsRepo = mRepo;

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
            List<MetricSubmissionReport> Report = new List<MetricSubmissionReport>();
            List<InfluxDatapoint<InfluxValueField>> points = new List<InfluxDatapoint<InfluxValueField>>();
            foreach (KeyValuePair<int,string> item in data){
                MetricSubmissionReport itemReport = new MetricSubmissionReport
                {
                    Id = item.Key
                };


                Metric metric = _metricsRepo.GetSingle(x=>x.Id == item.Key, x => x.Project,x=>x.Project.Portfolio, x=>x.Project.Portfolio.Organization);

                if (metric != null){
                    if (metric.Model.IsValid(item.Value)){
                        itemReport.IsAccepted = true;
						InfluxDatapoint<InfluxValueField> point = new InfluxDatapoint<InfluxValueField>();
						point.MeasurementName = metric.MetricMeasurementName;
						point.UtcTimestamp = DateTime.UtcNow;
						point.Precision = TimePrecision.Seconds;
						point.Fields.Add("metricId", new InfluxValueField(item.Key));
						point.Fields.Add("value", new InfluxValueField(item.Value));
                        itemReport.DataPoint = point;
                        itemReport.DBName = metric.MetricDatabaseName;
					}
                    else{
                        itemReport.IsAccepted = false;
                        itemReport.RejectionReason = "Metric value validation failed";
                    }					  
                }
                else{
                    itemReport.IsAccepted = false;
                    itemReport.RejectionReason = "Metric with this ID does not exist, please create a Metric first.";
                    itemReport.IsSavedSuccessfully = false;

                }
            Report.Add(itemReport);
            }
            //var result = await Task.FromResult(_client.PostPointsAsync("mkk", points));
            //return new ObjectResult(result);
            foreach (MetricSubmissionReport item in Report.Where(x => x.DataPoint != null))
            {
                item.IsSavedSuccessfully = await _client.PostPointAsync(item.DBName, item.DataPoint);
            }
            return new ObjectResult(Report);
        }
    }
}
