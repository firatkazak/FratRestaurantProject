using System.ComponentModel.DataAnnotations;

namespace FratRestaurantProject.Data.Entities;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter name..")]
    public string Name { get; set; }
}
