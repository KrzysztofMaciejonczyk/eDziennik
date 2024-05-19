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

  [Route("odata/Baza/Osobas")]
  public partial class OsobasController : ODataController
  {
    private Data.BazaContext context;

    public OsobasController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Osobas
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Osoba> GetOsobas()
    {
      var items = this.context.Osobas.AsQueryable<Models.Baza.Osoba>();
      this.OnOsobasRead(ref items);

      return items;
    }

    partial void OnOsobasRead(ref IQueryable<Models.Baza.Osoba> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Osobas(osoba_id={osoba_id})")]
    public SingleResult<Osoba> GetOsoba(int key)
    {
        var items = this.context.Osobas.Where(i=>i.osoba_id == key);
        this.OnOsobasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOsobasGet(ref IQueryable<Models.Baza.Osoba> items);

    partial void OnOsobaDeleted(Models.Baza.Osoba item);

    [HttpDelete("/odata/Baza/Osobas(osoba_id={osoba_id})")]
    public IActionResult DeleteOsoba(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Osobas
                .Where(i => i.osoba_id == key)
                .Include(i => i.Rodzics)
                .Include(i => i.Nauczyciels)
                .Include(i => i.Uczens)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnOsobaDeleted(itemToDelete);
            this.context.Osobas.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOsobaUpdated(Models.Baza.Osoba item);

    [HttpPut("/odata/Baza/Osobas(osoba_id={osoba_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOsoba(int key, [FromBody]Models.Baza.Osoba newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.osoba_id != key))
            {
                return BadRequest();
            }

            this.OnOsobaUpdated(newItem);
            this.context.Osobas.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Osobas.Where(i => i.osoba_id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Osobas(osoba_id={osoba_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOsoba(int key, [FromBody]Delta<Models.Baza.Osoba> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Osobas.Where(i => i.osoba_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnOsobaUpdated(itemToUpdate);
            this.context.Osobas.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Osobas.Where(i => i.osoba_id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOsobaCreated(Models.Baza.Osoba item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Osoba item)
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

            this.OnOsobaCreated(item);
            this.context.Osobas.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Baza/Osobas/{item.osoba_id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
