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

  [Route("odata/Baza/Uczens")]
  public partial class UczensController : ODataController
  {
    private Data.BazaContext context;

    public UczensController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Uczens
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Uczen> GetUczens()
    {
      var items = this.context.Uczens.AsQueryable<Models.Baza.Uczen>();
      this.OnUczensRead(ref items);

      return items;
    }

    partial void OnUczensRead(ref IQueryable<Models.Baza.Uczen> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Uczens(uczen_id={uczen_id})")]
    public SingleResult<Uczen> GetUczen(int key)
    {
        var items = this.context.Uczens.Where(i=>i.uczen_id == key);
        this.OnUczensGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnUczensGet(ref IQueryable<Models.Baza.Uczen> items);

    partial void OnUczenDeleted(Models.Baza.Uczen item);

    [HttpDelete("/odata/Baza/Uczens(uczen_id={uczen_id})")]
    public IActionResult DeleteUczen(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Uczens
                .Where(i => i.uczen_id == key)
                .Include(i => i.Uwagas)
                .Include(i => i.Ocenas)
                .Include(i => i.Obecnoscs)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnUczenDeleted(itemToDelete);
            this.context.Uczens.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUczenUpdated(Models.Baza.Uczen item);

    [HttpPut("/odata/Baza/Uczens(uczen_id={uczen_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutUczen(int key, [FromBody]Models.Baza.Uczen newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.uczen_id != key))
            {
                return BadRequest();
            }

            this.OnUczenUpdated(newItem);
            this.context.Uczens.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Uczens.Where(i => i.uczen_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba,Rodzic,Klasa");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Uczens(uczen_id={uczen_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchUczen(int key, [FromBody]Delta<Models.Baza.Uczen> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Uczens.Where(i => i.uczen_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnUczenUpdated(itemToUpdate);
            this.context.Uczens.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Uczens.Where(i => i.uczen_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba,Rodzic,Klasa");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUczenCreated(Models.Baza.Uczen item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Uczen item)
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

            this.OnUczenCreated(item);
            this.context.Uczens.Add(item);
            this.context.SaveChanges();

            var key = item.uczen_id;

            var itemToReturn = this.context.Uczens.Where(i => i.uczen_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Osoba,Rodzic,Klasa");

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
