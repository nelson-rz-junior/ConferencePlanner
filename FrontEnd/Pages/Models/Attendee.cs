using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Pages.Models;

public class Attendee : ConferencePlanner.DTO.Attendee
{
    [DisplayName("First Name")]
    public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

    [DisplayName("Last Name")]
    public override string LastName { get => base.LastName; set => base.LastName = value; }

    [DisplayName("E-mail address")]
    [DataType(DataType.EmailAddress)]
    public override string EmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }
}
