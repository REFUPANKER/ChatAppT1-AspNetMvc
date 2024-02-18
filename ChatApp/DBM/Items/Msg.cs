using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChatApp.DBM.Items
{
	public class Msg
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int sender { get; set; }
        public string? content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime msgDateTime { get; set; }

        [AllowNull]
        public string chat { get; set; }
    }
}
