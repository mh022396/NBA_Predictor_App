using Xunit;
namespace Persistence;
using Domain.Models;

public class UnitTestDataCollection
{
    [Fact]
    public void GetMVPPredicitionData_DataMatch_True()
    {
        // List<MVPPrediction> predictionsData = DataCollector.GetMVPPredicitionData(2022);
        // Assert.Equal(477, predictionsData[0].Id);
        // Assert.Equal(15, predictionsData[1].Id);
        // Assert.Equal(31, predictionsData[2].Id);
        // Assert.Equal(463, predictionsData[3].Id);
        // Assert.Equal(326, predictionsData[4].Id);
    }

    [Fact]
    public void GetMVPPredicitionData_TypeMatch_True()
    {
        // List<MVPPrediction> predictionsData = DataCollector.GetMVPPredicitionData(2022);
        // foreach(MVPPrediction prediction in predictionsData){
        //     Assert.Equal(typeof(long), prediction.Id.GetType());
        //     Assert.Equal(typeof(string), prediction.Name.GetType());
        //     Assert.Equal(typeof(decimal), prediction.PredictedShare.GetType());
        //     Assert.Equal(typeof(int), prediction.PredictedRank.GetType());
        // }
    }
}