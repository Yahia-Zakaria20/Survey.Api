using Survey.Basket.Api.CustomAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Survey.Basket.Api.Dto
{
    public class PollDto
    {
        public int Id { get; set; }

        public string Titel { get; set; }

        public string Summary { get; set; }

        public bool ISpublished { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
