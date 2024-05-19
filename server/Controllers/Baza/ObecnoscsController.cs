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

  [Route("odata/Baza/Obecnoscs")]
  public partial class ObecnoscsController : ODataController
  {
    private Data.BazaContext context;

    public ObecnoscsController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Obecnoscs
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Obecnosc> GetObecnoscs()
    {
      var items = this.context.Obecnoscs.AsQueryable<Models.Baza.Obecnosc>();
      this.OnObecnoscsRead(ref items);

      return items;
    }

    partial void OnObecnoscsRead(ref IQueryable<Models.Baza.Obecnosc> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Obecnoscs(ocena_id={ocena_id})")]
    public SingleResult<Obecnosc> GetObecnosc(int key)
    {
        var items = this.context.Obecnoscs.Where(i=>i.ocena_id == key);
        this.OnObecnoscsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnObecnoscsGet(ref IQueryable<Models.Baza.Obecnosc> items);

    partial void OnObecnoscDeleted(Models.Baza.Obecnosc item);

    [HttpDelete("/odata/Baza/Obecnoscs(ocena_id={ocena_id})")]
    public IActionResult DeleteObecnosc(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Obecnoscs
                .Where(i => i.ocena_id == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnObecnoscDeleted(itemToDelete);
            this.context.Obecnoscs.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnObecnoscUpdated(Models.Baza.Obecnosc item);

    [HttpPut("/odata/Baza/Obecnoscs(ocena_id={ocena_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutObecnosc(int key, [FromBody]Models.Baza.Obecnosc newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.ocena_id != key))
            {
                return BadRequest();
            }

            this.OnObecnoscUpdated(newItem);
            this.context.Obecnoscs.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Obecnoscs.Where(i => i.ocena_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Obecnoscs(ocena_id={ocena_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchObecnosc(int key, [FromBody]Delta<Models.Baza.Obecnosc> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Obecnoscs.Where(i => i.ocena_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnObecnoscUpdated(itemToUpdate);
            this.context.Obecnoscs.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Obecnoscs.Where(i => i.ocena_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnObecnoscCreated(Models.Baza.Obecnosc item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Obecnosc item)
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

            this.OnObecnoscCreated(item);
            this.context.Obecnoscs.Add(item);
            this.context.SaveChanges();

            var key = item.ocena_id;

            var itemToReturn = this.context.Obecnoscs.Where(i => i.ocena_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Uczen,DataOpi");

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
