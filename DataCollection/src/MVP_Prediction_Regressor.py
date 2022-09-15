import os
from sklearn.linear_model import Ridge, LinearRegression, RANSACRegressor, Lasso, BayesianRidge, PoissonRegressor, SGDRegressor, PassiveAggressiveRegressor, ARDRegression, GammaRegressor
from sklearn.neural_network import MLPRegressor
from sklearn.ensemble import RandomForestRegressor, GradientBoostingRegressor, AdaBoostRegressor
from sklearn.metrics import mean_squared_error, r2_score
from sklearn.tree import DecisionTreeRegressor
import matplotlib.pyplot as plt
from sklearn import preprocessing
import pandas


def find_average_precision(combination, k):
    actual = combination.sort_values("Share", ascending=False).head(k) #MAP@5
    predicted = combination.sort_values("predictions", ascending=False)
    ps = []
    found = 0
    seen = 1
    for index, row in predicted.iterrows():
        if row["Player"] in actual["Player"].values:
            found += 1
            ps.append(found / seen)
        seen += 1
    return sum(ps) / len(ps)


def add_ranks(combination):
    combination = combination.sort_values("Share", ascending=False)
    combination["Rk"] = list(range(1, combination.shape[0] + 1))
    combination = combination.sort_values("predictions", ascending=False)
    combination["Predicted_Rk"] = list(range(1, combination.shape[0] + 1))
    combination["Diff"] = combination["Rk"] - combination["Predicted_Rk"]
    return combination


def backtest(stats, model, years, predictors):
    aps = []
    all_predicitions = []
    #Take out earliest 5 years for minimum years for training at 5
    for year in years[5:]:
        train = stats[stats["Year"] < year]
        test = stats[stats["Year"] == year]
        model.fit(train[predictors], train["Share"])
        predictions = model.predict(test[predictors])
        predictions = pandas.DataFrame(predictions, columns=["predictions"], index=test.index)
        combination = pandas.concat([test[["Player", "Share"]], predictions], axis=1)
        combination = add_ranks(combination)
        all_predicitions.append(combination)
        aps.append(find_average_precision(combination, 5))
    return sum(aps) / len(aps), aps, pandas.concat(all_predicitions)


def start_end_backtest(stats, model, start, end, predictors):

    train = stats[stats["Year"] < end]
    train = train[train["Year"] >= start]
    test = stats[stats["Year"] == end]
    model.fit(train[predictors], train["Share"])
    predictions = model.predict(test[predictors])
    predictions = pandas.DataFrame(predictions, columns=["predictions"], index=test.index)
    combination = pandas.concat([test[["Player", "Share"]], predictions], axis=1)
    combination = add_ranks(combination)
    r2 = r2_score(combination["Share"], combination["predictions"])
    MAP5 = find_average_precision(combination, 5)
    #print(start.__str__() +", "+ end.__str__() +": MAP="+MAP5.__str__()+" r2="+r2.__str__())

    return MAP5, r2


def predict_current_year(year, model, stats, predictors, testStats):
    train = stats[stats["Year"] > year]
    model.fit(train[predictors], train["Share"])

    predictions = model.predict(testStats[predictors])
    predictions = pandas.DataFrame(predictions, columns=["predictions"], index=testStats.index)
    combination = pandas.concat([testStats[["Player", "Share"]], predictions], axis=1)
    combination = add_ranks(combination)
    combination.head(5).to_csv("../data/mvp_predictions_2022.csv")
    print(combination.head(30))


