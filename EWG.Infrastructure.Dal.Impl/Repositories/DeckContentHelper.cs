using System;
using System.Linq;
using EWG.Shared.Dto.Replays;

namespace EWG.Infrastructure.Dal.Impl.Repositories
{
    internal class DeckContentHelper
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/";

        private static int GetIndex(char c)
        {
            return Alphabet.IndexOf(c);
        }

        public static DeckInfoDto GetDeckInfo(string deckContent)
        {
            NameCodePair nationInfo;
            NameCodePair specInfo;

            GetNationAndSpecInfo(deckContent[0], deckContent[1], out nationInfo, out specInfo);
            
            var deckInfo = new DeckInfoDto
            {
                NationName = nationInfo.Name,
                NationCode = nationInfo.Code,
                SpecName = specInfo.Name,
                SpecCode = specInfo.Code
            };

            return deckInfo;
        }

        private static NameCodePair GetSpecInfo(int specIndex)
        {
            switch (specIndex)
            {
                case 0:
                    return new NameCodePair("Motorized", "motorized");
                case 1:
                    return new NameCodePair("Armored", "armor");
                case 2:
                    return new NameCodePair("Support", "support");
                case 3:
                    return new NameCodePair("Marine", "marine");
                case 4:
                    return new NameCodePair("Mechanized", "mech");
                case 5:
                    return new NameCodePair("Airborne", "airborne");
                case 6:
                    return new NameCodePair("Naval", "naval");
                case 7:
                    return new NameCodePair("General", "general");
                default:
                    return new NameCodePair();
            }
        }

        private static NameCodePair GetNationInfo(char eugenNationCode)
        {
            switch (eugenNationCode)
            {
                case 'B':
                    return new NameCodePair("USA", "usa");
                case 'D':
                    return new NameCodePair("UK", "uk");
                case 'F':
                    return new NameCodePair("France", "fr");
                case 'H':
                    return new NameCodePair("West Germany", "bdr");
                case 'J':
                    return new NameCodePair("Canada", "ca");
                case 'L':
                    return new NameCodePair("Denmark", "dk");
                case 'N':
                    return new NameCodePair("Sweden", "sw");
                case 'P':
                    return new NameCodePair("Norway", "norway");
                case 'R':
                    return new NameCodePair("ANZAC", "anzac");
                case 'T':
                    return new NameCodePair("Japan", "jp");
                case 'V':
                    return new NameCodePair("South Korea", "sk");
                case 'h':
                    return new NameCodePair("East Germany", "ddr");
                case 'j':
                    return new NameCodePair("USSR", "ussr");
                case 'n':
                    return new NameCodePair("Czechoslovakia", "cz");
                case 'l':
                    return new NameCodePair("Poland", "pl");
                case 'p':
                    return new NameCodePair("China", "cn");
                case 'r':
                    return new NameCodePair("North Korea", "nk");
                default:
                    return new NameCodePair();
            }
        }

        private static bool GetNationAndSpecInfo(
            char nationChar,
            char specChar,
            out NameCodePair nationInfo,
            out NameCodePair specInfo
            )
        {
            nationInfo = new NameCodePair();
            specInfo = new NameCodePair();

            var coalitionCodes = new[] { 'W', 'X', 's', 't' };

            if (!coalitionCodes.Contains(nationChar)){
                nationInfo = GetNationInfo(nationChar);

                var specIndex = specChar - 'I';
                specInfo = GetSpecInfo(specIndex);

                return true;
            }

            if (nationChar == 'W')
            {
                if (GetIndex(specChar) >= GetIndex('A') && GetIndex(specChar) <= GetIndex('H'))
                {
                    nationInfo = new NameCodePair("Eurocorps", "eu");
                    var specIndex = GetIndex(specChar) - GetIndex('A');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('I') && GetIndex(specChar) <= GetIndex('P'))
                {
                    nationInfo = new NameCodePair("Scandinavia", "scandi");
                    var specIndex = GetIndex(specChar) - GetIndex('I');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('Q') && GetIndex(specChar) <= GetIndex('X'))
                {
                    nationInfo = new NameCodePair("Commonwealth", "cw");
                    var specIndex = GetIndex(specChar) - GetIndex('Q');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('Y') && GetIndex(specChar) <= GetIndex('f'))
                {
                    nationInfo = new NameCodePair("Blue Dragons", "bd");
                    var specIndex = GetIndex(specChar) - GetIndex('Y');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }
            }

            if (nationChar == 'X')
            {
                if (GetIndex(specChar) >= GetIndex('A') && GetIndex(specChar) <= GetIndex('H'))
                {
                    nationInfo = new NameCodePair("Norad", "norad");
                    var specIndex = GetIndex(specChar) - GetIndex('A');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('I') && GetIndex(specChar) <= GetIndex('P'))
                {
                    nationInfo = new NameCodePair("Bluefor", "bluefor");
                    var specIndex = GetIndex(specChar) - GetIndex('I');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }
            }

            if (nationChar == 's')
            {
                if (GetIndex(specChar) >= GetIndex('g') && GetIndex(specChar) <= GetIndex('n'))
                {
                    nationInfo = new NameCodePair("Red Dragons", "rd");
                    var specIndex = GetIndex(specChar) - GetIndex('g');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('o') && GetIndex(specChar) <= GetIndex('v'))
                {
                    nationInfo = new NameCodePair("NSWP", "nswp");
                    var specIndex = GetIndex(specChar) - GetIndex('o');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }

                if (GetIndex(specChar) >= GetIndex('5') && GetIndex(specChar) <= GetIndex('/'))
                {
                    nationInfo = new NameCodePair("SovKor", "sovkor");
                    var specIndex = GetIndex(specChar) - GetIndex('5');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }
            }

            if (nationChar == 't')
            {
                if (GetIndex(specChar) >= GetIndex('I') && GetIndex(specChar) <= GetIndex('P'))
                {
                    nationInfo = new NameCodePair("Redfor", "redfor");
                    var specIndex = GetIndex(specChar) - GetIndex('I');
                    specInfo = GetSpecInfo(specIndex);
                    return true;
                }
            }

            return false;
        }

        private class NameCodePair
        {
            public NameCodePair()
            {
                
            }

            public NameCodePair(string name, string code)
            {
                Name = name;
                Code = code;
            }

            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}