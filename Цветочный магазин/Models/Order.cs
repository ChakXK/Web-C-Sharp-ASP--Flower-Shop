using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Цветочный_магазин.Models
{
    public class Order
    {
        [Display(Name = "Номер заказа")]
        public int id { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime? date { get; set; }

        [StringLength(20)]
        [Display(Name = "Имя")]
        [Required]
        public string name { get; set; }

        [StringLength(50)]
        [Display(Name = "Фамилия")]
        [Required]
        public string surname { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression(@"((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}", ErrorMessage = "Некорректный номер телефона")]
        [Display(Name = "Телефон")]
        public string phone { get; set; }

        public int? id_flower { get; set; }
        [Display(Name = "Растение")]
        public virtual Flower flower { get; set; }

        public string id_user { get; set; }
        [Display(Name = "Статус")]
        public virtual ApplicationUser User { get; set; }
    }
}