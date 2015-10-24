using System;

namespace EWG.Shared.Dto.Dictionaries
{
    public class GenericDictionaryItemDto : BaseDto
    {
        public string Name { get; set; }
        public string ResourceCode { get; set; }
        public string PublicCode { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}