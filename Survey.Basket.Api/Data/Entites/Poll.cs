using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace Survey.Basket.Api.Data.Entites
{
    public class Poll:AuditableEntite
    {
        public int Id { get; set; }

        public string Titel { get; set; } =string.Empty;

        public string Summary { get; set; } = string.Empty;

        public bool IsPublished { get; set; }

        public DateOnly StartsAt { get; set; }

        public DateOnly EndsAt { get; set; }

       
    }
}
