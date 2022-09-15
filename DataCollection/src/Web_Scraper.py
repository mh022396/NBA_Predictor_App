import requests
from bs4 import BeautifulSoup
from selenium import webdriver
import pandas
import time


def single_row(df):
    if df.shape[0] == 1:
        return df
    else:
        row = df[df["Tm"] == "TOT"]
        row["Tm"] = df.iloc[-1, :]["Tm"]
        return row


def Get_MVP_Player_Stats_HTML_ByYear(startYear, endYear):

    years = range(startYear, endYear) #inclusive 2022
    baseURL = "https://www.basketball-reference.com/awards/awards_{}.html"
    for y in years:
        url = baseURL.format(y)
        data = requests.get(url)
        with open("../mvps/{}.html".format(y), "w+", encoding="utf-8") as f:
            f.write(data.text)


def Get_All_Player_Stats_HTML_ByYear(startYear, endYear):

    driver = webdriver.Chrome(executable_path="chromedriver.exe")
    years = range(startYear, endYear) #inclusive 2022
    baseURL = "https://www.basketball-reference.com/leagues/NBA_{}_per_game.html"
    for y in years:
        url = baseURL.format(y)
        driver.get(url)
        driver.execute_script("window.scrollTo(1, 10000)")
        time.sleep(3)
        html = driver.page_source
        with open("../players/{}.html".format(y), "w+", encoding="utf-8") as f:
            f.write(html)


def Get_Standings_HTML_ByYear(startYear, endYear):

    years = range(startYear, endYear) #inclusive 2022
    baseURL = "https://www.basketball-reference.com/leagues/NBA_{}_standings.html"
    for y in years:
        url = baseURL.format(y)
        data = requests.get(url)
        with open("../standings/{}.html".format(y), "w+", encoding="utf-8") as f:
            f.write(data.text)


def Create_csv_html_mvp(startYear, endYear):

    years = range(startYear, endYear) #exclusion of last date
    dfs = []
    for y in years:
        with open("../mvps/{}.html".format(y), encoding="utf-8") as f:
            page = f.read()
        soup = BeautifulSoup(page, "html.parser")
        tHeads = soup.findAll('tr', class_='over_header')
        for tHead in tHeads:
            tHead.decompose()
        mvp_table = soup.find(id="mvp")
        mvp_t = pandas.read_html(str(mvp_table))[0]
        mvp_t["Year"] = y
        dfs.append(mvp_t)
    mvps = pandas.concat(dfs)
    mvps.to_csv("../data/mvps.csv")


def Create_csv_html_player(startYear, endYear):

    years = range(startYear, endYear) #exclusion of last date
    dfs = []
    for y in years:
        with open("../players/{}.html".format(y), encoding="utf-8") as f:
            page = f.read()
        soup = BeautifulSoup(page, "html.parser")
        tHeads = soup.findAll('tr', class_='thead')
        for tHead in tHeads:
            tHead.decompose()
        player_table = soup.find(id="per_game_stats")
        player_t = pandas.read_html(str(player_table))[0]
        player_t["Year"] = y
        dfs.append(player_t)
    players = pandas.concat(dfs)
    players.to_csv("../data/players.csv")


def Create_csv_html_standings(startYear, endYear):

    years = range(startYear, endYear) #exclusion of last date
    dfs = []
    for y in years:
        with open("../standings/{}.html".format(y), encoding="utf-8") as f:
            page = f.read()
        soup = BeautifulSoup(page, "html.parser")
        tHeads = soup.findAll('tr', class_='thead')
        for tHead in tHeads:
            tHead.decompose()

        standings_table = soup.find(id="divs_standings_E")
        standings_t = pandas.read_html(str(standings_table))[0]
        standings_t["Year"] = y
        standings_t["Team"] = standings_t["Eastern Conference"]
        del standings_t["Eastern Conference"]
        dfs.append(standings_t)

        standings_table = soup.find(id="divs_standings_W")
        standings_t = pandas.read_html(str(standings_table))[0]
        standings_t["Year"] = y
        standings_t["Team"] = standings_t["Western Conference"]
        del standings_t["Western Conference"]
        dfs.append(standings_t)

    standings = pandas.concat(dfs)
    standings.to_csv("../data/standings.csv")


def MostRecentStatsStandingsCSV(y):
    dfs = []
    with open("../standings/{}.html".format(y), encoding="utf-8") as f:
        page = f.read()
    soup = BeautifulSoup(page, "html.parser")
    tHeads = soup.findAll('tr', class_='thead')
    for tHead in tHeads:
        tHead.decompose()

    standings_table = soup.find(id="divs_standings_E")
    standings_t = pandas.read_html(str(standings_table))[0]
    standings_t["Year"] = y
    standings_t["Team"] = standings_t["Eastern Conference"]
    del standings_t["Eastern Conference"]
    dfs.append(standings_t)

    standings_table = soup.find(id="divs_standings_W")
    standings_t = pandas.read_html(str(standings_table))[0]
    standings_t["Year"] = y
    standings_t["Team"] = standings_t["Western Conference"]
    del standings_t["Western Conference"]
    dfs.append(standings_t)
    standings = pandas.concat(dfs)
    standings.to_csv("../data/standings{}.csv".format(y))

    with open("../players/{}.html".format(y), encoding="utf-8") as f:
        page = f.read()
    soup = BeautifulSoup(page, "html.parser")
    tHeads = soup.findAll('tr', class_='thead')
    for tHead in tHeads:
        tHead.decompose()
    player_table = soup.find(id="per_game_stats")
    player_t = pandas.read_html(str(player_table))[0]
    player_t["Year"] = y

    player_t.to_csv("../data/players{}.csv".format(y))


def MostRecentYearCSV():
    combined = pandas.read_csv("../data/players2022.csv")
    del combined["Rk"]
    del combined["Unnamed: 0"]

    combined["Player"] = combined["Player"].str.replace("*", "", regex=False)
    combined = combined.groupby(["Player", "Year"]).apply(single_row)
    combined.index = combined.index.droplevel()
    combined.index = combined.index.droplevel()

    standings = pandas.read_csv("../data/standings2022.csv")
    standings["Team"] = standings["Team"].str.replace("*", "", regex=False)

    del standings["Unnamed: 0"]
    nicknames = {}
    with open("../data/nicknames.csv") as f:
        lines = f.readlines()
        for line in lines[1:]:
            abbv, name = line.replace("\n", "").split(",")
            nicknames[abbv] = name
    combined["Team"] = combined["Tm"].map(nicknames)

    stats = combined.merge(standings, how="outer", on=["Team", "Year"])
    stats["GB"] = stats["GB"].str.replace("—", "0", regex=False)
    stats["FT%"] = stats["FT%"].fillna(0)
    stats["3P%"] = stats["3P%"].fillna(0)
    stats["2P%"] = stats["2P%"].fillna(0)
    stats["eFG%"] = stats["eFG%"].fillna(0)
    stats["FG%"] = stats["FG%"].fillna(0)
    stats["GB"] = pandas.to_numeric(stats["GB"])
    stats["Share"] = 0
    stats.to_csv("../data/test2022.csv")


def main():
    Get_MVP_Player_Stats_HTML_ByYear(1980, 2022)
    Get_Standings_HTML_ByYear(1980, 2022)
    Create_csv_html_mvp(1980, 2022)
    Create_csv_html_player(1980, 2022)
    Create_csv_html_standings(1980, 2022)
    #For recent year
    MostRecentStatsStandingsCSV(2022)
    MostRecentStatsStandingsCSV(2022)
    MostRecentYearCSV()


if __name__ == '__main__':
    main()