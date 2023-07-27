using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class BookingRequest
    {
        public Guid Request_Id { get; set; }
		[Required]
		public Guid Book_Id { get; set; }
		[Required]
		public int Category_Id { get; set; }
		[Required]
		public string Request_Image_Url { get; set; }
		[Required]
		public string Request_Book_Name { get; set; }
		[Required]
		public int Request_Quantity { get; set; }
		[Required]
		public float Request_Price { get; set; }
		[Required]
		public float Request_Amount { get; set; }
        public DateTime Request_Date { get; set; }
        public DateTime Request_Date_Done { get; set; }
        public string Request_Note { get; set; }
        public bool Is_RequestBook_Status { get; set; }
        public int Is_Request_Status { get; set; }
        public Book Book { get; set; }
    }
}
