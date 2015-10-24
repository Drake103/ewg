using System;
using System.IO;
using System.Text;
using EWG.Infrastructure.Services.ReplayParsing;
using EWG.Shared.Dto.ReplayParsing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EWG.Infrastructure.Services.Impl.ReplayParsing
{
    public class ReplayParser : IReplayParser
    {
        private const int JsonBegin = 56;

        public ReplayParsedDto ParseFile(Stream replayFile)
        {
            var result = GetJsonPart(replayFile);

            var replayObject = JObject.Parse(result);

            var gameSection = replayObject.SelectToken("game").ToString();

            ReplayParsedDto parsedReplayParsedDto = JsonConvert.DeserializeObject<ReplayParsedDto>(gameSection);

            var properties = replayObject.Properties();

            foreach (var property in properties)
            {
                if (property.Name.Contains("player_"))
                {
                    var playerSection = replayObject.SelectToken(property.Name).ToString();
                    PlayerParsedDto parsedPlayerParsedDto = JsonConvert.DeserializeObject<PlayerParsedDto>(playerSection);
                    parsedPlayerParsedDto.PlayerNumber = int.Parse(property.Name.Substring(7));

                    parsedReplayParsedDto.Players.Add(parsedPlayerParsedDto);
                }
            }

            return parsedReplayParsedDto;
        }

        private string GetJsonPart(Stream stream)
        {
            stream.Seek(JsonBegin, SeekOrigin.Begin);

            const int searchStringLength = 6;

            using (var streamReader = new StreamReader(stream))
            {
                var buffer = new char[searchStringLength];

                int charsCount = 0;

                bool jsonEndIsFound = false;

                while (!streamReader.EndOfStream)
                {
                    streamReader.ReadBlock(buffer, 0, searchStringLength);

                    if (new string(buffer).Equals("}}star"))
                    {
                        jsonEndIsFound = true;
                        break;
                    }

                    streamReader.BaseStream.Seek(JsonBegin + charsCount, SeekOrigin.Begin);

                    charsCount++;

                    streamReader.DiscardBufferedData();
                }

                if (!jsonEndIsFound)
                {
                    throw new NotSupportedException();
                }

                stream.Seek(JsonBegin, SeekOrigin.Begin);

                var bytesLengths = (charsCount * 2);
                var jsonBytes = new byte[bytesLengths];
                stream.Read(jsonBytes, 0, bytesLengths);

                var str = Encoding.UTF8.GetString(jsonBytes);

                return str.Substring(0, str.LastIndexOf("}}star", StringComparison.InvariantCulture) + 2);
            }
        }
    }
}