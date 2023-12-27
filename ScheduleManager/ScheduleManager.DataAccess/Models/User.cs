using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ScheduleManager.DataAccess.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required, MaxLength(50)]
        public virtual string FirstName { get; set; }

        [Required, MaxLength(50)]
        public virtual string LastName { get; set; }
    }
}
