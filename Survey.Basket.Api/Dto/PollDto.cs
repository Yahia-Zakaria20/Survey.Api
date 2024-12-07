using Survey.Basket.Api.CustomAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Survey.Basket.Api.Dto
{

    public class PollDto
    {
        public int Id { get; set; }

        public string Titel { get; set; }= string.Empty;

        public string Summary { get; set; } = string.Empty;

        public bool ISpublished { get; set; }

        public DateOnly StartsAt { get; set; }

        public DateOnly EndsAt { get; set; }
    }
}
