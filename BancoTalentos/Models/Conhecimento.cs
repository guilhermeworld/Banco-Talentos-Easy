//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BancoTalentos.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Data.Entity.Infrastructure;

	public partial class Conhecimento
    {
        public int Id { get; set; }
        public int IdCandidato { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int Ionic { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int Android { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int IOS { get; set; }
        public Nullable<int> HTML { get; set; }
        public Nullable<int> CSS { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int Bootstrap { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int JQuery { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int AngularJS { get; set; }
        public Nullable<int> Java { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int ASPMVC { get; set; }
        public Nullable<int> C { get; set; }
        public Nullable<int> CPP { get; set; }
        public Nullable<int> Cake { get; set; }
        public Nullable<int> Django { get; set; }
        public Nullable<int> Magento { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int PHP { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int Vue { get; set; }

		[Required(ErrorMessage = "*Este campo � obrigat�rio!")]
		public int Wordpress { get; set; }
        public Nullable<int> Python { get; set; }
        public Nullable<int> Ruby { get; set; }
        public Nullable<int> SQLServer { get; set; }
        public Nullable<int> MySQL { get; set; }
        public Nullable<int> Salesforce { get; set; }
        public Nullable<int> Photoshop { get; set; }
        public Nullable<int> Illustrator { get; set; }
        public Nullable<int> SEO { get; set; }
        public string Outro { get; set; }
    
        public virtual Candidato Candidato { get; set; }

	}
}