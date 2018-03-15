using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTalentos.Models
{
	public class CrudModelView
	{
		public Candidato candidato { get; set; } = new Candidato();
		public Conhecimento conhecimento { get; set; } = new Conhecimento();
	}
}