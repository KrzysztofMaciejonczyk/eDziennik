using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace EDziennik.Controllers.Baza
{
  using Models;
  using Data;
  using Models.Baza;

  [Route("odata/Baza/Klasas")]
  public partial class KlasasController : ODataController
  {
    private Data.BazaContext context;

    public KlasasController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Klasas
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Klasa> GetKlasas()
    {
      var items = this.context.Klasas.AsQueryable<Models.Baza.Klasa>();
      this.OnKlasasRead(ref items);

      return items;
    }

    partial void OnKlasasRead(ref IQueryable<Models.Baza.Klasa> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Klasas(klasa_id={klasa_id})")]
    public SingleResult<Klasa> GetKlasa(int key)
    {
        var items = this.context.Klasas.Where(i=>i.klasa_id == key);
        this.OnKlasasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnKlasasGet(ref IQueryable<Models.Baza.Klasa> items);

    partial void OnKlasaDeleted(Models.Baza.Klasa item);

    [HttpDelete("/odata/Baza/Klasas(klasa_id={klasa_id})")]
    public IActionResult DeleteKlasa(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Klasas
                .Where(i => i.klasa_id == key)
                .Include(i => i.Uczens)
                .Include(i => i.Przedmiots)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnKlasaDeleted(itemToDelete);
            this.context.Klasas.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnKlasaUpdated(Models.Baza.Klasa item);

    [HttpPut("/odata/Baza/Klasas(klasa_id={klasa_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutKlasa(int key, [FromBody]Models.Baza.Klasa newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.klasa_id != key))
            {
                return BadRequest();
            }

            this.OnKlasaUpdated(newItem);
            this.context.Klasas.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Klasas.Where(i => i.klasa_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Klasas(klasa_id={klasa_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchKlasa(int key, [FromBody]Delta<Models.Baza.Klasa> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Klasas.Where(i => i.klasa_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnKlasaUpdated(itemToUpdate);
            this.context.Klasas.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Klasas.Where(i => i.klasa_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnKlasaCreated(Models.Baza.Klasa item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Klasa item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnKlasaCreated(item);
            this.context.Klasas.Add(item);
            this.context.SaveChanges();

            var key = item.klasa_id;

            var itemToReturn = this.context.Klasas.Where(i => i.klasa_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel");

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
