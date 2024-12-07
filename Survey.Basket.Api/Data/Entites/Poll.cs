using Microsoft.VisualBasic;

namespace Survey.Basket.Api.Data.Entites
{
    public class Poll:BaseEntite
    {
        public string Titel { get; set; } =string.Empty;

        public string Summary { get; set; } = string.Empty;

        public bool IsPublished { get; set; }

        public DateOnly StartsAt { get; set; }

        public DateOnly EndsAt { get; set; }

    }
}
