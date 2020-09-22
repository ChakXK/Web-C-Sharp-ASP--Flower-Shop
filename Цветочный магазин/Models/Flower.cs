using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Цветочный_магазин.Models
{
    public class Flower
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flower()
        {
            orders = new HashSet<Order>();
        }

        [Display(Name = "Код")]
        public int id { get; set; }

        [StringLength(50)]
        [Display(Name = "Тип")]
        public string type { get; set; }

        [StringLength(150)]
        [Display(Name = "Название")]
        public string name { get; set; }
        [StringLength(1000)]
        [Display(Name = "Описание")]
        public string description { get; set; }
        [Display(Name = "Цена")]
        public double? price { get; set; }

        [StringLength(200)]
        [Display(Name = "Изображение")]
        public string image { get; set; }
        [Display(Name = "Наявность")]
        [Range(0, 1000000, ErrorMessage = "Недопустимое значение")]
        public int availability { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> orders { get; set; }
    }
}