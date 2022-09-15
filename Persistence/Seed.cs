using Domain.Models;
using System.Linq;
using static Persistence.ModelMappers.ModelDataMapper; 

namespace Persistence
{
    public class Seed
    {
        static string predictionsPath = @"C:\WebAppProjects\MVP_Predictor_App\DataCollection\data\mvp_predictions_2022.csv";
        static string pastPlayersPath = @"C:\WebAppProjects\MVP_Predictor_App\DataCollection\data\player_mvp_stats.csv";
        static string currentPlayersPath = @"C:\WebAppProjects\MVP_Predictor_App\DataCollection\data\players2022.csv";
        static string mvpsPath = @"C:\WebAppProjects\MVP_Predictor_App\DataCollection\data\mvps.csv";
        public static async Task SeedData(DataContext context){
            if(context.Predictions.Any()){ 
                return;
            }

            List<MVPPrediction> predictions =  DataCollector.GetDataFromCsv<MVPPrediction, MVPPredicitionClassMap>(predictionsPath);
            List<PastPlayer> pastPlayers =  DataCollector.GetDataFromCsv<PastPlayer, PastPlayerClassMap>(pastPlayersPath);
            List<CurrentPlayer> currentPlayers =  DataCollector.GetDataFromCsv<CurrentPlayer, CurrentPlayerClassMap>(currentPlayersPath);
            List<PastMVP> mvps =  DataCollector.GetDataFromCsv<PastMVP, PastMVPClassMap>(mvpsPath);

            await context.Predictions.AddRangeAsync(predictions);
            await context.PastPlayers.AddRangeAsync(pastPlayers);
            await context.CurrentPlayers.AddRangeAsync(currentPlayers);
            await context.PastMVPs.AddRangeAsync(mvps);
            await context.SaveChangesAsync();
        }
    }
}