using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Data
{
	public enum ModelType
	{
        [Display(Name="Any String")]
        [DisplayName("any string dn")]
        String,

        [Display(Name="Positive integer value: 0, 15, 500...")]
        PositiveInteger,
		
        [Display(Name="Percentage values: -10%, 5%, 100%")]
        Percentage,
		
        [Display(Name="SQALE Rating dictionary: A, B, C, D, E")]
        SQALE,
		
        [Display(Name = "RAG disctionary: Red, Amber, Green")]
        RAG,
		
        [Display(Name = "Boolean value: yes or no")]
        Boolean,

        [Display(Name = "Duration, in minutes: 1, 340, 42500")]
        DurationMinutes,

        [Display(Name = "Risk/Severity dictionary: Trivial, Low, Medium, High, Blocker")]
        TrivialLowMediumHighBlocker
	}

    public class MetricModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ModelType Type { get; set; }
        private System.Type ValueType { get; set; }
        public string ValueTypeName {
            get{
                return ValueType.Name;
            }
        }
        public bool IsDict { get; set; } = false;
        public List<string> Dict { get; set; } = null;
        public double MinValue { get; set; } = double.MinValue;
        public double MaxValue { get; set; } = double.MaxValue;
        public MetricModel(ModelType type)
        {
            Type = type;

            switch(type){
                case ModelType.String:
                    ValueType = typeof(string);
                    break;
                case ModelType.PositiveInteger:
                    ValueType = typeof(int);
                    MinValue = 0;
                    MaxValue = int.MaxValue;
                    break;
                case ModelType.Percentage:
                    ValueType = typeof(int);
                    MinValue = int.MinValue;
                    MaxValue = int.MaxValue;
                    break;
                case ModelType.RAG:
                    ValueType = typeof(char);
                    IsDict = true;
                    Dict = new List<string>(new string[]{"Red", "Amber", "Green"});
                    break;
            }
        }

        //todo: return validationResult instead with an error message
        public bool IsValid(string value){
            switch(Type){
                case ModelType.String:
                    return true;
                case ModelType.Percentage:
                    int result;
                    if (
                        (int.TryParse(value, out result) == true)
                        && (Enumerable.Range((int)MinValue,(int)MaxValue).Contains(result))
                    ){
                        return true;
                    }
                    else{
                        return false;
                    }
                case ModelType.RAG:
                    if (Dict.Contains(value)){
                        return true;
                    }
                    else {
                        return false;
                    }
            }
            return false;
        }

    }
}