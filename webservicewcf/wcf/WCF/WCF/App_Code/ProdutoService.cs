using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProdutoService" in code, svc and config file together.
public class ProdutoService : IProdutoService
{
    private wcfEntities _db; //my EF Context

    public ProdutoService()
    {
        _db = new wcfEntities();
        _db.Configuration.ProxyCreationEnabled = false;
    }

    public bool Delete(int id)
    {
        Produto produto = _db.Produto.Where(p => p.ProdutoId == id).First();
        _db.Set<Produto>().Remove(produto);
        _db.SaveChanges();
        return true;
    }    

    public Produto Find(int id)
    {
        return _db.Produto.Where(p => p.ProdutoId == id).First();
    }

    public List<Produto> FindAll()
    {
        return _db.Produto.Include("Cliente").ToList();
    }

    public Produto New(Produto produto)
    {
        _db.Produto.Add(produto);
        _db.SaveChanges();
        return produto;
    }

    public Produto Update(Produto produto)
    {
        _db.Entry(produto).State = EntityState.Modified;
        _db.SaveChanges();
        return produto;
    }
}
