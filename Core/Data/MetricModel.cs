using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
	public enum ModelType
	{
		PositiveInteger,
		Percentage,
		SQALE,
		RAG,
		Boolean
	}

    public class MetricModel
    {
        private ModelType Type;
        private Type ValueType { get; set; }
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
                case ModelType.Percentage:
                    ValueType = typeof(int);
                    MinValue = 0;
                    MaxValue = 100;
                    break;
                case ModelType.RAG:
                    ValueType = typeof(char);
                    IsDict = true;
                    Dict = new List<string>(new string[]{"Red", "Amber", "Green"});
                    break;
            }
        }

        public bool IsValid(string value){
            switch(Type){
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
