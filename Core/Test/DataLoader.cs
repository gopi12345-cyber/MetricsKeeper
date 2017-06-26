using System;
using Core.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Core.Tools;
using Microsoft.Extensions.Logging;

namespace Core.Test
{
    public class DataLoader
    {
        CoreContext _cont;
        public readonly ILogger _logger;
        public DataLoader(bool CleanDB = true)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoreContext>();
            optionsBuilder.UseMySql(new ConfigurationReader().Configuration["Data:MySQLDBConnectionString"]);
            _cont = new CoreContext(optionsBuilder.Options);
            if (CleanDB == true){
                WipeDBRecords();
            }

        }

        private void WipeDBRecords(){
            _cont.Database.ExecuteSqlCommand("DELETE FROM PROJECT");
            _cont.Database.ExecuteSqlCommand("DELETE FROM PORTFOLIO");
            _cont.Database.ExecuteSqlCommand("DELETE FROM ORG");
        }

        public void LoadData()
        {
            JObject json = JObject.Parse(File.ReadAllText(AppContext.BaseDirectory+"/Test/TestData.json"));
            foreach(JToken OToken in json["Orgs"].Children()){
                Org o = OToken.ToObject<Org>();
                _cont.Orgs.Add(o);
                _cont.SaveChanges();
                foreach(JToken prtToken in OToken["Portfolios"]){
                    Portfolio p = prtToken.ToObject<Portfolio>();
                    p.OrganizationId = o.Id;
                    _cont.Portfolios.Add(p);
                    _cont.SaveChanges();
                    foreach(JToken projToken in prtToken["Projects"]){
                        Project prj = projToken.ToObject<Project>();
                        prj.PortfolioId = p.Id;
                        _cont.Projects.Add(prj);
                        _cont.SaveChanges();
                    }
                }
            }

        }
    }
}
