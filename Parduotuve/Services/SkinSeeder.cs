﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parduotuve.Data;
using Parduotuve.Data.Entities;

namespace Parduotuve.Services;

/*
okey, bet jei nelabai rasi ko nors ar kitiem reikes sita daryti, tai cia mano supaprastina versija kaip prideti nauja table i duombaze:
- data/entities padarai nauja class kuriame fields bus table columns (tai bus entity)
- data/repositories padarai sitam entity nauja repository ir jam interface (gali daryt lygiai ta pati kaip kituose repositories padaryta)
- i StoreDataContext.cs pridet public DbSet sitam entity ir i onmodelcreating pridet totable bindings (sitas optional lygtais)
- i Program.cs prideti repository scoped services (salia kitu)
https://learn.microsoft.com/en-us/ef/core/modeling/
https://www.youtube.com/results?search_query=c%23+ms+entity+framework+
*/
public class SkinSeeder
{
    private readonly StoreDataContext _context;
    private readonly HttpClient _httpClient;

    public SkinSeeder(StoreDataContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task SeedSkinsAsync()
    {
        if (await _context.Skins.AnyAsync()) return;

        List<Skin>? skins = await GetSkins();

        if (skins != null)
        {
            await _context.Skins.AddRangeAsync(skins);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<List<Skin>?> GetSkins()
    {
        string champions = await _httpClient.GetStringAsync("https://cdn.communitydragon.org/latest/champions");
        List<string> keys;
        List<string> secondaryKeys;

        JArray? array = JsonConvert.DeserializeObject<JArray>(champions);

        if (array == null) return null;

        keys = array.Select(val => (string?)val["key"] ?? "None").Where(key => key != "None").ToList();


        List<Skin> skins = new();
        int idCounter = 1;
        int chromaIdCounter = 1;

        for (int x = 0; x < keys.Count; x++)
        {
            string championData =
                await _httpClient.GetStringAsync($"https://cdn.communitydragon.org/latest/champion/{keys[x]}/data");
            string secondaryData;

            try
            {
                secondaryData = await _httpClient.GetStringAsync(
                    $"https://cdn.merakianalytics.com/riot/lol/resources/latest/en-US/champions/{keys[x]}.json");
            }
            catch
            {
                secondaryData = await _httpClient.GetStringAsync(
                    $"https://cdn.merakianalytics.com/riot/lol/resources/latest/en-US/champions/{string.Join(null, keys[x].Select((x, index) => index == 0 ? char.ToUpper(x) : char.ToLower(x)).ToList())}.json");
            }


            JObject? parsedData = JsonConvert.DeserializeObject<JObject>(championData);
            JObject? secondaryParsed = JsonConvert.DeserializeObject<JObject>(secondaryData);
            if (parsedData == null || secondaryParsed == null) continue;

            JArray skinData = (JArray?)parsedData["skins"] ?? new JArray();
            JArray secondarySkinData = (JArray?)secondaryParsed["skins"] ?? new JArray();

            if (skinData.Count <= 1 || secondarySkinData.Count <= 1) continue;

            skinData.RemoveAt(0);
            secondarySkinData.RemoveAt(0);

            for (int y = 0; y < skinData.Count && y < secondarySkinData.Count; y++)
            {
                Skin skin = new();
                skin.Id = idCounter++;
                skin.ChampionName = (string?)parsedData["name"];
                skin.Name = (string?)skinData[y]["name"];
                skin.PriceRP = (string?)secondarySkinData[y]["cost"];
                skin.ReleaseDate = DateTime.Parse((string)secondarySkinData[y]["release"]);

                string rarity = (string?)skinData[y]["rarity"] ?? "kNoRarity";

                switch (rarity)
                {
                    case "kNoRarity":
                        skin.Price = 9.99;
                        break;
                    case "kEpic":
                        skin.Price = 14.99;
                        break;
                    case "kLegendary":
                        skin.Price = 19.99;
                        break;
                    case "kMythic":
                        skin.Price = 49.99;
                        break;
                    case "kUltimate":
                        skin.Price = 29.99;
                        break;
                    default:
                        skin.Price = 99.99;
                        break;
                }

                skin.CinematicSplashUrl = (string?)secondarySkinData[y]["uncenteredSplashPath"];
                skin.SplashUrl = (string?)secondarySkinData[y]["loadScreenPath"];
                skin.Quote = (string?)skinData[y]["description"];

                JArray chromaArray = (JArray?)secondarySkinData[y]["chromas"] ?? new JArray();

                List<Chroma> chromaList = new();

                foreach (JToken chroma in chromaArray)
                {
                    if (!chroma.HasValues) continue;
                    int chromaRarity = (int?)chroma["rarities"]?.Last?["rarity"] ?? 0;
                    string price;
                    switch (chromaRarity)
                    {
                        case 1:
                            price = "Bundle Exclusive";
                            break;
                        case 2:
                            price = "Loot Exclusive";
                            break;
                        case 3:
                            price = "Partner Program Exclusive";
                            break;
                        case 4:
                            price = "Limited";
                            break;
                        default:
                            price = "290";
                            break;
                    }

                    string chromaName = (string?)chroma["name"] ?? "Unknown";
                    string chromaUrl = (string?)chroma["chromaPath"] ?? "N/A";
                    Chroma realChroma = new();

                    realChroma.Id = chromaIdCounter++;
                    realChroma.Price = price;
                    realChroma.Name = chromaName;
                    realChroma.Url = chromaUrl;

                    realChroma.Skin = skin;

                    chromaList.Add(realChroma);
                }


                skin.ChromaList = chromaList;

                skins.Add(skin);
            }
        }

        return skins;
    }
}