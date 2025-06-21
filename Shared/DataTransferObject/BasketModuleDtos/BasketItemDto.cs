using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.BasketModuleDtos
{
    public  class BasketItemDto
    {
        // validation not written in model it written in Dtos

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1,150)]
        public int Quantity { get; set; }

    }
}
