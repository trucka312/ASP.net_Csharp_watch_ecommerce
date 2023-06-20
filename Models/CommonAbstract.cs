using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWatchOnline.Models
{
	public abstract class CommonAbstract
	{
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public string UpdateBy { get; set; }
	}
}