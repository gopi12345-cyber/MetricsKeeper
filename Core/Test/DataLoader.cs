using System;
using Core.Data;
namespace Core.Test
{
    public class DataLoader
    {
        public DataLoader(bool CleanDB)
        {
            if (CleanDB){
                this.CleanDB();
            }

        }

        public void CleanDB(){
            
        }
    }
}
