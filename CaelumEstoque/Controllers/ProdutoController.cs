using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System.Collections.Generic;
using System.Web.Mvc;
namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            var dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            ViewBag.Produtos = produtos;
            return View();
        }

        [HttpPost]
        public ActionResult Form()
        {
            CategoriasDAO categoriasDAO = new CategoriasDAO();
            ViewBag.Produto = new Produto();
            ViewBag.Categorias = categoriasDAO.Lista();
            return View();
        }

        public ActionResult Adiciona(Produto produto)
        {
            var idDaInformatica = 1;
            if (produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.InformaticaComPrecoInvalido", "Produtos da categoria informática devem ter preço maior do que 100");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Produto = produto;
                var categoriasDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();
                return View("Form");

            }

        }
    }
}