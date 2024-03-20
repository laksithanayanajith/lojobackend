using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lojobackend.DbContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace lojobackend.Models
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		public string url { get; set; } = string.Empty;

		public int ItemId { get; set; }

		public Item? Item { get; set; }

		public int ColorId { get; set; }

		public int SizeId { get; set; }
	}
}