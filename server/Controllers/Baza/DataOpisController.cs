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

  [Route("odata/Baza/DataOpis")]
  public partial class DataOpisController : ODataController
  {
    private Data.BazaContext context;

    public DataOpisController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/DataOpis
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.DataOpi> GetDataOpis()
    {
      var items = this.context.DataOpis.AsQueryable<Models.Baza.DataOpi>();
      this.OnDataOpisRead(ref items);

      return items;
    }

    partial void OnDataOpisRead(ref IQueryable<Models.Baza.DataOpi> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/DataOpis(data_opis_id={data_opis_id})")]
    public SingleResult<DataOpi> GetDataOpi(int key)
    {
        var items = this.context.DataOpis.Where(i=>i.data_opis_id == key);
        this.OnDataOpisGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnDataOpisGet(ref IQueryable<Models.Baza.DataOpi> items);

    partial void OnDataOpiDeleted(Models.Baza.DataOpi item);

    [HttpDelete("/odata/Baza/DataOpis(data_opis_id={data_opis_id})")]
    public IActionResult DeleteDataOpi(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.DataOpis
                .Where(i => i.data_opis_id == key)
                .Include(i => i.Uwagas)
                .Include(i => i.Ocenas)
                .Include(i => i.Obecnoscs)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnDataOpiDeleted(itemToDelete);
            this.context.DataOpis.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnDataOpiUpdated(Models.Baza.DataOpi item);

    [HttpPut("/odata/Baza/DataOpis(data_opis_id={data_opis_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutDataOpi(int key, [FromBody]Models.Baza.DataOpi newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.data_opis_id != key))
            {
                return BadRequest();
            }

            this.OnDataOpiUpdated(newItem);
            this.context.DataOpis.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.DataOpis.Where(i => i.data_opis_id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/DataOpis(data_opis_id={data_opis_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchDataOpi(int key, [FromBody]Delta<Models.Baza.DataOpi> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.DataOpis.Where(i => i.data_opis_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnDataOpiUpdated(itemToUpdate);
            this.context.DataOpis.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.DataOpis.Where(i => i.data_opis_id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnDataOpiCreated(Models.Baza.DataOpi item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.DataOpi item)
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

            this.OnDataOpiCreated(item);
            this.context.DataOpis.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Baza/DataOpis/{item.data_opis_id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
