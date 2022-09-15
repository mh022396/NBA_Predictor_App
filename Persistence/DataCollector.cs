using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace Persistence
{
    public class DataCollector
    {
        public static List<T> GetDataFromCsv<T, M>(string path) where M : ClassMap<T>{ //T = model type M = mapper
            StreamReader streamReader = new StreamReader(path);
            CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<M>();
            List<T> records = csvReader.GetRecords<T>().ToList();      
            return records;
        }
    }
}