import pandas


def single_row(df):
    if df.shape[0] == 1:
        return df
    else:
        row = df[df["Tm"] == "TOT"]
        row["Tm"] = df.iloc[-1, :]["Tm"]
        return row


def main():
    mvps = pandas.read_csv("../data/mvps.csv")
    mvps = mvps[["Player", "Year", "Pts Won", "Pts Max", "Share"]]

    players = pandas.read_csv("../data/players.csv")
    del players["Rk"]
    del players["Unnamed: 0"]

    players["Player"] = players["Player"].str.replace("*", "", regex=False)
    players = players.groupby(["Player", "Year"]).apply(single_row)
    players.index = players.index.droplevel()
    players.index = players.index.droplevel()

    combined = players.merge(mvps, how="outer", on=["Player", "Year"])
    combined[["Pts Won", "Pts Max", "Share"]] = combined[["Pts Won", "Pts Max", "Share"]].fillna(0)

    standings = pandas.read_csv("../data/standings.csv")
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
    stats["GB"] = pandas.to_numeric(stats["GB"])
    stats.to_csv("../data/player_mvp_stats.csv")


if __name__ == '__main__':
    main()