def main():
    
    stats = pandas.read_csv("../data/player_mvp_stats.csv")
    stats_c = stats.copy()

    testStats = pandas.read_csv("../data/test2022.csv")
    test_n = testStats.copy()

    del stats_c["Unnamed: 0"]
    del test_n["Unnamed: 0"]

    stats_c = stats.fillna(0)
    test_n = test_n.fillna(0)
    stats_r = stats_c[["PTS", "AST", "STL", "BLK", "3P", "FT", "TRB", "Year"]].groupby("Year").apply(
        lambda x: x / x.mean())
    test_r = test_n[["PTS", "AST", "STL", "BLK", "3P", "FT", "TRB", "Year"]].groupby("Year").apply(
        lambda x: x / x.mean())

    stats_c[["PTS_R", "AST_R", "STL_R", "BLK_R", "3P_R", "FT_R", "TRB_R"]] = stats_r[
        ["PTS", "AST", "STL", "BLK", "3P", "FT", "TRB"]]
    test_n[["PTS_R", "AST_R", "STL_R", "BLK_R", "3P_R", "FT_R", "TRB_R"]] = test_r[
        ["PTS", "AST", "STL", "BLK", "3P", "FT", "TRB"]]

    #Add dummy variables for categorical value: position
    positions = ["PG", "SG", "SF", "PF", "C"]
    for pos in positions:
        stats_c[pos] = 0
        test_n[pos] = 0
        stats_c.loc[stats_c["Pos"] == pos, pos] = 1

    teams = pandas.read_csv("../data/team_market.csv")
    stats_c["Market"] = 0
    stats_c["Value"] = 0

    for t, m, v in teams[["Team", "Market", "Value"]].values:
        stats_c.loc[stats_c["Team"] == t, "Market"] = m
        stats_c.loc[stats_c["Team"] == t, "Value"] = v
        test_n.loc[test_n["Team"] == t, "Market"] = m
        test_n.loc[test_n["Team"] == t, "Value"] = v

    predictors = ['Age', 'G', 'GS', 'MP', 'FG', 'FGA', 'FG%', '3P',
                  '3PA', '3P%', '2P', '2PA', '2P%', 'eFG%', 'FT', 'FTA', 'FT%', 'ORB',
                  'DRB', 'TRB', 'AST', 'STL', 'BLK', 'TOV', 'PF', 'PTS', 'W', 'L', 'W/L%', 'GB', 'PS/G',
                  'PA/G', 'SRS', "PTS_R", "AST_R", "STL_R", "BLK_R", "3P_R", "FT_R", "TRB_R", "C", "PF", "SF", "SG",
                  "PG", "Market", "Value"]

    #Regression Model Selection:

    #reg = RANSACRegressor()
    #reg = Lasso()
    #reg = SGDRegressor()
    #reg = GammaRegressor()
    #reg = PassiveAggressiveRegressor()
    #reg = AdaBoostRegressor(n_estimators=500, learning_rate=.1)
    #reg = ARDRegression()
    #reg = MLPRegressor(max_iter=5000, shuffle=True, random_state=None, learning_rate='constant', learning_rate_init=.1, alpha=0.1)
    #reg = Ridge(alpha=.01, max_iter=1000)
    #reg = BayesianRidge()
    #reg = PoissonRegressor(alpha=.004, max_iter=100000)
    #reg = RandomForestRegressor(n_estimators=100, random_state=1, min_samples_split=5)
    reg = LinearRegression()

    scaler = preprocessing.StandardScaler().fit(stats_c[predictors])
    stats_c[predictors] = scaler.transform(stats_c[predictors])
    test_n[predictors] = scaler.transform(test_n[predictors])

    predictors = [ 'GS', 'MP', 'FG', 'FGA', '3P', '3PA',  '2P', '2PA', 'FTA', 'DRB', 'TRB', 'AST', 'STL', 'BLK', 'PF', 'PTS', 'L',
                   'SRS', "PTS_R", "AST_R", "STL_R", "BLK_R", "3P_R", "FT_R", "TRB_R", "C", "PF", "SF", "SG", "PG", "Market"]

    year_data = []
    m_data = []
    r_data = []

    for i in range(1980, 2020):
        m, r = start_end_backtest(stats_c, reg, i, 2020, predictors)
        year_data.append(i)
        m_data.append(m)
        r_data.append(r)
    print("BACK TEST MAP AVERAGE: "+(sum(m_data)/len(m_data)).__str__())
    plt.figure().patch.set_facecolor('gray')

    plt.rcParams['axes.facecolor'] = 'white'
    plt.plot(year_data, m_data, label="MAP@5", lw=2, color='blue')
    plt.plot(year_data, r_data, label="R2", lw=2, color='red')
    plt.ylim([0, 1.1])
    plt.xlim([1980, 2019])

    plt.xlabel("Year")
    plt.title("2020 Scores By Training Random Forest Regression")
    plt.grid('on')

    plt.fill_between(year_data, m_data, alpha=.25, facecolor='blue')
    plt.fill_between(year_data, r_data, alpha=.5, facecolor='red')
    plt.show()

    stats_c = stats_c[stats_c["Year"] > 1990]
    predict_current_year(2010, reg, stats_c, predictors, test_n)


if __name__ == '__main__':
    main()
