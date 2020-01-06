using AspNetMVConsome.ProdutoServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVConsome.Controllers
{
    public class ProdutoController : Controller
    {
        readonly ProdutoServiceClient _wcf = new ProdutoServiceClient();

        // GET: Produto
        public ActionResult Index()
        {
            List<Produto> produtos = _wcf.FindAll().ToList();
            return View(produtos);
        }


        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            Produto produto = _wcf.Find(id);
            return View(produto);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            Produto produto = new Produto();
            //ViewBag.CategoriaId = new SelectList(_categoriaApp.GetAll(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _wcf.New(produto);
                return RedirectToAction("Index");
            }
            //ViewBag.CategoriaId = new SelectList(_categoriaApp.GetAll(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            Produto produto = _wcf.Find(id);
            //ViewBag.CategoriaId = new SelectList(_categoriaApp.GetAll(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _wcf.Update(produto);
                return RedirectToAction("Index");
            }
            //ViewBag.CategoriaId = new SelectList(_categoriaApp.GetAll(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            Produto produto = _wcf.Find(id);
            return View(produto);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Produto produto = _wcf.Find(id);
            _wcf.Delete(id);
            return RedirectToAction("Index");
        }

    }
}