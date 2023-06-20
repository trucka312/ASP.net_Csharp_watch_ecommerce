﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebWatchOnline.Models.Entity
{
    [Table("tb_Category")]
	public class Category: CommonAbstract
	{
        public Category() 
        {
            //một danh mục có thể ở nhiều tin tức
            this.News = new HashSet<News>();
        }
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục hãng không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
		//public string TypeCode { get; set; }
		//public string Link { get; set; }


		public string Description { get; set; }
		[StringLength(150)]
		public string SeoTitle { get; set; }
		[StringLength(150)]
		public string SeoDescription { get; set; }
		[StringLength(150)]

		public string SeoKeywords { get; set; }
        public int Position { get; set; }
		public bool IsActive { get; set; }


		public ICollection<News> News { get; set; }
		public ICollection<Posts> Posts { get; set; }


    }
}