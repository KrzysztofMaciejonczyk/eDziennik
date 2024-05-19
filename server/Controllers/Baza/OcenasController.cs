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

  [Route("odata/Baza/Ocenas")]
  public partial class OcenasController : ODataController
  {
    private Data.BazaContext context;

    public OcenasController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Ocenas
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Ocena> GetOcenas()
    {
      var items = this.context.Ocenas.AsQueryable<Models.Baza.Ocena>();
      this.OnOcenasRead(ref items);

      return items;
    }

    partial void OnOcenasRead(ref IQueryable<Models.Baza.Ocena> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Ocenas(ocena_id={ocena_id})")]
    public SingleResult<Ocena> GetOcena(int key)
    {
        var items = this.context.Ocenas.Where(i=>i.ocena_id == key);
        this.OnOcenasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOcenasGet(ref IQueryable<Models.Baza.Ocena> items);

    partial void OnOcenaDeleted(Models.Baza.Ocena item);

    [HttpDelete("/odata/Baza/Ocenas(ocena_id={ocena_id})")]
    public IActionResult DeleteOcena(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Ocenas
                .Where(i => i.ocena_id == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnOcenaDeleted(itemToDelete);
            this.context.Ocenas.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOcenaUpdated(Models.Baza.Ocena item);

    [HttpPut("/odata/Baza/Ocenas(ocena_id={ocena_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOcena(int key, [FromBody]Models.Baza.Ocena newItem)
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

            this.OnOcenaUpdated(newItem);
            this.context.Ocenas.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Ocenas.Where(i => i.ocena_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Ocenas(ocena_id={ocena_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOcena(int key, [FromBody]Delta<Models.Baza.Ocena> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Ocenas.Where(i => i.ocena_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnOcenaUpdated(itemToUpdate);
            this.context.Ocenas.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Ocenas.Where(i => i.ocena_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOcenaCreated(Models.Baza.Ocena item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Ocena item)
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

            this.OnOcenaCreated(item);
            this.context.Ocenas.Add(item);
            this.context.SaveChanges();

            var key = item.ocena_id;

            var itemToReturn = this.context.Ocenas.Where(i => i.ocena_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Uczen,DataOpi");

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
