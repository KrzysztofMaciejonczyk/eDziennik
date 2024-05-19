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

  [Route("odata/Baza/Uwagas")]
  public partial class UwagasController : ODataController
  {
    private Data.BazaContext context;

    public UwagasController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Uwagas
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Uwaga> GetUwagas()
    {
      var items = this.context.Uwagas.AsQueryable<Models.Baza.Uwaga>();
      this.OnUwagasRead(ref items);

      return items;
    }

    partial void OnUwagasRead(ref IQueryable<Models.Baza.Uwaga> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Uwagas(uwaga_id={uwaga_id})")]
    public SingleResult<Uwaga> GetUwaga(int key)
    {
        var items = this.context.Uwagas.Where(i=>i.uwaga_id == key);
        this.OnUwagasGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnUwagasGet(ref IQueryable<Models.Baza.Uwaga> items);

    partial void OnUwagaDeleted(Models.Baza.Uwaga item);

    [HttpDelete("/odata/Baza/Uwagas(uwaga_id={uwaga_id})")]
    public IActionResult DeleteUwaga(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Uwagas
                .Where(i => i.uwaga_id == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnUwagaDeleted(itemToDelete);
            this.context.Uwagas.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUwagaUpdated(Models.Baza.Uwaga item);

    [HttpPut("/odata/Baza/Uwagas(uwaga_id={uwaga_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutUwaga(int key, [FromBody]Models.Baza.Uwaga newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.uwaga_id != key))
            {
                return BadRequest();
            }

            this.OnUwagaUpdated(newItem);
            this.context.Uwagas.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Uwagas.Where(i => i.uwaga_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Uwagas(uwaga_id={uwaga_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchUwaga(int key, [FromBody]Delta<Models.Baza.Uwaga> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Uwagas.Where(i => i.uwaga_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnUwagaUpdated(itemToUpdate);
            this.context.Uwagas.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Uwagas.Where(i => i.uwaga_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Uczen,DataOpi");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUwagaCreated(Models.Baza.Uwaga item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Uwaga item)
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

            this.OnUwagaCreated(item);
            this.context.Uwagas.Add(item);
            this.context.SaveChanges();

            var key = item.uwaga_id;

            var itemToReturn = this.context.Uwagas.Where(i => i.uwaga_id == key);

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
