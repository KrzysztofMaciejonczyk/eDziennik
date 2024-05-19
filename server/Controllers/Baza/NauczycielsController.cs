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

  [Route("odata/Baza/Nauczyciels")]
  public partial class NauczycielsController : ODataController
  {
    private Data.BazaContext context;

    public NauczycielsController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Nauczyciels
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Nauczyciel> GetNauczyciels()
    {
      var items = this.context.Nauczyciels.AsQueryable<Models.Baza.Nauczyciel>();
      this.OnNauczycielsRead(ref items);

      return items;
    }

    partial void OnNauczycielsRead(ref IQueryable<Models.Baza.Nauczyciel> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Nauczyciels(nauczyciel_id={nauczyciel_id})")]
    public SingleResult<Nauczyciel> GetNauczyciel(int key)
    {
        var items = this.context.Nauczyciels.Where(i=>i.nauczyciel_id == key);
        this.OnNauczycielsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnNauczycielsGet(ref IQueryable<Models.Baza.Nauczyciel> items);

    partial void OnNauczycielDeleted(Models.Baza.Nauczyciel item);

    [HttpDelete("/odata/Baza/Nauczyciels(nauczyciel_id={nauczyciel_id})")]
    public IActionResult DeleteNauczyciel(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Nauczyciels
                .Where(i => i.nauczyciel_id == key)
                .Include(i => i.Klasas)
                .Include(i => i.Przedmiots)
                .Include(i => i.Uwagas)
                .Include(i => i.Ocenas)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnNauczycielDeleted(itemToDelete);
            this.context.Nauczyciels.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnNauczycielUpdated(Models.Baza.Nauczyciel item);

    [HttpPut("/odata/Baza/Nauczyciels(nauczyciel_id={nauczyciel_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutNauczyciel(int key, [FromBody]Models.Baza.Nauczyciel newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.nauczyciel_id != key))
            {
                return BadRequest();
            }

            this.OnNauczycielUpdated(newItem);
            this.context.Nauczyciels.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Nauczyciels.Where(i => i.nauczyciel_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Nauczyciels(nauczyciel_id={nauczyciel_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchNauczyciel(int key, [FromBody]Delta<Models.Baza.Nauczyciel> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Nauczyciels.Where(i => i.nauczyciel_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnNauczycielUpdated(itemToUpdate);
            this.context.Nauczyciels.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Nauczyciels.Where(i => i.nauczyciel_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnNauczycielCreated(Models.Baza.Nauczyciel item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Nauczyciel item)
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

            this.OnNauczycielCreated(item);
            this.context.Nauczyciels.Add(item);
            this.context.SaveChanges();

            var key = item.nauczyciel_id;

            var itemToReturn = this.context.Nauczyciels.Where(i => i.nauczyciel_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");

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
