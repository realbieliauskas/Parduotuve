using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parduotuve.Data;
using Parduotuve.Data.Entities;

namespace Parduotuve.Services
{
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
            if (await _context.Skins.AnyAsync())
            {
                return;
            }

            var skins = GetSkins();

            if (skins != null)
            {
                await _context.Skins.AddRangeAsync(skins);
                await _context.SaveChangesAsync();
            }
        }
        private List<Skin>? GetSkins()
        {
            string champions = _httpClient.GetStringAsync("https://cdn.communitydragon.org/latest/champions").Result;
            List<string> keys;
            List<string> secondaryKeys;

            JArray? array = JsonConvert.DeserializeObject<JArray>(champions);

            if (array == null)
            {
                return null;
            }

            keys = array.Select((val) => (string?)val["key"] ?? "None").Where(key => key != "None").ToList();


            List<Skin> skins = new List<Skin>();
            int idCounter = 1;

            for (int x = 0; x < keys.Count; x++)
            {
                string championData = _httpClient.GetStringAsync($"https://cdn.communitydragon.org/latest/champion/{keys[x]}/data").Result;
                string secondaryData;

                try
                {
                    secondaryData = _httpClient.GetStringAsync($"https://cdn.merakianalytics.com/riot/lol/resources/latest/en-US/champions/{keys[x]}.json").Result;
                }
                catch
                {
                    secondaryData = _httpClient.GetStringAsync($"https://cdn.merakianalytics.com/riot/lol/resources/latest/en-US/champions/{string.Join(null, keys[x].Select((x, index) => index == 0 ? char.ToUpper(x) : char.ToLower(x)).ToList())}.json").Result;
                }


                JObject? parsedData = JsonConvert.DeserializeObject<JObject>(championData);
                JObject? secondaryParsed = JsonConvert.DeserializeObject<JObject>(secondaryData);
                if (parsedData == null || secondaryParsed == null)
                {
                    continue;
                }

                JArray skinData = (JArray?)parsedData["skins"] ?? new JArray();
                JArray secondarySkinData = (JArray?)secondaryParsed["skins"] ?? new JArray();

                if (skinData.Count <= 1 || secondarySkinData.Count <= 1)
                {
                    continue;
                }

                skinData.RemoveAt(0);
                secondarySkinData.RemoveAt(0);

                for (int y = 0; y < skinData.Count; y++)
                {
                    Skin skin = new Skin();
                    skin.Id = idCounter;
                    idCounter++;
                    skin.ChampionName = (string?)parsedData["name"];
                    skin.Name = (string?)skinData[y]["name"];

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

                    JArray chromaArray = ((JArray?)secondarySkinData[y]["chromas"]) ?? new JArray();

                    List<(string price, string name, string url)> chromas = new List<(string price, string name, string url)>();

                    foreach (var chroma in chromaArray)
                    {
                        if (chroma is JToken)
                        {
                            continue;
                        }
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
                        string chromaName = ((string?)chroma["name"]) ?? "Unknown";
                        string chromaUrl = ((string?)chroma["chromaPath"]) ?? "N/A";

                        chromas.Add((price, chromaName, chromaUrl));
                    }

                    skin.ChromaURLs = "";
                    skin.Chromas = "";
                    skin.ChromaPrices = "";
                    foreach (var chroma in chromas)
                    {
                        skin.ChromaURLs += chroma.url + "[]";
                        skin.Chromas += chroma.name + ";";
                        skin.ChromaPrices += chroma.price + ";";
                    }

                    skin.ChromaURLs = skin.ChromaURLs.TrimEnd(new char[] { '[', ']' });
                    skin.Chromas = skin.Chromas.TrimEnd(';');
                    skin.ChromaPrices = skin.ChromaPrices.TrimEnd(';');
                    skins.Add(skin);
                }
            }
            return skins;
        }
    }
}
