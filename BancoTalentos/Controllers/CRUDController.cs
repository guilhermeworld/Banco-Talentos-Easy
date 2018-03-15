using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BancoTalentos.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;

namespace BancoTalentos.Controllers
{
    public class CRUDController : Controller
    {
        private BancoTalentosEntities db = new BancoTalentosEntities();

        // GET: Candidatoes
        public ActionResult Index()
        {
            return View(db.Candidato.ToList());
        }

        // GET: Candidatoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			CrudModelView myModel = new CrudModelView();
			myModel.candidato = db.Candidato.Find(id);
			myModel.conhecimento = db.Conhecimento.SingleOrDefault(x => x.IdCandidato == id);

			if (myModel == null)
            {
                return HttpNotFound();
            }
            return View(myModel);
        }

        // GET: Candidatoes/Create
        public ActionResult Create()
        {
			return View();
        }

        // POST: Candidatoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CrudModelView myModel)
        {
			myModel = ErroMensagens(myModel);			

			if (ModelState.IsValid)
            {
                db.Candidato.Add(myModel.candidato);
				db.Conhecimento.Add(myModel.conhecimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			else
				ModelState.AddModelError("","Preencha todos os campos que são obrigatórios!");
			

            return View();
        } 

        // GET: Candidatoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			CrudModelView myModel = new CrudModelView();
			myModel.candidato = db.Candidato.Find(id);
			myModel.conhecimento = db.Conhecimento.SingleOrDefault(x => x.IdCandidato == id);

			if (myModel == null)
            {
                return HttpNotFound();
            }
			
			return View(myModel);
        }

        // POST: Candidatoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CrudModelView myModel)
        {

			if (ModelState.IsValid)
            {
                db.Entry(myModel.candidato).State = EntityState.Modified;
				db.Entry(myModel.conhecimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			else
				ModelState.AddModelError("", "Preencha todos os campos que são obrigatórios!");

			return View();
        }

        // GET: Candidatoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato candidato = db.Candidato.Find(id);
            if (candidato == null)
            {
                return HttpNotFound();
            }
            return View(candidato);
        }

        // POST: Candidatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			CrudModelView myModel = new CrudModelView();
			myModel.candidato = db.Candidato.Find(id);
			myModel.conhecimento = db.Conhecimento.SingleOrDefault(x => x.IdCandidato == id);

			db.Candidato.Remove(myModel.candidato);
			db.Conhecimento.Remove(myModel.conhecimento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		public CrudModelView ErroMensagens(CrudModelView myModel)
		{
			var aux = myModel.candidato;
			if (aux.QuatroSeis == false && aux.MenosDeQuatro == false && aux.MaisOito == false && aux.SeisOito == false && aux.FimSemana == false)
				ModelState.AddModelError("Disponivel", "Este campo é obrigatório!");

			if (aux.PComercial == false && aux.PMadrugada == false && aux.PManha == false && aux.PTarde == false && aux.PNoite == false)
				ModelState.AddModelError("Periodo", "Este campo é obrigatório!");

			if (myModel.conhecimento.Ionic == 0)
				ModelState.AddModelError("Ionic", "Este campo é obrigatório!");
			else
				myModel.conhecimento.Ionic--;

			if (myModel.conhecimento.Android == 0)
				ModelState.AddModelError("Android", "Este campo é obrigatório!");
			else
				myModel.conhecimento.Android--;

			if (myModel.conhecimento.IOS == 0)
				ModelState.AddModelError("IOS", "Este campo é obrigatório!");
			else
				myModel.conhecimento.IOS--;

			if (myModel.conhecimento.Bootstrap == 0)
				ModelState.AddModelError("Bootstrap", "Este campo é obrigatório!");
			else
				myModel.conhecimento.Bootstrap--;

			if (myModel.conhecimento.JQuery == 0)
				ModelState.AddModelError("JQuery", "Este campo é obrigatório!");
			else
				myModel.conhecimento.JQuery--;

			if (myModel.conhecimento.AngularJS == 0)
				ModelState.AddModelError("AngularJS", "Este campo é obrigatório!");
			else
				myModel.conhecimento.AngularJS--;

			if (myModel.conhecimento.ASPMVC == 0)
				ModelState.AddModelError("ASPMVC", "Este campo é obrigatório!");
			else
				myModel.conhecimento.ASPMVC--;

			if (myModel.conhecimento.PHP == 0)
				ModelState.AddModelError("PHP", "Este campo é obrigatório!");
			else
				myModel.conhecimento.PHP--;

			if (myModel.conhecimento.Vue == 0)
				ModelState.AddModelError("Vue", "Este campo é obrigatório!");
			else
				myModel.conhecimento.Vue--;

			if (myModel.conhecimento.Wordpress == 0)
				ModelState.AddModelError("Wordpress", "Este campo é obrigatório!");
			else
				myModel.conhecimento.Wordpress--;

			return myModel;
		}

		public FileResult Download(int id)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				CrudModelView myModel = new CrudModelView();
				myModel.candidato = db.Candidato.Find(id);
				myModel.conhecimento = db.Conhecimento.SingleOrDefault(model => model.IdCandidato == id);

				//Preparando PDF
				Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
				PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
				Paragraph paragrafo = new Paragraph();
				paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
				Font normal = new Font(Font.NORMAL, 14, (int)System.Drawing.FontStyle.Regular);
				Font subtitulo = new Font(Font.NORMAL, 16, (int)System.Drawing.FontStyle.Bold);
				Font titulo = new Font(Font.NORMAL, 18, (int)System.Drawing.FontStyle.Bold);

				myModel = Verificacoes(myModel);

				//Adicionando dados ao PDF
				paragrafo.Font = titulo;
				paragrafo.Add("Dados Pessoais \n \n");
				paragrafo.Font = normal;
				paragrafo.Add("Nome: " + myModel.candidato.Nome + "\n");
				paragrafo.Add("Email: " + myModel.candidato.Email + "\n");
				paragrafo.Add("Skype: " + myModel.candidato.Skype + "\n");
				paragrafo.Add("Whatsapp: " + myModel.candidato.Whatsapp + "\n");
				paragrafo.Add("Linkedin: " + myModel.candidato.Linkedin + "\n");
				paragrafo.Add("Cidade: " + myModel.candidato.Cidade + "\n");
				paragrafo.Add("Estado: " + myModel.candidato.Estado + "\n");
				paragrafo.Add("Portifólio: " +myModel.candidato.Portfolio + "\n \n");
				paragrafo.Font = subtitulo;
				paragrafo.Add("Disponibilidade para trabalhar atualmente: \n");
				paragrafo.Font = normal;
				if (myModel.candidato.MenosDeQuatro)
					paragrafo.Add("Menos de quatro horas por dia \n");
				if (myModel.candidato.QuatroSeis)
					paragrafo.Add("Entre quatro e seis horas por dia \n");
				if (myModel.candidato.SeisOito)
					paragrafo.Add("Entre seis e oito horas por dia \n");
				if (myModel.candidato.MaisOito)
					paragrafo.Add("Mais de oito horas por dia \n");
				if (myModel.candidato.FimSemana)
					paragrafo.Add("Somente nos fim de semana \n");
				paragrafo.Add("\n");
				paragrafo.Font = subtitulo;
				paragrafo.Add("Melhor horário para trabalhar: \n");
				paragrafo.Font = normal;
				if (myModel.candidato.PManha)
					paragrafo.Add("Período da manhã (08:00 ás 12:00) \n");
				if (myModel.candidato.PTarde)
					paragrafo.Add("Período da tarde (13:00 ás 18:00) \n");
				if (myModel.candidato.PNoite)
					paragrafo.Add("Período da noite (19:00 as 22:00) \n");
				if (myModel.candidato.PMadrugada)
					paragrafo.Add("Durante a madrugada (22:00 em diante) \n");
				if (myModel.candidato.PComercial)
					paragrafo.Add("Horário comercial (das 9:00h às 18:00h). \n");
				paragrafo.Add("Pretensão salarial por hora: " + myModel.candidato.SalarioHora +" Reais. \n \n");
				paragrafo.Font = titulo;
				paragrafo.Add("Dados Bancários \n \n");
				paragrafo.Font = normal;
				paragrafo.Add("Nome: " + myModel.candidato.NomeClienteBanco + "\n");
				paragrafo.Add("CPF: " + myModel.candidato.CPF + "\n");
				paragrafo.Add("Nome do Banco: " + myModel.candidato.NomeBanco + "\n");
				paragrafo.Add("Agência: " + myModel.candidato.Agencia + "\n");
				paragrafo.Add("Número da conta: " + myModel.candidato.NumeroConta + "\n");
				paragrafo.Font = subtitulo;
				paragrafo.Add("Tipo da conta \n");
				paragrafo.Font = normal;
				if (myModel.candidato.ContaCorrente==false && myModel.candidato.ContaPoupanca==false)
					paragrafo.Add("Não informado \n");
				if (myModel.candidato.ContaPoupanca)
					paragrafo.Add("Conta Poupança \n");
				if (myModel.candidato.ContaCorrente)
					paragrafo.Add("Conta Corrente \n");
				paragrafo.Add("\n");
				paragrafo.Font = titulo;
				paragrafo.Add("Conhecimentos (Numa escala de 0 a 5) \n \n");
				paragrafo.Font = normal;
				paragrafo.Add("Nível em Ionic: " + myModel.conhecimento.Ionic + "\n");
				paragrafo.Add("Nível em Android: " + myModel.conhecimento.Android + "\n");
				paragrafo.Add("Nível em IOS: " + myModel.conhecimento.IOS + "\n");

				if (myModel.conhecimento.HTML == null)
					paragrafo.Add("Nível em HTML: Não informado \n");
				else
					paragrafo.Add("Nível em HTML: " + myModel.conhecimento.HTML + "\n");

				if (myModel.conhecimento.CSS == null)
					paragrafo.Add("Nível em CSS: Não informado \n");
				else
					paragrafo.Add("Nível em CSS: " + myModel.conhecimento.CSS + "\n");

				paragrafo.Add("Nível em Bootstrap: " + myModel.conhecimento.Bootstrap + "\n");
				paragrafo.Add("Nível em JQuery: " + myModel.conhecimento.JQuery + "\n");
				paragrafo.Add("Nível em AngularJS: " + myModel.conhecimento.AngularJS + "\n");

				if (myModel.conhecimento.Java == null)
					paragrafo.Add("Nível em Java: Não informado \n");
				else
					paragrafo.Add("Nível em Java: " + myModel.conhecimento.Java + "\n");

				paragrafo.Add("Nível em ASP.Net MVC: " + myModel.conhecimento.ASPMVC + "\n");

				if (myModel.conhecimento.C == null)
					paragrafo.Add("Nível em C: Não informado \n");
				else
					paragrafo.Add("Nível em C: " + myModel.conhecimento.C + "\n");
				if (myModel.conhecimento.CPP == null)
					paragrafo.Add("Nível em C++: Não informado \n");
				else
					paragrafo.Add("Nível em C++: " + myModel.conhecimento.CPP + "\n");
				if (myModel.conhecimento.Cake == null)
					paragrafo.Add("Nível em Cake: Não informado \n");
				else
					paragrafo.Add("Nível em Cake: " + myModel.conhecimento.Cake + "\n");
				if (myModel.conhecimento.Django == null)
					paragrafo.Add("Nível em Django: Não informado \n");
				else
					paragrafo.Add("Nível em Django: " + myModel.conhecimento.Django + "\n");
				if (myModel.conhecimento.Magento == null)
					paragrafo.Add("Nível em Magento: Não informado \n");
				else
					paragrafo.Add("Nível em Magento: " + myModel.conhecimento.Magento + "\n");

				paragrafo.Add("Nível em PHP: " + myModel.conhecimento.PHP + "\n");
				paragrafo.Add("Nível em Vue: " + myModel.conhecimento.Vue + "\n");
				paragrafo.Add("Nível em Wordpress: " + myModel.conhecimento.Wordpress + "\n");

				if (myModel.conhecimento.Python == null)
					paragrafo.Add("Nível em Python: Não informado \n");
				else
					paragrafo.Add("Nível em Python: " + myModel.conhecimento.Python + "\n");
				if (myModel.conhecimento.Ruby == null)
					paragrafo.Add("Nível em Ruby: Não informado \n");
				else
					paragrafo.Add("Nível em Ruby: " + myModel.conhecimento.Ruby + "\n");
				if (myModel.conhecimento.SQLServer == null)
					paragrafo.Add("Nível em SQL Server: Não informado \n");
				else
					paragrafo.Add("Nível em SQL Server: " + myModel.conhecimento.SQLServer + "\n");
				if (myModel.conhecimento.MySQL == null)
					paragrafo.Add("Nível em MySQL: Não informado \n");
				else
					paragrafo.Add("Nível em MySQL: " + myModel.conhecimento.MySQL + "\n");
				if (myModel.conhecimento.Salesforce == null)
					paragrafo.Add("Nível em Java: Não informado \n");
				else
					paragrafo.Add("Nível em Salesforce: " + myModel.conhecimento.Salesforce + "\n");
				if (myModel.conhecimento.Photoshop == null)
					paragrafo.Add("Nível em Photoshop: Não informado \n");
				else
					paragrafo.Add("Nível em Photoshop: " + myModel.conhecimento.Photoshop + "\n");
				if (myModel.conhecimento.Illustrator == null)
					paragrafo.Add("Nível em Illustrator: Não informado \n");
				else
					paragrafo.Add("Nível em Illustrator: " + myModel.conhecimento.Illustrator + "\n");
				if (myModel.conhecimento.SEO == null)
					paragrafo.Add("Nível em SEO: Não informado \n");
				else
					paragrafo.Add("Nível em SEO: " + myModel.conhecimento.SEO + "\n");
				paragrafo.Add("Outros conhecimentos: " + myModel.conhecimento.Outro + "\n \n");
				paragrafo.Add("Link CRUD: " + myModel.candidato.Link + "\n");

				pdfDoc.Open();
				pdfDoc.Add(paragrafo);
				pdfDoc.Close();
				return File(stream.ToArray(), "application/pdf", myModel.candidato.Nome+".pdf");
			}
		}
		public CrudModelView Verificacoes( CrudModelView myModel)
		{
			string aux = "Não informado";
			if (myModel.candidato.Linkedin == null)
				myModel.candidato.Linkedin = aux;

			if (myModel.candidato.Portfolio == null)
				myModel.candidato.Portfolio = aux;

			if (myModel.candidato.NomeClienteBanco == null)
				myModel.candidato.NomeClienteBanco = aux;

			if (myModel.candidato.NomeBanco == null)
				myModel.candidato.NomeBanco = aux;

			if (myModel.candidato.CPF == null)
				myModel.candidato.CPF = aux;

			if (myModel.candidato.Agencia == null)
				myModel.candidato.Agencia = aux;

			if (myModel.candidato.NumeroConta == null)
				myModel.candidato.NumeroConta = aux;

			if (myModel.candidato.Link == null)
				myModel.candidato.Link = aux;

			if (myModel.conhecimento.Outro == null)
				myModel.conhecimento.Outro = aux;


			return myModel;
		}
	}
}
